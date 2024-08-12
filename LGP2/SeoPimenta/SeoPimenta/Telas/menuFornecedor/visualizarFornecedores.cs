using SeoPimenta.Telas.estoqueProduto;
using SeoPimenta.Telas.menuFuncionarioUsuario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SeoPimenta.Telas.menuFornecedor
{
    public partial class visualizarFornecedores : Form
    {

        Thread thread1;

        int status;

        public visualizarFornecedores()
        {
            InitializeComponent();
        }

        /* 
         * Da linha 26 até a linha ... , está a barra de navegação lateral
         * são os eventos de clique para redirecionar o usuário para o menu desejado
         * cada título/descrição/ícone estão com o evento ativado
         */// !!! Lembrar de alterar as linhas conforme for mudando o código !!!

        private void txtProduto_visualizarFornecedores_Click(object sender, EventArgs e)
        {
            status = 1;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void txtFuncionario_visualizarFornecedores_Click(object sender, EventArgs e)
        {
            status = 2;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void txtFornecedor_visualizarFornecedores_Click(object sender, EventArgs e)
        {
            status = 3;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void dscProduto_visualizarFornecedores_Click(object sender, EventArgs e)
        {
            status = 1;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void dscFuncionario_visualizarFornecedores_Click(object sender, EventArgs e)
        {
            status = 2;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void dscFornecedor_visualizarFornecedores_Click(object sender, EventArgs e)
        {
            status = 3;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void iconProduto_visualizarFornecedores_Click(object sender, EventArgs e)
        {
            status = 1;
            this.Close();
            thread1 = new Thread(abrirJanela);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }

        private void iconFuncionario_visualizarFornecedores_Click(object sender, EventArgs e)
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

        }
    }
}
