using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;
using System.IO;
using ClosedXML.Excel;
using System.Windows.Forms.DataVisualization.Charting;

namespace tourfirma
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection con;
        private string connString = "Host=127.0.0.1;Username=postgres;Password=postpass;Database=TurFirm;Include Error Detail=true";
        private DataGridViewRow selectedRow;

        public Form1()
        {
            InitializeComponent();
            con = new NpgsqlConnection(connString);
            con.Open();
            // Загружаем только объединенные данные
            loadTouristsCombined();
            loadSeasons();
            loadPutevki();
            loadPayment();
            // Инициализируем ComboBox с запросами
            InitializeQueryComboBoxes();

            loadDiagrams();
        }
        private void loadDiagrams()
        {
            // Запрос для круговой диаграммы
            string sqlPie = @"
    SELECT t.tour_name,
           COUNT(p.putevki_id)::float / NULLIF(total.total_count, 0) * 100 AS payment_percentage
    FROM tours t
    LEFT JOIN seasons s ON s.tour_id = t.tour_id
    LEFT JOIN putevki p ON p.season_id = s.season_id
    CROSS JOIN (
        SELECT COUNT(*) AS total_count
        FROM putevki
    ) AS total
    GROUP BY t.tour_name, total.total_count
    HAVING COUNT(p.putevki_id) > 0
    ORDER BY payment_percentage DESC;";

            // Круговая диаграмма
            Chart PieChart = new Chart();
            PieChart.Titles.Add("Процент выкупа туров");
            PieChart.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);
            PieChart.Location = new Point(10, 75);
            PieChart.Size = new Size(300, 300);
            tabPage1.Controls.Add(PieChart);
            PieChart.ChartAreas.Add(new ChartArea());

            Series PieSeries = new Series("PaymentPercentage");
            PieSeries.ChartType = SeriesChartType.Pie;

            // Заполнение данных
            using (NpgsqlCommand cmdPie = new NpgsqlCommand(sqlPie, this.con))
            {
                using (NpgsqlDataReader reader = cmdPie.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tourName = reader["tour_name"].ToString();
                        double percentage = Convert.ToDouble(reader["payment_percentage"]);
                        PieSeries.Points.AddXY(tourName, percentage);
                    }
                }
            }


            PieSeries.Label = "#PERCENT{P0}";
            PieSeries.LegendText = "#VALX";
            PieSeries.Font = new Font("Arial", 8);
            PieChart.Series.Add(PieSeries);

            // Настройка легенды
            Legend pieLegend = new Legend();
            pieLegend.Docking = Docking.Bottom;
            pieLegend.Alignment = StringAlignment.Center;
            pieLegend.Font = new Font("Arial", 8);
            PieChart.Legends.Add(pieLegend);

            // Запрос для столбчатой диаграммы
            string sqlBar = @"
    SELECT 
        t.tour_name, 
        COALESCE(SUM(CAST(pay.summa AS numeric)), 0) AS total_payments
    FROM tours t
    JOIN seasons s ON s.tour_id = t.tour_id
    JOIN putevki p ON p.season_id = s.season_id
    JOIN payment pay ON pay.putevki_id = p.putevki_id
    GROUP BY t.tour_name
    ORDER BY total_payments DESC;";

            // Столбчатая диаграмма
            Chart BarChart = new Chart();
            BarChart.Titles.Add("Сумма выкупа туров");
            BarChart.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);
            BarChart.Location = new Point(400, 75);
            BarChart.Size = new Size(300, 300);
            tabPage1.Controls.Add(BarChart);

            BarChart.ChartAreas.Add(new ChartArea());

            Series BarSeries = new Series("TotalPayments");
            BarSeries.ChartType = SeriesChartType.Column;
            BarSeries.IsValueShownAsLabel = true;
            BarSeries.LabelFormat = "C2"; // Формат валюты

            // Заполнение данных для столбчатой диаграммы
            using (NpgsqlCommand cmdBar = new NpgsqlCommand(sqlBar, this.con))
            {
                using (NpgsqlDataReader reader = cmdBar.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tourName = reader["tour_name"].ToString();
                        decimal total = Convert.ToDecimal(reader["total_payments"]);
                        BarSeries.Points.AddXY(tourName, total);
                    }
                }
            }

            BarChart.Series.Add(BarSeries);
            BarChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            // Настройка внешнего вида столбчатой диаграммы
            BarChart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            BarChart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8);
            BarChart.ChartAreas[0].AxisY.LabelStyle.Format = "C0";
            BarChart.ChartAreas[0].AxisY.Title = "Сумма";
            BarChart.ChartAreas[0].AxisX.Title = "Тур";
        }
        private void InitializeQueryComboBoxes()
        {
            // Заполняем ComboBox для агрегированных запросов
            cmbAggregateQuery.Items.AddRange(new object[]
            {
                "Количество туристов|SELECT COUNT(*) FROM tourists",
                "Средняя цена путевки|SELECT AVG(price) FROM putevki",
                "Общая сумма всех путевок|SELECT SUM(price) FROM putevki",
                "Минимальная цена путевки|SELECT MIN(price) FROM putevki",
                "Максимальная цена путевки|SELECT MAX(price) FROM putevki"
            });
            cmbAggregateQuery.SelectedIndex = 0;
            cmbAggregateQuery.DisplayMember = "Text";
            cmbAggregateQuery.ValueMember = "Value";

            // Заполняем ComboBox для параметрических запросов
            cmbParametricQuery.Items.AddRange(new object[]
            {
                "Туристы с фамилией на 'Ивано%'|SELECT * FROM tourists WHERE tourist_surname LIKE 'Ивано%'",
                "Путевки дороже 50000|SELECT * FROM putevki WHERE price > 50000",
                "Сезоны после 2023 года|SELECT * FROM seasons WHERE start_date > '2023-01-01'",
                "Платежи от 10000 до 50000|SELECT * FROM payment WHERE amount BETWEEN 10000 AND 50000"
            });
            cmbParametricQuery.SelectedIndex = 0;
            cmbParametricQuery.DisplayMember = "Text";
            cmbParametricQuery.ValueMember = "Value";
        }
        private void dataGridViewTourists_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                selectedRow = dataGridView3.SelectedRows[0];

                if (selectedRow.Cells["tourist_id"].Value != null)
                {
                    Console.WriteLine($"Выбрана строка с ID: {selectedRow.Cells["tourist_id"].Value}");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Определяем текущую вкладку
                string activeTab = tabControl1.SelectedTab?.Name;
                if (activeTab == null) return;

                DataGridView currentGrid = null;
                string idColumnName = "";
                string tableName = "";
                string errorMessage = "Нельзя удалить запись, так как существуют связанные данные!\n" +
                                     "Сначала удалите связанные записи.";

                // Настраиваем параметры в зависимости от вкладки
                switch (activeTab)
                {
                    case "tabPage2": // Туристы
                        currentGrid = dataGridView3;
                        idColumnName = "tourist_id";
                        tableName = "tourists";
                        errorMessage = "Нельзя удалить туриста, для которого существуют путевки!\n" +
                                      "Сначала удалите связанные путевки.";
                        break;
                    case "tabPage3": // Сезоны
                        currentGrid = dataGridView4;
                        idColumnName = "season_id";
                        tableName = "seasons";
                        errorMessage = "Нельзя удалить сезон, для которого существуют путевки!\n" +
                                      "Сначала удалите связанные путевки.";
                        break;
                    case "tabPage4": // Путевки
                        currentGrid = dataGridView5;
                        idColumnName = "putevki_id";
                        tableName = "putevki";
                        errorMessage = "Нельзя удалить путевку, для которой существуют платежи!\n" +
                                      "Сначала удалите связанные платежи.";
                        break;
                    case "tabPage5": // Оплата
                        currentGrid = dataGridView6;
                        idColumnName = "payment_id";
                        tableName = "payment";
                        break;
                    default:
                        MessageBox.Show("Неизвестная вкладка!", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                // Проверяем, выбрана ли строка
                if (currentGrid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите строку для удаления!", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedRow = currentGrid.SelectedRows[0];
                if (selectedRow.Cells[idColumnName].Value == null)
                {
                    MessageBox.Show("Не удалось определить ID для удаления!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Подтверждение удаления
                var result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Подтверждение удаления",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;

                // Получаем ID для удаления
                int idToDelete = Convert.ToInt32(selectedRow.Cells[idColumnName].Value);

                // Формируем SQL-запрос
                string sql = $@"DELETE FROM {tableName} WHERE {idColumnName} = @id;";

                using (var transaction = con.BeginTransaction())
                using (var cmd = new NpgsqlCommand(sql, con, transaction))
                {
                    cmd.Parameters.AddWithValue("id", idToDelete);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                        MessageBox.Show("Запись успешно удалена!", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Обновляем данные в зависимости от вкладки
                        switch (activeTab)
                        {
                            case "tabPage2": loadTouristsCombined(); break;
                            case "tabPage3": loadSeasons(); break;
                            case "tabPage4": loadPutevki(); break;
                            case "tabPage5": loadPayment(); break;
                        }
                    }
                    catch (Npgsql.PostgresException ex) when (ex.SqlState == "23503")
                    {
                        transaction.Rollback();
                        MessageBox.Show(errorMessage, "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadTouristsCombined()
        {
            try
            {
                DataTable dt = new DataTable();
                // Запрос для объединенной таблицы или JOIN, если таблицы раздельные
                string sql = @"SELECT 
              tourist_id, 
              tourist_surname as Фамилия, 
              tourist_name as Имя, 
              tourist_otch as Отчество,
              passport as Паспорт,
              city as Город,
              country as Страна,
              phone as Телефон
              FROM tourists";

                new NpgsqlDataAdapter(sql, con).Fill(dt);
                dataGridView3.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadSeasons()
        {
            // ... (остается без изменений)
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adap = new NpgsqlDataAdapter("SELECT * FROM seasons", con);
            adap.Fill(dt);
            dataGridView4.DataSource = dt;
        }

        private void loadPutevki()
        {
            // ... (остается без изменений)
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adap = new NpgsqlDataAdapter("SELECT * FROM putevki", con);
            adap.Fill(dt);
            dataGridView5.DataSource = dt;
        }

        private void loadPayment()
        {
            // ... (остается без изменений)
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adap = new NpgsqlDataAdapter("SELECT * FROM payment", con);
            adap.Fill(dt);
            dataGridView6.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string activeTab = tabControl1.SelectedTab?.Name;
                if (activeTab == null) return;

                var form = new UniversalEditForm(activeTab, null, con);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RefreshCurrentTab();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы добавления: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string activeTab = tabControl1.SelectedTab?.Name;
                if (activeTab == null) return;

                DataGridView currentGrid = GetCurrentDataGridView(activeTab);

                if (currentGrid == null || currentGrid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите строку для редактирования!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedRow = currentGrid.SelectedRows[0];
                if (selectedRow == null || selectedRow.IsNewRow)
                {
                    MessageBox.Show("Выберите корректную строку для редактирования!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var form = new UniversalEditForm(activeTab, selectedRow, con);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RefreshCurrentTab();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы редактирования: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataGridView GetCurrentDataGridView(string activeTab)
        {
            switch (activeTab)
            {
                case "tabPage2": return dataGridView3; // Теперь это туристы
                case "tabPage3": return dataGridView4; // Сезоны
                case "tabPage4": return dataGridView5; // Путевки
                case "tabPage5": return dataGridView6; // Оплата
                default: return null;
            }
        }

        private void RefreshCurrentTab()
        {
            string activeTab = tabControl1.SelectedTab?.Name;
            if (activeTab == null) return;

            switch (activeTab)
            {
                case "tabPage2": loadTouristsCombined(); break;
                case "tabPage3": loadSeasons(); break;
                case "tabPage4": loadPutevki(); break;
                case "tabPage5": loadPayment(); break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cmbAggregateQuery.SelectedItem == null)
            {
                MessageBox.Show("Выберите агрегированный запрос!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем SQL из выбранного элемента (после |)
            string selectedItem = cmbAggregateQuery.SelectedItem.ToString();
            string query = selectedItem.Split('|')[1];

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    object result = cmd.ExecuteScalar();
                    MessageBox.Show($"Результат: {result}", "Агрегированный запрос",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cmbParametricQuery.SelectedItem == null)
            {
                MessageBox.Show("Выберите параметрический запрос!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем SQL из выбранного элемента (после |)
            string selectedItem = cmbParametricQuery.SelectedItem.ToString();
            string query = selectedItem.Split('|')[1];

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewResult.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, есть ли данные в dataGridView
                if (dataGridViewResult.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных для экспорта!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Создаем диалог выбора файла
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel файлы (*.xlsx)|*.xlsx",
                    Title = "Сохранить отчет",
                    FileName = "Отчет.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Создаем новую книгу Excel
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Отчет");

                        // Заголовки столбцов
                        for (int i = 0; i < dataGridViewResult.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dataGridViewResult.Columns[i].HeaderText;
                            worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                        }

                        // Данные из DataGridView
                        for (int i = 0; i < dataGridViewResult.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridViewResult.Columns.Count; j++)
                            {
                                worksheet.Cell(i + 2, j + 1).Value = dataGridViewResult.Rows[i].Cells[j].Value?.ToString() ?? "";
                            }
                        }

                        // Автоширина колонок
                        worksheet.Columns().AdjustToContents();

                        // Сохранение файла
                        workbook.SaveAs(filePath);
                    }

                    MessageBox.Show($"Файл успешно сохранен: {filePath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // Диалог выбора файла
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel файлы (*.xlsx)|*.xlsx",
                    Title = "Выберите файл для импорта"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    // Открываем файл Excel
                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var worksheet = workbook.Worksheet(1); // Берем первый лист
                        var range = worksheet.RangeUsed(); // Получаем используемый диапазон

                        if (range == null)
                        {
                            MessageBox.Show("Файл пуст или не содержит данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        DataTable dt = new DataTable();

                        // Читаем заголовки (первую строку)
                        foreach (var cell in range.FirstRow().CellsUsed())
                        {
                            dt.Columns.Add(cell.Value.ToString().Trim());
                        }

                        // Читаем строки (со второй)
                        foreach (var row in range.RowsUsed().Skip(1))
                        {
                            DataRow dataRow = dt.NewRow();
                            int columnIndex = 0;

                            foreach (var cell in row.CellsUsed())
                            {
                                dataRow[columnIndex] = cell.Value.ToString().Trim();
                                columnIndex++;
                            }

                            dt.Rows.Add(dataRow);
                        }

                        // Загружаем данные в DataGridView
                        dataGridViewResult.DataSource = dt;
                    }

                    MessageBox.Show("Данные успешно импортированы!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при импорте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Переход на вкладку "Путёвки"
            tabControl1.SelectedTab = tabPage4;

            // Обновление DataGridView с путёвками
            loadPutevki();

            MessageBox.Show("Триггер выполнен! Новому туристу назначен тур с минимальной ценой.");
        }
    }
}

