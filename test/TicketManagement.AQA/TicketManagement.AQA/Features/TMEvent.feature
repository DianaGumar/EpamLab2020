@user_eventmanager_account_exist
Feature: TMEvent
	As a eventmanager
	I want to be able to manage events
	So I can do it after avtorization with eventmanager role

#Background:
#    Given User is on TM
#	And User registrate with email "emexample@gmail.com" and password "x6@9hkrmWZNjmzY34"
#	When User clicks Login button
#	And Enters user "emexample@gmail.com" into autorizeinput
#	And Enters "x6@9hkrmWZNjmzY34" to Password autorizefield
#	And User clicks FinalLogin button

#@tmevent @correct
#Scenario: Delete event is passed
#	Given User is on TM
#	When User clicks "delete" button on event with Name "Open Cinema" and Id "3"
#	Then User can't see event with Name "Open Cinema" and Id "3" at index page
#
#@tmevent @fail
#Scenario: Delete event is faled because event has busy seats
#	Given User is on TM
#	And event with Name "Big Music Event" and Id "2" has busy seat
#	When User clicks "delete" button on event with Name "Big Music Event" and Id "2"
#	Then User can see event with Name "Big Music Event" and Id "2" at index page

@tmevent @correct
Scenario: Details event is passed
	Given User is on TM
	When User clicks "details" button on event with Name "Big Music Event" and Id "2"
	Then User can see buyTicketButton

@tmevent @correct
Scenario: Set areas price with event is passed
	Given User is on TM
	When User clicks "setprice" button on event with Name "Big Music Event" and Id "2"
	And User set Price "4" at Price field
	And User clicks SetPrise button
	And User clicks BackToList button
	And User clicks "details" button on event with Name "Big Music Event" and Id "2"
	Then User can see middle price = "4.00"

#@tmevent @fail
#Scenario: Set areas price as free with event is passed but event are not availible
#	Given User is on TM
#	When User clicks "setprice" button on event with Name "Big Music Event" and Id "2"
#	And User set Price "0" at Price field
#	And User clicks SetPrise button
#	And User clicks BackToList button
#	Then User can't see event with Name "Big Music Event" and Id "2" at index page
#	And User clicks SeeAllEvents button
#	Then User can see event with Name "Big Music Event" and Id "2" at index page
#	And User take back event price