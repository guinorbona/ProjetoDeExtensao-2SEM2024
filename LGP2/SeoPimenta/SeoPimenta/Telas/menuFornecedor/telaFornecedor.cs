using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using SeoPimenta.Telas.menuFuncionarioUsuario;
using SeoPimenta.Telas.estoqueProduto;

namespace SeoPimenta.Telas.menuFornecedor
{
    public partial class telaFornecedor : Form
    {

        Thread thread1;

        int status;

        public telaFornecedor()
        {
            InitializeComponent();
        }

        /* 
         * Da linha 26 até a linha ... , está a barra de navegação lateral
         * são os eventos de clique para redirecionar o usuário para o menu desejado
         * cada título/descrição/ícone estão com o evento ativado
         */// !!! Lembrar de alterar as linhas conforme for mudando o código !!!

        private void txtProduto_telaFornecedor_Click(object sender, EventArgs e)
        {
            status = 1;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void txtFuncionario_telaFornecedor_Click(object sender, EventArgs e)
        {
            status = 2;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void txtFornecedor_telaFornecedor_Click(object sender, EventArgs e)
        {
            status = 3;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void dscProduto_telaFornecedor_Click(object sender, EventArgs e)
        {
            status = 1;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void dscFuncionario_telaFornecedor_Click(object sender, EventArgs e)
        {
            status = 2;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void dscFornecedor_telaFornecedor_Click(object sender, EventArgs e)
        {
            status = 3;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void iconProduto_telaFornecedor_Click(object sender, EventArgs e)
        {
            status = 1;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void iconFuncionario_telaFornecedor_Click(object sender, EventArgs e)
        {
            status = 2;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void iconFornecedor_telaFornecedor_Click(object sender, EventArgs e)
        {
            status = 3;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        /*
        * Próximos eventos de clique são os botão para levar 
        * até as telas de visualizar/cadastrar/editar
        */

        private void btnVisualizar_telaFornecedor_Click(object sender, EventArgs e)
        {
            status = 4;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void btnCadastrar_telaFornecedor_Click(object sender, EventArgs e)
        {
            status = 5;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void btnEditar_telaFornecedor_Click(object sender, EventArgs e)
        {
            status = 6;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        /*
         * método usado para abrir as janelas
         */

        private void abrirJanela(object obj)
        {

            // Casos para os rótulos da navbar da tela de fornecedores

            if (status == 1)
            {
                Application.Run(new telaEstoqueProduto());
            }
            if (status == 2)
            {
                Application.Run(new telaFuncioUser());
            }
            if (status == 3)
            {
                Application.Run(new telaFornecedor());
            }

            // Casos para os botões do body da tela de fornecedores

            if (status == 4)
            {
                Application.Run(new visualizarFornecedores());
            }
            if (status == 5)
            {
                Application.Run(new cadastrarFornecedor());
            }
            if (status == 6)
            {
                Application.Run(new editarFornecedor());
            }
        }
    }
}
