using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SeoPimenta.Classes
{
    public class Usuario
    {
        Endereco endereco = new Endereco();
        private int usuarioID;
        private int nivel;
        private string nome;
        private string email;
        private string cpf;
        private string senha;
        private string buscaCidade;
        private byte[] imagem;  // Atributo para armazenar a imagem

        public void setUsuarioID(int usuarioID) { this.usuarioID = usuarioID; }
        public void setNome(string nome) { this.nome = nome; }
        public void setNivel(int nivel) { this.nivel = nivel; }
        public void setEmail(string email) { this.email = email; }
        public void setCpf(string cpf) { this.cpf = cpf; }
        public void setSenha(string senha) { this.senha = senha; }
        public void setbuscaCidade(string buscaCidade) { this.buscaCidade = buscaCidade; }
        public void setImagem(byte[] imagem) { this.imagem = imagem; }  // Definir a imagem

        public int getUsuarioID() {  return usuarioID; }
        public string getNome() { return this.nome;}
        public string getbuscaCidade() { return buscaCidade; }
        public int getNivel() { return nivel; }
        public string getEmail() { return email; }
        public string getCpf() { return cpf; }
        public string getSenha() { return senha; }
        public byte[] getImagem() { return imagem; }  // Obter a imagem

        public void setEndereco(Endereco endereco) { this.endereco = endereco; }
        public Endereco getEndereco() { return endereco; }
        public string getEnderecoString() { return endereco.getEndereco(); }

        public string GerarHashSHA256(string senha)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Converte a senha para bytes e gera o hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

                // Converte o array de bytes para uma string hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
