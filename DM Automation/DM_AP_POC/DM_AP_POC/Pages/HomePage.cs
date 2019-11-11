using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace DM_AP_POC.Pages
{
	class HomePage : PageBase
	{
		public HomePage(IWebDriver driver) : base(driver) 
		{
			
		}
		#region Elements
		[FindsBy(How = How.XPath, Using = "//*[@id=\"anchorSignIn\"]")]
		IWebElement signInButton;
		#endregion

		#region Actions
		public void ClickSignInButton() 
		{
			ClickButton(signInButton);
			Console.WriteLine("Clicking the sign in button");
		}
		#endregion
	}
}
