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
    public class DALNota
    {
        //-------------------------------------------------------------------------------------------------------------------
        private DALConexao cn;
        //-------------------------------------------------------------------------------------------------------------------
        public DALNota(DALConexao conexao)
        {
            this.cn = conexao;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void Incluir(ModeloNota obj)
        {
            try
            {              
                //command
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "insert into nota(pro_nome) values (@nome); select @@IDENTITY;";

                cmd.Parameters.AddWithValue("@nome", obj.pro_nome);

                cn.Conectar();
                obj.pro_cod = Convert.ToInt32(cmd.ExecuteScalar());

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
        public void Alterar(ModeloNota obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "UPDATE nota SET pro_nome = @nome WHERE nt_cod = @cod";
                cmd.Parameters.AddWithValue("@nome", obj.pro_nome);
                cmd.Parameters.AddWithValue("@cod", obj.nt_cod);
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
                cmd.CommandText = "delete from nota WHERE nt_cod = @cod";

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
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from nota", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public DataTable ListagemComFiltro(String valor)
        {
            DataTable tabela = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from nota where pro_nome like '%" +
                valor + "%'", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public int VerificaNota(String valor)//0 - não existe valor || > 0 existe
        {
            int r = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn.Conexao;
            cmd.CommandText = "select * from nota where pro_nome = @nome";
            cmd.Parameters.AddWithValue("@nome", valor);
            cn.Conectar();
            MySqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                r = Convert.ToInt32(registro["nt_cod"]);
            }
            return r;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public ModeloNota carregaModelo(int codigo)
        {
            ModeloNota modelo = new ModeloNota();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn.Conexao;
            cmd.CommandText = "select * from nota where nt_cod =" + codigo.ToString();
            cn.Conectar();
            MySqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.nt_cod = Convert.ToInt32(registro["nt_cod"]);
                modelo.pro_nome = Convert.ToString(registro["pro_nome"]);
            }
            return modelo;
        }
        //-------------------------------------------------------------------------------------------------------------------
    }
}
