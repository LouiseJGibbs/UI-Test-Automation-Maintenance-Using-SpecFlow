
Feature: Contact Us
	I want to be able to contact the business

Scenario Outline: Complete Contact Us Form with valid settings
	When I submit the following contact details <name>, <email>, <phone>, <subject> and <message>
	Then I should be told that the form was submitted
Examples: 
 | name | email         | phone       | subject | message                                |
 | Test | test@test.com | 01234567890 | Testing | Hello World, can I book a room please? |

Scenario: Complete each line individually
	Given I have generated some valid contact us message details
	When I complete the name field in the contact us form
	And I complete the email field in the contact us form
	And I complete the phone field in the contact us form 
	And I complete the subject field in the contact us form
	And I complete the message field in the contact us form
	And I click the submit button in the contact us form
	Then I should be told that the form was submitted
