using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using MySql.Data.MySqlClient;

namespace ControleDeEstoque.GUI
{
    public class MySqlServerBackup
    {
        public static ArrayList ObtemBancoDeDadosMySqlServer(string ConnString)
        {
            ArrayList lista = new ArrayList();
            //criou a conexao
            MySqlConnection cn = new MySqlConnection(ConnString);
            //criou o comando
            MySqlCommand cm = new MySqlCommand();
            cm.Connection = cn;
            cm.CommandText = "SELECT [name] FROM sysdatabases";
            //criou o DataReader
            MySqlDataReader dr;
            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(dr["name"]);
                }
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                cn.Close();
            }
            return lista;
        }

        public static void BackupDataBase(string ConnString, string nomeDB, string backupFile)
        {
            //string backup
            //criou a conexao
            MySqlConnection cn = new MySqlConnection(ConnString);
            //criou o comando
            MySqlCommand cm = new MySqlCommand();
            cm.Connection = cn;
            cm.CommandText = "BACKUP DATABASE [" + nomeDB + "] TO DISK = '" + backupFile + "'";

            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public static void RestaurarDataBase(string ConnString, string nomeDB, string backupFile)
        {
            MySqlConnection cn = new MySqlConnection(ConnString);
            //criou o comando
            MySqlCommand cm = new MySqlCommand();
            cm.Connection = cn;
            string MySql = "USE master ALTER DATABASE [" + nomeDB + "] SET OFFLINE WITH ROLLBACK IMMEDIATE; RESTORE DATABASE [" + nomeDB + "] FROM DISK = '" + backupFile + "' WITH REPLACE; USE master ALTER DATABASE [" + nomeDB + "] SET ONLINE";
            cm.CommandText = MySql;
            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
