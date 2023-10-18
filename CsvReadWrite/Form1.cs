using Csv;          // ���C�u����csv���g�p����̂ɕK�v
using System.Data;  // DataTable���g���̂ɕK�v
using System.Text;  // Encoding���g���̂ɕK�v
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

        // CSV�擾�{�^���N���b�N���̏���
        private void buttonCsvRead_Click(object sender, EventArgs e)
        {
            // �t�@�C�����J���E�B���h�E��CSV�t�@�C����I�����BOK�{�^�����N���b�N������
            if(openFileDialogCsv.ShowDialog() == DialogResult.OK)
            {
                // �t�@�C�����J���E�B���h�E�őI��CSV�̃t�@�C�������e�L�X�g�{�b�N�X�ɔ��f
                textBoxInputCSVFileName.Text = openFileDialogCsv.FileName;
                // �t�@�C���̑S���e�𕶎���ɓǂݍ��݂܂��B���{�ꂪ�ǂ߂��߂�悤�ɁA������utf-16�ŃG���R�[�h���܂��B
                string csv = File.ReadAllText(openFileDialogCsv.FileName,Encoding.GetEncoding("utf-16"));

                dataTable.TableName = "CSVTable";   // �����Ńe�[�u���������܂��B
                dataTable.Columns.Clear();          // �����e�[�u���ւ̃w�b�_�[��������(2�A���œǂݍ��񂾎��̔���)
                dataTable.Clear();                  // �����e�[�u���̃f�[�^��������(2�A���œǂݍ��񂾎��̔���)

                // CSV����w�b�_�[�����̃f�[�^���擾���A�����̃e�[�u���̃J�����ɐݒ�
                // csv����1�s�擾���A���ʂ�line�ϐ��ɓ����B
                foreach(ICsvLine line in CsvReader.ReadFromText(csv))
                {
                    // 1�s���̃f�[�^�̃w�b�_�[�̏������o��
                    foreach (var item in line.Headers)
                    {
                        dataTable.Columns.Add(item);    // �����̃e�[�u���̃J�����ɐݒ�
                    }
                    break;
                }
                // �ǂݍ���csv�̃f�[�^������̃e�[�u���Ɋ��蓖�Ă�
                foreach (ICsvLine line in CsvReader.ReadFromText(csv))
                {
                    dataTable.Rows.Add(line.Values);    // 1���R�[�h���܂Ƃ߂Đݒ�
                }
                dataGridViewCsv.DataSource = dataTable; // �\���p��DataGridView�ɓ����̃e�[�u��������
            }
        }
        
        // CSV�捞�{�^���N���b�N���̏���
        private void buttonCsvWrite_Click(object sender, EventArgs e)
        {
            // �t�@�C����ۑ�����E�B���h�E��CSV�t�@�C����I�����AOK�{�^�����N���b�N�����Ƃ�
            if(saveFileDialogCsv.ShowDialog() == DialogResult.OK)
            {
                // �t�@�C����ۑ�����E�B���h�E�őI��CSV�̃t�@�C�������e�L�X�g�{�b�N�X�ɔ��f
                textBoxOutputCSVFileName.Text = saveFileDialogCsv.FileName;
                // header�Ƃ����ϐ��ɓ����̃e�[�u���̃J��������ݒ�
                string[] header = new string[dataTable.Columns.Count];
                // �J�����̐��������[�v���ăJ�����̃f�[�^��ݒ�
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    header[i] = dataTable.Columns[i].ColumnName;
                }
                // newLine�Ƃ����ϐ��ɓ����̃e�[�u����\�̃C���[�W(2�����z��)�Őݒ�
                string[][] newLine = new string[dataTable.Rows.Count][];
                // �f�[�^�̐����������[�v���āA�f�[�^���擾
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    newLine[i] = new string[dataTable.Columns.Count];
                    // �Y������J�����A��̒l������̃e�[�u������AnewLine�ɐݒ�
                    for (int j = 0; j < newLine[i].Length; j++)
                    {
                        // null�̏ꍇ�́A""�Ƃ���newLine�ɐݒ�
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

                // �f�[�^����CSV�`���̕�����𐶐����܂�
                string outcsv = CsvWriter.WriteToText(header, newLine);
                // FileName�Ƃ������O�ŁAoutcsv�̒l��ۑ����܂��B������utf-16�ŃG���R�[�h���܂��B
                File.WriteAllText(saveFileDialogCsv.FileName, outcsv, Encoding.GetEncoding("utf-16"));
            }
        }
    }
}