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
        private void button1_Click(object sender, EventArgs e)
        {
            Uploader upload = new Form1.Uploader();
           
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedFile = openFileDialog1.FileName;

                Image im = Image.FromFile(selectedFile);
                pictureBox1.Image = im;
                picture = new Bitmap(im);
            }
            upload.setImage(picture);
        }

    }
}
