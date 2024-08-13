using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeoPimenta
{
    public partial class MenuPrincipal : Form
    {
        private telaLogin telaLogin;
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        public MenuPrincipal(telaLogin telaLogin)
        {
            InitializeComponent();
            this.telaLogin = telaLogin;
        }
        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
           
        }
    }
}
