Feature: Trello_Website
	These are the API and UI scenario's
	Later on the tags will be used together with a setup and teardown.


@API
Scenario: Simple API Check
	Given I send an API request to Trello for all boards
	Then It will find the board "KNAB_Assignment"