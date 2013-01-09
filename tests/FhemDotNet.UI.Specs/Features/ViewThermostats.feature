@WebDriver
@IntegrationTests
Feature: View Thermostats
	In order to see the current system status
	As a user
	I want to be able to view statistics for all thermostats

Scenario: Navigate to homepage
	When I navigate to "/Home"
	Then I should be on the "thermostat list" page
	
Scenario: Viewing thermostats
	Given I am on the "thermostat list" page
	Then I should see a list of thermostats
		And thermostats have a name
		And thermostats have a current temperature
		And thermostats have a mode