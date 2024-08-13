using MySql.Data.MySqlClient;
using SeoPimenta.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeoPimenta
{
    public partial class telaLogin : Form
    {
        public telaLogin()
        {
            InitializeComponent();
        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
                Application.Exit();
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
                try
                {
                    Usuario usuario = new Usuario();
                    ConexaoBanco conexao = new ConexaoBanco();

                // Comando SQL
                string command = "SELECT usuario.nome, usuario.email, usuario.senha, funcionario.id, funcionario.cpf, " +
                  "endereco.logradouro, endereco.numero, endereco.bairro, endereco.cidade, endereco.estado, endereco.cep " +
                  "FROM usuario " +
                  "INNER JOIN funcionario ON usuario.id_funcionario = funcionario.id " +
                  "INNER JOIN endereco ON funcionario.id_endereco = endereco.id " +
                  "WHERE usuario.email=@Email AND usuario.senha=@Senha";

                // Prepara o comando
                conexao.consulta(command);
                conexao.cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                conexao.cmd.Parameters.AddWithValue("@Senha", txtSenha.Text);


                conexao.consulta(command);
                    conexao.cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    conexao.cmd.Parameters.AddWithValue("@Senha", txtSenha.Text);

                    using (MySqlDataReader leitor = conexao.cmd.ExecuteReader())
                    {
                        if (leitor.HasRows && leitor.Read()) // Verifica se há linhas e avança para o primeiro registro
                        {

                            usuario.setNome(leitor["nome"].ToString());
                            usuario.setEmail(leitor["email"].ToString());
                            usuario.setCpf(leitor["cpf"].ToString());
                            usuario.setSenha(leitor["senha"].ToString());

                            // Preencha as informações do endereço
                            Endereco endereco = new Endereco();
                            endereco.setRua(leitor["logradouro"].ToString());
                            endereco.setNumero(leitor["numero"].ToString());
                            endereco.setBairro(leitor["bairro"].ToString());
                            endereco.setCidade(leitor["cidade"].ToString());
                            endereco.setEstado(leitor["estado"].ToString());
                            endereco.setCep(leitor["cep"].ToString());


                            usuario.setEndereco(endereco);

                            conexao.fecharConexao();

                             telaInicial telaIncial = new telaInicial(this);
                             this.Hide();
                             telaIncial.Show();
                        

                    }
                        else
                        {
                            throw new Exception("Login inválido");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao fazer login: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Text = "";
                    txtSenha.Text = "";
                    txtEmail.Select();
                }
            
        }
    }
}
