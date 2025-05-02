namespace tourfirma
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button8 = new Button();
            tabPage6 = new TabPage();
            cmbParametricQuery = new ComboBox();
            cmbAggregateQuery = new ComboBox();
            button7 = new Button();
            button6 = new Button();
            dataGridViewResult = new DataGridView();
            button5 = new Button();
            label2 = new Label();
            button4 = new Button();
            label1 = new Label();
            tabPage5 = new TabPage();
            dataGridView6 = new DataGridView();
            tabPage4 = new TabPage();
            dataGridView5 = new DataGridView();
            tabPage3 = new TabPage();
            dataGridView4 = new DataGridView();
            tabPage2 = new TabPage();
            dataGridView3 = new DataGridView();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResult).BeginInit();
            tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView6).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView5).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(192, 192, 255);
            button1.Location = new Point(28, 422);
            button1.Name = "button1";
            button1.Size = new Size(149, 48);
            button1.TabIndex = 1;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(192, 192, 255);
            button2.Location = new Point(528, 422);
            button2.Name = "button2";
            button2.Size = new Size(149, 48);
            button2.TabIndex = 2;
            button2.Text = "Изменить";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(192, 192, 255);
            button3.Location = new Point(763, 422);
            button3.Name = "button3";
            button3.Size = new Size(149, 48);
            button3.TabIndex = 3;
            button3.Text = "Удалить";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(192, 192, 255);
            button8.Location = new Point(264, 422);
            button8.Margin = new Padding(2);
            button8.Name = "button8";
            button8.Size = new Size(149, 48);
            button8.TabIndex = 5;
            button8.Text = "Перейти в туры";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // tabPage6
            // 
            tabPage6.Controls.Add(cmbParametricQuery);
            tabPage6.Controls.Add(cmbAggregateQuery);
            tabPage6.Controls.Add(button7);
            tabPage6.Controls.Add(button6);
            tabPage6.Controls.Add(dataGridViewResult);
            tabPage6.Controls.Add(button5);
            tabPage6.Controls.Add(label2);
            tabPage6.Controls.Add(button4);
            tabPage6.Controls.Add(label1);
            tabPage6.Location = new Point(4, 24);
            tabPage6.Margin = new Padding(2);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(2);
            tabPage6.Size = new Size(884, 358);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "Запросы";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // cmbParametricQuery
            // 
            cmbParametricQuery.FormattingEnabled = true;
            cmbParametricQuery.Location = new Point(20, 215);
            cmbParametricQuery.Margin = new Padding(2);
            cmbParametricQuery.Name = "cmbParametricQuery";
            cmbParametricQuery.Size = new Size(129, 23);
            cmbParametricQuery.TabIndex = 11;
            // 
            // cmbAggregateQuery
            // 
            cmbAggregateQuery.FormattingEnabled = true;
            cmbAggregateQuery.Location = new Point(17, 96);
            cmbAggregateQuery.Margin = new Padding(2);
            cmbAggregateQuery.Name = "cmbAggregateQuery";
            cmbAggregateQuery.Size = new Size(129, 23);
            cmbAggregateQuery.TabIndex = 10;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(192, 192, 255);
            button7.Location = new Point(209, 215);
            button7.Margin = new Padding(2);
            button7.Name = "button7";
            button7.Size = new Size(124, 20);
            button7.TabIndex = 9;
            button7.Text = "Импорт из MS Excel";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(192, 192, 255);
            button6.Location = new Point(209, 151);
            button6.Margin = new Padding(2);
            button6.Name = "button6";
            button6.Size = new Size(124, 23);
            button6.TabIndex = 8;
            button6.Text = "Экспорт в MS Excel";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // dataGridViewResult
            // 
            dataGridViewResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResult.Location = new Point(349, 18);
            dataGridViewResult.Margin = new Padding(2);
            dataGridViewResult.Name = "dataGridViewResult";
            dataGridViewResult.RowHeadersWidth = 62;
            dataGridViewResult.RowTemplate.Height = 33;
            dataGridViewResult.Size = new Size(499, 306);
            dataGridViewResult.TabIndex = 7;
            // 
            // button5
            // 
            button5.Location = new Point(20, 241);
            button5.Margin = new Padding(2);
            button5.Name = "button5";
            button5.Size = new Size(78, 20);
            button5.TabIndex = 5;
            button5.Text = "Выполнить";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 187);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(164, 15);
            label2.TabIndex = 3;
            label2.Text = "Параметризованный запрос";
            // 
            // button4
            // 
            button4.Location = new Point(17, 139);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(78, 20);
            button4.TabIndex = 2;
            button4.Text = "Выполнить";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 59);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(142, 15);
            label1.TabIndex = 0;
            label1.Text = "Агрегированный запрос";
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(dataGridView6);
            tabPage5.Location = new Point(4, 24);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(884, 358);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Оплата";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // dataGridView6
            // 
            dataGridView6.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView6.Location = new Point(15, 13);
            dataGridView6.Name = "dataGridView6";
            dataGridView6.RowHeadersWidth = 62;
            dataGridView6.RowTemplate.Height = 25;
            dataGridView6.Size = new Size(849, 326);
            dataGridView6.TabIndex = 0;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(dataGridView5);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(884, 358);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Сезоны";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView5
            // 
            dataGridView5.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView5.Location = new Point(15, 13);
            dataGridView5.Name = "dataGridView5";
            dataGridView5.RowHeadersWidth = 62;
            dataGridView5.RowTemplate.Height = 25;
            dataGridView5.Size = new Size(856, 317);
            dataGridView5.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(dataGridView4);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(884, 358);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Туры";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4.Location = new Point(18, 15);
            dataGridView4.Name = "dataGridView4";
            dataGridView4.RowHeadersWidth = 62;
            dataGridView4.RowTemplate.Height = 25;
            dataGridView4.Size = new Size(848, 320);
            dataGridView4.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridView3);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(884, 358);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Информация о туристах";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new Point(21, 19);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowHeadersWidth = 62;
            dataGridView3.RowTemplate.Height = 25;
            dataGridView3.Size = new Size(844, 317);
            dataGridView3.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(24, 30);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(892, 386);
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(884, 358);
            tabPage1.TabIndex = 6;
            tabPage1.Text = "Диаграммы";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(958, 499);
            Controls.Add(button8);
            Controls.Add(tabControl1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResult).EndInit();
            tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView6).EndInit();
            tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView5).EndInit();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button8;
        private TabPage tabPage6;
        private Button button7;
        private Button button6;
        private DataGridView dataGridViewResult;
        private Button button5;
        private Label label2;
        private Button button4;
        private Label label1;
        private TabPage tabPage5;
        private DataGridView dataGridView6;
        private TabPage tabPage4;
        private DataGridView dataGridView5;
        private TabPage tabPage3;
        private DataGridView dataGridView4;
        private TabPage tabPage2;
        private DataGridView dataGridView3;
        private TabControl tabControl1;
        private ComboBox cmbParametricQuery;
        private ComboBox cmbAggregateQuery;
        private TabPage tabPage1;
    }
}