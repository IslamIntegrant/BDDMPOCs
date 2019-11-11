using System.Data.SqlClient;

namespace DM_AP_POC.Data
{
	public class DBData
	{
		public int GetAlignmentProjectStatus(string AlignmentProjectName) 
		{
			string  sqlQueryString = "select StatusKey from[EF].SmartPumpAlignmentProject where ProjectName = '" + "Fevazateooiivic" + "'";
			SqlCommand sqlCommand = new SqlCommand(sqlQueryString, new SqlConnection(Properties.Settings.Default.TestADB));
			sqlCommand.Connection.Open();
			int AlignmentProjectStatus = (int) sqlCommand.ExecuteScalar();
			sqlCommand.Connection.Close();
			return AlignmentProjectStatus;
		}
	}
}
