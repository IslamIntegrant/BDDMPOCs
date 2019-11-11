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
			//options.AddUserProfilePreference("profile.default.content_settings.popups", 0);
			//options.AddArgument("--disable-print-preview");
			return options;
		}

		public static InternetExplorerOptions IEOptions()
		{
			InternetExplorerOptions options = new InternetExplorerOptions();
			options.PageLoadStrategy = PageLoadStrategy.Normal;
			//options.AddUserProfilePreference("profile.default.content_settings.popups", 0);
			//options.AddArgument("--disable-print-preview");
			return options;
		}

		public static void initializeDriver()
		{
			if (BrowserName.ToLower().Equals("chrome"))
			{
				System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", Environment.CurrentDirectory + "\\chromedriver.exe");
				Driver = new ChromeDriver(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath.Replace("%20", " ").TrimEnd("DM_AP_POC.DLL".ToCharArray()), ChromeOption(), TimeSpan.FromSeconds(90));
				//Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
				Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
				//Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(5);
			}
			else if (BrowserName.ToLower().Equals("firefox"))
			{
				System.Environment.SetEnvironmentVariable("webdriver.gecko.driver", Environment.CurrentDirectory + "\\geckodriver.exe");
				Driver = new FirefoxDriver(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath.Replace("%20", " ").TrimEnd("DM_AP_POC.DLL".ToCharArray()), FirefoxOption(), TimeSpan.FromSeconds(90));
			}
			else if (BrowserName.ToLower().Equals("ie"))
			{
				System.Environment.SetEnvironmentVariable("webdriver.ie.driver", Environment.CurrentDirectory + "\\IEDriverServer.exe");
				Driver = new InternetExplorerDriver(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath.Replace("%20", " ").TrimEnd("DM_AP_POC.DLL".ToCharArray()), IEOptions(), TimeSpan.FromSeconds(90));
			}
			else if (BrowserName.ToLower().Equals("edge"))
			{
				System.Environment.SetEnvironmentVariable("webdriver.edge.drive", Environment.CurrentDirectory + "\\MicrosoftWebDriver.exe");
				Driver = new EdgeDriver(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath.Replace("%20", " ").TrimEnd("DM_AP_POC.DLL".ToCharArray()), EDGEOptions(), TimeSpan.FromSeconds(90));
			}
			else if (BrowserName.ToLower().Equals("chrome-headless"))
			{
				System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", Environment.CurrentDirectory + "\\chromedriver.exe");
				ChromeOptions options = new ChromeOptions();
				options.AddArgument("--headless");
				options.AddArgument("--window-size=1920,1080");
				Driver = new ChromeDriver(options);
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

		[OneTimeTearDown]
		public static void StopDriver()
		{
			Driver.Quit();
		}
	}
}