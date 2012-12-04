using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaCeHuH
{
    public partial class MainBue : Form
    {
        public MainBue()
        {
            InitializeComponent();
        }

        private void MainBue_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = Convert.ToString(e.X) + "_" + Convert.ToString(e.Y);
        }
    }
}
