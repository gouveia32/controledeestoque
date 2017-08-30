using ControleDeEstoque.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoque.DAL
{
    public class DALTipoPagamento
    {
        //-------------------------------------------------------------------------------------------------------------------
        private DALConexao cn;
        //-------------------------------------------------------------------------------------------------------------------
        public DALTipoPagamento(DALConexao conexao)
        {
            this.cn = conexao;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void Incluir(ModeloTipoPagamento obj)
        {
            
            try
            {              
                //command
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "insert into tipopagamento(tpa_nome) values (@nome); select @@IDENTITY;";

                cmd.Parameters.AddWithValue("@nome", obj.tpa_nome);

                cn.Conectar();
                obj.tpa_cod = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (MySqlException ex)
            {
                throw new Exception("MySql ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR: " + ex.Message);
            }
            finally
            {
                //cn.Desconectar();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void Alterar(ModeloTipoPagamento obj)
        {
            
            try
            {
                
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "UPDATE tipopagamento SET tpa_nome = @nome WHERE tpa_cod = @cod";

                cmd.Parameters.AddWithValue("@nome", obj.tpa_nome);
                cmd.Parameters.AddWithValue("@cod", obj.tpa_cod);

                cn.Conectar();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Servidor MySql Erro: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //cn.Desconectar();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void Excluir(int codigo)
        {
            
            try
            {
                
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "delete from tipopagamento WHERE tpa_cod = @cod";

                cmd.Parameters.AddWithValue("@cod", codigo);

                cn.Conectar();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("MySql ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR: "+ex.Message);
            }
            finally
            {
                //cn.Desconectar();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        public DataTable Listagem()
        {
            DataTable tabela = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from tipopagamento", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public DataTable ListagemComFiltro(String valor)
        {
            DataTable tabela = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from tipopagamento where tpa_nome like '%" +
                valor + "%'", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public int VerificaTipoDePagamento(String valor)//0 - não existe valor || > 0 existe
        {
            int r = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn.Conexao;
            cmd.CommandText = "select * from tipopagamento where tpa_nome = @nome";
            cmd.Parameters.AddWithValue("@nome", valor);
            cn.Conectar();
            MySqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                r = Convert.ToInt32(registro["tpa_cod"]);

            }
            return r;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public ModeloTipoPagamento carregaModelo(int codigo)
        {
            ModeloTipoPagamento modelo = new ModeloTipoPagamento();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn.Conexao;
            cmd.CommandText = "select * from tipopagamento where tpa_cod =" + codigo.ToString();
            cn.Conectar();
            MySqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.tpa_cod = Convert.ToInt32(registro["tpa_cod"]);
                modelo.tpa_nome = Convert.ToString(registro["tpa_nome"]);
            }
            return modelo;
        }
        //-------------------------------------------------------------------------------------------------------------------
    }
}
