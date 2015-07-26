package me.unmesh.leanft.examples;

import static org.junit.Assert.*;
import static org.hamcrest.CoreMatchers.*;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;

import com.hp.lft.report.Reporter;
import com.hp.lft.report.Status;
import com.hp.lft.sdk.GeneralLeanFtException;
import com.hp.lft.sdk.WaitUntilTestObjectState;
import com.hp.lft.sdk.WaitUntilTestObjectState.WaitUntilEvaluator;
import com.hp.lft.sdk.web.*;

import unittesting.*;

/**
 * Sample test using LeanFT Java SDK
 * 
 * @author UNMESH
 *
 */
public class SearchTests extends UnitTestClassBase {

	// Browser instance
	Browser browser;

	@BeforeClass
	public static void beforeClass() throws Exception {
		globalSetup(SearchTests.class);
	}

	@AfterClass
	public static void afterClass() throws Exception {
		globalTearDown();
	}

	@Before
	public void setUp() throws Exception {
		// Open Internet Explorer Browser
		browser = BrowserFactory.launch(BrowserType.INTERNET_EXPLORER);

		// Navigate to the Application
		browser.navigate("http://demo.magentocommerce.com/");
		browser.sync();
	}

	@Test
	public void testSearch() throws Exception {
		try {

			// Expected product list
			List<String> expectedProductNames = Arrays.asList(
					"Madison Earbuds", "Madison Overear Headphones");

			// Get the Search Field using programmatic descriptions
			EditField searchField = browser.describe(
					EditField.class,
					new EditFieldDescription.Builder().type("search")
							.tagName("INPUT").name("q").build());

			// Set keyword value in Search Field
			searchField.setValue("phones");

			// Click on Search button - shortcut way
			browser.describe(
					Button.class,
					new ButtonDescription.Builder().buttonType("submit")
							.tagName("BUTTON").name("Search").build()).click();

			// Wait for Browser title to change
			WaitUntilTestObjectState.waitUntil(browser,
					new WaitUntilEvaluator<Browser>() {
						public boolean evaluate(Browser browser) {
							try {
								return browser.getTitle().equals(
										"Search results for: 'phones'");
							} catch (GeneralLeanFtException e) {
								return false;
							}
						}
					});

			// Get the list of product displayed on the page
			List<WebElement> products = Arrays.asList(browser.findChildren(
					WebElement.class, new WebElementDescription.Builder()
							.xpath("//h2[@class='product-name']/a").build()));

			// Get the list of product names
			List<String> productNames = new ArrayList<String>();
			for (WebElement product : products) {
				productNames.add(product.getInnerText());
			}

			// Check the size
			assertThat(productNames.size(), is(expectedProductNames.size()));

			// Check the list products are valid
			assertThat(productNames, is(expectedProductNames));

		} catch (Throwable ex) {
			// Report failure to LeanFT report
			Reporter.reportEvent("Simple Search", "Failed during validation",
					Status.Failed, ex);
			throw ex;
		}
	}

	@After
	public void tearDown() throws Exception {
		// Close the Browser
		browser.close();
	}

}
