
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace face
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class Uploader
        {
            Bitmap image;
            public Bitmap getImage()
            {
                return image;
            }
            public void setImage(Bitmap _image)
            {

                image = _image;
            }
        }
        string selectedFile;
        Bitmap picture;
        static Uploader upload;
        private void button1_Click(object sender, EventArgs e)
        {
            upload = new Form1.Uploader();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedFile = openFileDialog1.FileName;

                Image im = Image.FromFile(selectedFile);
                pictureBox1.Image = im;
                picture = new Bitmap(im);
            }
            upload.setImage(picture);

        }
        class PreProcessor
        {
            static Bitmap preImage;
            public Bitmap getPreImage()
            { return preImage; }
            public void setPreImage(Bitmap _preImage)
            { preImage = _preImage; }
            void grayscale()
            {
                int gray;
                setPreImage(upload.getImage());
                for (int i = 0; i < preImage.Width; i++)
                {
                    for (int j = 0; j < preImage.Height; j++)
                    {
                        gray = (preImage.GetPixel(i, j).R + preImage.GetPixel(i, j).G + preImage.GetPixel(i, j).B) / 3;
                        preImage.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                        //int grayScale = (int)((oc.R * 0.3) + (oc.G * 0.59) + (oc.B * 0.11));
                        //Color nc = Color.FromArgb(oc.A, grayScale, grayScale, grayScale);
                        //d.SetPixel(i, x, nc);

                    }
                }
                //  preImage.Save(@"C:/users/compaq/grayS.jpg");

            }
            void minimize()
            { }
            void threshold()
            { }
            public Bitmap preProcess()
            {
                grayscale();
                return preImage;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            PreProcessor pros = new PreProcessor();
            pictureBox1.Image = (Image)pros.preProcess();
            //  MessageBox.Show("done");
            //pictureBox1.Refresh();
        }
    }
}

