Feature: TMRegistrate
	As a user
	I want to be able to use more site functions
	So I need user account

@registration @fail
Scenario: Registrate to TM as eventmanager is failed with not email
	Given User is on TM
	When User clicks Register button
	And Enters "x6@9hkrmWZNjmzY" to Password field
	And Enters "x6@9hkrmWZNjmzY" to ConfirmPassword field
	And User clicks FinalRegister button
	Then Register form has error "The Email field is required."

@registration @fail
Scenario: Registrate to TM as eventmanager is failed with not password
	Given User is on TM
	When User clicks Register button
	And Enters "aaatest@gmail.com" to user Email input
	And User clicks FinalRegister button
	Then Register form has error "The Password field is required."

@registration @fail
Scenario: Registrate to TM as eventmanager is failed with not valid email
	Given User is on TM
	When User clicks Register button
	And Enters "a@.gmail.com" to user Email input
	And Enters "x6@9hkrmWZNjmzY" to Password field
	And Enters "x6@9hkrmWZNjmzY" to ConfirmPassword field
	And User clicks FinalRegister button
	Then Register form has error "The Email field is not a valid e-mail address."

@registration @fail
Scenario: Registrate to TM as eventmanager is failed with do not same password and confirm password
	Given User is on TM
	When User clicks Register button
	And Enters "testeventmanager@gmail.com" to user Email input
	And Enters "x6@9hkrmWZNjmzY" to Password field
	And Enters "x6@9hkrmWZNjmzY111" to ConfirmPassword field
	And User clicks FinalRegister button
	Then Register form has error "The password and confirmation password do not match."

@registration @fail
Scenario: Registrate to TM as eventmanager is failed with too short password
	Given User is on TM
	When User clicks Register button
	And Enters "testeventmanager@gmail.com" to user Email input
	And Enters "@9hmW" to Password field
	And Enters "@9hmW" to ConfirmPassword field
	And User clicks FinalRegister button
	Then Register form has error "The Password must be at least 6 characters long."

@registration @fail
Scenario: Registrate to TM as eventmanager is failed with not valid password
	Given User is on TM
	When User clicks Register button
	And Enters "testeventmanager@gmail.com" to user Email input
	And Enters "1111111" to Password field
	And Enters "1111111" to ConfirmPassword field
	And User clicks FinalRegister button
	Then Register form has error "Passwords must have at least one non letter or digit character. Passwords must have at least one lowercase ('a'-'z'). Passwords must have at least one uppercase ('A'-'Z')."

#Scenario: Registrate to TM as eventmanager is failed with already taken email
#	Given User is on TM
#	When User clicks Register button
#	And Enters "testeventmanager@gmail.com" to user Email input
#	And Enters "asdsafasf" to Password field
#	And Enters "asdsafasf" to ConfirmPassword field
#	And User clicks FinalRegister button
#	Then Register form has error "Name lantan.mp4@gmail.com is already taken. Email 'lantan.mp4@gmail.com' is already taken."

@registration @correct
Scenario: Registrate to TM as eventmanager is passed
	Given User is on TM
	When User clicks Register button
	And Enters user Email into input
	And Enters "x6@9hkrmWZNjmzY22" to Password field
	And Enters "x6@9hkrmWZNjmzY22" to ConfirmPassword field
	And User clicks FinalRegister button
	Then User see profile link with hello text
	And User Logout

@registration @correct
Scenario: Registrate to TM as authorizeduser is passed
	Given User is on TM
	When User clicks Register button
	And Enters user Email into input
	And Enters "x6@9hkrmWZNjmzY111" to Password field
	And Enters "x6@9hkrmWZNjmzY111" to ConfirmPassword field
	And User shouse "authorizeduser" role at UserRole field
	And User clicks FinalRegister button
	Then User see profile link with hello text
	And User Logout

@registration @correct
Scenario: Registrate to TM as venuemanager is passed
	Given User is on TM
	When User clicks Register button
	And Enters user Email into input
	And Enters "x6@9hkrmWZNjmzY33" to Password field
	And Enters "x6@9hkrmWZNjmzY33" to ConfirmPassword field
	And User shouse "venuemanager" role at UserRole field
	And User clicks FinalRegister button
	Then User see profile link with hello text
	And User Logout

