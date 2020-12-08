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

@tmevent @correct @delete_event
Scenario: Delete event is passed
	Given User is on TM
	When User clicks "delete" button on event with Name "Open Cinema" and Id "3"
	Then User can't see event with Name "Open Cinema" and Id "3" at index page

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

@tmevent @correct
Scenario: User edit event is passed
	Given User is on TM
	When User clicks "edit" button on event with Name "Big Music Event" and Id "2"
	And User set Description event "New test event desc"
	And User clicks FinalEdit button
	And User clicks BackToList button
	Then User can see event with Description "New test event desc" and Id "2" at index page

#@tmevent @fail @layout_busy_seats
#Scenario: User edit event layout is failed with busy seats
#	Given User is on TM
#	When User clicks "edit" button on event with Name "Big Music Event" and Id "2"
#	And User set Layout event "1"
#	And User clicks FinalEdit button
#	Then Event edit form has error "you has bought ticket on this layout"
#
#@tmevent @fail @layout_busy_seats
#Scenario: Delete event is faled because event has busy seats
#	Given User is on TM
#	And event with Name "Big Music Event" and Id "2" has busy seat
#	When User clicks "delete" button on event with Name "Big Music Event" and Id "2"
#	Then User can see event with Name "Big Music Event" and Id "2" at index page

@tmevent @fail
Scenario: User edit event is faled with past date
	Given User is on TM
	When User clicks "edit" button on event with Name "Big Music Event" and Id "2"
	And User set StartDate event "11/7/2020 1:25:00 AM"
	And User clicks FinalEdit button
	Then Event edit form has error "date is in a past"

@tmevent @fail
Scenario: User edit event is faled with end date before start
	Given User is on TM
	When User clicks "edit" button on event with Name "Big Music Event" and Id "2"
	And User set StartDate event "11/7/2021 1:25:00 AM"
	And User set EndDate event "10/7/2021 1:25:00 AM"
	And User clicks FinalEdit button
	Then Event edit form has error "end date before start date"

@tmevent @fail @exist_same_event
Scenario: User edit event is faled with busy date at this layout
	Given User is on TM
	When User clicks "edit" button on event with Name "Big Music Event" and Id "2"
	And User set StartDate event "12/7/2021 1:25:00 AM"
	And User set EndDate event "12/9/2021 1:25:00 AM"
	And User clicks FinalEdit button
	Then Event edit form has error "this venue is busy with another event at this time"

@tmevent @correct 
Scenario: User create event is passed
	Given User is on TM
	When User clicks CreateNew button
	And User set "Name" field "Test create event" in create event form
	And User set "Desc" field "Test create event desc" in create event form
	And User select from dropDown "1" layoutId
	And User set datetime "StartEvent" field date "12/7/2022" time "12:50AM" in create event form
	And User set datetime "EndEvent" field date "12/9/2022" time "12:50AM" in create event form
	And User clicks FinalCreate button
	And User set Price "4" at all Price fields
	And User clicks BackToList button
	Then User can see event with Name "Test create event" at index page
	And User clicks "delete" button on event with Name "Test create event"