using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_AP_POC.Pages
{
	class SecurityPage : PageBase
	{
		public SecurityPage(IWebDriver driver) : base(driver)
		{

		}
		#region Elements
		[FindsBy(How = How.Id, Using = "details-button")]
		IWebElement advancedButton;

		[FindsBy(How = How.Id, Using = "proceed-link")]
		IWebElement proceedLink;
		#endregion

		#region Actions
		public void ClickAdvancedButton()
		{
			ClickButton(advancedButton);
		}

		public void ClickProceedLink() 
		{
			ClickButton(proceedLink);
		}
		#endregion
	}
}
