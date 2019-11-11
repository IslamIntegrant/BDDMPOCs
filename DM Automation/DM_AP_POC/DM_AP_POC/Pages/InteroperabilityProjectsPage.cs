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
		public void WaitForDataLoad()
		{
			WaitForPageToBeReady();
		}

		public void searchForInteroberabilityProject(string projectName) 
		{
			try
			{
				// Using SendKeys() instead of setTextElementText() as it sets the value directlly which does not fire the refresh event
				searchtTextBox.SendKeys(projectName);				
			}
			catch (Exception e)
			{

				Console.WriteLine("Error occured when searching for the interoperability project: " + e.Message);
				throw e;
			}
		}

		public void openInteroperabilityProject() 
		{
			try
			{
				interoberabilityProjectGridRow.Click();
				ClickButton(openInteroberabilityProjectButton);
				WaitForPageToBeReady();				
			}
			catch (Exception e)
			{

				Console.WriteLine("Error occured when opening interoperability project: " + e.Message);
				throw e;
			}
		}

		#endregion
	}
}