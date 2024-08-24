using MySql.Data.MySqlClient;
using SeoPimenta.Classes;
using SeoPimenta.Telas.estoqueProduto;
using SeoPimenta.Telas.menuFornecedor;
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

namespace SeoPimenta.Telas.menuFuncionarioUsuario
{
    public partial class visualizarUser : Form
    {

        private string usuarioDesativacao;
        private string usuarioAtivacao;

        public visualizarUser()
        {
            InitializeComponent();
        }


        private void CarregarUsuariosAtivos()
        {
            var conexaoBanco = new ConexaoBanco();
            try
            {
                // Abre a conexão com o banco de dados
                conexaoBanco.abrirConexao();

                // Comando SQL para buscar todos os usuários com informações adicionais
                string query = "SELECT usuario.usuario, usuario.nivel, usuario.imagem, funcionario.id AS funcionario_id, funcionario.nome " +
                       "FROM seopimenta.usuario " +
                       "INNER JOIN seopimenta.funcionario ON usuario.id_funcionario = funcionario.id " +
                       "WHERE usuario.disponibilidade = 1";

                MySqlCommand command = conexaoBanco.consulta(query);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Cria um DataTable para armazenar os dados
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Define o DataSource do DataGridView para o DataTable
                    dataGridUsuariosAtivos.DataSource = dt;
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

        private void CarregarUsuariosInativos()
        {
            var conexaoBanco = new ConexaoBanco();
            try
            {
                // Abre a conexão com o banco de dados
                conexaoBanco.abrirConexao();

                // Comando SQL para buscar todos os usuários com informações adicionais
                string query = "SELECT usuario.usuario, usuario.nivel, usuario.imagem, funcionario.id AS funcionario_id, funcionario.nome " +
                       "FROM seopimenta.usuario " +
                       "INNER JOIN seopimenta.funcionario ON usuario.id_funcionario = funcionario.id " +
                       "WHERE usuario.disponibilidade = 0";

                MySqlCommand command = conexaoBanco.consulta(query);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Cria um DataTable para armazenar os dados
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Define o DataSource do DataGridView para o DataTable
                    dataGridUsuariosInativos.DataSource = dt;
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
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void body_visualizarUser_Enter(object sender, EventArgs e)
        {

        }

        private void visualizarUser_Load(object sender, EventArgs e)
        {
            CarregarUsuariosAtivos();
            CarregarUsuariosInativos();
        }

        private void dataGridUsuariosAtivos_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnDesativar_Click(object sender, EventArgs e)
        {
            if (dataGridUsuariosAtivos.SelectedRows.Count > 0)
            {
                // Obtém a linha selecionada
                DataGridViewRow row = dataGridUsuariosAtivos.SelectedRows[0];

                // Obtém os dados da linha selecionada
                usuarioDesativacao = row.Cells["usuario"].Value.ToString();

            }

            var conexaoBanco = new ConexaoBanco();
            try
            {
                // Abre a conexão
                conexaoBanco.abrirConexao();

                // Comando SQL para atualizar os dados, incluindo a imagem
                string query = @"UPDATE seopimenta.usuario 
                             SET disponibilidade = 0
                             WHERE usuario = @usuario";

                MySqlCommand command = conexaoBanco.consulta(query);


                command.Parameters.AddWithValue("@usuario", usuarioDesativacao);

                // Executa o comando
                command.ExecuteNonQuery();

                // Mostra mensagem de sucesso
                MessageBox.Show("Usuário Desativado com sucesso!");
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
                usuarioDesativacao = null;
            }
            CarregarUsuariosAtivos();
            CarregarUsuariosInativos();
        }

        private void btbAtivar_Click(object sender, EventArgs e)
        {
            if (dataGridUsuariosInativos.SelectedRows.Count > 0)
            {
                // Obtém a linha selecionada
                DataGridViewRow row = dataGridUsuariosInativos.SelectedRows[0];

                // Obtém os dados da linha selecionada
                usuarioAtivacao = row.Cells["usuario"].Value.ToString();

            }

            var conexaoBanco = new ConexaoBanco();
            try
            {
                // Abre a conexão
                conexaoBanco.abrirConexao();

                // Comando SQL para atualizar os dados, incluindo a imagem
                string query = @"UPDATE seopimenta.usuario 
                             SET disponibilidade = 1
                             WHERE usuario = @usuario";

                MySqlCommand command = conexaoBanco.consulta(query);


                command.Parameters.AddWithValue("@usuario", usuarioAtivacao);

                // Executa o comando
                command.ExecuteNonQuery();

                // Mostra mensagem de sucesso
                MessageBox.Show("Usuário Ativado com sucesso!");
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
                usuarioAtivacao = null;
            }
            CarregarUsuariosAtivos();
            CarregarUsuariosInativos();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
