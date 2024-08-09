using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeoPimenta.Classes
{
    internal class Usuario
    {
        Endereco endereco = new Endereco();
        private string nome;
        private string email;
        private string cpf;
        private string senha;
        private string buscaCidade;

        public void setNome(string nome) { this.nome = nome; }
        public void setEmail(string email) { this.email = email; }
        public void setCpf(string cpf) { this.cpf = cpf; }
        public void setSenha(string senha) { this.senha = senha; }
        public void setbuscaCidade(string buscaCidade)
        {
            this.buscaCidade = buscaCidade;
        }

        public string getbuscaCidade() { return buscaCidade; }
        public string getNome() { return nome; }
        public string getEmail() { return email; }
        public string getCpf() { return cpf; }
        public string getSenha() { return senha; }

        public void setEndereco(Endereco endereco)
        {
            this.endereco = endereco;
        }
        public Endereco getEndereco() { return endereco; }
        public string getEnderecoString()
        {
            return endereco.getEndereco();
        }
    }
}