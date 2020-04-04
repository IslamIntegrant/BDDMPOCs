using DM_AP_POC.TCs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace DM_AP_POC.Pages
{
	class PageBase
	{
		private IWebDriver driver;
		private IJavaScriptExecutor executor;		
		IWebElement loaderElementVisible;
				
		public IJavaScriptExecutor Executor { get => executor; set => executor = value; }
		protected IWebDriver Driver { get => driver; set => driver = value; }

		public ReadOnlyCollection<IWebElement> loadingMsg => Driver.FindElements(By.CssSelector("ajax-loader > div.loadingplaceholder"));
		public int numOfLoadingBars => Driver.FindElements(By.CssSelector("ajax-loader > div.loadingplaceholder")).Count;

		public PageBase(IWebDriver driver) 
		{
			Driver = driver;
			SeleniumExtras.PageObjects.PageFactory.InitElements(Driver, this);
			Executor = (IJavaScriptExecutor)driver;
		}

		public String GetAbsoluteXPath(IWebElement element)
		{
			string FileContents = File.ReadAllText(Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\JS" + "\\GetApsoultePath.JS"), Encoding.UTF8);
			return (String)((IJavaScriptExecutor)Driver).ExecuteScript(FileContents, element);
		}

		// This method sets the number of displayed grid rows in the page to the maximum available number
		public void SetPaginationToMaxLength() 
		{
			try
			{
				string paginatationMenuArrowClass = "ui-dropdown-trigger-icon ui-clickable pi pi-chevron-down";
				if (CheckIfElementExistByXPath("//*[contains(@class, '" + paginatationMenuArrowClass + "')]"))
				{
					Driver.FindElement(By.XPath("//*[contains(@class, '" + paginatationMenuArrowClass + "')]")).Click();
					IWebElement paginationDropdownMenuCollection = Driver.FindElement(By.XPath("/html/body/app/div/alignment-project-details/div/div/div[3]/tabset/div/alignment-project-acceptance-tab/div/div/div[1]/primeng-ptable/div/div/p-table/div/p-paginator/div/p-dropdown/div/div[3]"));
					Thread.Sleep(1);
					paginationDropdownMenuCollection.FindElements(By.TagName("div")).First().FindElements(By.TagName("ul")).First().FindElements(By.TagName("p-dropdownitem")).Last().FindElement(By.TagName("li")).FindElement(By.TagName("span")).Click();
					WaitForPageToBeReady();
				}
				else
				{
					return;
				}
			}
			catch (Exception e)
			{
				TestClass.test.Log(AventStack.ExtentReports.Status.Error, "Error occured while seting pagination to the maximum length");
				TestClass.test.Fail(e.Message + ", " + e.StackTrace);
				throw e;
			}
		}

		public bool CheckIfElementExistByXPath(string elementXPath) 
		{
			try
			{
				return Driver.FindElements(By.XPath(elementXPath)).Count > 0;
				
			}
			catch (Exception)
			{

				return false;
			}
		}

		public void WaitForPageToBeReady()
		{
			try
			{
				int initialLoadersCount = 0;
				do
				{
					Thread.Sleep(1000);
					initialLoadersCount = numOfLoadingBars;
				}
				while (initialLoadersCount > 0);				
			}
			catch (Exception e)
			{

				TestClass.test.Log(AventStack.ExtentReports.Status.Error,"Error occured when waiting for the loader to disappear");
				TestClass.test.Fail(e.Message);
				throw e;
			}
		}

		public void WaitForElementToBeClickable(string elementXPath) 
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
				wait.PollingInterval = TimeSpan.FromSeconds(1);
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(elementXPath)));
			}
			catch (Exception)
			{

				TestClass.test.Log(AventStack.ExtentReports.Status.Info, "Skipped waiting for the element to be clickable");
			}
		}

		public void WaitForElementToExistByID(IWebElement element) 
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
				wait.PollingInterval = TimeSpan.FromSeconds(1);
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id(element.GetAttribute("id"))));
			}
			catch (Exception e)
			{
				TestClass.test.Log(AventStack.ExtentReports.Status.Error, "Error occured while waiting for element");
				TestClass.test.Fail(e.Message);
				throw e;
			}
		}

		public void WaitForElementToExistByCSS(string cssSelector)
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
				wait.PollingInterval = TimeSpan.FromSeconds(1);
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(cssSelector)));
			}
			catch (Exception e)
			{
				TestClass.test.Log(AventStack.ExtentReports.Status.Error, "Error occured while waiting for element");
				TestClass.test.Fail(e.Message);
				throw;
			}
		}

		protected void ClickButton(IWebElement button) 
		{
			string buttonText = button.Text;
			try
			{				
				Executor.ExecuteScript("document.getElementById('" + button.GetAttribute("id") + "').click()");
				TestClass.test.Log(AventStack.ExtentReports.Status.Info,"Button " + buttonText + " Clicked successfully");
			}
			catch (Exception e) 
			{
				TestClass.test.Log(AventStack.ExtentReports.Status.Error,"Error occured when clicking the button " + buttonText);
				TestClass.test.Fail(e.Message);
				throw e;
			}
		}

		public void SetTextElelmentText(IWebElement textElement, string text) 
		{
			if (textElement != null)
			{
				Executor.ExecuteScript("document.getElementById('" + textElement.GetAttribute("id") + "').value = \"" + text + "\";");
			}
		}

		public void ScrollToVisibilityOfElement(IWebElement elementToShow) 
		{
			try
			{
				Executor = ((IJavaScriptExecutor)Driver);
				String scrollElementIntoMiddle = "var viewPortHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);"
												+ "var elementTop = arguments[0].getBoundingClientRect().top;"
												+ "window.scrollBy(0, elementTop-(viewPortHeight/2));";
				Executor.ExecuteScript(scrollElementIntoMiddle, elementToShow);
			}
			catch (Exception e)
			{
				TestClass.test.Log(AventStack.ExtentReports.Status.Error, "Error occured while scrolling to an element");
				TestClass.test.Fail(e.Message);
				throw e;
			}
		}

		public void ClearText(IWebElement textBoxToBeCleared)  
		{
			if (textBoxToBeCleared != null) 
			{
				try
				{
					Executor.ExecuteScript("document.getElementById('" + textBoxToBeCleared.GetAttribute("id") + "').value = '';");
				}
				catch (Exception e)
				{
					TestClass.test.Log(AventStack.ExtentReports.Status.Error, "Error occured while clearing the text box");
					TestClass.test.Fail(e.Message);
					throw e;
				}
			}			
		}
	}
}