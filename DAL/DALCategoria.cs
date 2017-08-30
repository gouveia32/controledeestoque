using ControleDeEstoque.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ControleDeEstoque.DAL
{
    public class DALCategoria
    {
        //-------------------------------------------------------------------------------------------------------------------
        private DALConexao cn;
        //-------------------------------------------------------------------------------------------------------------------
        public DALCategoria(DALConexao conexao)
        {
            this.cn = conexao;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void Incluir(ModeloCategoria obj)
        {
            try
            {              
                //command
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "insert into categoria(cat_nome) values (@nome); select @@IDENTITY;";

                cmd.Parameters.AddWithValue("@nome", obj.cat_nome);

                cn.Conectar();
                obj.cat_cod = Convert.ToInt32(cmd.ExecuteScalar());

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
        public void Alterar(ModeloCategoria obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "UPDATE categoria SET cat_nome = @nome WHERE cat_cod = @cod";
                cmd.Parameters.AddWithValue("@nome", obj.cat_nome);
                cmd.Parameters.AddWithValue("@cod", obj.cat_cod);
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
                cmd.CommandText = "delete from categoria WHERE cat_cod = @cod";

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
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from categoria", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public DataTable ListagemComFiltro(String valor)
        {
            DataTable tabela = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from categoria where cat_nome like '%" +
                valor + "%'", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public int VerificaCategoria(String valor)//0 - não existe valor || > 0 existe
        {
            int r = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn.Conexao;
            cmd.CommandText = "select * from categoria where cat_nome = @nome";
            cmd.Parameters.AddWithValue("@nome", valor);
            cn.Conectar();
            MySqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                r = Convert.ToInt32(registro["cat_cod"]);
            }
            return r;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public ModeloCategoria carregaModelo(int codigo)
        {
            ModeloCategoria modelo = new ModeloCategoria();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn.Conexao;
            cmd.CommandText = "select * from categoria where cat_cod =" + codigo.ToString();
            cn.Conectar();
            MySqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.cat_cod = Convert.ToInt32(registro["cat_cod"]);
                modelo.cat_nome = Convert.ToString(registro["cat_nome"]);
            }
            return modelo;
        }
        //-------------------------------------------------------------------------------------------------------------------
    }
}
