using System;

namespace DM_AP_POC.Data
{
	public class SharedData
	{
		// This class will be used to store the shared configuration data such test environment URL, Data base time out, etc...

		#region Timeouts
		// Below I will specify the amount of seconds the system shall wait for the Alignment project status key to change
		public TimeSpan waitForProjectStatusChange = TimeSpan.FromSeconds(120);
		#endregion

		#region URLs
		public Uri[] websiteURL = new Uri[] { new Uri("https://sd-test-a-2012.kp.cfnp.local"), new Uri("https://sdtest2012.kp.cfnp.local"), new Uri("https://las-stage-a-2012.kp.cfnp.local") };
		public Uri[] interoperabilityProjectsListPage = new Uri[] { new Uri("https://sd-test-a-2012.kp.cfnp.local/DataManager/#/InteroperabilityProjects/Active") , new Uri("https://sdtest2012.kp.cfnp.local/DataManager/#/InteroperabilityProjects/Active"), new Uri("https://las-stage-a-2012.kp.cfnp.local/DataManager/#/InteroperabilityProjects/Active") }; 
		#endregion

		#region Test accounts
		public string superUserUserName = "Islam.Alhamed@bd.com";
		public string superUserPassword = "Welcome@4";
		#endregion

		#region Test data
		public string idnName = "IslamDemo1";
		public string facilityName = "Fac1IslamDemo1";
		public string cernerInteroperabilityProjectName = "CernerWithPFMapping";
		public string epicInteroberabilityProjectName = "EPIC";
		public string acceptanceTabExcludeAction = "Exclude";
		#endregion

		
		#region Common pages data
		public string homePageTitle = "HealthSight™ Data Manager";
		#endregion
	}
}