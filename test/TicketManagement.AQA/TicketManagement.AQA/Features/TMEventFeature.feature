Feature: TMEventFeature
As a eventmanager
I want to be able to manage events
So I can do it after avtorization

Scenario: Delete event is passed
	Given User avtorized by eventmanager with email "emexample@gmail.com" and password "x6@9hkrmWZNjmzY34"
	When User clicks Delete button on event with Name "Open Cinema" and Id "3"
	Then User can't see event with Name "Open Cinema" and Id "3" at index page
	And User Logout

Scenario: Delete event is faled because event has busy seats
	Given User avtorized by eventmanager with email "emexample@gmail.com" and password "x6@9hkrmWZNjmzY34"
	And event with Name "Big Music Event" and Id "2" has busy seat
	When User clicks Delete button on event with Name "Big Music Event" and Id "2"
	Then User can see event with Name "Big Music Event" and Id "2" at index page
	And User Logout

Scenario: Details event is passed
	Given User avtorized by eventmanager with email "emexample@gmail.com" and password "x6@9hkrmWZNjmzY34"
	When User clicks Details button on event with Name "Big Music Event" and Id "2"
	Then User can see millde prise event with Name "Big Music Event" and Id "2" at index page
	And User Logout

Scenario: Set areas price with event is passed
	Given User avtorized by eventmanager with email "emexample@gmail.com" and password "x6@9hkrmWZNjmzY34"
	When User clicks SetPrice button on event with Name "Big Music Event" and Id "2"
	And User set Price "4.5" at Price field
	And User clicks SetPrise button
	And User clicks BackToList button
	And User clicks Details button on event with Name "Big Music Event" and Id "2"
	Then User can see middle price = "4.5"
	And User Logout

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