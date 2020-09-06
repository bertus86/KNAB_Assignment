Feature: Trello_API
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

	@API
Scenario: Simple API Check
	# First check to see if the API gives an expected response with the use of credentials
	Given I send an API request to Trello for all boards
	Then The API response will contain the following board: "KNAB_Assignment"
