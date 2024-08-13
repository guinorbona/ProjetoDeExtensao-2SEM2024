using SeoPimenta.Telas.menuFuncionarioUsuario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SeoPimenta
{
    public partial class telaInicial : Form
    {

        private telaLogin telaLogin;
        public telaInicial()
        {
            InitializeComponent();
        }
        public telaInicial(telaLogin telaLogin)
        {
            this.telaLogin = telaLogin;
        }

        private Form formularioAtivo = null;
        private void openChildFormInPanel(Form childForm)
        {
            if (formularioAtivo != null)
                formularioAtivo.Close();
                formularioAtivo = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                panelChildForm.Controls.Add(childForm);
                panelChildForm.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();
        }

        private void escodeSubMenu()
        {
            painelEstoqueProdutos.Visible = false;
            painelMenuFuncionarios.Visible = false;
            painelMenuFornecedores.Visible = false;
        }

        private void mostrarSubMenu(Panel subMenu)
        {
                if(subMenu.Visible == false) 
            {
                escodeSubMenu();
                subMenu.Visible = true;
            }
                else
                subMenu.Visible = false;
        }
        private void body_telaInicial_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void telaInicial_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }


        private void navBar_telaInicial_Enter(object sender, EventArgs e)
        {

        }

        private void btnVisualizarUser_telaFuncioUser_Click(object sender, EventArgs e)
        {
            
            this.Close();

           
        }

        private void txtFuncionario_telaInicial_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new telaFuncioUser());
        }

        private void btnEstoqueProdutos_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(painelEstoqueProdutos);
        }

        private void btnMenuFuncionarios_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(painelMenuFuncionarios);
        }

        private void btnExcluirUsuarios_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnVizualizarFuncionarios_Click(object sender, EventArgs e)
        {

        }

        private void btnEditarUsuarios_Click(object sender, EventArgs e)
        {

        }

        private void btnCadastrarFuncionarios_Click(object sender, EventArgs e)
        {

        }

        private void btnEditarFuncionarios_Click(object sender, EventArgs e)
        {

        }

        private void btnCadastrarUsuarios_Click(object sender, EventArgs e)
        {

        }

        private void btnMenuFuncionarios_Click_1(object sender, EventArgs e)
        {
            mostrarSubMenu(painelMenuFuncionarios);
        }

        private void btnCadastrarFuncionarios_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(painelMenuFornecedores);
        }
    }
}
