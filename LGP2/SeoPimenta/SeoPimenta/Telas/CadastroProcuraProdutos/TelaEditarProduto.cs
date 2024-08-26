using MySql.Data.MySqlClient;
using SeoPimenta.Classes;
using SeoPimenta.Classes.CadastroProduto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SeoPimenta.Telas.CadastroProcuraProdutos
{
    public partial class TelaEditarProduto : Form
    {
        ConexaoBanco con = new ConexaoBanco();
        Usuario usuarioLogado;
        private int idUsuarioAtualizacao;
        private string foto;

        public TelaEditarProduto(Usuario usuario)
        {
            InitializeComponent();
            this.usuarioLogado = usuario;
        }






        private void CarregarDetalhesProduto(string nomeProduto)
        {
            var conexaoBanco = new ConexaoBanco();
            try
            {
                // Abre a conexão com o banco de dados
                conexaoBanco.abrirConexao();

                // Comando SQL para buscar os detalhes do usuário selecionado
                string query = "SELECT produto.id, produto.nome, produto.descricao," +
                       "produto.imagem, produto.valor_compra, produto.valor_venda, produto.id_unidade_medida," +
                       "produto.id_categoria, produto.id_subcategoria " +
                "FROM produto " +
                "INNER JOIN undmedida ON produto.id_unidade_medida = undmedida.id " +
                "INNER JOIN categoria ON produto.id_categoria = categoria.id " +
                "WHERE produto.nome = @nome";

                MySqlCommand command = conexaoBanco.consulta(query);
                command.Parameters.AddWithValue("@nome", nomeProduto);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Verifique se os valores estão sendo retornados
                        int id = reader.GetInt32(0);
                        string descricao = reader.GetString(2);
                        byte[] imagemBytes = (byte[])reader["imagem"];
                        double valor_compra = reader.GetDouble(4);
                        double valor_venda = reader.GetDouble(5);
                        string id_unidade_medida = reader["id_unidade_medida"].ToString();
                        string id_categoria = reader["id_categoria"].ToString();
                        string id_subcategoria = reader["id_subcategoria"].ToString();
                        
                        

                        // Preenche os campos do formulário com as informações do usuário
                        txtNomeProd.Text = nomeProduto;
                        txtDescricao.Text = descricao;
                        txtVC.Text = valor_compra.ToString();
                        txtVV.Text = valor_venda.ToString();
                       
                       

                        // Verifique se os itens do cbbFuncionario estão corretamente configurados
                        if (cbCat.Items.Count > 0)
                        {
                            cbCat.SelectedValue = id_categoria; // Define o valor selecionado pelo ID
                        }
                        else
                        {
                            MessageBox.Show($"ID Funcionario {id_categoria} não encontrado na ComboBox.");
                        }

                        // Verifique se os itens do cbbFuncionario estão corretamente configurados
                        if (cbSubCat.Items.Count > 0)
                        {
                            cbSubCat.SelectedValue = id_subcategoria; // Define o valor selecionado pelo ID
                        }
                        else
                        {
                            MessageBox.Show($"ID Funcionario {id_subcategoria} não encontrado na ComboBox.");
                        }


                        // Verifique se os itens do cbbFuncionario estão corretamente configurados
                        if (cbUM.Items.Count > 0)
                        {
                            cbUM.SelectedValue = id_unidade_medida; // Define o valor selecionado pelo ID
                        }
                        else
                        {
                            MessageBox.Show($"ID Funcionario {id_unidade_medida} não encontrado na ComboBox.");
                        }

                        // Carregar a imagem
                        if (imagemBytes != null && imagemBytes.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(imagemBytes))
                            {
                                System.Drawing.Image imagem = System.Drawing.Image.FromStream(ms);
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


        private void CarregarProdutosCadastrados()
        {
            var conexaoBanco = new ConexaoBanco();
            try
            {
                // Abre a conexão com o banco de dados
                conexaoBanco.abrirConexao();

                // Comando SQL para buscar todos os usuários com informações adicionais
                string query = "SELECT produto.id, produto.nome, produto.descricao," +
               "produto.imagem, produto.valor_compra, produto.valor_venda, " +
               "undmedida.id AS unidade_medida, categoria.id AS categoria_id " +
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
                    dataGridViewProduto.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                // Mostra mensagem de erro
                MessageBox.Show("Erro ao carregar Produtos: " + ex.Message);
            }
            finally
            {
                // Fecha a conexão
                conexaoBanco.fecharConexao();
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





        private void dataGridViewProduto_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewProduto.SelectedRows.Count > 0)
            {
                // Obtém a linha selecionada
                DataGridViewRow row = dataGridViewProduto.SelectedRows[0];

                // Obtém os dados da linha selecionada
                string nomeProduto = row.Cells["nome"].Value.ToString();
                string id = row.Cells["id"].Value.ToString();
                string descricao = row.Cells["descricao"].Value.ToString();
                string valorCompra = row.Cells["valor_compra"].Value.ToString();
                string valorVenda = row.Cells["valor_venda"].Value.ToString();
         /*       string idCategoria = row.Cells["id_categoria"].Value.ToString();
                string idSubCategoria = row.Cells["id_subcategoria"].Value.ToString();
                string id_unidade_medida = row.Cells["id_unidade_medida"].Value.ToString();
         */
                // Carrega detalhes do usuário
                CarregarDetalhesProduto(nomeProduto);

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
            txtNomeProd.Text = string.Empty;
            cbCat.Text = string.Empty;
            cbSubCat.Text = string.Empty;
            txtVC.Text = string.Empty;
            txtVV.Text = string.Empty;
            cbUM.Text = string.Empty;
            limparFoto();
        }



        private void retorna_CB(ComboBox campo, string nomeTabela)
        {
            try
            {
                ConexaoBanco conexao = new ConexaoBanco();
                conexao.abrirConexao();

                string comando = "SELECT id, nome FROM seopimenta." + nomeTabela;
                conexao.consulta(comando);

                MySqlDataReader final = conexao.cmd.ExecuteReader();

                // Criar um dicionário para armazenar o id e o nome
                Dictionary<string, string> categoriasDict = new Dictionary<string, string>();

                string idOBJnulo = "";
                string nomeOBJnulo = "";

                // Adicionar o id e o nome ao dicionário
                categoriasDict.Add(idOBJnulo, nomeOBJnulo);

                while (final.Read())
                {
                    string idOBJ = final["id"].ToString();
                    string nomeOBJ = final["nome"].ToString();

                    // Adicionar o id e o nome ao dicionário
                    categoriasDict.Add(idOBJ, nomeOBJ);
                }

                // Vincular o dicionário ao ComboBox
                campo.DataSource = new BindingSource(categoriasDict, null);
                campo.DisplayMember = "Value";  // Exibir o nome
                campo.ValueMember = "Key";      // Armazenar o id

                conexao.fecharConexao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }


        private void TelaEditarProduto_Load(object sender, EventArgs e)
        {
            preencheCBCat();
            preencheCBUnidadeMedida();
            CarregarProdutosCadastrados();
            retorna_CB(cbUM, "undmedida");
            retorna_CB(cbCat, "categoria");
            retorna_CB(cbSubCat, "categoria");
            if (usuarioLogado.getNivel() == 1)
            {
                cbCat.Enabled = false;
                cbSubCat.Enabled = false;
                cbUM.Enabled = false;
            }
        }






        private void preencheCBUnidadeMedida()
        {
            string sql;
            sql = "SELECT id, nome FROM undmedida ORDER BY id asc";
            con.CarregarCB(cbUM, "nome", sql);
        }

        private void preencheCBCat()
        {
            string sql;
            sql = "SELECT id, nome FROM categoria ORDER BY id asc";
            con.CarregarCB(cbCat, "nome", sql);
            con.CarregarCB(cbSubCat, "nome", sql);
        }

       

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

            // Cria uma instância da classe Usuario
            Produto produtoCadastro;

            try
            {
                if (usuarioLogado.getNivel() == 1 && usuarioLogado.getUsuarioID() != idUsuarioAtualizacao)
                {
                    MessageBox.Show("Usuário de nível 1 não tem permissão para atualizar este produto.");
                    return;
                }


                if (string.IsNullOrWhiteSpace(txtNomeProd.Text))
                {
                    Ferramentas.mostraErroTextBox(txtNomeProd, "Insira um valor para o campo \"Nome\"");
                    return;
                }

                string nome = txtNomeProd.Text;


                //pega e valida os dados do textbox
                if (cbUM.SelectedValue == null)
                {
                    throw new Exception("Insira um valor para o campo unidade de medida");
                }

                int id_unidade_medida = Convert.ToInt32(cbUM.SelectedValue);

                //pega e valida os dados do textbox
                if (cbCat.SelectedValue == null)
                {
                    throw new Exception("Insira um valor para o campo unidade de medida");
                }

                int id_categoria = Convert.ToInt32(cbCat.SelectedValue);

                if (cbSubCat.SelectedValue == null)
                {
                    throw new Exception("Insira um valor para o campo unidade de medida");
                }

                int id_subcategoria = Convert.ToInt32(cbSubCat.SelectedValue);


                string descricao = txtDescricao.Text.Trim();

                if (descricao == "")
                {
                    Ferramentas.mostraErroTextBox(txtNomeProd, "Insira um valor para o campo \"Descrição\"");
                    return;
                }
                double valor_compra;
                try
                {
                    valor_compra = double.Parse(txtVC.Text.Trim());
                }
                catch (FormatException)
                {
                    Ferramentas.mostraErroTextBox(txtVC, "Insira um valor válido para o campo \"Valor de Compra\"");
                    return;
                }

                double valor_venda;
                try
                {
                    valor_venda = double.Parse(txtVV.Text.Trim());
                }
                catch (FormatException)
                {
                    Ferramentas.mostraErroTextBox(txtVV, "Insira um valor válido para o campo \"Valor de Venda\"");
                    return;
                }

                // Carrega a imagem selecionada em um array de bytes
                byte[] imagemBytes = GetImageCadastro();

                // Assume que você tem um identificador exclusivo para o usuário que será atualizado
                // int idUsuario = idUsuarioAtualizacao; // Método que obtém o ID do usuário a ser atualizado

                Produto produto = new Produto(/*id,*/ nome, descricao, imagemBytes, valor_compra, valor_venda, id_unidade_medida, id_categoria, id_subcategoria);

                var conexaoBanco = new ConexaoBanco();
                try
                {
                    // Abre a conexão
                    conexaoBanco.abrirConexao();

                    // Comando SQL para atualizar os dados, incluindo a imagem
                    string query = "UPDATE produto SET " +
                          "nome = @nome, " +
                          "descricao = @descricao, " +
                          "imagem = @imagem, " +
                          "valor_compra = @valor_compra, " +
                          "valor_venda = @valor_venda, " +
                          "id_unidade_medida = @id_unidade_medida, " +
                          "id_categoria = @id_categoria, " +
                          "id_subcategoria = @id_subcategoria " +
                          "WHERE id = @id";

                    MySqlCommand command = conexaoBanco.consulta(query);

                    // Adiciona os parâmetros
                    command.Parameters.AddWithValue("nome", produto.getNome());
                    command.Parameters.AddWithValue("descricao", produto.getDescricao());
                    command.Parameters.AddWithValue("@imagem", imagemBytes != null ? (object)imagemBytes : DBNull.Value);
                    command.Parameters.AddWithValue("valor_compra", produto.getValor_compra());
                    command.Parameters.AddWithValue("valor_venda", produto.getValor_venda());
                    command.Parameters.AddWithValue("id_unidade_medida", produto.getId_unidade_medida());
                    command.Parameters.AddWithValue("id_categoria", produto.getId_categoria());
                    command.Parameters.AddWithValue("id_subcategoria", produto.getId_Subcategoria());
                    command.Parameters.AddWithValue("id", idUsuarioAtualizacao); // Adiciona o parâmetro para identificar o usuário a ser atualizado
                    // Executa o comando
                    command.ExecuteNonQuery();

                    // Mostra mensagem de sucesso
                    MessageBox.Show("Produto atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    // Mostra mensagem de erro
                    MessageBox.Show("Erro ao atualizar produto: " + ex.Message);
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









    }
}
