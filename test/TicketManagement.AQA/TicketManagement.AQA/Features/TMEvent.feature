Feature: TMEvent
	As a eventmanager
	I want to be able to manage events
	So I can do it after avtorization with eventmanager role


Background:
    Given User is on TM
	And User has user account with email "emexample@gmail.com" and password "x6@9hkrmWZNjmzY34"
	When User clicks Login button
	And Enters user "emexample@gmail.com" into autorizeinput
	And Enters "x6@9hkrmWZNjmzY34" to Password autorizefield
	And User clicks FinalLogin button

@tmevent
Scenario: Delete event is passed
	#Given User avtorized by eventmanager with email "emexample@gmail.com" and password "x6@9hkrmWZNjmzY34"
	When User clicks Delete button on event with Name "Open Cinema" and Id "3"
	Then User can't see event with Name "Open Cinema" and Id "3" at index page
	And User Logout

@tmevent
Scenario: Delete event is faled because event has busy seats
	Given User avtorized by eventmanager with email "emexample@gmail.com" and password "x6@9hkrmWZNjmzY34"
	And event with Name "Big Music Event" and Id "2" has busy seat
	When User clicks Delete button on event with Name "Big Music Event" and Id "2"
	Then User can see event with Name "Big Music Event" and Id "2" at index page
	And User Logout

@tmevent
Scenario: Details event is passed
	Given User avtorized by eventmanager with email "emexample@gmail.com" and password "x6@9hkrmWZNjmzY34"
	When User clicks Details button on event with Name "Big Music Event" and Id "2"
	Then User can see millde prise event with Name "Big Music Event" and Id "2" at index page
	And User Logout

@tmevent
Scenario: Set areas price with event is passed
	Given User avtorized by eventmanager with email "emexample@gmail.com" and password "x6@9hkrmWZNjmzY34"
	When User clicks SetPrice button on event with Name "Big Music Event" and Id "2"
	And User set Price "4.5" at Price field
	And User clicks SetPrise button
	And User clicks BackToList button
	And User clicks Details button on event with Name "Big Music Event" and Id "2"
	Then User can see middle price = "4.5"
	And User Logout

@tmevent
Scenario: Set areas price as free with event is passed but event are not availible
	Given User avtorized by eventmanager with email "emexample@gmail.com" and password "x6@9hkrmWZNjmzY34"
	When User clicks SetPrice button on event with Name "Big Music Event" and Id "2"
	And User set Price "0" at Price field
	And User clicks SetPrise button
	And User clicks BackToList button
	Then User can't see event with Name "Big Music Event" and Id "2" at index page
	And User clicks SeeAllEvents button
	Then User can see event with Name "Big Music Event" and Id "2" at index page
	And User take back event price
	And User Logout

@tmevent
Scenario: No entered number to AreaPrise field with event is passed but event are not availible
	Given User avtorized by eventmanager with email "emexample@gmail.com" and password "x6@9hkrmWZNjmzY34"
	When User clicks SetPrice button on event with Name "Big Music Event" and Id "2"
	And User set Price "" at Price field
	And User clicks SetPrise button
	And User clicks BackToList button
	Then User can't see event with Name "Big Music Event" and Id "2" at index page
	And User clicks SeeAllEvents button
	Then User can see event with Name "Big Music Event" and Id "2" at index page
	And User take back event price
	And User Logout