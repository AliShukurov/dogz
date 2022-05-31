using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using mediaplayer;

namespace Mediaplayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = true, ValidateNames = true })
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    List<MediaFile> files = new List<MediaFile>();
                    foreach (string fileName in openFileDialog.FileNames)
                    {
                        FileInfo fileInfo = new FileInfo(fileName);
                        files.Add(new MediaFile()
                        { FileName = Path.GetFileNameWithoutExtension(fileInfo.FullName), Path = fileInfo.FullName });
                    }
                    listFile.DataSource = files;
                    listFile.ValueMember = "Path";
                    listFile.DisplayMember = "FileName";
                }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MediaFile file = listFile.SelectedItem as MediaFile;

            Task.Run(() =>
            {
                if (file != null)
                {
                    WMP.URL = file.Path;
                    WMP.Ctlcontrols.play();
                }
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000000; i++)
            {
                richTextBox2.Text = i.ToString();
                Application.DoEvents();
            }
        }
    }
}