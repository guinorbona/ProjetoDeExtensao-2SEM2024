using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeoPimenta.Classes.CadastroProduto
{
    public class Cadastro
    {
        private string connectionStr;
        private string sql;
        private ConexaoBanco con = new ConexaoBanco();

        //-------------------------------------------------------------------------

        public Cadastro()
        {
            connectionStr = @"server=localhost;uid=root;pwd=ifsp;database=seopimenta;ConnectionTimeout=1";
        }

        //-------------------------------------------------------------------------

        public void inserirProduto(Produto Produto)
        {
            MySqlConnection connectionBD = null;
            MySqlCommand cmdInsert = null;

            try
            {

                con.abrirConexao();

                sql = "INSERT INTO produto (id, nome, descricao, imagem, valor_compra, valor_venda, id_unidade_medida, id_categoria, id_subcategoria)" +
                                    "VALUES (@id, @nome, @descricao, @imagem, @valor_compra, @valor_venda, @id_unidade_medida, @id_categoria, @id_subcategoria)";

                cmdInsert = con.consulta(sql);

                //seta os parametros que deverão ser passados para a consulta sql
                cmdInsert.Parameters.AddWithValue("id", Produto.getId());
                cmdInsert.Parameters.AddWithValue("nome", Produto.getNome());
                cmdInsert.Parameters.AddWithValue("descricao", Produto.getDescricao());
                cmdInsert.Parameters.AddWithValue("imagem", Produto.getImagem());
                cmdInsert.Parameters.AddWithValue("valor_compra", Produto.getValor_compra());
                cmdInsert.Parameters.AddWithValue("valor_venda", Produto.getValor_venda());
                cmdInsert.Parameters.AddWithValue("id_unidade_medida", Produto.getId_unidade_medida());
                cmdInsert.Parameters.AddWithValue("id_categoria", Produto.getId_categoria());
                cmdInsert.Parameters.AddWithValue("id_subcategoria", Produto.getId_Subcategoria());

                //executa o comando / consulta sql
                cmdInsert.ExecuteNonQuery();

            }
            finally
            {
                //libera a memória utilizada pelos comandos
                if (cmdInsert != null) cmdInsert.Dispose();
                //fecha a conexão com o banco!
                if (connectionBD != null) connectionBD.Close();
            }
        }





        //-------------------------------------------------------------------------

        public Produto pesquisarProdutoNome(string nome)
        {

            MySqlConnection connectionBD = null;
            MySqlCommand cmdSelect = null;
            Produto Produto = null;

            try
            {
                //tenta criar uma conexão com o banco
                connectionBD = new MySqlConnection(connectionStr);

                //abre a conexão com o banco
                connectionBD.Open();

                // Executa um comando INSERT para inserir um registro na tabela 'Funcionario'
                // Como o INSERT não retorna valores, devemos executar o comando 'ExecuteNonQuery'
                cmdSelect = new MySqlCommand();

                //atribui uma conexão ao comando (obrigatório)
                cmdSelect.Connection = connectionBD;

                //seta o comando sql que será executado
                cmdSelect.CommandText = "SELECT produto.id, produto.nome, produto.descricao," +
                       "produto.imagem, produto.valor_compra, produto.valor_venda, produto.id_unidade_medida," +
                       "produto.id_categoria, produto.id_subcategoria " +
                "FROM produto " +
                "INNER JOIN undmedida ON produto.id_unidade_medida = undmedida.id " +
                "INNER JOIN categoria ON produto.id_categoria = categoria.id " +
                "WHERE produto.nome = @nome";


                //seta os parametros que deverão ser passados para a consulta sql
                cmdSelect.Parameters.AddWithValue("nome", nome);

                MySqlDataReader reader = cmdSelect.ExecuteReader(); //executa o comando que retornará um datareader
                                                                    //para acessar os dados da tabela

                //caso o comando select tenha retornado alguma linha
                if (reader.HasRows)
                {

                    //Lê a primeira linha (o cpf é único para cada funcionário) não é para retornar mais de uma linha!
                    reader.Read();

                    //pega os dados das colunas da linha
                    int id = reader.GetInt32(0);
                    string descricao = reader.GetString(2);
                    byte[] imagem = (byte[])reader["imagem"];
                    double valor_compra = reader.GetDouble(4);
                    double valor_venda = reader.GetDouble(5);
                    int id_unidade_medida = reader.GetInt32(6);
                    int id_categoria = reader.GetInt32(7);
                    int id_subcategoria = reader.GetInt32(8);




                    //cria um objeto do tipo Funcionario com os dados que vieram do banco!!!
                    Produto = new Produto(/*id,*/ nome, descricao, imagem, valor_compra, valor_venda, id_unidade_medida, id_categoria, id_subcategoria);
                }

                //tenho que fechar o reader após o uso !!!
                reader.Close();

            }
            finally
            {
                //libera a memória utilizada pelos comandos
                if (cmdSelect != null) cmdSelect.Dispose();
                //fecha a conexão com o banco!
                if (connectionBD != null) connectionBD.Close();
            }

            //retorna um objeto funcionario.
            return Produto;
        }

        //-------------------------------------------------------------------------

        //Remove a pessoa do cadastro que possuir um cpf igual ao passado como parâmetro.
        public bool removerProdutoNome(string nome)
        {
            //procura um funcionário que possua o cpf igual ao passado como parâmetro
            //caso encontre retorna uma referência para esse funcionário;
            //caso não encontre, retorna null
            Produto produto = pesquisarProdutoNome(nome);

            //caso encontre uma pessoa
            if (produto != null)
            {
                MySqlConnection connectionBD = null;
                MySqlCommand cmdDelete = null;

                try
                {
                    //tenta criar uma conexão com o banco
                    connectionBD = new MySqlConnection(connectionStr);

                    //abre a conexão com o banco
                    connectionBD.Open();

                    // Executa um comando INSERT para inserir um registro na tabela 'Funcionario'
                    // Como o INSERT não retorna valores, devemos executar o comando 'ExecuteNonQuery'
                    cmdDelete = new MySqlCommand();

                    //atribui uma conexão ao comando (obrigatório)
                    cmdDelete.Connection = connectionBD;

                    //seta o comando sql que será executado
                    cmdDelete.CommandText = "DELETE FROM produto " +
                                            "WHERE nome = @nome";

                    //seta os parametros que deverão ser passados para a consulta sql
                    cmdDelete.Parameters.AddWithValue("nome", nome);

                    //executa o comando / consulta sql
                    cmdDelete.ExecuteNonQuery();

                    //indica que conseguiu remover a pessoa
                    return true;

                }
                finally
                {
                    //libera a memória utilizada pelos comandos
                    if (cmdDelete != null) cmdDelete.Dispose();
                    //fecha a conexão com o banco!
                    if (connectionBD != null) connectionBD.Close();
                }

            }
            //caso não encontre o funcionário
            else
            {
                //indica que não conseguiu remover a pessoa
                return false;
            }

        }

        //-------------------------------------------------------------------------

        public bool atualizarProduto(string pesquisanome, Produto produto)
        {
            int numLinhasAfetadas = 0;
            MySqlConnection connectionBD = null;
            MySqlCommand cmdUpdate = null;

            try
            {
                //tenta criar uma conexão com o banco
                connectionBD = new MySqlConnection(connectionStr);

                //abre a conexão com o banco
                connectionBD.Open();

                // Executa um comando UPDATE para atualizar um registro na tabela 'Funcionario'
                // Como o UPDATE não retorna valores, devemos executar o comando 'ExecuteNonQuery'
                cmdUpdate = new MySqlCommand();

                //atribui uma conexão ao comando (obrigatório)
                cmdUpdate.Connection = connectionBD;

                //seta o comando sql que será executado
                cmdUpdate.CommandText = "UPDATE produto SET " +
                        "id =@id, " +
                        "nome = @nome, " +
                        "descricao = @descricao, " +
                        "imagem = @descricao, " +
                        "valor_compra = @valor_compra, " +
                        "valor_venda = @valor_venda, " +
                        "id_unidade_medida = @id_unidade_medida, " +
                        "id_categoria = @id_categoria, " +
                        "id_subcategoria = @id_subcategoria";

                //seta os parametros que deverão ser passados para a consulta sql
                cmdUpdate.Parameters.AddWithValue("id", produto.getId());
                cmdUpdate.Parameters.AddWithValue("nome", produto.getNome());
                cmdUpdate.Parameters.AddWithValue("descricao", produto.getDescricao());
                cmdUpdate.Parameters.AddWithValue("imagem", produto.getImagem());
                cmdUpdate.Parameters.AddWithValue("valor_compra", produto.getValor_compra());
                cmdUpdate.Parameters.AddWithValue("valor_venda", produto.getValor_venda());
                cmdUpdate.Parameters.AddWithValue("id_unidade_medida", produto.getId_unidade_medida());
                cmdUpdate.Parameters.AddWithValue("id_categoria", produto.getId_categoria());
                cmdUpdate.Parameters.AddWithValue("id_subcategoria", produto.getId_Subcategoria());
                cmdUpdate.Parameters.AddWithValue("pesquisanome", pesquisanome);

                //executa o comando / consulta sql
                numLinhasAfetadas = cmdUpdate.ExecuteNonQuery();

            }
            finally
            {
                //libera a memória utilizada pelos comandos
                if (cmdUpdate != null) cmdUpdate.Dispose();
                //fecha a conexão com o banco!
                if (connectionBD != null) connectionBD.Close();
            }

            if (numLinhasAfetadas != 0) return true;
            else return false;
        }


        //-------------------------------------------------------------------------

        public void gerarRelatorioFuncionario(DataTable table)
        {
            MySqlConnection connectionBD = null;
            MySqlCommand cmdSelect = null;

            try
            {
                //limpa qualquer dado anterior da tabela
                table.Clear();

                //tenta criar uma conexão com o banco
                connectionBD = new MySqlConnection(connectionStr);

                //abre a conexão com o banco
                connectionBD.Open();

                // Executa um comando INSERT para inserir um registro na tabela 'Funcionario'
                // Como o INSERT não retorna valores, devemos executar o comando 'ExecuteNonQuery'
                cmdSelect = new MySqlCommand();

                //atribui uma conexão ao comando (obrigatório)
                cmdSelect.Connection = connectionBD;

                //seta o comando sql que será executado
                cmdSelect.CommandText = "SELECT produto.id, produto.nome, produto.descricao," +
                                        "produto.imagem, produto.valor_compra, produto.valor_venda, produto.id_unidade_medida," +
                                        "produto.id_categoria, produto.id_subcategoria" +
                                        "FROM produto INNER JOIN undmedida ON(produto.id_unidade_medida = unmedida.id)" +
                                        "INNER JOIN categoria ON(produto.id_categoria = categoria.id, produto.id_subcategoria = categoria.id)";


                MySqlDataReader reader = cmdSelect.ExecuteReader(); //executa o comando que retornará um datareader
                                                                    //para acessar os dados da tabela

                //caso o comando select tenha retornado alguma linha
                if (reader.HasRows)
                {
                    //passa o datareader para a tabela ser inicializada com os seus dados!
                    table.Load(reader);
                }

                //tenho que fechar o reader após o uso !!!
                reader.Close();

            }
            finally
            {
                //libera a memória utilizada pelos comandos
                if (cmdSelect != null) cmdSelect.Dispose();
                //fecha a conexão com o banco!
                if (connectionBD != null) connectionBD.Close();
            }
        }


    }
}
