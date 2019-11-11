using System;

public class Class1
{
	public Class1()
	{
		public void LoseFocusOfTextElement(IWebElement textElement)
		{
			if (textElement != null)
			{
				textElement.SendKeys(Keys.Tab);
			}
		}

		public void ScrollToBottom()
		{
			Executor.ExecuteScript("scrollBy(0,2500)");
		}

		public void ScrollUp()
		{
			Executor.ExecuteScript("window.scrollBy(0,-2500)", "");
		}

		public void RefreshPage()
		{
			Executor.ExecuteScript("window.location.reload(true);");
		}

		public void OpenPageByURL(Uri url)
		{
			Executor.ExecuteScript("window.location.href = '" + url + "'");
		}

		public void OpenlastGridPage(double registeredItemsCount, double displayedItemsCount)
		{
			double result = registeredItemsCount / displayedItemsCount;
			int pageNO = (int)Math.Ceiling(result);
			IReadOnlyCollection<IWebElement> anchorItems = driver.FindElements(By.TagName("a"));
			foreach (IWebElement element in anchorItems)
			{
				if (element.GetAttribute("data-dt-idx") == pageNO.ToString())
				{
					element.Click();
					do
					{
						Thread.Sleep(1000);
					} while (!driver.FindElement(By.Id("ItemsGrid_next")).Enabled);
					return;
				}
			}
		}
	}
}
