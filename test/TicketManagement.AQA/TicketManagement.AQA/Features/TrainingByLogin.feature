Feature: TrainingByLogin
	As a user
	I want to be able to login site
	So I can do it from login form

Scenario: Login to training.by is failed with wrong credentials
	Given User is on training.by
	When User clicks Login button
	And Enters "pollux094@gmail.com" to user name input
	And User clicks Continue button
	And Enters "asdsafasf" to password field
	And User clicks FinalLogin button
	Then Login form has error "Invalid email or password."

