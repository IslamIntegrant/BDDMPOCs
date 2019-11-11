using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_AP_POC.Data.DB
{
	class DBConnection
	{
		public SqlConnection OpenDBConnection() 
		{
			SqlConnection DBConnection = new SqlConnection(Properties.Settings.Default.TestADB);
			DBConnection.Open();
			return DBConnection;
		}
	}
}
