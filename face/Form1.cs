
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
                    }
                }


            }
            void minimize()
            { int a, b, c, d,max;
                for (int i = 0; i < preImage.Width-2; i+=2)
                {
                    for (int j = 0; j < preImage.Height-2; j+=2)
                    {
                        a = preImage.GetPixel(i, j).R;
                       b= preImage.GetPixel(i+1, j).R;
                       c= preImage.GetPixel(i, j+1).R;
                       d= preImage.GetPixel(i+1, j+1).R;
                       max= Compare(a, b, c, d);
                    }
                }

            }
            void threshold()
            { }
            public Bitmap preProcess()
            {
                grayscale();
                preImage=ImageProcessingUtility.ScaleImage(preImage,50,50);
                preImage = ImageProcessingUtility.ScaleImage(preImage, 450, 400);
                return preImage;
            }
        }
        public static int Compare(int a, int b, int c, int d)
        {
            int max1, max2, max;
            if (a >= b)
                max1 = a;
            else
                max1 = b;
            if (c >= d)
                max2 = c;
            else
                max2 = d;
            if (max1 >= max2)
                max = max1;
            else
                max = max2;
            return max;
        }
        public static class ImageProcessingUtility
        {
            
            static public Bitmap ScaleImage(Image image, int maxWidth, int maxHeight)
            {
                var ratioX = (double)maxWidth / image.Width;
                var ratioY = (double)maxHeight / image.Height;
                var ratio = Math.Min(ratioX, ratioY);

                var newWidth = (int)(image.Width * ratio);
                var newHeight = (int)(image.Height * ratio);

                var newImage = new Bitmap(newWidth, newHeight);
                Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
                Bitmap bmp = new Bitmap(newImage);

                return bmp;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            PreProcessor pros = new PreProcessor();
            pictureBox1.Image = pros.preProcess();

        }
    }
}

