@delete
Feature: Delete book

  Scenario: Delete book successfully
    Given there is a book named "Git Pocket Guide" in user book collection
      | UserId                               | Username    | Password    |
      | db8771b6-01b0-4496-8ffc-2255d63d1254 | Salmon1705@ | Salmon1705@ |
    And the user logs into the application
    And the user is on the Profile page
    When the user search book "Git Pocket Guide"
    And the user clicks on Delete icon
    And the user clicks on OK button
    And the user clicks on OK button of alert "Book deleted."
    Then the book is not shown
