using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_AP_POC.HelperClasses
{
	class HelperClass
	{
		public string UserName;
		public string Password;
		public void ReadJsonData(string FilePath) 
		{
			// read JSON directly from a file
			using (StreamReader file = File.OpenText(FilePath))
			using (JsonTextReader reader = new JsonTextReader(file))
			{
				JObject JObject = (JObject)JToken.ReadFrom(reader);
				UserName = JObject.GetValue("SuperUserUserName").ToString();
				Password = JObject.GetValue("SuperUserPassword").ToString();
			}
		}
	}
}
