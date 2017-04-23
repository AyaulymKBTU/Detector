
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
        static string selectedFile;
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
            {
                int a, b, c, d, max;
                for (int i = 0; i < preImage.Width - 2; i += 2)
                {
                    for (int j = 0; j < preImage.Height - 2; j += 2)
                    {
                        a = preImage.GetPixel(i, j).R;
                        b = preImage.GetPixel(i + 1, j).R;
                        c = preImage.GetPixel(i, j + 1).R;
                        d = preImage.GetPixel(i + 1, j + 1).R;
                        max = Compare(a, b, c, d);
                    }
                }

            }
            void threshold()
            {
                int b, c, d;
                for (int i = 0; i < preImage.Width; i++)
                {
                    for (int j = 0; j < preImage.Height; j++)
                    {

                        b = preImage.GetPixel(i, j).R;
                        c = preImage.GetPixel(i, j).G;
                        d = preImage.GetPixel(i, j).B;
                        if (setColor(b, c, d))
                            preImage.SetPixel(i, j, Color.FromArgb(0,0,0));
                        else
                            preImage.SetPixel(i, j, Color.FromArgb(255,255,255));
                    }
                }

            }
            bool haveBlackNeighbour(int i, int j)
            {
                if (preImage.GetPixel(i - 1, j - 1) == Color.White && preImage.GetPixel(i - 1, j) == Color.White && preImage.GetPixel(i + 1, j + 1) == Color.White && preImage.GetPixel(i, j - 1) == Color.White && preImage.GetPixel(i, j + 1) == Color.White && preImage.GetPixel(i + 1, j) == Color.White && preImage.GetPixel(i + 1, j - 1) == Color.White && preImage.GetPixel(i - 1, j + 1) == Color.White)
                { return false; }
                return true;
            }
            bool getColor(int r, int g, int b)
            {
                if (r == 255 && g == 255 && b == 255)
                    return false;
                else if (r == 0 && g == 0 && b == 0)
                    return true;
                else
                {
                    MessageBox.Show("hhhhhhhhhhhhhhhhh");
                    return true;
                }
            }
            Bitmap findCorners()
            {
                int c = 0;
                Bitmap bn = preImage;
                for (int i = 1; i < preImage.Width - 1; i++)
                {
                    for (int j = 1; j < preImage.Height - 1; j++)
                    {

                        if (!getColor(preImage.GetPixel(i, j).R, preImage.GetPixel(i, j).G, preImage.GetPixel(i, j).B))
                        {

                          
                            if (getColor(preImage.GetPixel(i - 1, j - 1).R, preImage.GetPixel(i - 1, j - 1).G, preImage.GetPixel(i - 1, j - 1).B) ||
                                getColor(preImage.GetPixel(i - 1, j).R, preImage.GetPixel(i - 1, j).G, preImage.GetPixel(i - 1, j).B) ||
                                getColor(preImage.GetPixel(i + 1, j + 1).R, preImage.GetPixel(i + 1, j + 1).G, preImage.GetPixel(i + 1, j + 1).B) ||
                                getColor(preImage.GetPixel(i, j - 1).R, preImage.GetPixel(i, j - 1).G, preImage.GetPixel(i, j - 1).B) ||
                                getColor(preImage.GetPixel(i, j + 1).R, preImage.GetPixel(i, j + 1).G, preImage.GetPixel(i, j + 1).B) ||
                                getColor(preImage.GetPixel(i + 1, j).R, preImage.GetPixel(i + 1, j).G, preImage.GetPixel(i + 1, j).B) ||
                                getColor(preImage.GetPixel(i + 1, j - 1).R, preImage.GetPixel(i + 1, j - 1).G, preImage.GetPixel(i + 1, j - 1).B) ||
                                getColor(preImage.GetPixel(i - 1, j + 1).R, preImage.GetPixel(i - 1, j + 1).G, preImage.GetPixel(i - 1, j + 1).B))
                            //if(preImage.GetPixel(i,j).Equals(Color.White))
                            //{
                            //if (!(preImage.GetPixel(i + 1, j).Equals(Color.White) &&
                            //   preImage.GetPixel(i + 1, j - 1).Equals(Color.White) &&
                            //   preImage.GetPixel(i, j - 1).Equals(Color.White) &&
                            //   preImage.GetPixel(i, j + 1).Equals(Color.White) &&
                            //   preImage.GetPixel(i - 1, j - 1).Equals(Color.White) &&
                            //   preImage.GetPixel(i + 1, j + 1).Equals(Color.White) &&
                            //   preImage.GetPixel(i - 1, j + 1).Equals(Color.White) &&
                            //   preImage.GetPixel(i - 1, j).Equals(Color.White)))
                            { bn.SetPixel(i, j, Color.Red);c++; }
                            }
                        }
                    }
                MessageBox.Show(c.ToString());
                return bn;

            }
            public void saveResult()
            { preImage.Save(@"C:/users/compaq/desktop/pakita/results/" + "Jcorner.jpg"); }
            public Bitmap preProcess()
            {
                // grayscale();    
                setPreImage(upload.getImage());
                //  preImage =ImageProcessingUtility.ScaleImage(preImage,40,40);
                 // threshold();
                preImage = findCorners();
                saveResult();
                //preImage = ImageProcessingUtility.ScaleImage(preImage, 450, 400); 
                return preImage;
            }
        }
        public static bool setColor(int a, int b, int c)
        {
            if (a >=100 && b >= 100 && c >=100)
                return false;
            else
                return true;
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

