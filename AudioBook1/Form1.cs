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
using Bytescout.PDFExtractor;
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
            listViewPdfs.BackColor = Color.LightBlue;
            var mydata = e.Data.GetData(DataFormats.FileDrop) as string[];
            var Info = new FileInfo(mydata[0]);
            listViewPdfs.Items.Add(Info.FullName);
        }
        public string ImagePath { get; set; }
        private void listViewPdfs_DragEnter(object sender, DragEventArgs e)
        {
            listViewPdfs.BackColor = Color.BlueViolet;
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
            speech.SetOutputToDefaultAudioDevice();
            AllText = PdfTextExtractor.GetTextFromPage(reader, index);
            reader.Close();
        }
        int index = 1;
        private void buttonNext_Click(object sender, EventArgs e)
        {
            PdfReader reader = new PdfReader(FileName);
            ConvertPDFtoImage(++index);
            AllText = PdfTextExtractor.GetTextFromPage(reader, index);
            reader.Close();
        }
        public int CountLetter { get; set; }
        Timer timer = new Timer();
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            CountLetter = AllText.Length;
            progressBar1.Maximum = CountLetter;
            progressBar1.Value = 1;
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
            var current = speech.GetCurrentlySpokenPrompt();
            if (current != null)
                speech.SpeakAsyncCancel(current);
            speech.SpeakAsync(AllText);
        }
        private void buttonPre_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 1;
            PdfReader reader = new PdfReader(FileName);
            ConvertPDFtoImage(--index);
            speech.SetOutputToDefaultAudioDevice();
            AllText = PdfTextExtractor.GetTextFromPage(reader, index);
            reader.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 1;
            if (progressBar1.Value == CountLetter)
            {
                timer.Stop();
            }
            else
            {

            }
        }
    }
}





