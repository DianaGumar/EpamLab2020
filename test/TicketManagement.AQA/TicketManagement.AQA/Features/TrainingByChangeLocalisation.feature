Feature: TrainingByChangeLocalisation
	As a user
	I want to be able to change site language
	So I can do it through home page header

Scenario: Set localisation to Ukraine
	Given User is on training.by
	When User sets site language to "Українська"
	Then Banner text is "Jumpstart & Develop Your Career with Us"

Scenario: Set localisation to Russian
	Given User is on training.by
	When User sets site language to "Русский"
	Then Banner text is "Jumpstart & Develop Your Career with Us"

Scenario: Set localisation to English
	Given User is on training.by
	When User sets site language to "English"
	Then Banner text is "Jumpstart & Develop Your Career with Us"

