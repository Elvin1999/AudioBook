using System;
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

        }
        public FileInfo Info { get; set; }
        private void listViewPdfs_DragDrop(object sender, DragEventArgs e)
        {
            var mydata = e.Data.GetData(DataFormats.FileDrop) as string[];
            var Info = new FileInfo(mydata[0]);
            listViewPdfs.Items.Add(Info.FullName);
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
        private void listViewPdfs_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileName = listViewPdfs.SelectedItems[0].Text;
            PdfReader reader = new PdfReader(FileName);
            ConvertPDFtoImage(1);
            AllText = PdfTextExtractor.GetTextFromPage(reader, index);
            reader.Close();
        }
        int index = 1;
        private void buttonNext_Click(object sender, EventArgs e)
        {
            timer.Stop();
            progressBar1.Value = 1;
            PdfReader reader = new PdfReader(FileName);
            ConvertPDFtoImage(++index);
            AllText = PdfTextExtractor.GetTextFromPage(reader, index);
            var current = speech.GetCurrentlySpokenPrompt();
            if (current != null)
                speech.SpeakAsyncCancel(current);
            reader.Close();
        }
        public int CountLetter { get; set; }
        Timer timer = new Timer();
        int growth = 0;
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            CountLetter = AllText.Length;
            if (CountLetter != 0)
            {
                growth = progressBar1.Maximum / CountLetter - 1;
            }
            if (CountLetter <= 300)
            {
                growth = progressBar1.Maximum / CountLetter + 1;
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
        private void buttonPre_Click(object sender, EventArgs e)
        {
            timer.Stop();
            progressBar1.Value = 1;
            PdfReader reader = new PdfReader(FileName);
            ConvertPDFtoImage(--index);
            AllText = PdfTextExtractor.GetTextFromPage(reader, index);
            var current = speech.GetCurrentlySpokenPrompt();
            if (current != null)
                speech.SpeakAsyncCancel(current);
            reader.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer timer2 = new Timer();
            timer2.Interval = 150;
            timer2.Tick += Timer2_Tick;
            timer2.Start();
        }
        int r = 1, g = 1, b = 1;
        private void Timer2_Tick(object sender, EventArgs e)
        {


            if (g == 140)
            {
                g = 1;
                ++b;
                if (b == 140)
                {
                    b = 140;
                    ++r;
                }
            }
            else
            {
                ++g;
            }
            if (r == 140)
            {
                r = 1;g = 1;b = 1;
            }
            this.BackColor = Color.FromArgb(r, g, b);
            listViewPdfs.BackColor = Color.FromArgb(r, g, b);
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
    }
}





