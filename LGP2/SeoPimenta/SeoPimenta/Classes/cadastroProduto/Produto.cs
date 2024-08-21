using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeoPimenta.Classes.cadastroProduto
{
    public class Produto
    {
        private byte[] imagem;
        private string nome;
        private string descricao;
        private double valor_compra;
        private double valor_venda;
        private int id;
        private string id_unidade_medida;
        private int id_categoria;
        private int id_subcategoria;

        public Produto(int id, string nome, string descricao, byte[] imagem, double valor_compra,
                        double valor_venda, string id_unidade_medida, int id_categoria, int id_subcategoria)
        {

            this.id = id;
            this.nome = nome;
            this.descricao = descricao;
            this.imagem = imagem;
            this.valor_compra = valor_compra;
            this.valor_venda = valor_venda;
            this.id_unidade_medida = id_unidade_medida;
            this.id_categoria = id_categoria;
            this.id_subcategoria = id_subcategoria;
        }

        public byte[] getImagem() { return this.imagem; }
        public string getNome() { return nome; }
        public string getDescricao() { return descricao; }
        public int getId() { return id; }
        public string getId_unidade_medida() { return id_unidade_medida; }
        public int getId_categoria() { return id_categoria; }
        public int getId_Subcategoria() { return id_subcategoria; }
        public double getValor_compra() { return valor_compra; }
        public double getValor_venda() { return valor_venda; }

        public void setImage()
        {

        }
    }
}
