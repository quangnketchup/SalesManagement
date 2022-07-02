using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmOrderManagements : Form
    {
        public bool isAdmin { get; set; }
        public frmOrderManagements()
        {
            InitializeComponent();
        }

        private void frmOrderManagements_Load(object sender, EventArgs e)
        {

        }
    }
}
