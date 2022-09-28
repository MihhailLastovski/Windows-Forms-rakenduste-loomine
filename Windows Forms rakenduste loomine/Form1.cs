using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Forms_rakenduste_loomine
{
    public partial class ImageForm : Form
    {
        public ImageForm()
        {
            Text = "Picture Viewer";
            ClientSize = new Size(529, 330);
            colorDialog1 = new ColorDialog();
            openFileDialog1 = new OpenFileDialog();
            pictureBox1 = new PictureBox();
            checkBox1 = new CheckBox();
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                RowCount = 2,
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
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
                Text = "Stretch",
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
                Title = "Select a picture file"
            };
            Controls.Add(tableLayoutPanel);
            tableLayoutPanel.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel.Controls.Add(checkBox1, 0, 1);
            tableLayoutPanel.Controls.Add(flowLayoutPanel1, 1, 1);
            string[] textbutton = { "Show a picture", "Clear the picture", "Set the background color", "Close" };
            for (int i = 0; i < textbutton.Length; i++)
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
        }
        private void Tegevus(object sender, EventArgs e) 
        {
            Button nupp_sender = (Button)sender;
            if(nupp_sender.Text == "Clear the picture")
            {
                pictureBox1.Image = null;
            }
            else if (nupp_sender.Text == "Close")
            {
                Close();
            }
            else if(nupp_sender.Text == "Set the background color") 
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    pictureBox1.BackColor = colorDialog1.Color;
            }
            else if(nupp_sender.Text == "Show a picture") 
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Load(openFileDialog1.FileName);
                    Bitmap finalImg = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
                    pictureBox1.Image = finalImg;
                    pictureBox1.Show();
                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }
    }
}
