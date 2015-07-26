using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using HP.LFT.SDK;
using HP.LFT.SDK.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace LeanFTSpecFlowSample.specs
{
    [Binding]
    public class SearchSteps
    {
        //Browser instance
        IBrowser browser;

        [BeforeScenario]
        public void Setup()
        {
            //In Custom framework we need to initalize the LeanFT SDK
            SDK.Init(new SdkConfiguration());
            
            //Open Internet Explorer
            browser = BrowserFactory.Launch(BrowserType.InternetExplorer);
        }

        [Given(@"I am on home page")]
        public void GivenIAmOnHomePage()
        {
            //Navigate to the Application
            browser.Navigate("http://demo.magentocommerce.com/");
            browser.Sync();
        }

        [When(@"I search for ""(.*)""")]
        public void WhenISearchFor(string keyword)
        {
            //Get the Search field using Programmatic Descriptions
            IEditField searchField = browser.Describe<IEditField>(new EditFieldDescription
            {
                Type = @"search",
                TagName = @"INPUT",
                Name = @"q"
            });
            
            //Set the keyword
            searchField.SetValue(keyword);

            //Get the Search button and click on it - shortcut way
            browser.Describe<IButton>(new ButtonDescription
            {
                ButtonType = @"submit",
                TagName = @"BUTTON",
                Name = @"Search"
            }).Click();

            //Wait for Browser title to change
            browser.WaitUntil(b => b.Title.Equals("Search results for: '" + keyword + "'"));
        }

        [Then(@"I should see following results:")]
        public void ThenIShouldSeeFollowingResults(Table expectedProducts)
        {
            //Find all the product name elements using Programmatic Descriptions
            var products = browser.FindChildren<IWebElement>(
                new XPathDescription("//h2[@class='product-name']/a"));

            //Get the product name displayed on the page by reading InnerText property
            var productNames = products.Select(p => p.InnerText).ToList();

            //Get the expected product name list from Table
            List<string> expectedProductNames = new List<string>();
            foreach (TableRow row in expectedProducts.Rows) {
                expectedProductNames.Add(row["Products"]);
            }

            //Check the size
            Assert.AreEqual(expectedProductNames.Count, productNames.Count);

            //Check the expected and actual list
            CollectionAssert.AreEqual(expectedProductNames, productNames);
        }

        [AfterScenario]
        public void TearDown()
        {
            //Close the Browser
            browser.Close();
            SDK.Cleanup();
        }
    }
}
