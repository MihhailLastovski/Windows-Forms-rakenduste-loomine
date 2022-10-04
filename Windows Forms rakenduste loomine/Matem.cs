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
        Timer timer = new Timer { Interval = 1000 };
        NumericUpDown[] numericUpDown = new NumericUpDown[4];
        Random rnd = new Random();
        TableLayoutPanel tableLayoutPanel;
        Label timelabel;
        int[] intnum = new int[4];
        int[] intnum2 = new int[4];
        string[] mathsymbol = new string[4] { "+", "-", "*", "/" };
        string text;
        public Matem()
        {            
            Text = "Math Quiz";
            ClientSize = new Size(500, 180);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            
            timelabel = new Label
            {
                Name = "Timer",
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(80, 30),
                Location = new System.Drawing.Point(200, 0),
                Font = new Font("Arial", 10, FontStyle.Bold)

            };
            tableLayoutPanel = new TableLayoutPanel
            {
                AutoSize = true,
                ColumnCount = 5,
                RowCount = 4,
                Location = new System.Drawing.Point(0, 60),
                BackColor = System.Drawing.Color.LightGray,
            };
            Button button = new Button
            {
                Text = "Start",
                Location = new System.Drawing.Point(200, 30),
                Size = new Size(80, 30),
            };
            timer.Enabled = true;
            button.Click += Button_Click;
            Controls.Add(button);
            timer.Tick += Timer_Tick;
            for (int i = 0; i < 4; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
                for (int j = 0; j < 5; j++)
                {
                    tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));

                    if (j == 1)
                    {
                        text = mathsymbol[i];
                    }
                    else if (j == 3)
                    {
                        text = "=";
                    }
                    else if (j==0)
                    {
                        int a = rnd.Next(1, 20);
                        text = a.ToString();
                        intnum[i] = a;
                    }
                    else if (j == 2)
                    {
                        if (mathsymbol[i] == "/" || mathsymbol[i] == "*")
                        {
                            int a = rnd.Next(1, 5);
                            text = a.ToString();
                            intnum2[i] = a;
                        }
                        else
                        {
                            int a = rnd.Next(1, 20);
                            text = a.ToString();
                            intnum2[i] = a;
                        }
                    }
               
                    if (j == 4)
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
                        Label l = new Label { Text = text };
                        tableLayoutPanel.Controls.Add(l, j, i);
                    }



                }
            }
            this.Controls.Add(tableLayoutPanel);

        }

        
        int tik = 0;
        private void Button_Click(object sender, EventArgs e)
        {
            timer.Start();
            timelabel.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(timelabel);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tik++;
            timelabel.Text = "Timer: "+tik.ToString();
            
            if (check_ans()) 
            {
                timer.Stop();
                MessageBox.Show("Very well","Very good");
            }
        }
        public bool check_ans() 
        {
                if (intnum[0] + intnum2[0] == numericUpDown[0].Value &&
                intnum[1] - intnum2[1] == numericUpDown[1].Value &&
                intnum[2] * intnum2[2] == numericUpDown[2].Value && 
                intnum[3] / intnum2[3] == numericUpDown[3].Value) { return true; }
                else { return false; }
            
        }
    }
}
