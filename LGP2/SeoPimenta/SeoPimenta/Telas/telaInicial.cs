using SeoPimenta.Classes;
using SeoPimenta.Classes.CadastroProduto;
using SeoPimenta.Telas.CadastroProcuraProdutos;
using SeoPimenta.Telas.menuFuncionarioUsuario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SeoPimenta
{
    public partial class telaInicial : Form
    {
        private Cadastro cadastro; //<-- Cadastro de produtos
        Usuario usuarioLogado;
        telaLogin telaLogin;

        public telaInicial()
        {
            InitializeComponent();
        }

        public telaInicial(telaLogin telaLogin, Usuario usuarioLogado)
        {
            InitializeComponent(); // Chama a inicialização dos componentes
            this.telaLogin = telaLogin;
            this.usuarioLogado = usuarioLogado;
        }

        private Form formularioAtivo = null;

        private void openChildFormInPanel(Form childForm)
        {
            if (formularioAtivo != null)
            {
                formularioAtivo.Close();
            }
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
            escodeSubMenu();
            subMenu.Visible = !subMenu.Visible;
        }

        private void carregarImagemPerfil()
        {
            byte[] imagemBytes = usuarioLogado.getImagem();

            if (imagemBytes != null && imagemBytes.Length > 0)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(imagemBytes))
                    {
                        imgUsuario.Image = Image.FromStream(ms);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar a imagem: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    imgUsuario.Image = null; // Ou defina uma imagem padrão de erro
                }
            }
            else
            {
                imgUsuario.Image = null; // Ou defina uma imagem padrão
            }
        }
        private void carregarUsuario()
        {
            try
            {
                lblName.Text = usuarioLogado.getNome() +"!";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar nome: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                imgUsuario.Image = null; // Ou defina uma imagem padrão de erro
            }
            carregarImagemPerfil();
        }
        private void telaInicial_Load(object sender, EventArgs e)
        {
            escodeSubMenu();
            carregarUsuario();
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
            // Implementação do evento de exclusão de usuários
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Implementação do evento para o botão 1
        }

        private void btnVizualizarFuncionarios_Click(object sender, EventArgs e)
        {
            // Implementação do evento de visualização de funcionários
        }

     

        private void btnCadastrarFuncionarios_Click(object sender, EventArgs e)
        {
            // Implementação do evento de cadastro de funcionários
        }

        private void btnEditarFuncionarios_Click(object sender, EventArgs e)
        {
            // Implementação do evento de edição de funcionários
        }

        private void btnMenuFuncionarios_Click_1(object sender, EventArgs e)
        {
            mostrarSubMenu(painelMenuFuncionarios);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(painelMenuFornecedores);
        }

        private void btnCadastrarUsuarios_Click_1(object sender, EventArgs e)
        {
            // Implementação do evento de cadastro de usuários
        }

        private void btnCadastrarUsuarios_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new cadastrarUser());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void navBar_telaInicial_Enter(object sender, EventArgs e)
        {
            // Implementação do evento quando a barra de navegação é selecionada
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Implementação do evento quando a barra de navegação é selecionada
            this.WindowState = FormWindowState.Minimized;
        }



        private void btnVoltar_Click(object sender, EventArgs e)
        {
            if (formularioAtivo != null)
            {
                formularioAtivo.Close();
                formularioAtivo = null; // Opcional: Limpa a referência para evitar problemas futuros
            }
        }

        private void btnEditarUsuarios_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new editarUser(usuarioLogado));

        }

        private void btnVizualizarUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se o nível do usuário logado é 2
                if (usuarioLogado.getNivel() != 2)
                {
                    // Lança uma exceção se o nível não for 2
                    throw new UnauthorizedAccessException("Você não tem permissão para acessar esta funcionalidade.");
                }

                // Se o usuário tiver nível 2, abre o formulário de visualização
                openChildFormInPanel(new visualizarUser());
            }
            catch (UnauthorizedAccessException ex)
            {
                // Mostra uma mensagem de erro caso o usuário não tenha o nível necessário
                MessageBox.Show(ex.Message, "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Captura qualquer outro erro que possa ocorrer
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCadastrarProdutos_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new TelaCadastroProduto(cadastro));
        }
    }
}
