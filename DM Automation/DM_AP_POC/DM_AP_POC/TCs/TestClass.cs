using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DM_AP_POC.Data;
using DM_AP_POC.Pages;
using NUnit.Framework;
using System;

namespace DM_AP_POC.TCs
{
	[TestFixture]
	class TestClass : TestBase
	{
		static HomePage homePage;
		static LoginPage loginPage;
		static SharedData sharedData;
		static HomePage2 homePage2;
		static InteroperabilityProjectsPage projectsPage;
		EditInteroberabilityProjectPage editProjectPageObject;
		public static ExtentReports extent;
		public static ExtentHtmlReporter htmlReporter;
		public static ExtentTest test;
		int itemsPairIndex;
		public bool isSetupCompleted = false;  // This variable is used to indecate whatever the user is logged in + the IDN is impersonated + The interoperability project is opened on the alignment tab or not

		#region One Time Setup

		[OneTimeSetUp]
		public void InitializingReports() 
		{
			string ReportPath = AppDomain.CurrentDomain.BaseDirectory;
			int PathLength = ReportPath.IndexOf("\\bin");
			ReportPath = ReportPath.Substring(0, PathLength) + "\\TCs\\TestReport\\";
			htmlReporter = new ExtentHtmlReporter(ReportPath + "\\TestClassReport.html");
			htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
			htmlReporter.Config.DocumentTitle = "Test class report";
			htmlReporter.Config.ReportName = "Test class report";
			extent = new ExtentReports();
			extent.AttachReporter(htmlReporter);			
		}
		
		public void Setup()
		{
			
				// Initializing objects of the pages required for the login process
				homePage = new HomePage(Driver);
				loginPage = new LoginPage(Driver);
				sharedData = new SharedData();

				// Initializing objects of the pages required for accessing the interoberability project
				homePage2 = new HomePage2(Driver);
				projectsPage = new InteroperabilityProjectsPage(Driver);

				//Logging in
				homePage.ClickSignInButton();
				loginPage.Login(sharedData.superUserUserName, sharedData.superUserPassword);
				test.Log(Status.Info, "Logged in sucessfully");
				
				// Impersonating the health system
				homePage2.ImpersonateIDN(sharedData.idnName);
				homePage2.WaitForPageToBeReady();
				test.Log(Status.Info, "Health system impersonated");

				// Opening the desired interoperability project
				homePage2.openInteroperabilityProjectsPage();
				homePage2.WaitForPageToBeReady();

				test.Log(Status.Info, "Navigated to interoperability projects page");

				// Searching for the desired interoperability project
				projectsPage.searchForInteroberabilityProject(sharedData.cernerInteroperabilityProjectName);
				projectsPage.WaitForPageToBeReady();
				test.Log(Status.Info, "Interoperability project search completed successfully");

				// Opening the desured interoperability project
				projectsPage.openInteroperabilityProject();
				test.Log(Status.Info, "Interoperability project opened successfully");

				// Declaring the edit interoperability page object
				editProjectPageObject = new EditInteroberabilityProjectPage(Driver);
				
				// Opening the alignment tab			
				editProjectPageObject.GotoAlignmentTab();
				editProjectPageObject.WaitForPageToBeReady();
				test.Log(Status.Info, "Alignment tab header clicked successfully");
				
				// Setting this flags with true to let all subsequent TCs know that the alignment tab is already opened
				isSetupCompleted = true;
						
			
		}		
		#endregion

		[SetUp]
		public void BeforeTest()
		{
			test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
		}

		// Generate random number
		public int generatRandomNumber(int minValue, int maxValue)
		{
			Random randomNumber = new Random();
			return randomNumber.Next(minValue, maxValue);
		}

		#region TestCases;
		[TestCase(TestName = "AutomaticallyAlignItemsWithActionUpdate"), Order(1)]
		public void AutomaticallyAlignItemsWithActionUpdate() 
		{	
				if (isSetupCompleted == false)
				{
					// Calling this method logs in to the system + impersonates the IDN + opens the interoperability projects page + 
					// Opens the desired interoperability project and finally opens the alignment tab for that project
					Setup();
				}
				else
				{
					editProjectPageObject.GotoAlignmentTab();
					editProjectPageObject.WaitForPageToBeReady();
				}
				// Picking random items pair
				itemsPairIndex = generatRandomNumber(1, 10);

				// Opening alignmentTab if not opened
				editProjectPageObject.GotoAlignmentTab();

				// Applying the update action to the desired pair
				editProjectPageObject.ApplyUpdateActionForSelectedPair(itemsPairIndex);

				editProjectPageObject.gotoAcceptanceTab();
				editProjectPageObject.SetPaginationToMaxLength();

				// Assert that the item is sent to the acceptance tab
				Assert.That(editProjectPageObject.VerifyExistenceOfPairWithAction("Update"), "The item is not sent to the acceptance tab");
			
		}

		[TestCase(TestName = "AutomaticallyAlignItemsWithActionExclude"), Order(2)]
		public void AutomaticallyAlignItemsWithActionExclude() 
		{			
			if (isSetupCompleted == false)
				{
					// Calling this method logs in to the system + impersonates the IDN + opens the interoperability projects page + 
					// Opens the desired interoperability project and finally opens the alignment tab for that project
					Setup();
				}
				else
				{
					editProjectPageObject.GotoAlignmentTab();
					editProjectPageObject.WaitForPageToBeReady();
				}
				// Picking random items pair
				itemsPairIndex = generatRandomNumber(0, 9);

				// Applying the exclude action to the selected pair
				editProjectPageObject.ApplyExcludeActionForSelectedPair(itemsPairIndex);
				
				// Assertion
				editProjectPageObject.gotoAcceptanceTab();
				editProjectPageObject.SetPaginationToMaximumLength();
				// Assert that the item is sent to the acceptance tab
				Assert.That(editProjectPageObject.VerifyExistenceOfPairWithAction("Exclude"), "The item is not sent to the acceptance tab");
			
		}

		[TestCase(TestName = "ManualAlignWithActionAlign"), Order(3)]
		public void ManualAlignWithActionAlign() 
		{			
			if (isSetupCompleted == false)
				{
					// Calling this method logs in to the system + impersonates the IDN + opens the interoperability projects page + 
					// Opens the desired interoperability project and finally opens the alignment tab for that project
					Setup();
				}
				else
				{
					editProjectPageObject.GotoAlignmentTab();
					editProjectPageObject.WaitForPageToBeReady();
				}
				// Picking random items pair
				itemsPairIndex = generatRandomNumber(1, 10);
				editProjectPageObject.ApplyManulAlignWithAlignAction(itemsPairIndex);
				editProjectPageObject.gotoAcceptanceTab();
				editProjectPageObject.SetPaginationToMaximumLength();

				// Assert that the item is sent to the acceptance tab
				Assert.That(editProjectPageObject.VerifyExistenceOfPairWithAction("Align"), "The item is not sent to the acceptance tab");

		}

		[TestCase(TestName = "ManualAlignWithActionUpdateWithNewEMR"), Order(4)]
		public void ApplyManulAlignWithUpdateActionWithNewEMR()
		{		
			if (isSetupCompleted == false)
				{
					// Calling this method logs in to the system + impersonates the IDN + opens the interoperability projects page + 
					// Opens the desired interoperability project and finally opens the alignment tab for that project
					Setup();
				}
				else
				{
					editProjectPageObject.GotoAlignmentTab();
					editProjectPageObject.WaitForPageToBeReady();
				}
				// Picking random items pair
				itemsPairIndex = generatRandomNumber(1, 10);
				editProjectPageObject.ApplyManulAlignWithUpdateActionWithNewEMR(itemsPairIndex);
				editProjectPageObject.gotoAcceptanceTab();
				editProjectPageObject.SetPaginationToMaximumLength();

				// Assert that the item is sent to the acceptance tab
				Assert.That(editProjectPageObject.VerifyExistenceOfPairWithAction("Update"), "The item is not sent to the acceptance tab");
			
		}

		[TestCase(TestName = "ManualAlignWithActionUpdateWithNewDERS"), Order(5)]
		public void ApplyManulAlignWithUpdateActionWithNewDERS()
		{
				if (isSetupCompleted == false)
				{
					// Calling this method logs in to the system + impersonates the IDN + opens the interoperability projects page + 
					// Opens the desired interoperability project and finally opens the alignment tab for that project
					Setup();
				}
				else
				{
					editProjectPageObject.GotoAlignmentTab();
					editProjectPageObject.WaitForPageToBeReady();
				}
				// Picking random items pair
				itemsPairIndex = generatRandomNumber(1, 10);
				editProjectPageObject.ApplyManulAlignWithUpdateActionWithNewDERS(itemsPairIndex);
				editProjectPageObject.gotoAcceptanceTab();
				editProjectPageObject.SetPaginationToMaximumLength();

				// Assert that the item is sent to the acceptance tab
				Assert.That(editProjectPageObject.VerifyExistenceOfPairWithAction("Update"), "The item is not sent to the acceptance tab");
			
		}

			
		#endregion
	}
}