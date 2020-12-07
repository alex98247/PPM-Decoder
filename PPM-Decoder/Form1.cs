using System;
using System.IO;
using System.Windows.Forms;

namespace PPM_Decoder
{
    public partial class Form1 : Form
    {
        private string _openFileName;
        private PpmDecompressor ppmDecompressor;

        public Form1()
        {
            ppmDecompressor = new PpmDecompressor();
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _openFileName = saveFileDialog.FileName;
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    var filePath = openFileDialog.FileName;
                    textBox1.Text = filePath;
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var filePath = textBox1.Text;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var writer = new FileStream(_openFileName, FileMode.OpenOrCreate);
            var reader = new FileStream(filePath, FileMode.Open);
            ppmDecompressor.Decompress(new BitInputStream(reader), writer);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender,
            System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Разархивирование завершено");
        }
    }
}