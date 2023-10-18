using Csv;          // ライブラリcsvを使用するのに必要
using System.Data;  // DataTableを使うのに必要
using System.Text;  // Encodingを使うのに必要
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

        // CSV取得ボタンクリック時の処理
        private void buttonCsvRead_Click(object sender, EventArgs e)
        {
            // ファイルを開くウィンドウでCSVファイルを選択し。OKボタンをクリックした時
            if(openFileDialogCsv.ShowDialog() == DialogResult.OK)
            {
                // ファイルを開くウィンドウで選んだCSVのファイル名をテキストボックスに反映
                textBoxInputCSVFileName.Text = openFileDialogCsv.FileName;
                // ファイルの全内容を文字列に読み込みます。日本語が読めこめるように、文字はutf-16でエンコードします。
                string csv = File.ReadAllText(openFileDialogCsv.FileName,Encoding.GetEncoding("utf-16"));

                dataTable.TableName = "CSVTable";   // 内部でテーブル生成します。
                dataTable.Columns.Clear();          // 内部テーブルへのヘッダーを初期化(2連続で読み込んだ時の反応)
                dataTable.Clear();                  // 内部テーブルのデータを初期化(2連続で読み込んだ時の反応)

                // CSVからヘッダー部分のデータを取得し、内部のテーブルのカラムに設定
                // csvから1行取得し、結果をline変数に入れる。
                foreach(ICsvLine line in CsvReader.ReadFromText(csv))
                {
                    // 1行分のデータのヘッダーの情報を取り出す
                    foreach (var item in line.Headers)
                    {
                        dataTable.Columns.Add(item);    // 内部のテーブルのカラムに設定
                    }
                    break;
                }
                // 読み込んだcsvのデータを内部のテーブルに割り当てる
                foreach (ICsvLine line in CsvReader.ReadFromText(csv))
                {
                    dataTable.Rows.Add(line.Values);    // 1レコード分まとめて設定
                }
                dataGridViewCsv.DataSource = dataTable; // 表示用のDataGridViewに内部のテーブルを割当
            }
        }
        
        // CSV取込ボタンクリック時の処理
        private void buttonCsvWrite_Click(object sender, EventArgs e)
        {
            // ファイルを保存するウィンドウでCSVファイルを選択し、OKボタンをクリックしたとき
            if(saveFileDialogCsv.ShowDialog() == DialogResult.OK)
            {
                // ファイルを保存するウィンドウで選んだCSVのファイル名をテキストボックスに反映
                textBoxOutputCSVFileName.Text = saveFileDialogCsv.FileName;
                // headerという変数に内部のテーブルのカラム名を設定
                string[] header = new string[dataTable.Columns.Count];
                // カラムの数だけループしてカラムのデータを設定
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    header[i] = dataTable.Columns[i].ColumnName;
                }
                // newLineという変数に内部のテーブルを表のイメージ(2次元配列)で設定
                string[][] newLine = new string[dataTable.Rows.Count][];
                // データの数分だけループして、データを取得
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    newLine[i] = new string[dataTable.Columns.Count];
                    // 該当するカラム、列の値を内部のテーブルから、newLineに設定
                    for (int j = 0; j < newLine[i].Length; j++)
                    {
                        // nullの場合は、""としてnewLineに設定
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

                // データからCSV形式の文字列を生成します
                string outcsv = CsvWriter.WriteToText(header, newLine);
                // FileNameという名前で、outcsvの値を保存します。文字はutf-16でエンコードします。
                File.WriteAllText(saveFileDialogCsv.FileName, outcsv, Encoding.GetEncoding("utf-16"));
            }
        }
    }
}