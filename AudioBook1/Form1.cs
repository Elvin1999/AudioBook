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
        public void ConvertPDFtoImage()
        {
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(FileName);
            f.ImageOptions.Dpi = 200;

            //string jpegfile = System.IO.Path.Combine(jpegdir,
            //    $"{FileName} Page {7}.jpg");
            pictureBox1.Image = f.ToDrawingImage(20);

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void listViewPdfs_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileName = listViewPdfs.SelectedItems[0].Text;
            PdfReader reader = new PdfReader(FileName);
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < 2; i++)
            {
                sb.Append(PdfTextExtractor.GetTextFromPage(reader, i));

            }
            SpeechSynthesizer speech = new SpeechSynthesizer();
            speech.SetOutputToDefaultAudioDevice();
            AllText = sb.ToString(); reader.Close();
            MessageBox.Show("Test");
            ConvertPDFtoImage();


            //speech.Speak(AllText);



        }
    }
}





