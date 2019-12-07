using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DM_AP_POC.Data;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;
using System.Reflection;

namespace DM_AP_POC.TCs
{
	//[SetUpFixture]
	public class TestBase
	{
		private static IWebDriver driver;
		private static ITestResult result;
		private static string browserName;
		protected ExtentReports _extent;
		protected ExtentTest _test;
		private SharedData data;


		public static IWebDriver Driver
		{
			get
			{
				return driver;
			}

			set
			{
				driver = value;
			}
		}

		public static ITestResult Result
		{
			get
			{
				return result;
			}

			set
			{
				result = value;
			}
		}

		public static string BrowserName
		{
			get
			{
				return browserName;
			}

			set
			{
				browserName = value;
			}
		}

		public TestBase() 
		{
			data = new SharedData();
	}
	
		public static FirefoxOptions FirefoxOption()
		{
			FirefoxOptions Options = new FirefoxOptions();
			Options.PageLoadStrategy = PageLoadStrategy.Normal;
			Options.SetPreference("browser.download.folderList", 2);
			Options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf");
			Options.SetPreference("browser.download.manager.showWhenStarting", false);
			Options.SetPreference("pdfjs.disabled", true);
			Options.AcceptInsecureCertificates = true;
			return Options;
		}

		public static ChromeOptions ChromeOption()
		{
			ChromeOptions options = new ChromeOptions();
			options.PageLoadStrategy = PageLoadStrategy.Normal;
			options.AddUserProfilePreference("profile.default.content_settings.popups", 0);
			options.AddArgument("--incognito");
			return options;
		}

		public static EdgeOptions EDGEOptions()
		{
			EdgeOptions options = new EdgeOptions();
			options.PageLoadStrategy = PageLoadStrategy.Normal;
			return options;
		}

		public static InternetExplorerOptions IEOptions()
		{
			InternetExplorerOptions options = new InternetExplorerOptions();
			options.PageLoadStrategy = PageLoadStrategy.Normal;
			return options;
		}

		public static void initializeDriver()
		{
			switch (browserName.ToLower())
			{
				case "chrome":
					System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", Environment.CurrentDirectory + "\\chromedriver.exe");
					Driver = new ChromeDriver(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath.Replace("%20", " ").TrimEnd("DM_AP_POC.DLL".ToCharArray()), ChromeOption(), TimeSpan.FromSeconds(90));
					//Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
					Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
					break;

				case "firefox":
					System.Environment.SetEnvironmentVariable("webdriver.gecko.driver", Environment.CurrentDirectory + "\\geckodriver.exe");
					Driver = new FirefoxDriver(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath.Replace("%20", " ").TrimEnd("DM_AP_POC.DLL".ToCharArray()), FirefoxOption(), TimeSpan.FromSeconds(90));
					break;

				case "ie":
					System.Environment.SetEnvironmentVariable("webdriver.ie.driver", Environment.CurrentDirectory + "\\IEDriverServer.exe");
					Driver = new InternetExplorerDriver(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath.Replace("%20", " ").TrimEnd("DM_AP_POC.DLL".ToCharArray()), IEOptions(), TimeSpan.FromSeconds(90));
					break;

				case "edge":
					System.Environment.SetEnvironmentVariable("webdriver.edge.drive", Environment.CurrentDirectory + "\\MicrosoftWebDriver.exe");
					Driver = new EdgeDriver(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath.Replace("%20", " ").TrimEnd("DM_AP_POC.DLL".ToCharArray()), EDGEOptions(), TimeSpan.FromSeconds(90));
					break;

				case "chrome-headless":
					System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", Environment.CurrentDirectory + "\\chromedriver.exe");
					ChromeOptions options = new ChromeOptions();
					options.AddArgument("--headless");
					options.AddArgument("--window-size=1920,1080");
					Driver = new ChromeDriver(options);
					break;

				default:
					break;
			}			
		}

		[OneTimeSetUp]
		public void StartDriver()
		{
			BrowserName = "chrome";
			initializeDriver();
			Driver.Manage().Cookies.DeleteAllCookies();
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(data.websiteURL[2]);
		}
		[OneTimeSetUp]
		public void ReportsSetup() 
		{
			var dir = TestContext.CurrentContext.TestDirectory + "\\";
			var fileName = this.GetType().ToString() + ".html";
			var htmlReporter = new ExtentHtmlReporter(dir + fileName);

			_extent = new ExtentReports();
			_extent.AttachReporter(htmlReporter);
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

			TestClass.test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
			TestClass.extent.Flush();
		}
		[OneTimeTearDown]
		public static void StopDriver()
		{
			Driver.Quit();
		}
	}
}