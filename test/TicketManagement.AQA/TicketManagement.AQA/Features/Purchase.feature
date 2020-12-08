Feature: Purchase
	As a register user
	I want to bye ticket
	So I can do it if has anouth money

@purchase @correct
Scenario: Autorized user bye ticked is passed
	Given User is on TM
	When User clicks "byeTicket" button on event with Name "Big Music Event" and Id "2"
	And User clicks ChooseSeats button
	And User enters in NumbersSeats field "51,52,57"
	And User clicks Bye button
	Then User can see seats "51,52,57" at own purchase history
