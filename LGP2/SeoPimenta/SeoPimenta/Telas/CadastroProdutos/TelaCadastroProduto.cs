using MySql.Data.MySqlClient;
using SeoPimenta.Classes.cadastroProduto;
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

namespace SeoPimenta.Telas.Cadastro_produto
{
    public partial class TelaCadastroProduto : Form
    {
        private Cadastro cadastro;
        private Produto produto;
        private string foto;

        public TelaCadastroProduto(Cadastro cadastro)
        {
            InitializeComponent();
            
        }


        //limpa os textbox de dados
        private void clearTextBox()
        {
            txbNome.Clear();
            txbId.Clear();
            txbDescricao.Clear();
            txbCategoria.Clear();
            txbSubCategoria.Clear();
            txbUM.Clear();
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
            string id_unidade_medida = txbUM.Text.Trim();

            if (id_unidade_medida == "")
            {
                Ferramentas.mostraErroTextBox(txbNome, "Insira um valor para o campo \"Nome\"");
                return;
            }

            string descricao = txbDescricao.Text.Trim();

            if (id_unidade_medida == "")
            {
                Ferramentas.mostraErroTextBox(txbNome, "Insira um valor para o campo \"Nome\"");
                return;
            }
            byte[] imagem = img();

            if (imagem == null || imagem.Length == 0)
            {
                Ferramentas.mostraErroPictureBox(image, "Insira um valor para o campo \"Nome\"");
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

            int id_categoria;
            try
            {
                id_categoria = int.Parse(txbId.Text.Trim());
            }
            catch (FormatException)
            {
                Ferramentas.mostraErroTextBox(txbCategoria, "Insira um valor para o campo \"Id Categoria\"");
                return;
            }

            int id_subcategoria;
            try
            {
                id_subcategoria = int.Parse(txbId.Text.Trim());
            }
            catch (FormatException)
            {
                Ferramentas.mostraErroTextBox(txbSubCategoria, "Insira um valor para o campo \"Id Sub Categoria\"");
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
                 produto = new Produto(id, nome, descricao, imagem, valor_compra, valor_venda, id_unidade_medida, id_categoria, id_subcategoria);

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


    }
}

