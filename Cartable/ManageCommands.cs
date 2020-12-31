using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Configuration;

namespace Cartable
{
    public class ManageCommands
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
            SqlConnection myConnection;
            public ManageCommands(SqlParameter[] parameters, string StoreProcedureName)
            {
                SqlCommand cmd = new SqlCommand();
                myConnection = new SqlConnection();
                myConnection.ConnectionString = connectionString;
                cmd.Connection = myConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = StoreProcedureName;
                myConnection.Open();
                foreach (SqlParameter parameter in parameters)
                    cmd.Parameters.Add(parameter);
                cmd.ExecuteNonQuery();
                myConnection.Close();            
             }
    }
    public class RetrieveDataAdapter
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private SqlConnection cnn = new SqlConnection();
        public SqlDataAdapter GetDataAdapter(string store_procedure_name, SqlParameter[] parameters)
        {
            SqlCommand command = this.Acknowledgment(store_procedure_name, parameters);
            SqlDataAdapter data_adapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand
                {
                    Connection = new SqlConnection(connectionString),
                    CommandText = store_procedure_name,
                    CommandType = CommandType.StoredProcedure
                }
            };
            data_adapter.SelectCommand = command;
            return data_adapter;
        }
        public SqlDataAdapter GetDataAdapter(string store_procedure_name)
        {
            SqlDataAdapter data_adapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand
                {
                    Connection = new SqlConnection(connectionString),
                    CommandText = store_procedure_name,
                    CommandType = CommandType.StoredProcedure
                }
            };
            return data_adapter;
        }
        public DataSet RunCommandText(string commandText, string tableName)
        {
            cnn.ConnectionString = connectionString;
            DataSet dataSet = new DataSet();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            SqlCommand command = new SqlCommand(commandText, cnn);
            command.CommandType = CommandType.Text;
            SqlDataAdapter dataAdaptor = new SqlDataAdapter(command);
            dataAdaptor.Fill(dataSet, tableName);
            cnn.Close();
            return dataSet;
        }
        public List<DropDownItems> Fill_DropDownList(string commandText)
        {
            List<DropDownItems> lst = new List<DropDownItems>();
            //try
            {
                cnn.ConnectionString = connectionString;
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlCommand command = new SqlCommand(commandText, cnn);
                command.CommandType = CommandType.Text;
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    DropDownItems obj = new DropDownItems();
                    if (dr[0] != null)
                        obj.srl = Convert.ToInt32(dr[0]);
                    if (dr[1] != null)
                        obj.title_dir = dr[1].ToString();
                    lst.Add(obj);
                }
                cnn.Close();
                dr.Close(); dr.Dispose();
            }
            //catch { }
            return lst;
        }
        public SqlCommand Prepare_Parameters(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(storedProcName, cnn);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
                command.Parameters.Add(parameter);
            return command;
        }
        public SqlCommand Acknowledgment(string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = this.Prepare_Parameters(storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        public IDataReader DataReader(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(storedProcName, cnn);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
                command.Parameters.Add(parameter);
            cnn.Open();
            IDataReader dr = command.ExecuteReader();
            return dr;
        }
    }
    public class DropDownItems
    {
        public int srl { get; set; }
        public string title_dir { get; set; }
    }
}
