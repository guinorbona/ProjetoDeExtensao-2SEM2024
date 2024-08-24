using MySql.Data.MySqlClient;
using SeoPimenta.Classes;
using SeoPimenta.Classes.CadastroProduto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


/*
 ===========================================================================================================================================================
        ERRO LINHA 115 - tenho que colocar o else como 1 pois senão dá erro e crasha o programa
 */

namespace SeoPimenta.Telas.CadastroProcuraProdutos
{
    public partial class TelaCadastroProduto : Form
    {
        private ConexaoBanco con = new ConexaoBanco();
        private Cadastro cadastro;
        private string foto;

        public TelaCadastroProduto(Cadastro cadastro)
        {
            InitializeComponent();
            this.cadastro = cadastro;

        }


        //limpa os textbox de dados
        private void clearTextBox()
        {
            txbNome.Clear();
            txbId.Clear();
            txbDescricao.Clear();


            txbValorCompra.Clear();
            txbValorVenda.Clear();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {







            //recupera o texto do componente textbox e remove os espaços em branco do começo e fim
            string nome = txbNome.Text.Trim();
            if (nome == "")
            {
                Ferramentas.mostraErroTextBox(txbNome, "Insira um valor para o campo \"Nome\"");
                return;
            }



            //pega e valida os dados do textbox
            int id_unidade_medida;

            try
            {
                if (cb_UM.Text.Equals("ml"))
                {
                    id_unidade_medida = 1;
                }
                else
                {
                    id_unidade_medida = 0;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Insira um valor para o campo \" unidade de medida\"");
                return;
            }


            string descricao = txbDescricao.Text.Trim();

            if (descricao == "")
            {
                Ferramentas.mostraErroTextBox(txbNome, "Insira um valor para o campo \"Descrição\"");
                return;
            }
            byte[] imagem = img();

            if (imagem == null || imagem.Length == 0)
            {
                Ferramentas.mostraErroPictureBox(image, "Insira um valor para o campo \"Imagem\"");
                return;
            }


            //recupera o texto do componente textbox e remove os espaços em branco do começo e fim
            int id;
            try
            {
                id = int.Parse(txbId.Text.Trim());
            }
            catch (FormatException)
            {
                Ferramentas.mostraErroTextBox(txbId, "Insira um valor para o campo \"Id\"");
                return;
            }


            //=================================================================================================================================================================
            // está dando erro em id_ categofia concertar mais tarde 

            int id_categoria;
            try
            {
                if (cbCategoria.Text.Equals("fruta"))
                {
                    id_categoria = 1;
                }
                if (cbCategoria.Text.Equals("suco"))
                {
                    id_categoria = 2;
                }
                else
                {
                    id_categoria = 1;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Insira um valor para o campo \"Id Categoria\"");
                return;
            }

            int id_subcategoria;
            try
            {
                if (cb_SubCat.Text.Equals("fruta"))
                {
                    id_subcategoria = 1;
                }
                if (cb_SubCat.Text.Equals("suco"))
                {
                    id_subcategoria = 2;
                }
                else
                {
                    id_subcategoria = 0;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Insira um valor para o campo \"Id Sub Categoria\"");
                return;
            }

            //pega e valida os dados do textbox
            double valor_compra;
            try
            {
                valor_compra = double.Parse(txbValorCompra.Text.Trim());
            }
            catch (FormatException)
            {
                Ferramentas.mostraErroTextBox(txbValorCompra, "Insira um valor válido para o campo \"Valor de Compra\"");
                return;
            }

            double valor_venda;
            try
            {
                valor_venda = double.Parse(txbValorVenda.Text.Trim());
            }
            catch (FormatException)
            {
                Ferramentas.mostraErroTextBox(txbValorVenda, "Insira um valor válido para o campo \"Valor de Venda\"");
                return;
            }

            try
            {
                //cria um objeto funcionário com os dados dos textbox
                Produto produto = new Produto(id, nome, descricao, imagem, valor_compra, valor_venda, id_unidade_medida, id_categoria, id_subcategoria);

                //cadastra o funcionario no banco
                cadastro.inserirProduto(produto);

                //limpa os textbox com os dados do funcionario
                clearTextBox();

                //informa o usuário que o funcionario foi cadastrado no banco
                MessageBox.Show("Dados Salvos no Banco!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //trata os erros relacionados ao banco
            catch (MySqlException erro)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(erro.GetType().ToString());
                sb.AppendLine(erro.Message);
                sb.Append(erro.SqlState);
                sb.AppendLine("\n");
                sb.AppendLine(erro.StackTrace);
                MessageBox.Show(sb.ToString(), "ERRO BANCO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //tratamento dos demais erros que possam ocorrer
            catch (Exception erro)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(erro.GetType().ToString());
                sb.AppendLine(erro.Message);
                sb.AppendLine("\n");
                sb.AppendLine(erro.StackTrace);
                MessageBox.Show(sb.ToString(), "ERRO Desconhecido!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            //fecha a janela
            Close();
        }



        private void image_Click(object sender, EventArgs e)
        {


            OpenFileDialog Dialog = new OpenFileDialog();
            Dialog.Filter = "Arquivos (* .jpg) | *.jpg | Arquivos (*.PNG) |*.png; | All (*.*) | *.*"; //mostra uma de cada vez
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                foto = Dialog.FileName.ToString();
                image.ImageLocation = foto;
                //alterouImagem = "sim";
            }
        }

        private byte[] img() //esse é um metodo padrao, serve sempre que quiser enviar uma imagem para o banco de dados
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
            image.Image = Properties.Resources.sem_foto_prod
                ;
            foto = "image/sem_foto_prod.gif";
        }






        private void preencheCBUnidadeMedida()
        {
            string sql;
            sql = "SELECT id, nome FROM undmedida ORDER BY id asc";
            con.CarregarCB(cb_UM, "nome", sql);
        }

        private void preencheCBCat()
        {
            string sql;
            sql = "SELECT id, nome FROM categoria ORDER BY id asc";
            con.CarregarCB(cbCategoria, "nome", sql);
            con.CarregarCB(cb_SubCat, "nome", sql);
        }



        private void TelaCadastroProduto_Load(object sender, EventArgs e)
        {
            preencheCBUnidadeMedida();
            preencheCBCat();
        }
    }
}
