﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf.parser;
using System.Speech;
using System.Drawing.Imaging;
using System.Speech.Synthesis;
namespace AudioBook1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonPlay.Enabled = false;
            buttonPre.Enabled = false;
            buttonNext.Enabled = false;
            ImageList il = new ImageList();
            var image = Properties.Resources.pdfimage;
            il.Images.Add(image);
            il.ImageSize = new Size(28, 28);
            listViewPdfs.LargeImageList = il;
        }
        int count = 0;
        public FileInfo Info { get; set; }
        private void listViewPdfs_DragDrop(object sender, DragEventArgs e)
        {
            var mydata = e.Data.GetData(DataFormats.FileDrop) as string[];
            var Info = new FileInfo(mydata[0]);
            ListViewItem item = new ListViewItem();
            item.Text = Info.FullName;
            item.ImageIndex = count;
            listViewPdfs.Items.Add(item);
        }
        public string ImagePath { get; set; }
        private void listViewPdfs_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        public string AllText { get; set; }
        public string FileName { get; set; }
        string jpegdir = System.IO.Path.GetDirectoryName(@"Picture\Picture");
        public void ConvertPDFtoImage(int page)
        {
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(FileName);
            f.ImageOptions.Dpi = 50;
            pictureBox1.Image = f.ToDrawingImage(page);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        SpeechSynthesizer speech = new SpeechSynthesizer();
        Point point = new Point();
        private void listViewPdfs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                timer.Stop();
                progressBar1.Value = 1;
                buttonPlay.Enabled = true;
                buttonPre.Enabled = true;
                buttonNext.Enabled = true;
                var current = speech.GetCurrentlySpokenPrompt();
                if (current != null)
                    speech.SpeakAsyncCancel(current);
                FileName = listViewPdfs.SelectedItems[currentIndex].Text;
                PdfReader reader1 = new PdfReader(FileName);
                ConvertPDFtoImage(1);
                AllText = PdfTextExtractor.GetTextFromPage(reader1, index);
                reader1.Close();
            }
            catch (Exception)
            {
                try
                {
                    FileName = listViewPdfs.SelectedItems[currentIndex].Text;
                    PdfReader reader = new PdfReader(FileName);
                    ConvertPDFtoImage(1);
                    AllText = PdfTextExtractor.GetTextFromPage(reader, index);
                    reader.Close();
                }
                catch (Exception)
                {

                }

            }


        }
        int index = 1;
        private void buttonNext_Click(object sender, EventArgs e)
        {

            timer.Stop();
            progressBar1.Value = 1;
            PdfReader reader = new PdfReader(FileName);
            if (index < reader.NumberOfPages)
            {
                ConvertPDFtoImage(++index);
                AllText = PdfTextExtractor.GetTextFromPage(reader, index);
                var current = speech.GetCurrentlySpokenPrompt();
                if (current != null)
                    speech.SpeakAsyncCancel(current);
            }
            reader.Close();
        }
        public int CountLetter { get; set; }
        Timer timer = new Timer();
        int growth = 0;
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (AllText.Length != 0)
            {
                CountLetter = AllText.Length;

                if (CountLetter <= 300)
                {
                    growth = progressBar1.Maximum / CountLetter + 1;
                }
                else
                {
                    growth = progressBar1.Maximum / CountLetter - 1;
                }
                if (growth <= 0)
                {
                    growth = 1;
                }
                progressBar1.Maximum = CountLetter;
                progressBar1.Value = 1;
                timer.Interval = 150;
                timer.Tick += Timer_Tick;
                timer.Start();
                var current = speech.GetCurrentlySpokenPrompt();
                if (current != null)
                    speech.SpeakAsyncCancel(current);
                speech.SpeakAsync(AllText);
            }
        }
        private void buttonPre_Click(object sender, EventArgs e)
        {
            timer.Stop();

            progressBar1.Value = 1;
            PdfReader reader = new PdfReader(FileName);
            if (index > 0)
            {
                ConvertPDFtoImage(--index);
                AllText = PdfTextExtractor.GetTextFromPage(reader, index);
                var current = speech.GetCurrentlySpokenPrompt();
                if (current != null)
                    speech.SpeakAsyncCancel(current);
            }

            reader.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer t = new Timer();
            t.Interval = 100;
            t.Tick += T_Tick;
            t.Start();
        }
        int b = 100;
        int r = 1;int g = 1;
        private void T_Tick(object sender, EventArgs e)
        {

            listViewPdfs.BackColor = Color.FromArgb(r, g, b);
            this.BackColor= Color.FromArgb(r, g, b);
            buttonNext.BackColor = Color.FromArgb(r, g, b);
            buttonPlay.BackColor = Color.FromArgb(r, g, b);
            buttonPre.BackColor = Color.FromArgb(r, g, b);
            progressBar1.BackColor = Color.FromArgb(r, g, b);
            ++r;
            if (r % 10 == 0)
            {
                ++g;
            }
            if (g % 10 == 0)
            {
                ++b;
            }
            if (r == 255)
            {
                r = 1;
            }
            if (g == 255)
            {
                g = 1;
            }
            if (b == 255)
            {
                b = 1;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value + growth <= progressBar1.Maximum)
            {
                progressBar1.Value += growth;
            }
            else
            {
                timer.Stop();
            }
        }
        int currentIndex = 0;
        private void listViewPdfs_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void listViewPdfs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var visualItem = listViewPdfs.GetItemAt(e.Location.X, e.Location.Y);
                if (visualItem == null) return;
                var index = listViewPdfs.Items.IndexOf(visualItem);
                currentIndex = index;
            }
            try
            {
                timer.Stop();
                progressBar1.Value = 1;
                buttonPlay.Enabled = true;
                buttonPre.Enabled = true;
                buttonNext.Enabled = true;
                var current = speech.GetCurrentlySpokenPrompt();
                if (current != null)
                    speech.SpeakAsyncCancel(current);
                FileName = listViewPdfs.SelectedItems[currentIndex].Text;
                PdfReader reader = new PdfReader(FileName);
                ConvertPDFtoImage(1);
                AllText = PdfTextExtractor.GetTextFromPage(reader, index);
                reader.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}





