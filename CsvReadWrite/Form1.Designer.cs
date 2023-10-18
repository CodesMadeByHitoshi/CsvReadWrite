﻿namespace CsvReadWrite
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
            textBoxInputCSVFileName = new TextBox();
            buttonCsvRead = new Button();
            buttonCsvWrite = new Button();
            textBoxOutputCSVFileName = new TextBox();
            dataGridViewCsv = new DataGridView();
            openFileDialogCsv = new OpenFileDialog();
            saveFileDialogCsv = new SaveFileDialog();
            buttonCsvCreate = new Button();
            textBoxRowLength = new TextBox();
            textBoxColumnLength = new TextBox();
            labelColumnLength = new Label();
            labelRowLength = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCsv).BeginInit();
            SuspendLayout();
            // 
            // textBoxInputCSVFileName
            // 
            textBoxInputCSVFileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxInputCSVFileName.Location = new Point(17, 69);
            textBoxInputCSVFileName.Margin = new Padding(4, 5, 4, 5);
            textBoxInputCSVFileName.Name = "textBoxInputCSVFileName";
            textBoxInputCSVFileName.Size = new Size(895, 31);
            textBoxInputCSVFileName.TabIndex = 0;
            // 
            // buttonCsvRead
            // 
            buttonCsvRead.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCsvRead.Location = new Point(923, 62);
            buttonCsvRead.Margin = new Padding(4, 5, 4, 5);
            buttonCsvRead.Name = "buttonCsvRead";
            buttonCsvRead.Size = new Size(203, 38);
            buttonCsvRead.TabIndex = 1;
            buttonCsvRead.Text = "CSV取得";
            buttonCsvRead.UseVisualStyleBackColor = true;
            buttonCsvRead.Click += buttonCsvRead_Click;
            // 
            // buttonCsvWrite
            // 
            buttonCsvWrite.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCsvWrite.Location = new Point(923, 657);
            buttonCsvWrite.Margin = new Padding(4, 5, 4, 5);
            buttonCsvWrite.Name = "buttonCsvWrite";
            buttonCsvWrite.Size = new Size(203, 38);
            buttonCsvWrite.TabIndex = 3;
            buttonCsvWrite.Text = "CSV出力";
            buttonCsvWrite.UseVisualStyleBackColor = true;
            buttonCsvWrite.Click += buttonCsvWrite_Click;
            // 
            // textBoxOutputCSVFileName
            // 
            textBoxOutputCSVFileName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxOutputCSVFileName.Location = new Point(17, 657);
            textBoxOutputCSVFileName.Margin = new Padding(4, 5, 4, 5);
            textBoxOutputCSVFileName.Name = "textBoxOutputCSVFileName";
            textBoxOutputCSVFileName.Size = new Size(895, 31);
            textBoxOutputCSVFileName.TabIndex = 2;
            // 
            // dataGridViewCsv
            // 
            dataGridViewCsv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCsv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCsv.Location = new Point(16, 125);
            dataGridViewCsv.Margin = new Padding(4, 5, 4, 5);
            dataGridViewCsv.Name = "dataGridViewCsv";
            dataGridViewCsv.RowHeadersWidth = 62;
            dataGridViewCsv.RowTemplate.Height = 25;
            dataGridViewCsv.Size = new Size(1110, 499);
            dataGridViewCsv.TabIndex = 4;
            // 
            // openFileDialogCsv
            // 
            openFileDialogCsv.FileName = "*.csv";
            openFileDialogCsv.InitialDirectory = ".\\";
            // 
            // saveFileDialogCsv
            // 
            saveFileDialogCsv.Filter = "CSVファイル|*.csv|すべてのファイル|*.*";
            saveFileDialogCsv.InitialDirectory = ".\\";
            // 
            // buttonCsvCreate
            // 
            buttonCsvCreate.Location = new Point(923, 16);
            buttonCsvCreate.Name = "buttonCsvCreate";
            buttonCsvCreate.Size = new Size(203, 38);
            buttonCsvCreate.TabIndex = 5;
            buttonCsvCreate.Text = "CSV作成";
            buttonCsvCreate.UseVisualStyleBackColor = true;
            buttonCsvCreate.Click += buttonCsvCreate_Click;
            // 
            // textBoxRowLength
            // 
            textBoxRowLength.Location = new Point(643, 20);
            textBoxRowLength.Name = "textBoxRowLength";
            textBoxRowLength.Size = new Size(256, 31);
            textBoxRowLength.TabIndex = 6;
            // 
            // textBoxColumnLength
            // 
            textBoxColumnLength.Location = new Point(199, 20);
            textBoxColumnLength.Name = "textBoxColumnLength";
            textBoxColumnLength.Size = new Size(256, 31);
            textBoxColumnLength.TabIndex = 7;
            // 
            // labelColumnLength
            // 
            labelColumnLength.AutoSize = true;
            labelColumnLength.Location = new Point(17, 23);
            labelColumnLength.Name = "labelColumnLength";
            labelColumnLength.Size = new Size(176, 25);
            labelColumnLength.TabIndex = 8;
            labelColumnLength.Text = "行数を入力してください";
            // 
            // labelRowLength
            // 
            labelRowLength.AutoSize = true;
            labelRowLength.Location = new Point(461, 23);
            labelRowLength.Name = "labelRowLength";
            labelRowLength.Size = new Size(176, 25);
            labelRowLength.TabIndex = 9;
            labelRowLength.Text = "列数を入力してください";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 750);
            Controls.Add(labelRowLength);
            Controls.Add(labelColumnLength);
            Controls.Add(textBoxColumnLength);
            Controls.Add(textBoxRowLength);
            Controls.Add(buttonCsvCreate);
            Controls.Add(dataGridViewCsv);
            Controls.Add(buttonCsvWrite);
            Controls.Add(textBoxOutputCSVFileName);
            Controls.Add(buttonCsvRead);
            Controls.Add(textBoxInputCSVFileName);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "CSVの読み書き";
            ((System.ComponentModel.ISupportInitialize)dataGridViewCsv).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxInputCSVFileName;
        private Button buttonCsvRead;
        private Button buttonCsvWrite;
        private TextBox textBoxOutputCSVFileName;
        private DataGridView dataGridViewCsv;
        private OpenFileDialog openFileDialogCsv;
        private SaveFileDialog saveFileDialogCsv;
        private Button buttonCsvCreate;
        private TextBox textBoxRowLength;
        private TextBox textBoxColumnLength;
        private Label labelColumnLength;
        private Label labelRowLength;
    }
}