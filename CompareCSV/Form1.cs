using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.LinkLabel;

namespace CompareCSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_open1_Click(object sender, EventArgs e)
        {
            RichTextBox rt1 = this.richTextBox1;
            open(rt1);
        }
        private void btn_open2_Click(object sender, EventArgs e)
        {
            RichTextBox rt2 = this.richTextBox2;
            open(rt2);
        }

        private void open (RichTextBox rt)
        {
            openFileDialog1.Filter = "CSV Files (*.csv)|*.csv|TXT Files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream arquivo = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);  
                    StreamReader sr = new StreamReader(arquivo);
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);
                    rt.Text = "";
                    string linha = sr.ReadLine();
                    while(linha != null)
                    {
                        rt.Text += linha + "\n";
                        linha = sr.ReadLine();
                    }
                    sr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro de leitura: " + ex.Message, "Erro ao ler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bt_compare_Click(object sender, EventArgs e)
        {
            repeatedLines();
        }

        private void repeatedLines ()
        {
            // Converte a string do campo em array
            string[] rt1 = richTextBox1.Text.Split('\n');
            string[] rt2 = richTextBox2.Text.Split('\n');

            //Fazer a comparação dos arrays
            var crossArray = rt1.Intersect(rt2).ToArray();
            richTextBox3.Text = "";
            for (int i = 0; crossArray[i] != ""; i++)
            {
                richTextBox3.Text += crossArray[i] + "\n";
            }
        }
    }
}
