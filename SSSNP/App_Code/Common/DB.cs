using System;
using System.Data;
using System.Data.SqlClient;



namespace DB
{

    public class DB
    {
        
        private SqlConnection cnn = new SqlConnection();

        public DB()
        {

        }        

        public void OpenConnection()
        {
            if (cnn.State == 0)
            {
                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString; //@"Data Source=.;Initial Catalog=citizencharter;USER id=sa;password=sa";
                cnn.Open();
            }
        }

        public void OpenConnection(string cnStr)
        {
            //if (cnn.State !=0)
            if (cnn.State == 0)
            {
                cnn.ConnectionString = cnStr;
                cnn.Open();
            }
        }

        public SqlConnection GetConnection
        {
            get
            {
                return cnn;
            }
        }

        public void CloseConnection()
        {
            if (cnn.State != 0)
                cnn.Close();
        }

        public DataSet ExecuteQuery(String MySQL)
        {


            DataSet myDataSet = new System.Data.DataSet(); 
            myDataSet.EnforceConstraints = false;
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("getDMSAbs", GetConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SQL", SqlDbType.VarChar).Value = MySQL;

                SqlDataAdapter myDataAdapter;
                myDataAdapter = new SqlDataAdapter(cmd);
                
                myDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                myDataAdapter.Fill(myDataSet);

                return myDataSet;

            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
                CloseConnection();
            }

        }
        public int ExecuteNonQuery(String MySQL)
        {
            
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand(MySQL, GetConnection);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

        }
        public SqlDataReader ExecuteSelectQuery(String MySQL, SqlConnection con)
        {
            SqlDataReader Mydr = null;
            try
            {
                SqlCommand myCommand = new SqlCommand(MySQL, con);
                Mydr = myCommand.ExecuteReader();

            }
            catch (Exception ex)
            {
                // throw ex;
            }
            return Mydr;
            
        }
    }
}
