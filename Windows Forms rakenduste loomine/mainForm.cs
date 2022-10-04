using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Windows_Forms_rakenduste_loomine
{
    public partial class mainForm : Form
    { 
        TextBox textBox;
        public mainForm()
        {
            CenterToScreen();
            ClientSize = new Size(800, 300);
            Text = "Main Form";
            Label label = new Label 
            {
                AutoSize = false,
                Size = new Size(400, 100),
                Location = new Point(200, 80),
                Text = "Write: \n/run(form1/form2/form3) \nand press 'Enter'",
                Font = new Font("Segoe UI", 18, FontStyle.Regular),
                BorderStyle = BorderStyle.Fixed3D,
                BackColor = Color.GhostWhite,
            };
            textBox = new TextBox
            {
                AutoSize = false,
                Size = new Size(400, 50),
                Location = new Point(200, 200),
                Text = "",
                Font = new Font("Segoe UI", 21, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.GhostWhite,

            };
            textBox.KeyDown += TextBox_KeyDown;
            this.Controls.Add(textBox);
            this.Controls.Add(label);

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonTest_Click(this, new EventArgs());
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            ImageForm imgForm = new ImageForm();
            Matem mathForm = new Matem();
            Matching_game matchingForm = new Matching_game();
            if (textBox.Text == "/run(form1)")
            {    
               imgForm.ShowDialog();

            }
            else if(textBox.Text == "/run(form2)")
            {
                mathForm.ShowDialog();
            }
            else if (textBox.Text == "/run(form3)")
            {
                matchingForm.ShowDialog();
            }
            else 
            {
                MessageBox.Show("You wrote something wrong!", "Error");
            }
        }


    }
}
