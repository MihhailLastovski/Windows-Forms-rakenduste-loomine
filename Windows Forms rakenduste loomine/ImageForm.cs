using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Forms_rakenduste_loomine
{
    public partial class ImageForm : Form
    {
        Random rnd = new Random();
        TextBox textBox1, textBox2;
        public ImageForm()
        {
            CenterToScreen(); //Tsentreerib vormi
            Text = "Piltide vaatamine";
            ClientSize = new Size(1200, 600);
            colorDialog1 = new ColorDialog();
            openFileDialog1 = new OpenFileDialog();
            pictureBox1 = new PictureBox();
            checkBox1 = new CheckBox();
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel //Tabeli koostamine ridade ja veergudega
            {
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                RowCount = 2,
            };
            //Ridade ja veergude lisamine
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            //Vormi elementide loomine
            pictureBox1 = new PictureBox
            {
                BorderStyle = BorderStyle.Fixed3D,
                Dock = DockStyle.Fill,
                TabStop = false,
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            tableLayoutPanel.SetColumnSpan(pictureBox1, 2);
            checkBox1 = new CheckBox
            {
                AutoSize = true,
                Text = "Venitada",
                UseVisualStyleBackColor = true,
            };
            checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
            };
            openFileDialog1 = new OpenFileDialog
            {
                Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All file" + "s (*.*)|*.*",
                Title = "Valige pildifail"
            };
            textBox1 = new TextBox 
            {
                Text = "Laius",
            };
            textBox2 = new TextBox
            {
                Text = "Kõrgus",
            };
            Controls.Add(tableLayoutPanel);
            tableLayoutPanel.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel.Controls.Add(checkBox1, 0, 1);
            tableLayoutPanel.Controls.Add(flowLayoutPanel1, 1, 1);       
            string[] textbutton = { "Naita pilti", "Tuhjenda pilt", "Maarake taustavarv", "Sulge", "Juhuslik pilt", "Teie fail kaustas", "Pildi värvi muutmine", "Muudab suurust" };
            for (int i = 0; i < textbutton.Length; i++) //Nuppude loomine
            {
                Button zxc = new Button
                {
                    AutoSize = true,
                    UseVisualStyleBackColor = true,
                    Text = textbutton[i]

                };
                zxc.Click += Tegevus;
                flowLayoutPanel1.Controls.Add(zxc);
            }
            flowLayoutPanel1.Controls.Add(textBox1);
            flowLayoutPanel1.Controls.Add(textBox2);
            
        }
        private void Tegevus(object sender, EventArgs e)  //Nupu vajutamisel toimub toiming
        {
            //Massiivi loomine, mis võtab failid kataloogist
            string[] files_jpg = Directory.GetFiles(@"..\..\..\randompic", "*.jpg");
            string[] files_png = Directory.GetFiles(@"..\..\..\randompic", "*.png");
            //Loendi loomine ja sellele kahe massiivi lisamine  
            List<string> files = new List<string>();
            files.AddRange(files_png);
            files.AddRange(files_jpg);
            Button nupp_sender = (Button)sender;
            var cm = new ColorMatrix(new float[][]
            {
              new float[] {1, 0, 0, 0, 0},
              new float[] {0, 1, 1, 0, 0},
              new float[] {0, 0, 1, 0, 0},
              new float[] {0, 0, 0, 1, 0},
              new float[] {0, 0, 0.5f, 0, 1}
            });
            if (nupp_sender.Text == "Tuhjenda pilt")
            {
                pictureBox1.Image = null;
            }
            else if (nupp_sender.Text == "Sulge")
            {
                Close();
            }
            else if(nupp_sender.Text == "Maarake taustavarv") //Avab akna, kus saate valida taustavärvi
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    pictureBox1.BackColor = colorDialog1.Color;
            }
            else if(nupp_sender.Text == "Naita pilti") //Näitab dialoogiboksis valitud pilti
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Load(openFileDialog1.FileName);
                    Bitmap finalImg = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height); //Venitab pilti
                    pictureBox1.Image = finalImg;
                    pictureBox1.Show();
                }
            }
            else if (nupp_sender.Text == "Juhuslik pilt") //Nupp valib juhusliku pildi
            {
                
                pictureBox1.Load(files[rnd.Next(0,files.Count)]);
                Bitmap finalImg = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = finalImg;
                pictureBox1.Show();
            }
            else if(nupp_sender.Text == "Teie fail kaustas") //Avaneb dialoogiboks, kus saad valida faili ja see kantakse piltidega kausta
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string sourceFile = openFileDialog1.SafeFileName;
                    string destinationFile = @"..\..\..\randompic\" + sourceFile;
                    File.Move(openFileDialog1.FileName, destinationFile);
                }
            }
            else if(nupp_sender.Text == "Muudab suurust") 
            {
                Bitmap finalImg = new Bitmap(pictureBox1.Image, int.Parse(textBox1.Text), int.Parse(textBox2.Text)); //Muudab pildi suurust vastavalt etteantud parameetritele
                pictureBox1.Image = finalImg;
                pictureBox1.Show();
            }
            else if(nupp_sender.Text == "Pildi värvi muutmine") //Muudab pildi värvipaleti rohkem siniseks/lillaks
            {
                var img = Image.FromFile(pictureBox1.ImageLocation);
                var ia = new ImageAttributes();
                ia.SetColorMatrix(cm);

                var bmp = new Bitmap(img.Width, img.Height);
                var gfx = Graphics.FromImage(bmp);
                var rect = new Rectangle(0, 0, img.Width, img.Height);

                gfx.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
                Bitmap finalImg = new Bitmap(bmp, pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = finalImg;
                pictureBox1.Show();
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) //Venitab pilti
        {
            if (checkBox1.Checked)
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }

    }
}