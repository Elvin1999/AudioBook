﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioBook1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public FileInfo Info { get; set; }
        private void listViewPdfs_DragDrop(object sender, DragEventArgs e)
        {
            this.BackColor = Color.DarkBlue;
            var mydata = e.Data.GetData(DataFormats.FileDrop) as string[];
            var Info = new FileInfo(mydata[0]);
            listViewPdfs.Items.Add(Info.FullName);
        }

        private void listViewPdfs_DragEnter(object sender, DragEventArgs e)
        {
            listViewPdfs.BackColor = Color.BlueViolet;
            e.Effect = DragDropEffects.All;
        }
        public string AllText { get; set; }
        public string FileName { get; set; }
        private void listViewPdfs_SelectedIndexChanged(object sender, EventArgs e)
        {

            FileName = listViewPdfs.SelectedItems[0].Text;            
            var read = File.ReadAllText(FileName);
        }
    }
}
