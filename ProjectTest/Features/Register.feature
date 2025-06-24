@register
Feature: Register

  Scenario: Register a new student with all fields successfully
    Given the user is on Student Registration Form page
    When the user input valid data into fields
      | Field       | Value                        |
      | firstName   | John                         |
      | lastName    | Doe                          |
      | email       | john.doe@example.com         |
      | gender      | Male                         |
      | phone       |                   1234567890 |
      | dateOfBirth |                   1990-05-18 |
      | subjects    | Maths,Computer Science       |
      | hobbies     | Sports,Reading               |
      | picture     | TestData\\Images\\avatar.jpg |
      | address     |     123 Main St, Springfield |
      | state       | NCR                          |
      | city        | Delhi                        |
    And the user clicks on Submit button
    Then an successfully message "Thanks for submitting the form" is shown
    And all information of student form is shown

  Scenario: Register a new student with required fields successfully
    Given the user is on Student Registration Form page
    When the user input valid data into fields
      | Field       | Value      |
      | firstName   | John       |
      | lastName    | Doe        |
      | gender      | Female     |
      | phone       | 1234567890 |
      | dateOfBirth | 1990-05-18 |
    And the user clicks on Submit button
    Then an successfully message "Thanks for submitting the form" is shown
    And all information of student form is shown
