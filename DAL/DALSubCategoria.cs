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
    public class DALSubCategoria
    {
        //-------------------------------------------------------------------------------------------------------------------
        private DALConexao cn;
        //-------------------------------------------------------------------------------------------------------------------
        public DALSubCategoria(DALConexao conexao)
        {
            this.cn = conexao;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void Incluir(ModeloSubCategoria obj)
        {
            
            try
            {              
                //command
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "insert into subcategoria(scat_nome, cat_cod) values (@scatnome, @catcod); select @@IDENTITY;";

                cmd.Parameters.AddWithValue("@scatnome", obj.scat_nome);
                cmd.Parameters.AddWithValue("@catcod", obj.cat_cod);

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
        public void Alterar(ModeloSubCategoria obj)
        {
            
            try
            {
                
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "UPDATE subcategoria SET scat_nome = @nome, cat_cod = @catcod WHERE scat_cod = @cod";

                cmd.Parameters.AddWithValue("@nome", obj.scat_nome);
                cmd.Parameters.AddWithValue("@cod", obj.scat_cod);
                cmd.Parameters.AddWithValue("@catcod", obj.cat_cod);

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
                cmd.CommandText = "delete from subcategoria WHERE scat_cod = @cod";

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
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from subcategoria", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public DataTable ListagemComFiltro(String valor)
        {
            DataTable tabela = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from subcategoria where scat_nome like '%" + valor + "%'", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public DataTable ListagemComCodigo(int valor)
        {
            DataTable tabela = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from subcategoria where cat_cod like '%" + valor + "%'", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public int VerificaSubCategoria(String valor)//0 - não existe valor || > 0 existe
        {
            int r = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn.Conexao;
            cmd.CommandText = "select * from subcategoria where scat_nome = @nome";
            cmd.Parameters.AddWithValue("@nome", valor);
            cn.Conectar();
            MySqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                r = Convert.ToInt32(registro["scat_cod"]);
            }
            return r;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public ModeloSubCategoria carregaModelo(int codigo)
        {
            ModeloSubCategoria modelo = new ModeloSubCategoria();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn.Conexao;
            cmd.CommandText = "select * from subcategoria where scat_cod =" + codigo.ToString();
            cn.Conectar();
            MySqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.scat_cod = Convert.ToInt32(registro["scat_cod"]);
                modelo.scat_nome = Convert.ToString(registro["scat_nome"]);
                modelo.cat_cod = Convert.ToInt32(registro["cat_cod"]);
            }
            return modelo;
        }
        //-------------------------------------------------------------------------------------------------------------------
    }
}
