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
            Size = new Size(400, 400);
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel 
            {
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                RowCount = 2,
                Size = new Size(529, 330),
                TabIndex = 0,
            };
            this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
            this.tableLayoutPanel.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.checkBox1, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel1, 1, 1);
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            PictureBox pictureBox1 = new PictureBox 
            {
                BorderStyle = BorderStyle.Fixed3D,
                Dock = DockStyle.Fill,
                Location = new Point(3, 3),
                Size = new Size(523, 291),
                TabIndex = 0,
                TabStop = false
        };
            this.tableLayoutPanel.SetColumnSpan(this.pictureBox1, 2);
            CheckBox checkBox1 = new CheckBox 
            {
                AutoSize = true,
                Location = new Point(3, 300),
                Size = new Size(60, 17),
                TabIndex = 1,
                Text = "Stretch",
                UseVisualStyleBackColor = true,
            };
            checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            InitializeComponent();
            
        }
        private void ShowButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }


        private void clearButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;

        }

        private void backgroundButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                pictureBox1.BackColor = colorDialog1.Color;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
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
