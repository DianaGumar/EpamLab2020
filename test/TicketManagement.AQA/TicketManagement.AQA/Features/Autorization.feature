Feature: Autorization
	As a user
	I want to be able to use more site functions
	So I can see t after autorization

@autorization @correct @user_account_exist
Scenario: User autorization is passed
	Given User is on TM
	When User clicks Login button
	And Enters user "emexample@gmail.com" into autorizeinput
	And Enters "x6@9hkrmWZNjmzY34" to Password autorizefield
	And User clicks FinalLogin button
	Then User see profile link with hello text
	And User Logout

@autorization @fail
Scenario: User autorization is failed with not valid email
	Given User is on TM
	When User clicks Login button
	And Enters user "e@.gmail.com" into autorizeinput
	And Enters "x6@9hkrmWZNjmzY34" to Password autorizefield
	And User clicks FinalLogin button
	Then login email form has error "The Email field is not a valid e-mail address."

@autorization @fail
Scenario: User autorization is failed with empty email field
	Given User is on TM
	When User clicks Login button
	And Enters user "" into autorizeinput
	And Enters "x6@9hkrmWZNjmzY34" to Password autorizefield
	And User clicks FinalLogin button
	Then login email form has error "The Email field is required."

@autorization @fail
Scenario: User autorization is failed with empty password field
	Given User is on TM
	When User clicks Login button
	And Enters user "emexample@gmail.com" into autorizeinput
	And Enters "" to Password autorizefield
	And User clicks FinalLogin button
	Then login password form has error "The Password field is required."

@autorization @fail @user_account_exist
Scenario: User autorization is faled with wrong password
	Given User is on TM
	When User clicks Login button
	And Enters user "emexample@gmail.com" into autorizeinput
	And Enters "x6@9hkrmWZNjmzY34111111" to Password autorizefield
	And User clicks FinalLogin button
	Then login form has error "Invalid login attempt."