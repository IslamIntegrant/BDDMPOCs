using DM_AP_POC.Data;
using DM_AP_POC.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_AP_POC.TCs
{	
	[SetUpFixture]
	class Setup : TestBase
	{
		static HomePage homePage;
		static LoginPage loginPage;
		static SharedData sharedData;
		static HomePage2 homePage2;
		static InteroperabilityProjectsPage projectsPage;
		static TestClass testClass;
		
		#region One Time Setup

		[OneTimeSetUp(), Order(1)]
		public static void Login() 
		{
		    // Initializing objects of the pages required for the login process
			homePage = new HomePage(Driver);
			loginPage = new LoginPage(Driver);
			sharedData = new SharedData();

			// Initializing objects of the pages  required for accessing the interoberability project
			homePage2 = new HomePage2(Driver);
			projectsPage = new InteroperabilityProjectsPage(Driver);

			//Logging in
			homePage.ClickSignInButton();
			loginPage.Login(sharedData.superUserUserName, sharedData.superUserPassword);
			waitForGridLoading(homePage2.emptyToDoGridRow, homePage2.todoPagingResult);
		}
		// Impersonating the health system
		[OneTimeSetUp, Order(2)]
		public static void impersonateHealthSystem() 
		{			
			homePage2.ImpersonateIDN(sharedData.idnName);
			waitForGridLoading(homePage2.emptyToDoGridRow, homePage2.todoPagingResult);
		}

		// Opening the desired interoperability project
		[OneTimeSetUp, Order(3)]
		public  void openInteroberabilityProject() 
		{
			sharedData = new SharedData();
			//testClass = new TestClass(Driver);
			// Navigating to interoberability projects pages
			Driver.Navigate().GoToUrl(sharedData.interoperabilityProjectsListPage.AbsoluteUri);
			waitForGridLoading(projectsPage.emptyGridRow, projectsPage.interoberabilityProjectsPagingResult);

			projectsPage.searchForInteroberabilityProject(sharedData.cernerInteroperabilityProjectName);
			waitForGridLoading(projectsPage.emptyGridRow, projectsPage.interoberabilityProjectsPagingResult);
			projectsPage.openInteroperabilityProject();
			
		}

		#endregion
				
	}
}
