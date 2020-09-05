Feature: Trello_Website
	These are the API and UI scenario's
	Later on the tags will be used together with a setup and teardown.


@API
Scenario: Simple API Check
	# First check to see if the API gives an expected response with the use of credentials
	Given I send an API request to Trello for all boards
	Then It will find the board "KNAB_Assignment"

@UI
Scenario: Login Error at Trello
	# First check to see if the website can be reached and an incorrect login attempt can be done
	Given the website is opened
	When I enter the wrong login credentials
	Then it will show an error