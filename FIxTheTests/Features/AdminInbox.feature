Feature: AdminInbox
	As an admin user, I want to read messages customers have sent me 

Scenario: Read message sent via Contact Us form
	Given I have generated some valid contact us message details
	And I have entered and submitted the contact us details
	When I view the admin email inbox
	Then I can see the message in the list of unread messages
	When I open the message from the admin inbox
	Then the message window should appear containing the expected message details
