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
    public class DALUndMedida
    {
        //-------------------------------------------------------------------------------------------------------------------
        private DALConexao cn;
        //-------------------------------------------------------------------------------------------------------------------
        public DALUndMedida(DALConexao conexao)
        {
            this.cn = conexao;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void Incluir(ModeloUndMedida obj)
        {
            
            try
            {              
                //command
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "insert into undmedida(umed_nome) values (@nome); select @@IDENTITY;";

                cmd.Parameters.AddWithValue("@nome", obj.umed_nome);

                cn.Conectar();
                obj.umed_cod = Convert.ToInt32(cmd.ExecuteScalar());

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
        public void Alterar(ModeloUndMedida obj)
        {
            
            try
            {
                
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "UPDATE undmedida SET umed_nome = @nome WHERE umed_cod = @cod";

                cmd.Parameters.AddWithValue("@nome", obj.umed_nome);
                cmd.Parameters.AddWithValue("@cod", obj.umed_cod);

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
                cmd.CommandText = "delete from undmedida WHERE umed_cod = @cod";

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
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from undmedida", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public DataTable ListagemComFiltro(String valor)
        {
            DataTable tabela = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from undmedida where umed_nome like '%" +
                valor + "%'", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public int VerificaUnidadeDeMedida(String valor)//0 - não existe valor || > 0 existe
        {
            int r = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn.Conexao;
            cmd.CommandText = "select * from undmedida where umed_nome = @nome";
            cmd.Parameters.AddWithValue("@nome", valor);
            cn.Conectar();
            MySqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                r = Convert.ToInt32(registro["umed_cod"]);

            }
            return r;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public ModeloUndMedida carregaModelo(int codigo)
        {
            ModeloUndMedida modelo = new ModeloUndMedida();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn.Conexao;
            cmd.CommandText = "select * from undmedida where umed_cod =" + codigo.ToString();
            cn.Conectar();
            MySqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.umed_cod = Convert.ToInt32(registro["umed_cod"]);
                modelo.umed_nome = Convert.ToString(registro["umed_nome"]);
            }
            return modelo;
        }
        //-------------------------------------------------------------------------------------------------------------------
    }
}
