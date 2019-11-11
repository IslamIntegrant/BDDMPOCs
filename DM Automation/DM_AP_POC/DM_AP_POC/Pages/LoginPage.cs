using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace DM_AP_POC.Pages
{
	class LoginPage : PageBase
	{
		public LoginPage(IWebDriver driver) : base(driver)
		{

		}
		#region Elements
		[FindsBy(How = How.Id, Using = "username")]
		IWebElement userNameTextBox;

		[FindsBy(How = How.Id, Using = "password")]
		IWebElement passwordTextBox;

		[FindsBy(How = How.Id, Using = "sign-in")]
		IWebElement signinButton;
		#endregion

		#region Actions
		public void Login(string userName, string password)
		{
			try
			{
				SetTextElelmentText(userNameTextBox, userName);
				SetTextElelmentText(passwordTextBox, password);
				ClickButton(signinButton);
				WaitForPageToBeReady();
			}
			catch (Exception e)
			{

				Console.WriteLine("Failed to login: " + e.Message);
				throw e;
			}
			
		}
		#endregion
	}
}