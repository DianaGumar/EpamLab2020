Feature: TMRegistrate
	As a user
	I want to be able to use more site functions
	So I can do it from registrate and then login pages

	
Scenario: Registrate to TM as eventmanager is failed with not valid email
	Given User is on TM
	When User clicks Register button
	And Enters "testeventmanager@gmail.com" to user Email input
	And Enters "asdsafasf" to Password field
	And Enters "asdsafasf" to ConfirmPassword field
	And User shouse eventmanager role at UserRole field
	And User clicks FinalRegister button
	Then Register form has error "The Email field is not a valid e-mail address."

Scenario: Registrate to TM as eventmanager is failed with do not same password and confirm password
	Given User is on TM
	When User clicks Register button
	And Enters "testeventmanager@gmail.com" to user Email input
	And Enters "asdsafasf" to Password field
	And Enters "asdsafasf" to ConfirmPassword field
	And User shouse eventmanager role at UserRole field
	And User clicks FinalRegister button
	Then Register form has error "The password and confirmation password do not match."

Scenario: Registrate to TM as eventmanager is failed with too short password
	Given User is on TM
	When User clicks Register button
	And Enters "testeventmanager@gmail.com" to user Email input
	And Enters "asdsafasf" to Password field
	And Enters "asdsafasf" to ConfirmPassword field
	And User shouse eventmanager role at UserRole field
	And User clicks FinalRegister button
	Then Register form has error "The Password must be at least 6 characters long."

Scenario: Registrate to TM as eventmanager is failed with not valid password
	Given User is on TM
	When User clicks Register button
	And Enters "testeventmanager@gmail.com" to user Email input
	And Enters "asdsafasf" to Password field
	And Enters "asdsafasf" to ConfirmPassword field
	And User shouse eventmanager role at UserRole field
	And User clicks FinalRegister button
	Then Register form has error "Passwords must have at least one non letter or digit character. Passwords must have at least one lowercase ('a'-'z'). Passwords must have at least one uppercase ('A'-'Z')."

Scenario: Registrate to TM as eventmanager is failed with already taken email
	Given User is on TM
	When User clicks Register button
	And Enters "testeventmanager@gmail.com" to user Email input
	And Enters "asdsafasf" to Password field
	And Enters "asdsafasf" to ConfirmPassword field
	And User shouse eventmanager role at UserRole field
	And User clicks FinalRegister button
	Then Register form has error "Name lantan.mp4@gmail.com is already taken. Email 'lantan.mp4@gmail.com' is already taken."

Scenario: Registrate to TM as eventmanager is passed
	Given User is on TM
	When User clicks Register button
	And Enters "testeventmanager@gmail.com" to user Email input
	And Enters "asdsafasf" to Password field
	And Enters "asdsafasf" to ConfirmPassword field
	And User shouse eventmanager role at UserRole field
	And User clicks FinalRegister button
	Then User see start Page

Scenario: Registrate to TM as authorizeduser is passed
	Given User is on TM
	When User clicks Register button
	And Enters "testeventmanager@gmail.com" to user Email input
	And Enters "asdsafasf" to Password field
	And Enters "asdsafasf" to ConfirmPassword field
	And User shouse authorizeduser role at UserRole field
	And User clicks FinalRegister button
	Then User see start Page

Scenario: Registrate to TM as venuemanager is passed
	Given User is on TM
	When User clicks Register button
	And Enters "testeventmanager@gmail.com" to user Email input
	And Enters "asdsafasf" to Password field
	And Enters "asdsafasf" to ConfirmPassword field
	And User shouse venuemanager role at UserRole field
	And User clicks FinalRegister button
	Then User see start Page

