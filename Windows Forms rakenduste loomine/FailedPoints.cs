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
    public partial class FailedPoints : Form
    {
        Label lblpoints;
        public FailedPoints(string[] from_file)
        {
            CenterToScreen();
            ClientSize = new Size(500, 500);
            WebBrowser webBrowser = new WebBrowser();
            foreach (var item in from_file)
            {
                int y=0;
                lblpoints = new Label 
                { 
                    Text = item,
                    Location = new Point(50,y),
                    Size = new Size(50,50)
                };
                y += 100;
                Controls.Add(lblpoints);  
            }
        }
    }
}
