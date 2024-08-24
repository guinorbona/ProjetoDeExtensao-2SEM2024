using MySql.Data.MySqlClient;
using SeoPimenta.Classes;
using SeoPimenta.Telas.estoqueProduto;
using SeoPimenta.Telas.menuFornecedor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SeoPimenta.Telas.menuFuncionarioUsuario
{
    public partial class cadastrarUser : Form
    {

     

       
        private string foto;
        public object conexaoBanco { get; private set; }

        public cadastrarUser()
        {
            InitializeComponent();

        }

    

       

        private void adiciona_nivel_permissao()
        {
            try
            {
                // Adicionar os valores 1 e 2 ao ComboBox cbbNivel
                cbbNivel.Items.Add("");
                cbbNivel.Items.Add("1");
                cbbNivel.Items.Add("2");

                // Se quiser definir um valor padrão selecionado, por exemplo, "1"
                cbbNivel.SelectedIndex = 0; // Isso seleciona o primeiro item (índice 0)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar itens ao ComboBox: " + ex.Message);
            }
        }
        private void retorna_funcionarios()
        {
            try
            {
                ConexaoBanco conexao = new ConexaoBanco();
                conexao.abrirConexao();

                string comando = "SELECT id, nome FROM seopimenta.funcionario";
                conexao.consulta(comando);

                MySqlDataReader final = conexao.cmd.ExecuteReader();

                // Criar um dicionário para armazenar o id e o nome
                Dictionary<string, string> funcionarioDict = new Dictionary<string, string>();

                string idFuncionarionulo = "";
                string nomeFuncionarionulo = "";

                // Adicionar o id e o nome ao dicionário
                funcionarioDict.Add(idFuncionarionulo, nomeFuncionarionulo);

                while (final.Read())
                {
                    string idFuncionario = final["id"].ToString();
                    string nomeFuncionario = final["nome"].ToString();

                    // Adicionar o id e o nome ao dicionário
                    funcionarioDict.Add(idFuncionario, nomeFuncionario);
                }

                // Vincular o dicionário ao ComboBox
                cbbFuncionario.DataSource = new BindingSource(funcionarioDict, null);
                cbbFuncionario.DisplayMember = "Value";  // Exibir o nome
                cbbFuncionario.ValueMember = "Key";      // Armazenar o id

                conexao.fecharConexao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

            private void body_cadastrarUser_Enter(object sender, EventArgs e)
        {

        }

        private void cadastrarUser_Load_1(object sender, EventArgs e)
        {
            txtSenha.PasswordChar = '•';
            retorna_funcionarios();
            adiciona_nivel_permissao();
        }

        private void cbbFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

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

        private byte[] GetImageCadastro()
        {

            byte[] image_byte = null; //essa var é usada para enviar o comprimento da imagem para o banco de dados
            if (foto == null)
            {
                return null;
            }

            FileStream fs = new FileStream(foto, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            image_byte = br.ReadBytes((int)fs.Length);
            return image_byte;
        }
        private void limparFoto()
        {
            imageCadastro.Image = Properties.Resources.sem_foto_prod
                ;
            foto = "image/sem_foto_prod.gif";
        }


        private void image_Click(object sender, EventArgs e)
        {

            OpenFileDialog Dialog = new OpenFileDialog();
            Dialog.Filter = "Arquivos (* .jpg) | *.jpg | Arquivos (*.PNG) |*.png; | All (*.*) | *.*"; //mostra uma de cada vez
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                foto = Dialog.FileName.ToString();
                imageCadastro.ImageLocation = foto;
                //alterouImagem = "sim";
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            // Cria uma instância da classe Usuario
            Usuario usuarioCadastro = new Usuario();

            // Define as propriedades do usuário
            usuarioCadastro.setEmail(txtUsuario.Text);
            usuarioCadastro.setNivel(Convert.ToInt32(cbbNivel.SelectedItem.ToString()));
            usuarioCadastro.setSenha(txtSenha.Text);
            int idFuncionario = Convert.ToInt32(cbbFuncionario.SelectedValue);

            // Gera o hash da senha
            string hashSenha = usuarioCadastro.GerarHashSHA256(usuarioCadastro.getSenha());

            // Carrega a imagem selecionada em um array de bytes
            byte[] imagemBytes = null;

            imagemBytes = GetImageCadastro();



            var conexaoBanco = new ConexaoBanco();
            try
            {
                // Abre a conexão
                conexaoBanco.abrirConexao();

                // Comando SQL para inserir os dados incluindo a imagem
                string query = @"INSERT INTO seopimenta.usuario (nivel, usuario, senha, id_funcionario, imagem) 
                         VALUES (@nivel, @usuario, @senha, @id_funcionario, @imagem)";

                // Configura o comando com a consulta SQL e a conexão aberta
                MySqlCommand command = conexaoBanco.consulta(query);

                // Adiciona os parâmetros
                command.Parameters.AddWithValue("@nivel", usuarioCadastro.getNivel());
                command.Parameters.AddWithValue("@usuario", usuarioCadastro.getEmail());
                command.Parameters.AddWithValue("@senha", hashSenha);  // Armazena o hash da senha
                command.Parameters.AddWithValue("@id_funcionario", idFuncionario);

                // Adiciona o parâmetro da imagem, se uma imagem foi selecionada
                if (imagemBytes != null)
                {
                    command.Parameters.AddWithValue("@imagem", imagemBytes);
                }
                else
                {
                    command.Parameters.AddWithValue("@imagem", DBNull.Value);
                }

                // Executa o comando
                command.ExecuteNonQuery();

                // Mostra mensagem de sucesso
                MessageBox.Show("Usuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                // Mostra mensagem de erro
                MessageBox.Show("Erro ao cadastrar usuário: " + ex.Message);
            }
            finally
            {
                // Fecha a conexão
                conexaoBanco.fecharConexao();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
           txtUsuario.Text = string.Empty;
           cbbFuncionario.Text = string.Empty;
           cbbNivel.Text = string.Empty;
           txtSenha.Text = string.Empty;
           limparFoto();

        }
    }
}
