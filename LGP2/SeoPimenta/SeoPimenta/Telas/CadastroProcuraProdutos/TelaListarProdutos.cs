using MySql.Data.MySqlClient;
using SeoPimenta.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeoPimenta.Telas.CadastroProcuraProdutos
{
    public partial class TelaListarProdutos : Form
    {
        public TelaListarProdutos()
        {
            InitializeComponent();
        }


        private void CarregarProdutos()
        {
            var conexaoBanco = new ConexaoBanco();
            try
            {
                // Abre a conexão com o banco de dados
                conexaoBanco.abrirConexao();

                // Comando SQL para buscar todos os usuários com informações adicionais
                string query = "SELECT produto.id, produto.nome, produto.descricao," +
               "produto.imagem, produto.valor_compra, produto.valor_venda, " +
               "undmedida.nome AS unidade_medida, categoria.nome AS categoria_nome " +
               "FROM produto " +
               "INNER JOIN undmedida ON produto.id_unidade_medida = undmedida.id " +
               "INNER JOIN categoria ON produto.id_categoria = categoria.id ";


                MySqlCommand command = conexaoBanco.consulta(query);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Cria um DataTable para armazenar os dados
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Define o DataSource do DataGridView para o DataTable
                    dataGridProduos.DataSource = dt;
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

        private void TelaListarProdutos_Load(object sender, EventArgs e)
        {
            CarregarProdutos();
        }


    }
}
