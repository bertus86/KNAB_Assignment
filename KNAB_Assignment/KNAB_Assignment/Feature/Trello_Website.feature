﻿Feature: Trello_Website
	These are the API and UI scenario's
	Later on the tags will be used together with a setup and teardown.


@smoke @Web
Scenario: Login Error at Trello
	# First check to see if the website can be reached and an incorrect login attempt can be done
	Given the website is opened
	When I enter the wrong login credentials
	Then it will show an error

@smoke @Web
Scenario: Succesfull Login at Trello
	# First check to see if the website can be reached and an incorrect login attempt can be done
	Given the website is opened
	When I enter the login credentials
	Then the user is logged in succesfully

@Web @UI  
Scenario: Creating and closing a new board
	# Creating the board and closing it
	Given the user is logged in
	When I create a board named "Test" 
	Then The board with name "Test" is created
	When I close and remove board with the name "Test" 
	Then the board with title "Test" is not visible in the boardspage


