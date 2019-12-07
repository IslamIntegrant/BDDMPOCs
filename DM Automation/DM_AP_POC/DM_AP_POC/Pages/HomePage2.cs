using DM_AP_POC.Data;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace DM_AP_POC.Pages
{
	class HomePage2 : PageBase
	{
		public HomePage2(IWebDriver driver) : base(driver)
		{

		}
		#region Elements
		[FindsBy(How = How.CssSelector, Using = "#ActingAsDropdown")]
		IWebElement actingAsMenu;

		[FindsBy(How = How.XPath, Using = "//body/app//top-menu/ul/li[4]/select-health-system-widget/div/div/div[2]/div[2]")]
		IWebElement IdnContainer;

		[FindsBy(How = How.XPath, Using = "/html/body/app/div/ng-component/div/div[2]/home-to-do-list/div/div[1]/div/div/div/div/input")]
		public IWebElement searchTextBox;

		[FindsBy(How = How.ClassName, Using = "loading-animation")]
		public IWebElement loader;

		[FindsBy(How = How.XPath, Using = "/html/body/app/div/ng-component/div/div[2]/home-to-do-list/div/div[2]/ng2-smart-table/table/tbody/tr/td")]
		public IWebElement emptyToDoGridRow;

		[FindsBy(How = How.XPath, Using = "/html/body/app/div/ng-component/div/div[2]/home-to-do-list/div/div[3]/show-results/div/div/div[1]")]
		public IWebElement todoPagingResult;
		#endregion


		#region Actions

		public void ImpersonateIDN(string IdnName)
		{
			
				WaitForElementToExistByID(actingAsMenu);
				ClickButton(actingAsMenu);
				ClearText(actingAsMenu);
				actingAsMenu.SendKeys(IdnName);				
			
		}

		public void openInteroperabilityProjectsPage() 
		{
			SharedData sharedData = new SharedData();
			// Navigating to interoberability projects pages
			Driver.Navigate().GoToUrl(sharedData.interoperabilityProjectsListPage[2].AbsoluteUri);			
		}

		#endregion
	}
}