Feature: RoomBookingBasicAsserts,
 - A room booking form appears when the 'book this room' button is clicked for a particular room
 - When the user submits valid details, including a valid date range, then the room booking can be submitted
 

Scenario: 01 Submit form with no details
	Given I have loaded the room booking form for the first available room
	When I submit the room booking form
	Then I should see at least 1 warning messages underneath the room booking form

Scenario: 02 Submit form with no details
	Given I have loaded the room booking form for the first available room
	When I submit the room booking form
	Then I should not see 9 warning messages underneath the room booking form

Scenario: 03 Submit form with no details 
	Given I have loaded the room booking form for the first available room
	When I submit the room booking form
	Then I should see 9 warning messages underneath the room booking form

Scenario: 04 Submit form with no details
	Given I have loaded the room booking form for the first available room
	When I submit the room booking form
	Then I should see less than 13 warning messages underneath the room booking form

Scenario: 05 Submit form with no date range
	Given I have loaded the room booking form for the first available room
	And I have generated some valid Room Booking details
	When I complete the room booking form for the selected room
	And I submit the room booking form
	Then the first message that appears under the room booking form should be 'must not be null'

Scenario: 06 Submit form with no date range
	Given I have loaded the room booking form for the first available room
	And I have generated some valid Room Booking details
	When I complete the room booking form for the selected room
	And I submit the room booking form
	Then the first message that appears under the room booking form should not be 'successful'

Scenario: 07 Submit form with no date range
	Given I have loaded the room booking form for the first available room
	And I have generated some valid Room Booking details
	When I complete the room booking form for the selected room
	And I submit the room booking form
	Then the first message that appears under the room booking form should contain 'must'

Scenario: 08 Submit form with no date range
	Given I have loaded the room booking form for the first available room
	And I have generated some valid Room Booking details
	When I complete the room booking form for the selected room
	And I submit the room booking form
	Then the first message that appears under the room booking form should not contain 'success'

Scenario: 09 Check 'Book' button exists after loading a room booking form
	Given I have loaded the room booking form for the first available room
	Then I should see the 'book' button

Scenario: 10 Check the 'Book' button is not visible before loading the room booking form
	Given I have generated some valid Room Booking details
	Then I should not see the 'book' button

