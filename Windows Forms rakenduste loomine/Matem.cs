using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Windows_Forms_rakenduste_loomine
{
    public partial class Matem : Form
    {
        int x, y;
        Timer timer = new Timer { Interval = 1000 };
        NumericUpDown[] numericUpDown = new NumericUpDown[4];
        Random rnd = new Random();
        TableLayoutPanel tableLayoutPanel;
        Label timelabel;
        //Massiivide deklareerimine arvude jaoks näidetest
        int[] intnum = new int[4];
        int[] intnum2 = new int[4];
        //Massiivide deklareerimine matemaatiliste märkidega
        string[] mathsymbol = new string[4] { "+", "-", "*", "/" };
        string text;
        Button showAns;
        public Matem()
        {
            CenterToScreen(); //Tsentreerib vormi  
            Text = "Matemaatika Quiz";
            ClientSize = new Size(600, 180);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false; //Takistab vormi avamist täisekraanil
            Label difficult = new Label //Sildi loomine
            {
                Text = "Valige raskusaste",
                Location = new Point(225, 15),
                Size = new Size(200, 20),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            this.Controls.Add(difficult);
            string[] buttonstext = { "Lihtne", "Tavaline", "Raske" }; //Massiiv nupul oleva tekstiga
            int x = 170;
            for (int i = 0; i < buttonstext.Length; i++) //Nuppude loomine
            {

                Button buttons = new Button
                {
                    Text = buttonstext[i],
                    Location = new Point(x, 50),
                    Size = new Size(80, 40)
                };
                buttons.Click += difficultChoice; //Nuppude lisamise meetod
                this.Controls.Add(buttons);
                x += 80;
            }
        }
        public Matem(int x, int y) 
        {
            this.x = x;
            this.y = y;
            CenterToScreen(); //Tsentreerib vormi  
            Text = "Matemaatika Quiz";
            ClientSize = new Size(600, 180);

            timelabel = new Label
            {
                Name = "Taimer",
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(80, 30),
                Location = new System.Drawing.Point(200, 0),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            tableLayoutPanel = new TableLayoutPanel //Taimeri sildi loomine
            {
                AutoSize = true,
                ColumnCount = 6,
                RowCount = 4,
                Location = new System.Drawing.Point(0, 60),
                BackColor = System.Drawing.Color.LightGray,
            };
            //Nuppude loomine
            Button button = new Button
            {
                Text = "Alusta",
                Location = new System.Drawing.Point(200, 30),
                Size = new Size(80, 30),
            };
            Button checkans = new Button
            {
                Text = "Kontrollige\n vastuseid",
                Location = new System.Drawing.Point(300, 25),
                Size = new Size(80, 35),
            };

            showAns = new Button
            {
                Text = "Näita\n vastuseid",
                Location = new System.Drawing.Point(100, 25),
                Size = new Size(80, 35),
            };
            Button newExamples = new Button
            {
                Text = "Uued\n näited",
                Location = new System.Drawing.Point(400, 25),
                Size = new Size(80, 35),
            };
            //Funktsioonide lisamine nuppudele
            checkans.Click += Checkans_Click;
            button.Click += Button_Click;
            showAns.Click += showTrueAns;
            newExamples.Click += NewExamples_Click;
            Controls.Add(newExamples);
            Controls.Add(button);
            Controls.Add(checkans);
            timer.Tick += Timer_Tick;
            this.Controls.Add(tableLayoutPanel);

            //Massiivide ja vormi puhastamine
            intnum = new int[4];
            intnum2 = new int[4];
            showAns.Controls.Clear();
            tableLayoutPanel.Controls.Clear();
            if (showAns.Enabled == false) //Kui nupp on keelatud, siis lubage see
            {
                showAns.Enabled = true;
            }
            for (int i = 0; i < 4; i++) //Ridade ja veergude loomine
            {
                tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
                for (int j = 0; j < 5; j++)
                {
                    tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));

                    if (j == 1) //Tekst muutub olenevalt veerust
                    {
                        text = mathsymbol[i];
                    }
                    else if (j == 3)
                    {
                        text = "=";
                    }
                    else if (j == 0)
                    {
                        int a = rnd.Next(1, x);
                        text = a.ToString();
                        intnum[i] = a;
                    }
                    else if (j == 2)
                    {
                        if (mathsymbol[i] == "/" || mathsymbol[i] == "*")
                        {
                            int a = rnd.Next(1, y);
                            text = a.ToString();
                            intnum2[i] = a;
                        }
                        else
                        {
                            int a = rnd.Next(1, x);
                            text = a.ToString();
                            intnum2[i] = a;
                        }
                    }

                    if (j == 4) //Veerg 5 loob objektid klassist NumericUpDown
                    {
                        numericUpDown[i] = new NumericUpDown
                        {
                            Font = new Font("Arial", 12, FontStyle.Bold),
                            Name = mathsymbol[i],
                            Size = new Size(50, 50),
                            DecimalPlaces = 2,
                            Minimum = -20
                        };

                        tableLayoutPanel.Controls.Add(numericUpDown[i], j, i);
                    }
                    else
                    {
                        Label l = new Label { Text = text }; //Konkreetse tekstiga sildi loomine
                        tableLayoutPanel.Controls.Add(l, j, i);
                    }



                }
            }

        }
        Matem matem;
        
        public void difficultChoice(object sender, EventArgs e) 
        {
            this.Controls.Clear();
            Button nupp_sender = (Button)sender;
            if (nupp_sender.Text == "Lihtne")
            {
                x = 20;
                y = 2;
            }
            else if (nupp_sender.Text == "Tavaline")
            {
                x = 30;
                y = 3;
            }
            else if (nupp_sender.Text == "Raske")
            {
                x = 50;
                y = 5;
            }
            matem = new Matem(x, y);
            matem.Show();
        }
        public void NewExamples_Click(object sender, EventArgs e) //Meetod tühjendab vormi ja täidab selle uute näidetega
        {
            matem = new Matem(this.x, this.y);
            matem.Show();
        }

        private void Checkans_Click(object sender, EventArgs e) //Sisestatud vastuste kinnitamise meetod
        {
            
            if (intnum[0] + intnum2[0] == numericUpDown[0].Value &&
            intnum[1] - intnum2[1] == numericUpDown[1].Value &&
            intnum[2] * intnum2[2] == numericUpDown[2].Value &&
            intnum[3] / intnum2[3] == numericUpDown[3].Value)
            {
                timer.Stop();
            }
            else 
            {
                Controls.Add(showAns); //Lisab veel ühe nupu
            }
            
        }

        int tik = 0;
        private void Button_Click(object sender, EventArgs e) //Taimer käivitub
        {
            timer.Start();
            timelabel.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(timelabel);
        }

        private void Timer_Tick(object sender, EventArgs e) //Taimeri töö
        {
            tik++;
            timelabel.Text = "Taimer: " + tik.ToString();
            
        }
        private void showTrueAns(object sender, EventArgs e) //Meetod loendab näiteid ja kuvab õiged vastused
        {
            int[] ans = new int[4];
            ans[0] = intnum[0] + intnum2[0];
            ans[1] = intnum[1] - intnum2[1];
            ans[2] = intnum[2] * intnum2[2];
            ans[3] = intnum[3] / intnum2[3];
            for (int i = 0; i < 4; i++)
            {
                Label l = new Label { Text = ans[i].ToString() };
                tableLayoutPanel.Controls.Add(l, 6, i);
            }
            showAns.Enabled = false;

        }
    }
}
