﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeoPimenta.Classes.CadastroProduto
{
    internal class Ferramentas
    {

        //Exibe uma mensagem de erro e marca o textbox com a cor vermelha
        public static void mostraErroTextBox(TextBox txb, string message)
        {

            Color cor = txb.BackColor; //guarda a cor original de fundo do textbox
            txb.BackColor = Color.Red; //seta a cor de fundo para vermelha

            //exibe uma mensagem de erro
            MessageBox.Show(message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //volta  o textbox para a cor original
            txb.BackColor = cor;

            //colocar o foco do cursor no textbox
            txb.Focus();
        }

        public static void mostraErroPictureBox(PictureBox PB, string message)
        {

            Color cor = PB.BackColor; //guarda a cor original de fundo do textbox
            PB.BackColor = Color.Red; //seta a cor de fundo para vermelha

            //exibe uma mensagem de erro
            MessageBox.Show(message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //volta  o textbox para a cor original
            PB.BackColor = cor;

            //colocar o foco do cursor no textbox
            PB.Focus();
        }
    }
}
