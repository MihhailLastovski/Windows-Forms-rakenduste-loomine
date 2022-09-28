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
    public partial class Matem : Form
    {
        public Matem()
        {
            TableLayoutPanel tableLayoutPanel;
            Label timelabel, sumleftLabel, sumrightLabel;
            String[,] l_nimi;
            string[] mathsymbol = new string[4] { "+", "-", "*", "/" };
            Text = "Math Quiz";
            ClientSize = new Size(500, 400);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Text = "Enter";
            Label mainlbl = new Label
            {
                Name = "TimeTable",
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(200, 30),
                Font = new Font("Arial",16,FontStyle.Bold)

            };
            
            
        }
    }
}
