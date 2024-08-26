using MySql.Data.MySqlClient;
using SeoPimenta.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography;
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


        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void btnEntrar_Click(object sender, EventArgs e)
        {

            ConexaoBanco conexao = new ConexaoBanco();
            Usuario usuario = new Usuario();
            try
            {

                string senhahash = usuario.GerarHashSHA256(txtSenha.Text);

                // Comando SQL
                string command = "SELECT usuario.id,usuario.nivel, usuario.usuario, usuario.senha,usuario.imagem, funcionario.id AS funcionario_id,funcionario.nome, funcionario.cpf, " +
                    "endereco.logradouro, endereco.numero, endereco.bairro, endereco.cidade, endereco.estado, endereco.cep " +
                    "FROM seopimenta.usuario " +
                    "INNER JOIN seopimenta.funcionario ON usuario.id_funcionario = funcionario.id " +
                    "INNER JOIN seopimenta.endereco ON funcionario.id_endereco = endereco.id " +
                    "WHERE usuario.usuario = @Usuario AND usuario.senha = @Senha AND usuario.disponibilidade = 1";

                // Prepara o comando
                conexao.consulta(command);
                conexao.cmd.Parameters.Clear(); // Limpa parâmetros anteriores, se houver
                conexao.cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                conexao.cmd.Parameters.AddWithValue("@Senha", senhahash);

                using (MySqlDataReader leitor = conexao.cmd.ExecuteReader())
                {
                    if (leitor.HasRows && leitor.Read())
                    {
                        // Utiliza Convert.ToInt32 e Convert.ToString para garantir conversões seguras
                        usuario.setUsuarioID(Convert.ToInt32(leitor["id"]));
                        usuario.setNome((Convert.ToString(leitor["nome"])));
                        usuario.setNivel(Convert.ToInt32(leitor["nivel"]));
                        usuario.setEmail(Convert.ToString(leitor["usuario"])); // Certifique-se de que isso é correto
                        usuario.setCpf(Convert.ToString(leitor["cpf"]));
                        usuario.setSenha(Convert.ToString(leitor["senha"]));
                        // Conversão segura para byte[]
                        byte[] imagem = null;
                        if (!Convert.IsDBNull(leitor["imagem"]))
                        {
                            imagem = (byte[])leitor["imagem"];
                        }
                        usuario.setImagem(imagem);

                        // Preencha as informações do endereço
                        Endereco endereco = new Endereco();
                        endereco.setRua(Convert.ToString(leitor["logradouro"]));
                        endereco.setNumero(Convert.ToString(leitor["numero"]));
                        endereco.setBairro(Convert.ToString(leitor["bairro"]));
                        endereco.setCidade(Convert.ToString(leitor["cidade"]));
                        endereco.setEstado(Convert.ToString(leitor["estado"]));
                        endereco.setCep(Convert.ToString(leitor["cep"]));

                        usuario.setEndereco(endereco);

                        conexao.fecharConexao();

                        telaInicial telaIncial = new telaInicial(this,usuario);
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
                txtUsuario.Text = "";
                txtSenha.Text = "";
                txtUsuario.Select();
            }

        }

        private string GetLocalIPAddress()
        {
            string localIP = "";
            using (System.Net.Sockets.Socket socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                System.Net.IPEndPoint endPoint = socket.LocalEndPoint as System.Net.IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return localIP;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtSenha.PasswordChar = '\0'; // Exibe a senha
            }
            else
            {
                txtSenha.PasswordChar = '•'; // Oculta a senha
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void telaLogin_Load(object sender, EventArgs e)
        {
            txtSenha.PasswordChar = '•';
        }
    }
}
