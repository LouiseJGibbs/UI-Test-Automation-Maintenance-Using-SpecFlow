Feature: RoomBooking_BasicAsserts
 - A room booking form appears when the 'book this room' button is clicked for a particular room
 - When the user submits valid details, including a valid date range, then the room booking can be submitted
 
Background: 
Given I have generated some valid Room Booking details

Scenario: 01 Book a room with valid details
	Given I have loaded the room booking form for the first available room
	When I complete the room booking form for the selected room
	And I select the first available week for the selected room
	And I submit the room booking form
	Then I should see the successful booking message with the expected header
	And I should see the successful booking message with the expected body text

Scenario: 02 Attempt to book a room with missing information
	Given I have generated a blank <field> for the room booking form
	And I have loaded the room booking form for the first available room
	When I complete the room booking form for the selected room
	And I select the first available week for the selected room
	And I submit the room booking form
	Then I should see the warning message <message> under the room booking form

Examples: 
| field        | message                       |
| First Name   | Firstname should not be blank |
| Last Name    | Lastname should not be blank  |
| Email		   | must not be empty             |
| Phone Number | must not be empty             |      

Scenario: 03 Attempt to book a room with a value that is too short
	Given I have generated a <field> value that is too short for the room booking form
	And I have loaded the room booking form for the first available room
	When I complete the room booking form for the selected room
	And I select the first available week for the selected room
	And I submit the room booking form
	Then I should see the warning message <message> under the room booking form

Examples: 
| field      | message                       |
| First Name | size must be between 3 and 18 |
| Last Name  | size must be between 3 and 30 |
| Phone Number | size must be between 11 and 21 |

Scenario: 04 Attempt to book a room with an invalid email
	Given I have generated an invalid email address
	And I have loaded the room booking form for the first available room
	When I complete the room booking form for the selected room
	And I select the first available week for the selected room
	And I submit the room booking form
	Then I should see the warning message must be a well-formed email address under the room booking form

Scenario: 05 Attempt to book a room without selecting a date range
	Given I have loaded the room booking form for the first available room
	When I complete the room booking form for the selected room
	And I submit the room booking form
	Then I should see the warning message must not be null under the room booking form

