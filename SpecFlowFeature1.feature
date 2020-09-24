Feature: Test login feature of PayPal.

@mytag

Scenario: Invalid username and password
	Given user navigate to login page	
	When username "abc" is entered
	And password "abc" is entered
	Then invalid message should be prompted

Scenario: blank password
	Given user navigate to login page
	When username "abc" is entered
	And password "" is entered
	Then required message should be prompted

Scenario: invalid mobile number
	Given user navigate to login page
	When username "12345678" is entered
	Then invalid mobile number message should be prompted