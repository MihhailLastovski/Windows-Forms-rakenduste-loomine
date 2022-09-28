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
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            colorDialog1 = new ColorDialog();
            openFileDialog1 = new OpenFileDialog();
            pictureBox1 = new PictureBox();
            checkBox1 = new CheckBox();
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                RowCount = 2,
                Size = new Size(529, 330),
                TabIndex = 0,
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
                Location = new Point(3, 3),
                Size = new Size(523, 291),
                TabIndex = 0,
                TabStop = false,
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            tableLayoutPanel.SetColumnSpan(pictureBox1, 2);
            checkBox1 = new CheckBox
            {
                AutoSize = true,
                Location = new Point(3, 300),
                Size = new Size(60, 17),
                TabIndex = 1,
                Text = "Stretch",
                UseVisualStyleBackColor = true,
            };
            checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Location = new Point(82, 300),
                Size = new Size(444, 27),
                TabIndex = 2
            };

            Button showButton = new Button
            {
                AutoSize = true,
                Location = new Point(353, 3),
                Name = "showButton",
                Size = new Size(88, 23),
                TabIndex = 0,
                Text = "Show a picture",
                UseVisualStyleBackColor = true
            };
            Button clearButton = new Button
            {
                AutoSize = true,
                Location = new Point(253, 3),
                Name = "clearButton",
                Size = new Size(94, 23),
                TabIndex = 1,
                Text = "Clear the picture",
                UseVisualStyleBackColor = true
            };
            Button backgroundButton = new Button
            {
                AutoSize = true,
                Location = new Point(110, 3),
                Name = "backgroundButton",
                Size = new Size(137, 23),
                TabIndex = 2,
                Text = "Set the background color",
                UseVisualStyleBackColor = true
            };
            Button closeButton = new Button
            {
                AutoSize = true,
                Location = new Point(29, 3),
                Name = "closeButton",
                Size = new Size(75, 23),
                TabIndex = 3,
                Text = "Close",
                UseVisualStyleBackColor = true
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
            Button[] butttons = { showButton, clearButton, backgroundButton, closeButton};
            foreach (Button item in butttons)
            {
                item.Click += Tegevus;
                flowLayoutPanel1.Controls.Add(item);
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
        //private void ShowButton_Click(object sender, EventArgs e)
        //{
        //    if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        pictureBox1.Load(openFileDialog1.FileName);
        //        Bitmap finalImg = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
        //        pictureBox1.Image = finalImg;
        //        pictureBox1.Show();
        //    }
        //}


        //private void clearButton_Click(object sender, EventArgs e)
        //{
        //    pictureBox1.Image = null;

        //}

        //private void backgroundButton_Click(object sender, EventArgs e)
        //{
        //    if (colorDialog1.ShowDialog() == DialogResult.OK)
        //        pictureBox1.BackColor = colorDialog1.Color;
        //}

        //private void closeButton_Click(object sender, EventArgs e)
        //{
        //    Close();
        //}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }
    }
}
