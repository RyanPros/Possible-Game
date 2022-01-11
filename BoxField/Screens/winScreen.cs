using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BoxField
{
    public partial class winScreen : UserControl
    {
        public winScreen()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameScreen gs = new GameScreen();
            this.Controls.Add(gs);
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
