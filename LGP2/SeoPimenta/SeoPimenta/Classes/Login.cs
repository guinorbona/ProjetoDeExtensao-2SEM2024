using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeoPimenta.Classes
{
    public class Login
    {
        
        private string email;
        private string senha;
        public Login( string email, string senha)
        {
          
            this.email = email;
            this.senha = senha;
        }
   
        public string getEmail() { return email; }
        public string getSenha() { return senha; }
    }
}
