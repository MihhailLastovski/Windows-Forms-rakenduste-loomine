using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Windows_Forms_rakenduste_loomine
{
    public partial class Matching_game : Form
    {
        Random rnd = new Random();
        TableLayoutPanel tableLayoutPanel;
        Label firstClicked = null;
        Label secondClicked = null;
        Timer timer1 = new Timer { Interval = 750 };
        Timer timer = new Timer { Interval = 1000 };
        int score = 0;
        string[] from_file;
        int tik = 0;
        Label difficult;

        public Matching_game()
        {
            CenterToScreen();
            timer.Tick += Timer_Tick;
            timer1.Tick += timer1_Tick;
            Text = "Matching game";
            ClientSize = new Size(550, 550);
            BackColor = Color.Salmon;
            difficult = new Label 
            {
                Text = "Valige raskusaste",
                Location = new Point(110,100),
                Size = new Size(400,100),
                Font = new Font("Arial", 28, FontStyle.Bold)
            };
            this.Controls.Add(difficult);
            string[] buttonstext = { "Lihtne", "Tavaline", "Raske" };
            int y = 200;
            for (int i = 0; i < buttonstext.Length; i++)
            {
                
                Button button = new Button 
                {
                    Text = buttonstext[i], 
                    Location = new Point(210,y),
                    Size = new Size(100,80)
                };
                button.Click += Button_Click;
                this.Controls.Add(button);
                y += 100;
            }
            
            
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button nupp_sender = (Button)sender;
            this.Controls.Clear();
            tableLayoutPanel = new TableLayoutPanel
            {
                BackColor = Color.MediumVioletRed,
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset,
            };
            this.Controls.Add(tableLayoutPanel);
            if (nupp_sender.Text == "Lihtne")
            {
                List<string> icons = new List<string>()
                {
                    "!", "!", "N", "N", "r", "r",
                    "b", "b", "v", "v", "~", "~",
                };
                for (int i = 0; i < 4; i++)
                {
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                    for (int j = 0; j < 3; j++)
                    {

                        Label lbl = new Label
                        {
                            BackColor = Color.Coral,
                            AutoSize = false,
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Webdings", 48, FontStyle.Bold),
                        };


                        tableLayoutPanel.Controls.Add(lbl, i, j);
                    };

                }
                foreach (Control control in tableLayoutPanel.Controls)
                {
                    Label iconLabel = control as Label;
                    if (iconLabel != null)
                    {
                        int randomNumber = rnd.Next(icons.Count);
                        iconLabel.Text = icons[randomNumber];
                        icons.RemoveAt(randomNumber);
                    }
                    iconLabel.ForeColor = iconLabel.BackColor;
                    iconLabel.Click += label1_Click;
                }
            }
            else if(nupp_sender.Text == "Tavaline") 
            {
                List<string> icons = new List<string>()
                {
                    "!", "!", "N", "N", ",", ",", "k", "k",
                    "b", "b", "v", "v", "w", "w", "z", "z"
                };
                for (int i = 0; i < 4; i++)
                {
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                    for (int j = 0; j < 4; j++)
                    {

                        Label lbl = new Label
                        {
                            BackColor = Color.Coral,
                            AutoSize = false,
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Webdings", 48, FontStyle.Bold),
                        };


                        tableLayoutPanel.Controls.Add(lbl, i, j);
                    };

                }
                foreach (Control control in tableLayoutPanel.Controls)
                {
                    Label iconLabel = control as Label;
                    if (iconLabel != null)
                    {
                        int randomNumber = rnd.Next(icons.Count);
                        iconLabel.Text = icons[randomNumber];
                        icons.RemoveAt(randomNumber);
                    }
                    iconLabel.ForeColor = iconLabel.BackColor;
                    iconLabel.Click += label1_Click;
                }
            }
            else if (nupp_sender.Text == "Raske") 
            {
                List<string> icons = new List<string>()
                {
                    "!", "!", "N", "N", ",", ",", "k", "k", "`", "`",
                    "b", "b", "v", "v", "w", "w", "z", "z", "f", "f"
                };
                for (int i = 0; i < 5; i++)
                {
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                    for (int j = 0; j < 4; j++)
                    {

                        Label lbl = new Label
                        {
                            BackColor = Color.Coral,
                            AutoSize = false,
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Webdings", 48, FontStyle.Bold),
                        };


                        tableLayoutPanel.Controls.Add(lbl, i, j);
                    };

                }
                foreach (Control control in tableLayoutPanel.Controls)
                {
                    Label iconLabel = control as Label;
                    if (iconLabel != null)
                    {
                        int randomNumber = rnd.Next(icons.Count);
                        iconLabel.Text = icons[randomNumber];
                        icons.RemoveAt(randomNumber);
                    }
                    iconLabel.ForeColor = iconLabel.BackColor;
                    iconLabel.Click += label1_Click;
                }
            }
            
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tik++;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            timer.Start();
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                timer1.Start();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {  
            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked.ForeColor = firstClicked.ForeColor;
                secondClicked.ForeColor = secondClicked.ForeColor;
            }
            else 
            {
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = secondClicked.BackColor;
                score++;
            }
            firstClicked = null;
            secondClicked = null;
            timer1.Stop();
            CheckForWinner();
        }
        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }
            timer.Stop();
            FailedScoreTofile(score);
            MessageBox.Show("Sa sobitasid kõik ikoonid!", "Palju õnne");
            MessageBox.Show($"Vead: {score.ToString()}\nAeg sekundid: {tik.ToString()}!", "Tulemus!");
        }
        public void FailedScoreTofile(int score) 
        {
            StreamWriter to_file = new StreamWriter(@"..\..\..\Score.txt", true);

            to_file.Write(score.ToString()+ " -- " + tik.ToString()+"sek"+"\n");
            to_file.Close();
        }



    }
}
