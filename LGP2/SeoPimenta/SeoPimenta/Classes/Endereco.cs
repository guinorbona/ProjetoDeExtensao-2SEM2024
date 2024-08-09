using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeoPimenta.Classes
{
    public class Endereco
    {
        private string rua;
        private string numero;
        private string bairro;
        private string cidade;
        private string estado;
        private string cep;


        public Endereco()
        {

        }
        public Endereco(string rua, string numero, string bairro, string cidade, string estado, string cep)
        {
            this.rua = rua;
            this.numero = numero;
            this.bairro = bairro;
            this.cidade = cidade;
            this.estado = estado;
            this.cep = cep;
        }
        public Endereco(Endereco endereco)
        {
            this.rua = endereco.rua;
            this.numero = endereco.numero;
            this.bairro = endereco.bairro;
            this.cidade = endereco.cidade;
            this.estado = endereco.estado;
            this.cep = endereco.cep;
        }
        public void setRua(string rua)
        {
            this.rua = rua;
        }

        public void setNumero(string numero)
        {
            this.numero = numero;
        }
        public void setBairro(string bairro)
        {
            this.bairro = bairro;
        }
        public void setCidade(string cidade)
        {
            this.cidade = cidade;
        }
        public void setEstado(string estado)
        {
            this.estado = estado;
        }
        public void setCep(string cep)
        {
            this.cep = cep;
        }

        public string getRua()
        {
            return rua;
        }
        public string getNumero()
        {
            return numero;
        }
        public string getBairro() { return bairro; }
        public string getCidade() { return cidade; }
        public string getEstado() { return estado; }
        public string getCep() { return cep; }
        public string getEndereco()
        {
            return ($"{rua}, {numero} - {bairro}, {cidade} - {estado}");
        }
    }
}
