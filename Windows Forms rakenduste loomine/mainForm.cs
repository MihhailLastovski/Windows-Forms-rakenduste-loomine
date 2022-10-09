
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
        ListBox listbox;
        ImageForm imageForm = new ImageForm();
        Matem matem = new Matem();
        Matching_game matching_Game = new Matching_game();
        public mainForm()
        {
            CenterToScreen(); //Tsentreerib vormi  
            BackColor = Color.AliceBlue;
            ClientSize = new Size(800, 300);
            Text = "Peavorm";
            Label label = new Label //Sildi loomine
            {
                AutoSize = false,
                Size = new Size(150, 40),
                Location = new Point(315, 70),
                Text = "Valige vorm",
                Font = new Font("Segoe UI", 18, FontStyle.Regular),
                BorderStyle = BorderStyle.Fixed3D,
                BackColor = Color.GhostWhite,
            };
            listbox = new ListBox //ListBox klassi objekti loomine
            {
                AutoSize = false,
                Size = new Size(400, 100),
                Location = new Point(200, 150),
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.GhostWhite,

            };
            for (int i = 1; i < 4; i++) //Andmete lisamine ListBoxi
            {
                listbox.Items.Add("Vorm "+i.ToString());
            }
            Button button = new Button //Nupu loomine
            {
                Location = new Point(315, 250),
                Size = new Size(150, 40),
                Text = "Vali"
            };
            this.Controls.Add(listbox);
            this.Controls.Add(label);
            this.Controls.Add(button);
            button.Click += Button_Click;

        }

        private void Button_Click(object sender, EventArgs e) //Meetod, mis töötab nupu klõpsamisel ja kontrollib, mis on ListBoxi valitud
        {
            if (listbox.Items[listbox.SelectedIndex].ToString() == "Vorm 1")
            {
                imageForm.ShowDialog();
            }
            else if (listbox.Items[listbox.SelectedIndex].ToString() == "Vorm 2")
            {
                matem.ShowDialog();
            }
            else if (listbox.Items[listbox.SelectedIndex].ToString() == "Vorm 3")
            {
                matching_Game.ShowDialog();
            }
        }
    }
}
