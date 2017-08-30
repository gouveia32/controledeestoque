﻿using ControleDeEstoque.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoque.DAL
{
    public class DALParcelasCompra
    {
        //-------------------------------------------------------------------------------------------------------------------
        public void Incluir(ModeloParcelascompra obj)
        {
            //conexao
            MySqlConnection cn = new MySqlConnection();
            try
            {
                cn.ConnectionString = DALDadosDoBanco.stringDeConexao;
                //command
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "insert into parcelascompra (com_cod, pco_valor, pco_cod, pco_datapagto, pco_datavecto) values (@com_cod, @pco_valor, @pco_cod, @pco_datapagto, @pco_datavecto);";
                cmd.Parameters.AddWithValue("@com_cod", obj.com_cod);
                cmd.Parameters.AddWithValue("@pco_valor", obj.pco_valor);
                cmd.Parameters.AddWithValue("@pco_cod", obj.pco_cod);
                //cmd.Parameters.AddWithValue("@pve_datapagto", obj.pve_datapagto);
                //cmd.Parameters.AddWithValue("@pve_datavecto", obj.pve_datavecto);


                cmd.Parameters.Add("@pco_datapagto", MySqlDbType.Date);
                cmd.Parameters.Add("@pco_datavecto", MySqlDbType.Date);
                if (obj.pco_datapagto == null)
                {
                    cmd.Parameters["@pco_datapagto"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@pco_datapagto"].Value = obj.pco_datapagto;
                }
                if (obj.pco_datavecto == null)
                {
                    cmd.Parameters["@pco_datavecto"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@pco_datavecto"].Value = obj.pco_datavecto;
                }
                cn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                throw new Exception("MySql ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //cn.Close();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void Incluir(ModeloParcelascompra obj, MySqlConnection cn, MySqlTransaction tran)
        {
            //conexao
            //MySqlConnection cn = new MySqlConnection();
            try
            {
                //cn.ConnectionString = DadosDoBanco.stringDeConexao;
                //command
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.CommandText = "insert into parcelascompra (com_cod, pco_valor, pco_cod, pco_datapagto, pco_datavecto) values (@com_cod, @pco_valor, @pco_cod, @pco_datapagto, @pco_datavecto);";
                cmd.Parameters.AddWithValue("@com_cod", obj.com_cod);
                cmd.Parameters.AddWithValue("@pco_valor", obj.pco_valor);
                cmd.Parameters.AddWithValue("@pco_cod", obj.pco_cod);
                //cmd.Parameters.AddWithValue("@pve_datapagto", obj.pve_datapagto);
                //cmd.Parameters.AddWithValue("@pve_datavecto", obj.pve_datavecto);


                cmd.Parameters.Add("@pco_datapagto", MySqlDbType.Date);
                cmd.Parameters.Add("@pco_datavecto", MySqlDbType.Date);
                if (obj.pco_datapagto == null)
                {
                    cmd.Parameters["@pco_datapagto"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@pco_datapagto"].Value = obj.pco_datapagto;
                }
                if (obj.pco_datavecto == null)
                {
                    cmd.Parameters["@pco_datavecto"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@pco_datavecto"].Value = obj.pco_datavecto;
                }
                //cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("MySql ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //cn.Close();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void Alterar(ModeloParcelascompra obj)
        {
            //conexão
            MySqlConnection cn = new MySqlConnection();
            try
            {
                cn.ConnectionString = DALDadosDoBanco.stringDeConexao;
                //comando
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "UPDATE parcelascompra set pco_valor = @pco_valor, pco_datapagto = @pco_datapagto, pco_datavecto = @pco_datavecto " +
                    " where com_cod = @com_cod and pco_cod = @pco_cod, ";

                cmd.Parameters.AddWithValue("@com_cod", obj.com_cod);
                cmd.Parameters.AddWithValue("@pco_cod", obj.pco_cod);
                cmd.Parameters.AddWithValue("@pco_valor", obj.pco_valor);
                //cmd.Parameters.AddWithValue("@pve_datapagto", obj.pve_datapagto);
                //cmd.Parameters.AddWithValue("@pve_datavecto", obj.pve_datavecto);
                cmd.Parameters.Add("@pco_datapagto", MySqlDbType.DateTime);
                cmd.Parameters.Add("@pco_datavecto", MySqlDbType.DateTime);
                if (obj.pco_datapagto == null)
                {
                    cmd.Parameters["@pco_datapagto"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@pco_datapagto"].Value = obj.pco_datapagto;
                }
                if (obj.pco_datavecto == null)
                {
                    cmd.Parameters["@pco_datavecto"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@pco_datavecto"].Value = obj.pco_datavecto;
                }
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("MySql ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //cn.Close();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        //deleta todas as parcelas da venda
        public void Excluir(int com_cod)
        {
            MySqlConnection cn = new MySqlConnection();
            try
            {
                cn.ConnectionString = DALDadosDoBanco.stringDeConexao;
                //comando
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "delete from parcelascompra  WHERE com_cod = @com_cod";
                cmd.Parameters.AddWithValue("@com_cod", com_cod);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("MySql ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //cn.Close();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        //deleta uma parcela especifica
        public void Excluir(int com_cod, int pco_cod)
        {
            MySqlConnection cn = new MySqlConnection();
            try
            {
                cn.ConnectionString = DALDadosDoBanco.stringDeConexao;
                //comando
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "delete from parcelascompra  WHERE com_cod = @com_cod  and  pco_cod = @pco_cod";
                cmd.Parameters.AddWithValue("@com_cod", com_cod);
                cmd.Parameters.AddWithValue("@pco_cod", pco_cod);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("MySql ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //cn.Close();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        public DataTable Listagem()
        {
            DataTable tabela = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from parcelascompra", DALDadosDoBanco.stringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        //lista com o codigo da venda
        public DataTable ListagemComFiltro(int com_cod)
        {
            DataTable tabela = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from parcelascompra where com_cod =" +
                com_cod.ToString(), DALDadosDoBanco.stringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public ModeloParcelascompra carregaModelo(int pco_cod, int com_cod)
        {
            ModeloParcelascompra modelo = new ModeloParcelascompra();
            MySqlConnection cn = new MySqlConnection();
            cn.ConnectionString = DALDadosDoBanco.stringDeConexao;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from parcelascompra where pco_cod =" + pco_cod.ToString() +
                " and com_cod =" + com_cod.ToString();
            cn.Open();
            MySqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.pco_cod = Convert.ToInt32(registro["pco_cod"]);
                modelo.pco_valor = Convert.ToDouble(registro["pco_valor"]);
                modelo.pco_datapagto = Convert.ToDateTime(registro["pco_datapagto"]);
                modelo.pco_datavecto = Convert.ToDateTime(registro["pco_datavecto"]);
                modelo.com_cod = Convert.ToInt32(registro["com_cod"]);
            }
            return modelo;
        }
        //-------------------------------------------------------------------------------------------------------------------
    }
}
