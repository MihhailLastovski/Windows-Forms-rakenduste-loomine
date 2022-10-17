using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Windows_Forms_rakenduste_loomine
{
    public partial class Matching_game : Form
    {
        Random rnd = new Random();
        TableLayoutPanel tableLayoutPanel;
        Label firstClicked = null;
        Label secondClicked = null;
        //Taimerite ja nende intervallide loomine
        Timer timer1 = new Timer { Interval = 750 };
        Timer timer = new Timer { Interval = 1000 };
        int score = 0;
        int tik = 0;
        Label difficult;
        public Matching_game()
        {
            CenterToScreen(); //Tsentreerib vormi  
            Text = "Matching game";
            ClientSize = new Size(550, 550);
            BackColor = Color.Salmon;
            difficult = new Label //Sildi loomine
            {
                Text = "Valige raskusaste",
                Location = new Point(110, 100),
                Size = new Size(400, 100),
                Font = new Font("Arial", 28, FontStyle.Bold)
            };
            this.Controls.Add(difficult);
            string[] buttonstext = { "Lihtne", "Tavaline", "Raske"}; //Massiiv nupul oleva tekstiga
            int y = 200;
            for (int i = 0; i < buttonstext.Length; i++) //Nuppude loomine
            {

                Button button = new Button
                {
                    Text = buttonstext[i],
                    Location = new Point(210, y),
                    Size = new Size(100, 80)
                };
                button.Click += Button_Click; //Nuppude lisamise meetod
                this.Controls.Add(button);
                y += 100;
            }


        }
        public Matching_game(int x, int y, List<string> icons, TableLayoutPanel tableLayoutPanel) //Mänguklassi loomine
        {
            //Taimerite meetodi lisamine
            timer.Tick += Timer_Tick;
            timer1.Tick += timer1_Tick;
            tableLayoutPanel.Hide();
            for (int i = 0; i < x; i++) //Sildi loomine
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                for (int j = 0; j < y; j++)
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
            foreach (Control control in tableLayoutPanel.Controls) //Sirvib läbi kõik tabelisLayoutPanel olevad komponendid
            {
                Label iconLabel = control as Label;
                if (iconLabel != null) //Kui silt on olemas, siis
                {
                    int randomNumber = rnd.Next(icons.Count); //Genereerib juhusliku arvu
                    iconLabel.Text = icons[randomNumber]; //Võrdleb sildi teksti indeksi järgi massiivi väärtusega
                    icons.RemoveAt(randomNumber); //Eemaldab selle väärtuse massiivist
                }
                iconLabel.ForeColor = iconLabel.BackColor;
                iconLabel.Click += label1_Click;
            }
            tableLayoutPanel.Show();
            void Timer_Tick(object sender, EventArgs e)
            {
                tik++;
            }

            void label1_Click(object sender, EventArgs e)
            {
                timer.Start();
                if (timer1.Enabled == true) //Kontrollib, kas taimer on käivitatud, kontrollides atribuudi Enabled väärtust.
                    return;

                Label clickedLabel = sender as Label;

                if (clickedLabel != null) //Kui mängija valib esimese ja teise sildielemendi ning taimer käivitub
                {
                    if (clickedLabel.ForeColor == Color.Black) //Kui klõpsate juba nähtaval sildil, siis ei juhtu midagi
                        return;

                    if (firstClicked == null)
                    {
                        firstClicked = clickedLabel;
                        firstClicked.ForeColor = Color.Black;
                        return;
                    }

                    secondClicked = clickedLabel; //Jälgib teist klõpsu ja määrab sildi mustaks
                    secondClicked.ForeColor = Color.Black;
                    timer1.Start(); //Käivitab taimeri
                }
            }

            void timer1_Tick(object sender, EventArgs e)
            {
                //Kontrollib iga taimeri linnukest
                if (firstClicked.Text == secondClicked.Text) //Kui see sobib, muudab see sildi värvi mustaks
                {
                    firstClicked.ForeColor = firstClicked.ForeColor;
                    secondClicked.ForeColor = secondClicked.ForeColor;
                }
                else //Muidu peidab
                {
                    firstClicked.ForeColor = firstClicked.BackColor;
                    secondClicked.ForeColor = secondClicked.BackColor;
                    score++;
                }
                firstClicked = null;
                secondClicked = null;
                timer1.Stop(); //Peatab taimeri
                CheckForWinner(); //Käivitab funktsiooni
            }

            void CheckForWinner()
            {
                foreach (Control control in tableLayoutPanel.Controls) //Sirvib läbi kõik tabelisLayoutPanel olevad komponendid
                {
                    //Kui silmus läbib kõik oksad ja ei naase, siis on mäng läbi
                    Label iconLabel = control as Label;

                    if (iconLabel != null)
                    {
                        if (iconLabel.ForeColor == iconLabel.BackColor)
                            return;
                    }
                }
                timer.Stop(); //Peatab taimeri
                FailedScoreTofile(score); //Käivitab funktsiooni
                PlaySound();
                System.Threading.Thread.Sleep(1000);
                MessageBox.Show("Sa sobitasid kõik ikoonid!", "Palju õnne"); //Kuvab teate mängu lõppemise kohta
                FromFile();
                restarGame(); //Peatab taimeri
            }
            void restarGame() //Taaskäivitab vormi sõltuvalt vastusest
            {
                this.Controls.Clear();
                if (MessageBox.Show($"Vead: {score.ToString()}\nAeg sekundid: {tik.ToString()}!\nKas soovite uuesti mängida?", "Tulemus!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Controls.Clear();
                    Application.Restart();
                    Environment.Exit(0);
                }
                else
                {
                    this.Controls.Clear();
                    Application.Exit();
                }

            }

            void FailedScoreTofile(int score) //Meetod kirjutab faili halbade vastete arvu ja kulunud aja
            {
                StreamWriter to_file = new StreamWriter(@"..\..\..\Score.txt", true);

                to_file.Write($"Vead: {score.ToString()} -- Aeg sekundid: {tik.ToString()}sek\n");
                to_file.Close();
            }
            void FromFile() //Meetod loob vormi, milles loob sildid ja kuvab neis olevast failist teavet
            {
                int y_ = 20;
                Form form = new Form();
                form.Text = "Punktide tabel";
                MaximizeBox = false;
                form.ClientSize = new Size(400, 1200);
                string[] readText = File.ReadAllLines(@"..\..\..\Score.txt");
                for (int i = 0; i < readText.Length; i++)
                {
                    Label lbl = new Label
                    {
                        AutoSize = true,
                        Text = readText[i],
                        BackColor = Color.Azure,
                        Location = new Point(20,y_),
                        Font = new Font("Arial", 18, FontStyle.Bold),
                    };
                    form.Controls.Add(lbl);
                    y_ += 50;
                }
                form.ShowDialog();
            }

            void PlaySound() //Meetod mängib muusikat
            {
                SoundPlayer player = new SoundPlayer(@"..\..\..\effect\win.wav");
                player.Play();
            }

        }
        //Ikoonidega loendid
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", "r", "r",
            "b", "b", "v", "v", "~", "~",
        };
        List<string> icons_2 = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };
        List<string> icons_3 = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k", "`", "`",
            "b", "b", "v", "v", "w", "w", "z", "z", "f", "f"
        };
        private void Button_Click(object sender, EventArgs e) //Meetod raskete mängude valimiseks
        {
            Button nupp_sender = (Button)sender;
            this.Controls.Clear(); //Tühjendab vormi
            tableLayoutPanel = new TableLayoutPanel
            {
                BackColor = Color.MediumVioletRed,
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset,
            };
            Controls.Add(tableLayoutPanel);
            //Kontrollib, millist nuppu vajutati
            if (nupp_sender.Text == "Lihtne")
            {

                new Matching_game(4, 3, icons, tableLayoutPanel);

            }
            else if (nupp_sender.Text == "Tavaline")
            {

                new Matching_game(4, 4, icons_2, tableLayoutPanel);

            }
            else if (nupp_sender.Text == "Raske")
            {

                new Matching_game(5, 4, icons_3, tableLayoutPanel);
            }
        }
    }
}
      