using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Windows_Forms_rakenduste_loomine
{
    public partial class Matem : Form
    {
        public Matem()
        {
            TableLayoutPanel tableLayoutPanel;
            Label timelabel, sumleftLabel, sumrightLabel;
            Random rnd = new Random();
            string[] mathsymbol = new string[4] { "+", "-", "*", "/" };
            Text = "Math Quiz";
            ClientSize = new Size(500, 180);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            string text;
            timelabel = new Label
            {
                Name = "Timer",
                Text = "Timer",
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(80, 30),
                Location = new System.Drawing.Point(200, 0),
                Font = new Font("Arial", 16, FontStyle.Bold)

            };
            tableLayoutPanel = new TableLayoutPanel
            {
                AutoSize = true,
                ColumnCount = 5,
                RowCount = 4,
                Location = new System.Drawing.Point(0, 60),
                BackColor = System.Drawing.Color.LightGray,
            };
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
                    else if (j == 4)
                    {
                        text = "";
                    }
                    else
                    {
                        if (mathsymbol[i] == "/" || mathsymbol[i] == "*")
                        {
                            text = rnd.Next(1, 5).ToString();
                        }
                        else { text = rnd.Next(1, 40).ToString(); }
                    }
                    Label l = new Label { Text = text };
                    tableLayoutPanel.Controls.Add(l, j, i);
                    if (j==4)
                    {
                        NumericUpDown numericUpDown = new NumericUpDown
                        {
                            Font = new Font("Arial", 16, FontStyle.Bold),
                            Size = new Size(10,10),
                        };
                        
                        tableLayoutPanel.Controls.Add(numericUpDown, j, i);
                    }
                }
            }
            this.Controls.Add(tableLayoutPanel);
            this.Controls.Add(timelabel);

        }
    }
}
