using System;

public class Class1
{
	public UnusecTestBaseMethods()
	{
		public static void WaitForLoaderByClass(string className)
		{
			WebDriverWait Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
			Wait.PollingInterval = TimeSpan.FromSeconds(1);
			Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.ClassName(className)));
		}

		public static void WaitForElementToBeClickableByClass(string className)
		{
			WebDriverWait Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
			Wait.PollingInterval = TimeSpan.FromSeconds(1);
			Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName(className)));
		}

		public static void WaitForElementToBeClickableByXPath(string XPath)
		{
			WebDriverWait Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
			Wait.PollingInterval = TimeSpan.FromSeconds(1);
			Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(XPath)));
		}

		public static void WaitForElementToBeClickableByID(string elementID)
		{
			WebDriverWait Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
			Wait.PollingInterval = TimeSpan.FromSeconds(1);
			Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(elementID)));
		}

		public static void WaitForPageToBeWithTitle(string pageTitle)
		{
			SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(pageTitle);
			//var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, new TimeSpan(0, 0, 30));
			//var element = wait.Until(ExpectedConditions.TitleContains(PageTitle));
		}
		public static void waitForTitleToContain(string urlPartialText)
		{
			SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(urlPartialText);
		}
		public static void WaitForElementToBeVisibleByID(string elementID)
		{
			SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(elementID));
		}

		public static void waitForGridLoading(IWebElement emptyGridRow, IWebElement gridPagingResult)
		{
			try
			{
				WebDriverWait Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
				Wait.PollingInterval = TimeSpan.FromSeconds(1);
				Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(emptyGridRow, "No data found"));
			}
			catch (WebDriverTimeoutException)
			{
				WebDriverWait Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
				Wait.PollingInterval = TimeSpan.FromSeconds(1);
				Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(gridPagingResult.GetAttribute("id"))));
			}
		}

		public static void WaitForElementToBeVisibleByClass(string className)
		{
			SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName(className));
		}

		public static void WaitForLoaderByID(string loaderID)
		{
			WebDriverWait Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
			Wait.PollingInterval = TimeSpan.FromSeconds(1);
			Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.Id(loaderID)));
		}

		public static void waitForPageLoad()
		{
			new WebDriverWait(driver, TimeSpan.FromSeconds(120)).Until(
			d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
		}
		public static void SwitchBetweenBrowserWindows(int index)
		{
			IReadOnlyCollection<string> WindowHandles = Driver.WindowHandles;
			if (WindowHandles.Count > 0)
			{
				Driver.SwitchTo().Window((String)WindowHandles.ToArray()[index]);
			}
		}
		public static DataTable DrawTable(string[] columnsNames, DataTable table)
		{
			if (columnsNames != null && columnsNames.GetLength(0) > 0)
			{
				for (int i = 0; i < columnsNames.GetLength(0); i++)
				{
					table.Columns.Add(columnsNames[i], typeof(string));
				}
			}
			return table;
		}


	}
}
