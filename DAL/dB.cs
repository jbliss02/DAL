using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Db
    {

        public string stConn;

        public enum connections { UNDEFINED }

        public Db()
        {

        }

        public DataTable DataTableFromProc(string procName, string stcon)
        {
            SqlConnection con = new SqlConnection(stcon);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();

            return dt;
        }

        public DataTable DataTableFromProc(string procName, string stcon, List<SqlParameter> paras)
        {
            SqlConnection con = new SqlConnection(stcon);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;

            foreach (SqlParameter p in paras)
            {
                cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
            }


            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();

            return dt;
        }

        public DataTable DataTableFromProc(string procName, string stcon, SqlParameter para)
        {

            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(para);
            return DataTableFromProc(procName, stcon, paras);

        }

        public DataSet DataSetFromProc(string procName, string stcon, SqlParameter para)
        {

            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(para);
            return DataSetFromProc(procName, stcon, paras);

        }

        public DataSet DataSetFromProc(string procName, string stcon, List<SqlParameter> paras)
        {
            SqlConnection con = new SqlConnection(stcon);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;

            foreach (SqlParameter p in paras)
            {
                cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
            }

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);

            con.Close();

            return ds;
        }

        public DataSet DataSetFromSQL(string stsql, string stcon)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = stcon;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(stsql, con);

            DataSet ds = new DataSet();
            da.Fill(ds, "companies");

            con.Close();

            return ds;
        }

        public DataSet DataSetFromSQL(string stsql, SqlConnection con)
        {

            SqlDataAdapter da = new SqlDataAdapter(stsql, con);

            DataSet ds = new DataSet();
            da.Fill(ds, "companies");

            return ds;
        }

        public SqlConnection ReturnLiveConnection(string stcon)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = stcon;
            con.Open();
            return con;
        }


        public DataTable DataTableFromSQL(string stsql, SqlConnection con)
        {

            SqlDataAdapter da = new SqlDataAdapter(stsql, con);

            DataSet ds = new DataSet();
            da.Fill(ds, "companies");

            return ds.Tables[0];
        }

        public DataTable DataTableFromTable(string procName, string stcon, string stsql)
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = stcon;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(stsql, con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();

            return dt;

        }

        public string RunProcAndReturn(string procName, string stcon, SqlParameter p)
        {
            string st = string.Empty;

            SqlConnection con = new SqlConnection(stcon);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;

            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);

            //add the return value
            SqlParameter retval = cmd.Parameters.Add("@ret", SqlDbType.VarChar);
            retval.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            st = (string)cmd.Parameters["@ret"].Value.ToString();

            con.Close();

            return st;
        }

        public void RunProc(string procName, string stcon, List<SqlParameter> paras)
        {
            SqlConnection con = new SqlConnection(stcon);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;

            foreach (SqlParameter p in paras)
            {
                cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
            }


            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void RunProc(string procName, string stcon, SqlParameter para)
        {

            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(para);
            RunProc(procName, stcon, paras);
        }

        public void RunProc(string procName, string stcon)
        {

            List<SqlParameter> paras = new List<SqlParameter>();
            RunProc(procName, stcon, paras);
        }

        public void RunProc2(string procName, string stcon, SqlParameter para)
        {
            SqlConnection con = new SqlConnection(stcon);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;


            cmd.Parameters.AddWithValue(para.ParameterName, para.Value);



            cmd.ExecuteNonQuery();


            con.Close();
        }

        public void RunSql(string stsql, string stcon)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = stcon;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = stsql;
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void RunSql(string stsql, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = stsql;
            cmd.ExecuteNonQuery();

        }

        public DataSet dsFromSql(string stsql, string stcon)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = stcon;

            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = stsql;
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();
            da.Fill(ds);

            con.Close();

            return ds;
        }

        public void RunProc(string procName, List<SqlParameter> paras, SqlConnection con)
        {
            //runs a proc on the passed in connection
            //saves opening a connection each time
            //when using db heavy programs

            if (con.State == ConnectionState.Closed) { con.Open(); }

            SqlCommand cmd = new SqlCommand();
            foreach (SqlParameter p in paras)
            {
                cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
            }

            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            cmd.ExecuteNonQuery();

        }

        public void RunProc(string procName, SqlParameter para, SqlConnection con)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(para);
            RunProc(procName, paras, con);
        }

        public SqlParameter returnParam(string parameterName, DbType parameterType)
        {
            SqlParameter p = new SqlParameter();
            p.DbType = parameterType;
            p.ParameterName = parameterName;

            return p;

        }





    }



}
