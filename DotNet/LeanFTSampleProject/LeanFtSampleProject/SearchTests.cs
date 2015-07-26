using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HP.LFT.SDK;
using HP.LFT.SDK.Web;
using HP.LFT.Report;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace LeanFtSampleProject
{
    [TestClass]
    public class SearchTests : UnitTestClassBase<SearchTests>
    {
        IBrowser browser; //Browser instance
        MagentoCommerce app; //Application Model

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            GlobalSetup(context);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            //Open the Internet Exolorer Browser
            browser = BrowserFactory.Launch(BrowserType.InternetExplorer);
           
            //Navigate to the Application
            browser.Navigate("http://demo.magentocommerce.com/");
            browser.Sync();
            
            //Initialize the Application model
            app = new MagentoCommerce(browser);
        }

        [TestMethod]
        public void TestSimpleSearch()
        {
            try
            {
                //Expected list of product names should be displayed upon search for given keyword
                string[] expectedProductNames = { "Madison Earbuds", 
                                              "Madison Overear Headphones"};

                //Enter search keyword
                app.MadisonIslandPage.QEditField.SetValue("phones");

                //Click on Search button
                app.MadisonIslandPage.SearchButton.Click();

                //Wait for Browser title to change
                browser.WaitUntil(b => b.Title.Equals("Search results for: 'phones'"));

                //Find all the product name elements using Programmatic Descriptions
                var products = browser.FindChildren<IWebElement>(
                    new XPathDescription("//h2[@class='product-name']/a"));

                //Check the count of items
                Assert.AreEqual(expectedProductNames.Length, products.Length);

                //Get the product name displayed on the page by reading InnerText property
                var productNames = products.Select(p => p.InnerText).ToArray();

                //Check the product names
                CollectionAssert.AreEqual(expectedProductNames, productNames);
            }
            catch (AssertFailedException ex) 
            {
                //Add failure information in LeanFT Report
                Reporter.ReportEvent("Simple Search", "Failed during validation", Status.Failed, ex);
                throw;
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //Close the Browser
            browser.Close();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            GlobalTearDown();
        }
    }
}
