﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeoPimenta.Classes
{
    public class ConexaoBanco
    {
        public string data_source = @"server=localhost;
                              port=3306;
                              uid=root;
                              pwd=ifsp;database=seopimenta;
                              ConnectionTimeout=1";
        public MySqlConnection Connection;
        public MySqlCommand cmd;

        public void abrirConexao()
        {
            try
            {
                Connection = new MySqlConnection(data_source);
                Connection.Open();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlConnection consultaConexao()
        {
            SqlConnection c = new SqlConnection(data_source);
            return c;
        }

        public MySqlCommand consulta(string comando)
        {
            fecharConexao();

            Connection = new MySqlConnection(data_source);
            Connection.Open();

            cmd = new MySqlCommand(comando, Connection);
            return cmd;
        }

        public void fecharConexao()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }


        //Serve para carregar os dados das tabelas para os comboBoxs

        public void CarregarCB(ComboBox nome, string dado, string sql)
        {

            MySqlCommand cmd;

            abrirConexao();


            try
            {
                cmd = consulta(sql);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable table = new DataTable();
                da.Fill(table);
                nome.DataSource = table;
                nome.DisplayMember = dado;
                fecharConexao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }



        public void CarregarId(TextBox nome, string sql)
        {

            MySqlCommand cmd;

            abrirConexao();


            try
            {
                 cmd = consulta(sql);
                object valor = cmd.ExecuteScalar();
                if (valor != null)
                {
                    string temp = valor.ToString();
                    int temp2 = int.Parse(temp) + 1;
                    nome.Text = temp2.ToString();
                }
                else
                {
                    nome.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }


    }
}