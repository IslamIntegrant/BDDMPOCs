using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DM_AP_POC.Pages
{
	class EditInteroberabilityProjectPage : PageBase
	{

		#region Elements
		[FindsBy(How = How.CssSelector, Using = "#alignmentProjectBDAlignItems > div > div.ui-table-wrapper.ng-star-inserted > table")]
		IWebElement alignItemsTable;

		[FindsBy(How = How.XPath, Using = "/html/body/app/div/alignment-project-details/div/div/div[3]/tabset/ul/li[2]/a/span")]
		public IWebElement alignmentTab;

		[FindsBy(How = How.Id, Using = "CancelDetailAlignmentProject")]
		public IWebElement backButton;

		[FindsBy(How = How.Id, Using = "applyChanges")]
		IWebElement applyButton;

		[FindsBy(How = How.ClassName, Using = "ng-tns-c18-25")]
		IWebElement excludeListItem;

		#region Manual align modal elements
		[FindsBy(How = How.Id, Using = "SearchIVEntryItem")]
		IWebElement manualAlignModalEmrSearchBox;

		[FindsBy(How = How.Id, Using = "newIvEntry")]
		IWebElement manualAlignModalCreateEmrRadioButton;

		[FindsBy(How = How.Id, Using = "IVDataCustomerNotes")]
		IWebElement EmrCustomerNotesTextBox;

		[FindsBy(How = How.Id, Using = "IVDataInternalNotes")]
		IWebElement EmrInternalNotesTextBox;

		[FindsBy(How = How.Id, Using = "SearchPumpDataEntryItem")]
		IWebElement manualAlignModalDersSearchBox;

		[FindsBy(How = How.Id, Using = "newPumpDataEntry")]
		IWebElement manualAlignModalCreateDersRadioButton;

		[FindsBy(How = How.Id, Using = "PumpDataCustomerNotes")]
		IWebElement pumpDataCustomerNotesTextBox;

		[FindsBy(How = How.Id, Using = "PumpDataInternalNotes")]
		IWebElement pumpDataInternalNotesTextBox;

		[FindsBy(How = How.Id, Using = "updateManualAlignButton")]
		IWebElement manualAlignModalSaveButton;

		[FindsBy(How = How.Id, Using = "cancelManualAlignButton")]
		IWebElement manualAlignModalCancelButton;

		[FindsBy(How = How.XPath, Using = "/html/body/app/div/alignment-project-details/div/div/div[3]/tabset/div/alignment-project-bd-align-tab/manual-align-dialog/modal[3]/div/div/div/div[2]/div/div[2]/div/button[2]")]
		IWebElement ConcMismatchMsgBoxContinueButton;

		[FindsBy(How = How.XPath, Using = "/html/body/app/div/alignment-project-details/div/div/div[3]/tabset/div/alignment-project-bd-align-tab/manual-align-dialog/modal[2]/div/div/div/div[2]/div/div[2]/div/button[2]")]
		IWebElement strengthMismatchMsgBoxContinueButton;

		[FindsBy(How = How.Id, Using = "newIVType")]
		IWebElement ivTypeDropdown;

		[FindsBy(How = How.Id, Using = "listItemnewIVType1")]
		IWebElement ivPrimaryType;

		[FindsBy(How = How.Id, Using = "listItemnewIVType2")]
		IWebElement ivDiluentType;

		[FindsBy(How = How.Id, Using = "newIVName")]
		IWebElement newIVNameTextBox;

		[FindsBy(How = How.Id, Using = "newIVAmt")]
		IWebElement newIVAmountTextBox;

		[FindsBy(How = How.Id, Using = "newIVUnits")]
		IWebElement newIVUnitsDropDown;

		[FindsBy(How = How.Id, Using = "listItemnewIVUnits0")]
		IWebElement mcgIVUnits;

		[FindsBy(How = How.XPath, Using = "/html/body/app/div/alignment-project-details/div/div/div[3]/tabset/div/alignment-project-bd-align-tab/manual-align-dialog/modal[1]/div/div/div/div[2]/div/div[1]/div[2]/div/div[2]/create-new-iv-data-item/div/form[1]/div/div[1]/div[5]/button/i")]
		IWebElement addIVComponentButton;

		[FindsBy(How = How.Id, Using = "newIVRoute")]
		IWebElement newIVRouteDropdown;

		[FindsBy(How = How.Id, Using = "listItemnewIVRoute0")]
		IWebElement IVRouteDropdownItem;

		[FindsBy(How = How.Id, Using = "Description")]
		IWebElement dersDescriptionTextBox;

		[FindsBy(How = How.Id, Using = "DrugAmount")]
		IWebElement dersAmountTextBox;

		[FindsBy(How = How.Id, Using = "DiluentVolume")]
		IWebElement DiluentVolumeTextBox;

		[FindsBy(How = How.XPath, Using = "/html/body/app/div/alignment-project-details/div/div/div[3]/tabset/ul/li[3]/a/span")]
		IWebElement acceptanceTabHeader;

		[FindsBy(How = How.Id, Using = "listItemnewIVUnits0")]
		IWebElement mcgEmrDataItemUnit;
		#endregion

		#endregion

		#region Declerations
		string manualAlignDersDataItemRadiosXPath = "//manual-align-dialog/modal/div/div/div/div[2]/div/div/div/div/div[3]/pump-data-item-search-create-new/div/div/div[2]/div//input[@type = 'radio']";
		string manualAlignEmrDataItemsRadiosXPath = "//manual-align-dialog/modal/div/div/div/div[2]/div/div/div[2]/div/div[3]/iv-data-item-search-create-new/div/div/div[2]/div//input[@type = 'radio']";
		#endregion

		public EditInteroberabilityProjectPage(IWebDriver driver) : base(driver)
		{
		}

		#region Actions

		// Generate random number
		public int generatRandomNumber(int minValue, int maxValue)
		{
			Random randomNumber = new Random();
			return randomNumber.Next(minValue, maxValue);
		}

		
		public void GotoAlignmentTab()
		{
			try
			{
				alignmentTab.Click();
				
			}
			catch (Exception e)
			{

				Console.WriteLine("Error occured when opening alignment tab header: " + e.Message);
				throw e;
			}
		}

		// This method has 2 usages 
		// 1- in the alignment tab it takes random selected pair then clicks on its web elements (etc actions drop down menu, check box or manual align) as required 
		// 2- In manual align modal it takes random number for selecting DERS Data item and EMR Data item for pairing them. 
		public void SetSelections(int pairIndex, string elementsXPath) 
		{	
			
				// Getting a list of all desired elements in the displayed page
				WaitForElementToExistByCSS("#alignmentProjectBDAlignItems > div > div.ui-table-wrapper.ng-star-inserted > table");
				IReadOnlyCollection<IWebElement> alignmentPairsElements = alignItemsTable.FindElements(By.XPath(elementsXPath));

				// Scrolling to the desired pair
				ScrollToVisibilityOfElement(alignmentPairsElements.ElementAt(pairIndex));
				Console.WriteLine("Scrolled the desired pair into the center of the screen");

			try
			{
				// Clicking on the desired pair element
				WaitForElementToBeClickable(GetAbsoluteXPath(alignmentPairsElements.ElementAt(pairIndex)));
			}
			catch (Exception)
			{

				
			}
				alignmentPairsElements.ElementAt(pairIndex).Click();
				Console.WriteLine("Successfully clicked in the desired pair element");
		}

		public void RandomlySelectManualAlignPairItems(bool canSelectDers, bool canSelectEMR) 
		{
			if (canSelectDers)
			{
				try
				{
					// Selecting random DERS Data item
					int DersDataItemsCount = Driver.FindElements(By.XPath(manualAlignDersDataItemRadiosXPath)).Count;
					SetSelections(generatRandomNumber(0, DersDataItemsCount -2), manualAlignDersDataItemRadiosXPath);  /*-2 Here to avoid selecting "Create new concentration option as it is out of scope of our test cases" and adjust the handling of pair index*/
					WaitForPageToBeReady();
					Console.WriteLine("Successfully selected random DERS Data item");
				}
				catch (Exception e)
				{

					Console.WriteLine("Error in selecting random DERS Data item: " + e.Message);
					throw e;
				}
			}
			if (canSelectEMR)
			{
				try
				{
					// Selecting random EMR Item
					int EmrDataItemsCount = Driver.FindElements(By.XPath(manualAlignEmrDataItemsRadiosXPath)).Count;
					if (EmrDataItemsCount >= 1)
					{
						SetSelections(generatRandomNumber(0, EmrDataItemsCount - 1), manualAlignEmrDataItemsRadiosXPath); // -1 Here to adjust the handling of the pair index
						WaitForPageToBeReady();
						Console.WriteLine("Successfully selected random EMR Item"); 
					}
					else
					{
						Console.WriteLine("No EMR Data items found.");
						return;
					}
				}
				catch (Exception e)
				{

					Console.WriteLine("Error in selecting random EMR Item: " + e.Message);
					throw e;
				}
			}
		}

		public void createNewEmrItem() 
		{
			WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			ClickButton(manualAlignModalCreateEmrRadioButton);
			try
			{
				ivTypeDropdown.Click();
				ivTypeDropdown.SendKeys("Primary");
				Console.WriteLine("Selected primary menu item");
			}
			catch (Exception e)
			{

				Console.WriteLine("Error in selecting primary menu item: " + e.Message);
				throw e;
			}
			newIVNameTextBox.SendKeys(generatRandomNumber(1, 999).ToString());
			newIVAmountTextBox.SendKeys(generatRandomNumber(1, 999).ToString());
			try
			{
				newIVUnitsDropDown.Click();
				newIVUnitsDropDown.Click();
				Console.WriteLine("Successfully selected primary component units");
			}
			catch (Exception e)
			{

				Console.WriteLine("Error in selecting primary component units: " + e.Message);
				throw e;
			}
			addIVComponentButton.Click();
			try
			{
				ivTypeDropdown.Click();
				ivTypeDropdown.SendKeys("Diluent");
				Console.WriteLine("Selected Diluent menu item");
			}
			catch (Exception e)
			{

				Console.WriteLine("Error in selecting Diluent menu item: " + e.Message);
				throw e;
			}
			newIVNameTextBox.SendKeys(generatRandomNumber(1, 999).ToString());
			newIVAmountTextBox.SendKeys(generatRandomNumber(1, 999).ToString());
			addIVComponentButton.Click();
			try
			{
				newIVRouteDropdown.Click();
				newIVRouteDropdown.SendKeys("push iv");
				Console.WriteLine("Successfully selected new EMR Data item route");
			}
			catch (Exception e)
			{

				Console.WriteLine("Error selecting new EMR Data item route: " + e.Message);
				throw e;
			}
		}

		public void createNewDERSItem()
		{
			ClickButton(manualAlignModalCreateDersRadioButton);
			WaitForPageToBeReady();
			try
			{
				dersDescriptionTextBox.SendKeys("Test" + generatRandomNumber(1, 999).ToString());
				dersAmountTextBox.SendKeys(generatRandomNumber(1, 999).ToString());
				DiluentVolumeTextBox.SendKeys(generatRandomNumber(1, 999).ToString());
				Console.WriteLine("Successfully entered new DERS item text data");
			}
			catch (Exception e)
			{

				Console.WriteLine("Error in entering new DERS item text data: " + e.Message);
				throw e;
			}
		}

		public void ApplyUpdateActionForSelectedPair(int pairIndex) 
		{
			// Sending the XPath of actions check boxes to the SetAction method 
			SetSelections(pairIndex, "//table/tbody/tr/td//action-dropdown//input[@type='text']");

			// Clicking the update action
			ClickButton(Driver.FindElement(By.XPath("(//table/tbody/tr/td//alignmentdropdown//a[@title='Update'])[" + pairIndex + "]")));

			// Clicking apply button
			ClickButton(applyButton);

			// Wait for data load
			WaitForPageToBeReady();
		}

		public void ApplyExcludeActionForSelectedPair(int pairIndex)
		{
			// Sending the XPath of actions drop down menus to the SetAction method 
			SetSelections(pairIndex, "//table/tbody/tr/td//action-dropdown//input[@type='text']");

			// Clicking the Exclude action
			ClickButton(Driver.FindElement(By.XPath("(//table/tbody/tr/td//alignmentdropdown//a[@title='Exclude'])[" + pairIndex +"]")));

			// Clicking apply button
			ClickButton(applyButton);

			// Wait for data load
			WaitForPageToBeReady();
		}

		public void ApplyManulAlignWithAlignAction(int pairIndex) 
		{
			// Sending the XPath of manual align links to the SetAction method 
			SetSelections(pairIndex, "//table/tbody/tr/td//edit-notes-manual-align-cell//a");

			// Wait for data load
			WaitForPageToBeReady();

			// Selecting pair items
			RandomlySelectManualAlignPairItems(true, true);

			// Saving
			ClickButton(manualAlignModalSaveButton);

			// Waiting for the loader
			WaitForPageToBeReady();

			// Clicking the continue button of strength mismatch message box if it is displayed			
			if (CheckIfElementExistByXPath("/html/body/app/div/alignment-project-details/div/div/div[3]/tabset/div/alignment-project-bd-align-tab/manual-align-dialog/modal[2]/div/div/div/div[2]/div/div[2]/div/button[2]") && strengthMismatchMsgBoxContinueButton.Displayed)
			{
				strengthMismatchMsgBoxContinueButton.Click();
				WaitForPageToBeReady();
			}

			// Clicking the continue button of concentration mismatch message box if it is displayed
			if (CheckIfElementExistByXPath("/html/body/app/div/alignment-project-details/div/div/div[3]/tabset/div/alignment-project-bd-align-tab/manual-align-dialog/modal[3]/div/div/div/div[2]/div/div[2]/div/button[2]") && ConcMismatchMsgBoxContinueButton.Displayed)
			{
				ConcMismatchMsgBoxContinueButton.Click();				
			}

			// Waiting for the loader
			WaitForPageToBeReady();
		}

		public void ApplyManulAlignWithUpdateActionWithNewEMR(int pairIndex)
		{
			// Sending the XPath of manual align links to the SetAction method 
			SetSelections(pairIndex, "//table/tbody/tr/td//edit-notes-manual-align-cell//a");

			// Wait for data load
			WaitForPageToBeReady();

			// Selecting pair items
			RandomlySelectManualAlignPairItems(true, false);

			// Creating new EMR Data item
			createNewEmrItem();

			// Saving
			ClickButton(manualAlignModalSaveButton);

			// Waiting for the loader
			WaitForPageToBeReady();

			// Clicking the continue button of strength mismatch message box if it is displayed
			if (strengthMismatchMsgBoxContinueButton.Displayed)
			{
				strengthMismatchMsgBoxContinueButton.Click();
				WaitForPageToBeReady();
			}

			// Clicking the continue button of concentration mismatch message box if it is displayed
			if (ConcMismatchMsgBoxContinueButton.Displayed)
			{
				ConcMismatchMsgBoxContinueButton.Click();				
			}

			// Waiting for the loader
			WaitForPageToBeReady();
		}

		public void ApplyManulAlignWithUpdateActionWithNewDERS(int pairIndex)
		{
			// Sending the XPath of manual align links to the SetAction method 
			SetSelections(pairIndex, "//table/tbody/tr/td//edit-notes-manual-align-cell//a");

			// Wait for data load
			WaitForPageToBeReady();

			// Selecting pair items
			RandomlySelectManualAlignPairItems(false, true);

			// Creating new DERS Data item
			createNewDERSItem();

			// Saving
			ClickButton(manualAlignModalSaveButton);

			// Waiting for the loader
			WaitForPageToBeReady();

			// Clicking the continue button of strength mismatch message box if it is displayed
			if (strengthMismatchMsgBoxContinueButton.Displayed)
			{
				strengthMismatchMsgBoxContinueButton.Click();
				WaitForPageToBeReady();
			}

			// Clicking the continue button of concentration mismatch message box if it is displayed
			if (ConcMismatchMsgBoxContinueButton.Displayed)
			{
				ConcMismatchMsgBoxContinueButton.Click();				
			}

    		// Waiting for the loader
			WaitForPageToBeReady();
		}

		// This method sets the number of displayed grid rows in the page to the maximum available number
		public void SetPaginationToMaximumLength() 
		{
			SetPaginationToMaxLength();
		}

		public void gotoAcceptanceTab() 
		{
			WaitForPageToBeReady();
			acceptanceTabHeader.Click();
			WaitForPageToBeReady();
		}

		// This method gets a pair with that matches the desired action
		public IWebElement GetPairWithAction(string pairAction)
		{
			IList<IWebElement> tableRows = Driver.FindElements(By.XPath("//table//tr"));

			// Removing the table header row
			int rowIndex = 1; // Used 1 to start iterating the tableRows list from the second row.
			
			// Finding pairs that match the action
			foreach (IWebElement element in tableRows)
			{
				IList<IWebElement> tdElements = tableRows.ElementAt(rowIndex).FindElements(By.TagName("td"));

				// Finding the 4th td element (The one for the action)
				IWebElement actionElement = tdElements[3].FindElements(By.ClassName("ng-star-inserted")).FirstOrDefault().FindElements(By.TagName("acceptance-action")).FirstOrDefault().FindElements(By.ClassName("padding-top-18")).FirstOrDefault().FindElements(By.ClassName("ng-star-inserted")).FirstOrDefault().FindElements(By.ClassName("ng-star-inserted")).FirstOrDefault().FindElement(By.TagName("div"));
				ScrollToVisibilityOfElement(actionElement);

				// Verifying the action element value
				string actionElementValue = actionElement.GetAttribute("innerHTML");

				if (actionElementValue.ToLower().Contains(pairAction.ToLower()))
				{
					return actionElement;
				}
				rowIndex++;
			}
			return null;
		}

		// This method is used to verify that the aligned pairs aligned in the test cases are sent to the acceptance tab
		public bool VerifyExistenceOfPairWithAction(string pairAction) 
		{
			if (GetPairWithAction(pairAction) != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
}