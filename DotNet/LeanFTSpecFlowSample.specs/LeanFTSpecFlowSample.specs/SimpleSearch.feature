Feature: Search
	In order to find relevant products
	As a Buyer
	I want to be able to search products available on the site

Scenario: Simple Search
	Given I am on home page
	When I search for "phones"
	Then I should see following results:
		| Products					 |
		| Madison Earbuds			 |
		| Madison Overear Headphones |