using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DM_AP_POC.Data;
using DM_AP_POC.Pages;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.IO;

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
		private ExtentReports extent;
		ExtentHtmlReporter htmlReporter;
		ExtentTest test;
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
			try
			{
				test = extent.CreateTest("Setup");

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
				homePage2.WaitForDataLoad();
				test.Log(Status.Info, "Health system impersonated");

				// Opening the desired interoperability project
				homePage2.openInteroperabilityProjectsPage();
				homePage2.WaitForDataLoad();

				test.Log(Status.Info, "Navigated to interoperability projects page");

				// Searching for the desired interoperability project
				projectsPage.searchForInteroberabilityProject(sharedData.cernerInteroperabilityProjectName);
				projectsPage.WaitForDataLoad();
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
			catch (Exception e)
			{

				test.Fail(e.Message + " " + e.StackTrace);
				Assert.Fail("An error occured while opening the alignment tab: " + e.Message + " " + e.Source);
			}
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
			try
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
				editProjectPageObject.SetPaginationToMaximumLength();

				// Assert that the item is sent to the acceptance tab
				Assert.That(editProjectPageObject.VerifyExistenceOfPairWithAction("Update"), "The item is not sent to the acceptance tab");
			}
			catch (Exception e)
			{

				test.Fail(e.Message + " " + e.StackTrace);
				Assert.Fail();
			}
		}

		[TestCase(TestName = "AutomaticallyAlignItemsWithActionExclude"), Order(2)]
		public void AutomaticallyAlignItemsWithActionExclude() 
		{			
			try
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
			catch (Exception e)
			{

				test.Fail(e.Message + " " + e.StackTrace);
				Assert.Fail();
			}
		}

		[TestCase(TestName = "ManualAlignWithActionAlign"), Order(3)]
		public void ManualAlignWithActionAlign() 
		{			
			try
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
			catch (Exception e)
			{

				test.Fail(e.Message + " " + e.StackTrace);
				Assert.Fail();
			}
		}

		[TestCase(TestName = "ManualAlignWithActionUpdateWithNewEMR"), Order(4)]
		public void ApplyManulAlignWithUpdateActionWithNewEMR()
		{			
			try
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
			catch (Exception e)
			{
				test.Fail(e.Message + " " + e.StackTrace);
				Assert.Fail();
			}
		}

		[TestCase(TestName = "ManualAlignWithActionUpdateWithNewDERS"), Order(5)]
		public void ApplyManulAlignWithUpdateActionWithNewDERS()
		{			
			try
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
			catch (Exception e)
			{

				test.Fail(e.Message + " " + e.StackTrace);
				Assert.Fail();
			}
		}

		[TearDown, Order(1)]
		public static void ScreenshotOnFailure()
		{
			if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
			{
				string ScreenShotPath = AppDomain.CurrentDomain.BaseDirectory;
				int PathLength = ScreenShotPath.IndexOf("\\bin");
				ScreenShotPath = ScreenShotPath.Substring(0, PathLength) + "\\TCs\\Screenshots\\" + TestContext.CurrentContext.Test.MethodName + DateTime.Now.ToString("HHmmss") + ".png";
				try
				{
					Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
					ss.SaveAsFile(ScreenShotPath);
				}
				catch (Exception e)
				{

					throw new IOException(e.Message);
				}
			}
		}

		[TearDown, Order(2)]
		public void AfterTest()
		{
			var status = TestContext.CurrentContext.Result.Outcome.Status;
			var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
					? ""
					: string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
			Status logstatus;

			switch (status)
			{
				case TestStatus.Failed:
					logstatus = Status.Fail;
					break;
				case TestStatus.Inconclusive:
					logstatus = Status.Warning;
					break;
				case TestStatus.Skipped:
					logstatus = Status.Skip;
					break;
				default:
					logstatus = Status.Pass;
					break;
			}

			test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
			extent.Flush();
		}	
		#endregion
	}
}