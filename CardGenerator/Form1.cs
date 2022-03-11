using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardGenerator
{
    public partial class Form1 : Form
    {
        string filePath = string.Empty;
        public Form1()
        {
            InitializeComponent();
            progressBar1.Visible = false;
            MaximizeBox = false;
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CodeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void DataFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }

            path.Text = filePath;

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {


            bool isOK = true;
            if (HeaderTextBox.Text == "")
            {
                MessageBox.Show("Введите имя председателя!");
                isOK = false;
            }
            if (filePath == "")
            {
                MessageBox.Show("Укажите путь к данным");
                isOK = false;
            }
            if (CodeTextBox.Text == "")
            {
                MessageBox.Show("Укажите код факультета");
                isOK = false;
            }
            if (YearTextBox.Text == "" || YearTextBox.Text.Length != 4)
            {
                if (YearTextBox.Text == "")
                    YearTextBox.Text = DateTime.Now.Year.ToString();
                else
                {
                    MessageBox.Show("Укажите год корректно! Например, 2021");
                    isOK = false; 
                }

            }

            if (isOK)
            {
                progressBar1.Visible = true;
                try
                {
                    int currentValue = 0;
                    Thread t = new Thread(delegate () {


                        this.Invoke(new MethodInvoker(() =>
                        {
                            YearTextBox.Enabled = false;
                            HeaderTextBox.Enabled = false;
                            CodeTextBox.Enabled = false;
                            StartButton.Enabled = false;
                            DataFile.Enabled = false;
                            FileName.Enabled = false;
                        }));

                        List<Student> tmpList;
                        int tmpInt = Student.GenerateStudentList(YearTextBox.Text.Substring(2), CodeTextBox.Text, filePath, out tmpList);

                        this.Invoke(new MethodInvoker(() =>
                        {
                            MessageBox.Show("Удалено из списка: " + (tmpInt - tmpList.Count));
                        }));

                        string textBuffer = FileName.Text;

                        this.Invoke(new MethodInvoker(() =>
                        {
                            progressBar1.Maximum = tmpList.Count;
                        }));

                        if (textBuffer != "" && !textBuffer.Contains(".docx"))
                        {
                            textBuffer += ".docx";
                            DocxParser.Fill(tmpList, YearTextBox.Text.Substring(2), HeaderTextBox.Text, out currentValue, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), textBuffer);

                        }
                        else if (textBuffer != "" && textBuffer.Contains(".docx"))
                        {
                            DocxParser.Fill(tmpList, YearTextBox.Text.Substring(2), HeaderTextBox.Text, out currentValue, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), textBuffer);

                        }
                        else
                        {
                            DocxParser.Fill(tmpList, YearTextBox.Text.Substring(2), HeaderTextBox.Text, out currentValue, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

                        }


                        this.Invoke(new MethodInvoker(() =>
                        {
                            YearTextBox.Enabled = true;
                            HeaderTextBox.Enabled = true;
                            CodeTextBox.Enabled = true;
                            StartButton.Enabled = true;
                            DataFile.Enabled = true;
                            FileName.Enabled = true;
                            
                        }));
                        
                        
                       
                    });
                    Thread progressBarThread = new Thread(delegate () {
                        while (t.ThreadState != ThreadState.Stopped)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                this.progressBar1.Value = currentValue;
                            }));
                        }


                        this.Invoke(new MethodInvoker(() =>
                        {
                            MessageBox.Show("Завершено!");
                            this.progressBar1.Value = 0;
                            progressBar1.Visible = false;
                            t.Abort();
                        }));
                        

                    });

                    t.Start();
                    progressBarThread.Start();

                }
                catch (Exception exc)
                {
                    var res = MessageBox.Show("Сломалось что-то, никто не знает что. Проверьте данные, а после начните молиться, программа самоуничтожается");
                    
                    if (res == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                    
                    
                }
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
