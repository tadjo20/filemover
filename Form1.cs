using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form 

    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {

                // user selects the file to move
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Report files (*.repx)|*.repx|Text files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.Multiselect = true;



                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (var formnames in ofd.FileNames)
                    {
                        textBox1.Text = formnames;


                        FileInfo oldFilename = new FileInfo(formnames); //FileInfo oldFilename = new FileInfo(ofd.FileName); // used to get just the name of the file


                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = comboBox1.Text;
                    sfd.FileName = oldFilename.Name;

                    //// using combo box1 the user selects the evnt to move the file to, adding just the name and the path the system can check to see if another version exists  
                    string replaceMe = comboBox1.Text + oldFilename.Name;

                    // breaking appart thae string to add the date to it and then put it all back together
                    #region string append manipulation
                    Random ran = new Random();
                    int randummm = ran.Next(0, 100000);
                    int dotIndex = replaceMe.LastIndexOf(".");
                    int endIndex = replaceMe.Length;
                    string rep0 = replaceMe.Substring(0, dotIndex);
                    string rep2 = replaceMe.Substring(dotIndex);
                    string rep1 = "__" + randummm + "__" + DateTime.Now.ToString();
                    string rep5 = rep1.Replace("/", "_");
                    int whiteIndex = rep5.IndexOf(" ");
                    string rep6 = rep5.Substring(0, whiteIndex);
                    string replacementName = rep0 + rep6 + rep2;
                    #endregion

                    if (System.IO.File.Exists(replaceMe)) // if file exists append the origanal file with a date and replace it
                    {

                        // this is the file moving magic 
                        File.Move(replaceMe, replacementName);//formnames
                        File.Copy(formnames, replaceMe); //  File.Copy(ofd.FileName, replaceMe);
                        textBox1.Text = string.Format("File {0} moved and saved, also backup file made {1}", replaceMe, replacementName);
                        

                    }
                    else
                    {
                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK) // in the event that the file does not exist 
                        {
                            File.Create(sfd.FileName);
                            textBox1.Text =  string.Format("File {0} saved" , sfd.FileName);
                        }
                        else
                        {
                            textBox1.Text = "plese select a file and the destination";
                            
                        }

                    }

                }
            }
                else
                {
                    textBox1.Text = "plese select a file and the destination";
                }
            }
            else
            {
                textBox1.Text = "Select a destination from the drop down";
            }
        }


        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            label1.Text = e.ProgressPercentage.ToString();
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            textBox1.Text = "...";
            
        }

        void clear()
        {
            textBox1.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }
    }
}
