using Csv;          // ���C�u����csv���g�p����̂ɕK�v
using System.Data;  // DataTable���g���̂ɕK�v
using System.Text;  // Encoding���g���̂ɕK�v
using System.Windows.Forms;

namespace CsvReadWrite
{
    public partial class Form1 : Form
    {
        // �����Ńf�[�^��ێ�����e�[�u����p�ӂ��܂��B
        DataTable dataTable = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �I������CSV���捞�A�f�[�^�e�[�u���Ƃ��ďo�͂��܂��B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCsvRead_Click(object sender, EventArgs e)
        {
            // �t�@�C�����J���E�B���h�E��CSV�t�@�C����I�����AOK�{�^�����N���b�N�������B
            if (openFileDialogCsv.ShowDialog() == DialogResult.OK)
            {
                // �t�@�C�����J���E�B���h�E�őI��CSV�̃t�@�C�������e�L�X�g�{�b�N�X�ɔ��f�B
                textBoxInputCSVFileName.Text = openFileDialogCsv.FileName;
                // �t�@�C���̑S���e�𕶎���ɓǂݍ��݂܂��B���{�ꂪ�ǂ߂��߂�悤�ɁA������utf-16�ŃG���R�[�h���܂��B
                string csv = File.ReadAllText(openFileDialogCsv.FileName, Encoding.GetEncoding("utf-16"));

                ClearDataTable();

                // CSV����w�b�_�[�����̃f�[�^���擾���A�����̃e�[�u���̃J�����ɐݒ�B
                // csv����1�s�擾���A���ʂ�line�ϐ��ɓ����B
                foreach (ICsvLine line in CsvReader.ReadFromText(csv))
                {
                    // 1�s���̃f�[�^�̃w�b�_�[�̏������o���܂��B
                    foreach (var item in line.Headers)
                    {
                        // �����̃e�[�u���̃J�����ɐݒ�B
                        dataTable.Columns.Add(item);
                    }
                    break;
                }
                // �ǂݍ���csv�̃f�[�^������̃e�[�u���Ɋ��蓖�Ă�
                foreach (ICsvLine line in CsvReader.ReadFromText(csv))
                {
                    // 1���R�[�h���܂Ƃ߂Đݒ�B
                    dataTable.Rows.Add(line.Values);
                }
                // �\���p��DataGridView�ɓ����̃e�[�u���������B
                dataGridViewCsv.DataSource = dataTable;
            }
        }

        /// <summary>
        /// �f�[�^�e�[�u����̃f�[�^CSV�t�@�C���Ƃ��ďo�͂��܂��B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void buttonCsvWrite_Click(object sender, EventArgs e)
        {
            // �t�@�C����ۑ�����E�B���h�E��CSV�t�@�C����I�����AOK�{�^�����N���b�N�����Ƃ��B
            if (saveFileDialogCsv.ShowDialog() == DialogResult.OK)
            {
                // �t�@�C����ۑ�����E�B���h�E�őI��CSV�̃t�@�C�������e�L�X�g�{�b�N�X�ɔ��f�B
                textBoxOutputCSVFileName.Text = saveFileDialogCsv.FileName;
                // DataGridView�̓��e���擾
                dataTable = (DataTable)dataGridViewCsv.DataSource;

                // header�Ƃ����ϐ��ɓ����̃e�[�u���̃J��������ݒ�
                string[] header = new string[dataTable.Columns.Count];
                // �J�����̐��������[�v���ăJ�����̃f�[�^��ݒ�
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    header[i] = dataTable.Columns[i].ColumnName;
                }
                // newLine�Ƃ����ϐ��ɓ����̃e�[�u����\�̃C���[�W(2�����z��)�Őݒ�B
                string[][] newLine = new string[dataTable.Rows.Count][];
                // �f�[�^�̐����������[�v���āA�f�[�^���擾
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    newLine[i] = new string[dataTable.Columns.Count];
                    // �Y������J�����A��̒l������̃e�[�u������AnewLine�ɐݒ�B
                    for (int j = 0; j < newLine[i].Length; j++)
                    {
                        // null�̏ꍇ�́A""�Ƃ���newLine�ɐݒ�B
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

                // �f�[�^����CSV�`���̕�����𐶐����܂��B
                string outcsv = CsvWriter.WriteToText(header, newLine);
                // FileName�Ƃ������O�ŁAoutcsv�̒l��ۑ����܂��B������utf-16�ŃG���R�[�h���܂��B
                File.WriteAllText(saveFileDialogCsv.FileName, outcsv, Encoding.GetEncoding("utf-16"));
            }
        }
        /// <summary>
        /// CSV�t�@�C����V�K�쐬���邽�߂̃f�[�^�e�[�u�����쐬���܂��B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCsvCreate_Click(object sender, EventArgs e)
        {
            try
            {
                ClearDataTable();

                // ���l�`�F�b�N�����܂��B
                int newColumnLength = Convert.ToInt32(textBoxRowsLength.Text);
                int newRowLength = Convert.ToInt32(textBoxColumnLength.Text);
                DataTable dtForNewCsv = new DataTable();

                // �s�̖��̐ݒ�B
                for (int i = 1; i <= newColumnLength; i++)
                {
                    dtForNewCsv.Columns.Add("�f�[�^" + i.ToString(), typeof(String));
                }

                // ��̒l��ݒ�B
                for (int i = 0; i < newRowLength; i++)
                {
                    dtForNewCsv.Rows.Add("");
                }

                // �\���p��DataGridView�ɓ����̃e�[�u���������B
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
        /// �f�[�^�e�[�u�������������܂�
        /// </summary>
        private void ClearDataTable()
        {
            dataTable.TableName = "CSVTable";   // �����Ńe�[�u���������܂��B
            dataTable.Columns.Clear();          // �����e�[�u���ւ̃w�b�_�[��������(2�A���œǂݍ��񂾎��̔���)
            dataTable.Clear();                  // �����e�[�u���̃f�[�^��������(2�A���œǂݍ��񂾎��̔���)
        }
    }
}