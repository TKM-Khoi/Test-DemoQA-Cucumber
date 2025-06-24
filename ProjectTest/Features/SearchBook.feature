@search
Feature: Search Book

  Scenario: Search book successfully
    Given there are books named "Learning JavaScript Design Patterns" and "Designing Evolvable Web APIs with ASP.NET"
    And the user is on the Book Store page
    When the user inputs book name "<search criteria>"
    Then all books match with input criteria will be displayed.

      Examples: 
      | search criteria |
      | Design          |
      | design          |