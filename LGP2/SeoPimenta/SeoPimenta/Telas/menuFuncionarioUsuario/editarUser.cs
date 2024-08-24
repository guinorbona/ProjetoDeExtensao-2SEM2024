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

namespace SeoPimenta.Telas.menuFuncionarioUsuario
{
    public partial class editarUser : Form
    {
        Usuario usuarioLogado;

        private int idUsuarioAtualizacao;

        private string foto;
        public editarUser(Usuario usuario)
        {
            InitializeComponent();
            this.usuarioLogado = usuario;
        }

        /* 
         * Da linha 26 até a linha ... , está a barra de navegação lateral
         * são os eventos de clique para redirecionar o usuário para o menu desejado
         * cada título/descrição/ícone estão com o evento ativado
         */// !!! Lembrar de alterar as linhas conforme for mudando o código !!!

        private void CarregarDetalhesUsuario(string nomeUsuario)
        {
            var conexaoBanco = new ConexaoBanco();
            try
            {
                // Abre a conexão com o banco de dados
                conexaoBanco.abrirConexao();

                // Comando SQL para buscar os detalhes do usuário selecionado
                string query = "SELECT usuario.id,usuario.usuario, usuario.nivel, usuario.imagem, funcionario.id AS funcionario_id, funcionario.nome " +
                               "FROM seopimenta.usuario " +
                               "INNER JOIN seopimenta.funcionario ON usuario.id_funcionario = funcionario.id " +
                               "WHERE usuario.usuario = @usuario";

                MySqlCommand command = conexaoBanco.consulta(query);
                command.Parameters.AddWithValue("@usuario", nomeUsuario);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Verifique se os valores estão sendo retornados
                        string usuario = reader["usuario"].ToString();
                        string nivel = reader["nivel"].ToString();
                        byte[] imagemBytes = reader["imagem"] as byte[]; // Assumindo que a imagem é um array de bytes
                        string idFuncionario = reader["funcionario_id"].ToString();
                        string nomeFuncionario = reader["nome"].ToString();
                        idUsuarioAtualizacao = Convert.ToInt32(reader["id"]);
                       
                        // Preenche os campos do formulário com as informações do usuário
                        txtUsuario.Text = usuario;

                        // Verifique se os itens do cbbNivel estão corretamente configurados
                        if (cbbNivel.Items.Contains(nivel))
                        {
                            cbbNivel.SelectedItem = nivel;
                        }
                        else
                        {
                            MessageBox.Show($"Nível {nivel} não encontrado na ComboBox.");
                        }

                        // Verifique se os itens do cbbFuncionario estão corretamente configurados
                        if (cbbFuncionario.Items.Count > 0)
                        {
                            cbbFuncionario.SelectedValue = idFuncionario; // Define o valor selecionado pelo ID
                        }
                        else
                        {
                            MessageBox.Show($"ID Funcionario {idFuncionario} não encontrado na ComboBox.");
                        }

                        // Carregar a imagem
                        if (imagemBytes != null && imagemBytes.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(imagemBytes))
                            {
                                Image imagem = Image.FromStream(ms);
                                // Supondo que você tenha um PictureBox chamado 'picImagem'
                                imageCadastro.Image = imagem;
                            }
                        }
                        else
                        {
                            // Defina uma imagem padrão se não houver imagem
                            imageCadastro.Image = null; // Ou você pode definir uma imagem padrão
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nenhum detalhe encontrado para o usuário selecionado.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Mostra mensagem de erro
                MessageBox.Show("Erro ao carregar detalhes do usuário: " + ex.Message);
            }
            finally
            {
                // Fecha a conexão
                conexaoBanco.fecharConexao();
            }
        }

        private void CarregarUsuariosCadastrados()
        {
            var conexaoBanco = new ConexaoBanco();
            try
            {
                // Abre a conexão com o banco de dados
                conexaoBanco.abrirConexao();

                // Comando SQL para buscar todos os usuários com informações adicionais
                string query = "SELECT usuario.usuario, usuario.nivel, usuario.imagem, funcionario.id AS funcionario_id, funcionario.nome " +
                       "FROM seopimenta.usuario " +
                       "INNER JOIN seopimenta.funcionario ON usuario.id_funcionario = funcionario.id";

                MySqlCommand command = conexaoBanco.consulta(query);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Cria um DataTable para armazenar os dados
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Define o DataSource do DataGridView para o DataTable
                    dataGridViewUsuarios.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                // Mostra mensagem de erro
                MessageBox.Show("Erro ao carregar usuários: " + ex.Message);
            }
            finally
            {
                // Fecha a conexão
                conexaoBanco.fecharConexao();
            }
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

        private void editarUser_Load(object sender, EventArgs e)
        {
            txtSenha.PasswordChar = '•';
            CarregarUsuariosCadastrados();
            adiciona_nivel_permissao();
            retorna_funcionarios();
            if(usuarioLogado.getNivel() == 1)
            {
                cbbNivel.Enabled = false;
                cbbFuncionario.Enabled = false;
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

        private void dataGridViewUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.SelectedRows.Count > 0)
            {
                // Obtém a linha selecionada
                DataGridViewRow row = dataGridViewUsuarios.SelectedRows[0];

                // Obtém os dados da linha selecionada
                string nomeUsuario = row.Cells["usuario"].Value.ToString();
                string nivel = row.Cells["nivel"].Value.ToString();
                string nome = row.Cells["nome"].Value.ToString();

                // Carrega detalhes do usuário
                CarregarDetalhesUsuario(nomeUsuario);
            }
        }


        private void imageCadastro_Click(object sender, EventArgs e)
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


        private void limparFoto()
        {
            imageCadastro.Image = Properties.Resources.sem_foto_prod
                ;
            foto = "image/sem_foto_prod.gif";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = string.Empty;
            cbbFuncionario.Text = string.Empty;
            cbbNivel.Text = string.Empty;
            txtSenha.Text = string.Empty;
            limparFoto();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

            // Cria uma instância da classe Usuario
            Usuario usuarioCadastro = new Usuario();

            try
            {
                // Verifica se o usuário logado tem permissão para atualizar o usuário
                if (usuarioLogado.getNivel() == 1 && usuarioLogado.getUsuarioID() != idUsuarioAtualizacao)
                {
                   MessageBox.Show("Usuário de nível 1 não tem permissão para atualizar este usuário.");
                    return;
                }

                // Valida se o campo de e-mail está preenchido
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    throw new Exception("O campo de usuário deve ser preenchido.");
                }

                // Valida se o campo de senha está preenchido
                if (string.IsNullOrWhiteSpace(txtSenha.Text))
                {
                    throw new Exception("O campo de senha deve ser preenchido.");
                }

                // Valida se o campo de nível está selecionado
                if (cbbNivel.SelectedItem == null)
                {
                    throw new Exception("O campo de nível deve ser selecionado.");
                }

                // Define as propriedades do usuário
                usuarioCadastro.setEmail(txtUsuario.Text);
                usuarioCadastro.setNivel(Convert.ToInt32(cbbNivel.SelectedItem.ToString()));
                usuarioCadastro.setSenha(txtSenha.Text);

                // Verifica se idFuncionario é nulo
                if (cbbFuncionario.SelectedValue == null)
                {
                    throw new Exception("O campo de funcionário deve ser selecionado.");
                }

                int idFuncionario = Convert.ToInt32(cbbFuncionario.SelectedValue);

                // Gera o hash da senha
                string hashSenha = usuarioCadastro.GerarHashSHA256(usuarioCadastro.getSenha());

                // Carrega a imagem selecionada em um array de bytes
                byte[] imagemBytes = GetImageCadastro();

                // Assume que você tem um identificador exclusivo para o usuário que será atualizado
               // int idUsuario = idUsuarioAtualizacao; // Método que obtém o ID do usuário a ser atualizado

                var conexaoBanco = new ConexaoBanco();
                try
                {
                    // Abre a conexão
                    conexaoBanco.abrirConexao();

                    // Comando SQL para atualizar os dados, incluindo a imagem
                    string query = @"UPDATE seopimenta.usuario 
                             SET nivel = @nivel, usuario = @usuario, senha = @senha, id_funcionario = @id_funcionario, imagem = @imagem 
                             WHERE id = @id";

                    MySqlCommand command = conexaoBanco.consulta(query);

                    // Adiciona os parâmetros
                    command.Parameters.AddWithValue("@nivel", usuarioCadastro.getNivel());
                    command.Parameters.AddWithValue("@usuario", usuarioCadastro.getEmail());
                    command.Parameters.AddWithValue("@senha", hashSenha);  // Armazena o hash da senha
                    command.Parameters.AddWithValue("@id_funcionario", idFuncionario);
                    command.Parameters.AddWithValue("@imagem", imagemBytes != null ? (object)imagemBytes : DBNull.Value);
                    command.Parameters.AddWithValue("@id", idUsuarioAtualizacao); // Adiciona o parâmetro para identificar o usuário a ser atualizado

                    // Executa o comando
                    command.ExecuteNonQuery();

                    // Mostra mensagem de sucesso
                    MessageBox.Show("Usuário atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    // Mostra mensagem de erro
                    MessageBox.Show("Erro ao atualizar usuário: " + ex.Message);
                }
                finally
                {
                    // Fecha a conexão no bloco finally para garantir que seja fechada
                    conexaoBanco.fecharConexao();
                }
            }
            catch (Exception ex)
            {
                // Mostra mensagem de erro
                MessageBox.Show("Favor completar todos os campos: " + ex.Message);
            }

            // Chama o evento do botão Cancelar
            btnCancelar_Click(sender, e);
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
