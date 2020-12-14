@registeruser_account_exist
Feature: Purchase
	As a register user
	I want to bye ticket
	So I can do it if has anouth money

@purchase @correct @layout_has_free_seats
Scenario: Autorized user buy ticked is passed
	Given User is on TM 
	And Autorized User has balance "1000"
	When User go to event description
	And User choose free seats
	And User buy tickets
	Then User can see "1" new seats at own purchase history
	And User return ticket

@purchase @fail @layout_has_free_seats
Scenario: Autorized user buy ticked is faled with not enought balance
	Given User is on TM 
	And Autorized User has balance "0"
	When User go to event description
	And User choose free seats
	And User buy tickets
	Then User can see error text "You has not enoth money. Top up balance"

@purchase @fail
Scenario: Autorized user buy ticked is faled with not seats choosen
	Given User is on TM 
	When User go to event description
	And User buy tickets
	Then User can see error text "Chouse seats"

@purchase @fail @layout_has_busy_seats
Scenario: Autorized user buy ticked is faled with busy seats
	Given User is on TM 
	When User go to event description
	And User choose busy seats
	And User buy tickets
	Then User can see error text "Seats olready chousen or not exist"