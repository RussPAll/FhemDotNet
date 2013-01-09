@WebDriver
@IntegrationTests
Feature: Configure Thermostats
	In order to change the system state
	As a user
	I want to be able to configure thermostat behaviour

Scenario: Thermostats allow temperature to be set
	Given I am on the "thermostat list" page
	Then I should see a list of thermostats
		And I can input a desired temperature "5"
		And I can submit the page
		And I should be on the "thermostat list" page

Scenario: Thermostats allow mode to be set to manual
	Given I am on the "thermostat list" page
	Then I should see a list of thermostats
		And I can set the thermostat mode to "auto"
		And I can submit the page
		And I can set the thermostat mode to "manual"
		And I can submit the page
		And I should be on the "thermostat list" page

Scenario: Changes are reflected in the UI
	Given I am on the "thermostat list" page
	When I have set the thermostat "Bathroom" to temperature "5"
		And I have submitted the page
	Then the "Bathroom" thermostat should show a pending desired temperature "5"
#		And the device should show the existing state
#		And the existing state is not the same as the requested state