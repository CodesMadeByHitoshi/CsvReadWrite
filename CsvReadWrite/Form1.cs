using Csv;          // ライブラリcsvを使用するのに必要
using System.Data;  // DataTableを使うのに必要
using System.Text;  // Encodingを使うのに必要
using System.Windows.Forms;

namespace CsvReadWrite
{
    public partial class Form1 : Form
    {
        // 内部でデータを保持するテーブルを用意します。
        DataTable dataTable = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 選択したCSVを取込、データテーブルとして出力します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCsvRead_Click(object sender, EventArgs e)
        {
            // ファイルを開くウィンドウでCSVファイルを選択し、OKボタンをクリックした時。
            if (openFileDialogCsv.ShowDialog() == DialogResult.OK)
            {
                // ファイルを開くウィンドウで選んだCSVのファイル名をテキストボックスに反映。
                textBoxInputCSVFileName.Text = openFileDialogCsv.FileName;
                // ファイルの全内容を文字列に読み込みます。日本語が読めこめるように、文字はutf-16でエンコードします。
                string csv = File.ReadAllText(openFileDialogCsv.FileName, Encoding.GetEncoding("utf-16"));

                ClearDataTable();

                // CSVからヘッダー部分のデータを取得し、内部のテーブルのカラムに設定。
                // csvから1行取得し、結果をline変数に入れる。
                foreach (ICsvLine line in CsvReader.ReadFromText(csv))
                {
                    // 1行分のデータのヘッダーの情報を取り出します。
                    foreach (var item in line.Headers)
                    {
                        // 内部のテーブルのカラムに設定。
                        dataTable.Columns.Add(item);
                    }
                    break;
                }
                // 読み込んだcsvのデータを内部のテーブルに割り当てる
                foreach (ICsvLine line in CsvReader.ReadFromText(csv))
                {
                    // 1レコード分まとめて設定。
                    dataTable.Rows.Add(line.Values);
                }
                // 表示用のDataGridViewに内部のテーブルを割当。
                dataGridViewCsv.DataSource = dataTable;
            }
        }

        /// <summary>
        /// データテーブル状のデータCSVファイルとして出力します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void buttonCsvWrite_Click(object sender, EventArgs e)
        {
            // ファイルを保存するウィンドウでCSVファイルを選択し、OKボタンをクリックしたとき。
            if (saveFileDialogCsv.ShowDialog() == DialogResult.OK)
            {
                // ファイルを保存するウィンドウで選んだCSVのファイル名をテキストボックスに反映。
                textBoxOutputCSVFileName.Text = saveFileDialogCsv.FileName;
                // DataGridViewの内容を取得
                dataTable = (DataTable)dataGridViewCsv.DataSource;

                // headerという変数に内部のテーブルのカラム名を設定
                string[] header = new string[dataTable.Columns.Count];
                // カラムの数だけループしてカラムのデータを設定
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    header[i] = dataTable.Columns[i].ColumnName;
                }
                // newLineという変数に内部のテーブルを表のイメージ(2次元配列)で設定。
                string[][] newLine = new string[dataTable.Rows.Count][];
                // データの数分だけループして、データを取得
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    newLine[i] = new string[dataTable.Columns.Count];
                    // 該当するカラム、列の値を内部のテーブルから、newLineに設定。
                    for (int j = 0; j < newLine[i].Length; j++)
                    {
                        // nullの場合は、""としてnewLineに設定。
                        if (dataTable.Rows[i][j] == DBNull.Value)
                        {
                            newLine[i][j] = "";
                        }
                        else
                        {
                            newLine[i][j] = (string)dataTable.Rows[i][j];
                        }
                    }
                }

                // データからCSV形式の文字列を生成します。
                string outcsv = CsvWriter.WriteToText(header, newLine);
                // FileNameという名前で、outcsvの値を保存します。文字はutf-16でエンコードします。
                File.WriteAllText(saveFileDialogCsv.FileName, outcsv, Encoding.GetEncoding("utf-16"));
            }
        }
        /// <summary>
        /// CSVファイルを新規作成するためのデータテーブルを作成します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCsvCreate_Click(object sender, EventArgs e)
        {
            try
            {
                ClearDataTable();

                // 数値チェックをします。
                int newColumnLength = Convert.ToInt32(textBoxRowsLength.Text);
                int newRowLength = Convert.ToInt32(textBoxColumnLength.Text);
                DataTable dtForNewCsv = new DataTable();

                // 行の名称設定。
                for (int i = 1; i <= newColumnLength; i++)
                {
                    dtForNewCsv.Columns.Add("データ" + i.ToString(), typeof(String));
                }

                // 列の値を設定。
                for (int i = 0; i < newRowLength; i++)
                {
                    dtForNewCsv.Rows.Add("");
                }

                // 表示用のDataGridViewに内部のテーブルを割当。
                dataGridViewCsv.DataSource = dtForNewCsv;

            }
            catch (FormatException)
            {
                String message = "Please Enter Lengths for new CSV";
                String caption = "Error Lengths are not entered on textbox";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                return;
            }
        }
        /// <summary>
        /// データテーブルを初期化します
        /// </summary>
        private void ClearDataTable()
        {
            dataTable.TableName = "CSVTable";   // 内部でテーブル生成します。
            dataTable.Columns.Clear();          // 内部テーブルへのヘッダーを初期化(2連続で読み込んだ時の反応)
            dataTable.Clear();                  // 内部テーブルのデータを初期化(2連続で読み込んだ時の反応)
        }
    }
}