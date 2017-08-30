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
    public class DALItensVenda
    {
        //-------------------------------------------------------------------------------------------------------------------
        private DALConexao cn;
        //-------------------------------------------------------------------------------------------------------------------
        public DALItensVenda(DALConexao conexao)
        {
            this.cn = conexao;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void Incluir(ModeloItensVenda obj)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "insert into itensvenda(itv_cod, itv_qtde, itv_valor, ven_cod, pro_cod) values (@itvcod, @itvqtde, @itvvalor, @vencod, @procod); select @@IDENTITY;";
                cmd.Parameters.AddWithValue("@itvcod", obj.itv_cod);
                cmd.Parameters.AddWithValue("@itvqtde", obj.itv_qtde);
                cmd.Parameters.AddWithValue("@itvvalor", obj.itv_valor);
                cmd.Parameters.AddWithValue("@vencod", obj.ven_cod);
                cmd.Parameters.AddWithValue("@procod", obj.pro_cod);
                cn.Conectar();
                obj.itv_cod = Convert.ToInt32(cmd.ExecuteScalar());

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
        public void Incluir(ModeloItensVenda obj, MySqlConnection cn, MySqlTransaction tran)
        {
            //MySqlConnection cn = new MySqlConnection();
            try
            {
                //cn.ConnectionString = DadosDoBanco.stringDeConexao;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.CommandText = "insert into itensvenda (itv_cod, itv_qtde, itv_valor, ven_cod, pro_cod) values (@itv_cod, @itv_qtde, @itv_valor, @ven_cod, @pro_cod);";
                cmd.Parameters.AddWithValue("@itv_cod", obj.itv_cod);
                cmd.Parameters.AddWithValue("@itv_qtde", obj.itv_qtde);
                cmd.Parameters.AddWithValue("@itv_valor", obj.itv_valor);
                cmd.Parameters.AddWithValue("@ven_cod", obj.ven_cod);
                cmd.Parameters.AddWithValue("@pro_cod", obj.pro_cod);

                //cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException MySqlError)
            {
                throw new Exception("MySql error:" + MySqlError.Message);
            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
            finally
            {
                //cn.Close();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void Alterar(ModeloItensVenda obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn.Conexao;
                cmd.CommandText = "UPDATE itensvenda SET itv_qtde, itv_valor, ven_cod, pro_cod = @itvqtde, @itvvalor, @vencod, @procod, WHERE itv_cod = @itvcod";
                cmd.Parameters.AddWithValue("@itvcod", obj.itv_cod);
                cmd.Parameters.AddWithValue("@itvqtde", obj.itv_qtde);
                cmd.Parameters.AddWithValue("@itvvalor", obj.itv_valor);
                cmd.Parameters.AddWithValue("@vencod", obj.ven_cod);
                cmd.Parameters.AddWithValue("@procod", obj.pro_cod);

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
                cmd.CommandText = "delete From itensvenda WHERE itv_cod = @itvcod";

                cmd.Parameters.AddWithValue("@itvcod", codigo);

                cn.Conectar();
                cmd.ExecuteNonQuery();
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
        public DataTable Listagem()
        {
            DataTable tabela = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from itensvenda", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public DataTable ListagemComFiltro(string valor)
        {
            DataTable tabela = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from itensvenda where itv_qtde like '%" + valor + "%'", cn.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        //exclui todos os itens com base no código da venda
        public void ExcluirTodosOsItens(int codigo)
        {
            MySqlConnection cn = new MySqlConnection();
            try
            {
                cn.ConnectionString = DALDadosDoBanco.stringDeConexao;
                //comando
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "delete from itensvenda  WHERE ven_cod = @ven_cod";
                cmd.Parameters.AddWithValue("@ven_cod", codigo);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("MySql ERROR: " + ex.Number);
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
        //lista com o codigo da venda
        public DataTable ListagemComFiltro(int ven_cod)
        {
            DataTable tabela = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from itensvenda where ven_cod =" +
                ven_cod.ToString(), DALDadosDoBanco.stringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public ModeloItensVenda carregaModelo(int codigo, int ven_cod)
        {
            ModeloItensVenda modelo = new ModeloItensVenda();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn.Conexao;
            cmd.CommandText = "select * from itensvenda where itv_cod = " + codigo.ToString();
            cn.Conectar();
            MySqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.itv_cod = Convert.ToInt32(registro["itv_cod"]);
                modelo.itv_qtde = Convert.ToInt32(registro["itv_qtde"]);
                modelo.itv_valor = Convert.ToInt32(registro["itv_valor"]);
                modelo.ven_cod = Convert.ToInt32(registro["ven_cod"]);
                modelo.pro_cod = Convert.ToInt32(registro["pro_cod"]);
            }
            return modelo;
        }
        //-------------------------------------------------------------------------------------------------------------------
    }
}
