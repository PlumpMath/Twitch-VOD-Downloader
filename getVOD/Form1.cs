using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace getVOD
{
    public partial class Form1 : Form
    {
        public static bool IsApplicationInstalled(string p_name)
        {
            string displayName;
            RegistryKey key;

            // search in: CurrentUser
            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (displayName != null)
                {
                    if (p_name.Contains(displayName) == true)
                    {
                        return true;
                    }
                }
            }

            // search in: LocalMachine_32
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (displayName != null)
                {
                    if (p_name.Contains(displayName) == true)
                    {
                        return true;
                    }
                }
            }

            // search in: LocalMachine_64
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (displayName != null)
                {
                    if (p_name.Contains(displayName) == true)
                    {
                        return true;
                    }
                }
            }

            // NOT FOUND
            return false;
        }

        public Form1()
        {
            InitializeComponent();
            if (!IsApplicationInstalled("Livestreamer 1.12.2"))
            {
                MessageBox.Show("You don't have Livestreamer 1.12.2 installed.\nWithout this program this application will not work, please install it.");
            }
            else if(!IsApplicationInstalled("VLC media player")){
                MessageBox.Show("You don't have VLC Media Player installed.\nWithout this program this application will not work, please install it.");
            }
        }

        public void ChooseFolder()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseFolder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || !textBox1.Text.Contains("https://")){
                MessageBox.Show("You haven't typed in a valid VOD URL, please type one in");
            }else if (textBox2.Text == "" || !textBox2.Text.Contains("\\")){
                MessageBox.Show("You haven't chosen a valid save location, please pick one");
            }else if (textBox3.Text == ""){
                MessageBox.Show("You haven't given the file a name, please give it a name");
            }else {
                string strCmdText;
                strCmdText = "/C livestreamer -o " + "\"" + textBox2.Text + "\\" + textBox3.Text + ".mp4\" --hls-segment-threads 4 " + textBox1.Text + " best";
                Process.Start("cmd.exe", strCmdText);
            }
        }
    }
}
