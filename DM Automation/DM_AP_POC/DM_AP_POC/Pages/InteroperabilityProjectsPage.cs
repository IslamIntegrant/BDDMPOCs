using DM_AP_POC.TCs;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace DM_AP_POC.Pages
{
	class InteroperabilityProjectsPage : PageBase
	{
		public InteroperabilityProjectsPage(IWebDriver driver) : base(driver)
		{

		}

		#region Elements
		[FindsBy(How = How.XPath, Using = "/html/body/app/div/ng-component/div/div[2]/tabset/div/ng-component/div/div/div/primeng-ptable/div/div/p-table/div/div[1]/div/div[1]/div[1]/div/input")]
		IWebElement searchtTextBox;

		[FindsBy(How = How.XPath, Using = "/html/body/app/div/ng-component/div/div[2]/tabset/div/ng-component/div/div/div/primeng-ptable/div/div/p-table/div/div[2]/table/tbody/tr")]
		IWebElement interoberabilityProjectGridRow;

		[FindsBy(How = How.Id, Using = "EditAlignmentProject")]
		IWebElement openInteroberabilityProjectButton;

		[FindsBy(How = How.XPath, Using = "/html/body/app/div/ng-component/div/div[2]/tabset/div/ng-component/div/div/div/primeng-ptable/div/div/p-table/div/div[2]/table/tbody/tr/td")]
		public IWebElement emptyGridRow;

		[FindsBy(How = How.XPath, Using = "/html/body/app/div/ng-component/div/div[2]/tabset/div/ng-component/div/div/div/primeng-ptable/div/div/p-table/div/div[2]/table/tfoot/div")]
		public IWebElement interoberabilityProjectsPagingResult;
		#endregion

		#region Actions
		
		public void searchForInteroberabilityProject(string projectName) 
		{
			try
			{
				// Using SendKeys() instead of setTextElementText() as it sets the value directlly which does not fire the refresh event
				searchtTextBox.SendKeys(projectName);				
			}
			catch (Exception e)
			{

				TestClass.test.Log(AventStack.ExtentReports.Status.Error,"Error occured when searching for the interoperability project: " + e.Message);
				TestClass.test.Fail(e.Message);
				throw e;
			}
		}

		public void openInteroperabilityProject() 
		{
			
				interoberabilityProjectGridRow.Click();
				ClickButton(openInteroberabilityProjectButton);
				WaitForPageToBeReady();	
		}

		#endregion
	}
}