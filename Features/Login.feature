Feature: Login Functionality

  Scenario: Unsuccessful login using email
    Given I am on the login page
    When I login with invalid email "invalid@example.com" and password "wrongPassword"
    Then I should see the error message "Check your credentials. We couldn’t match your email, username, or password."

  Scenario: Successful login using email
    Given I am on the login page
    When I login with valid email "jovandvasiljevic@protonmail.com" and password "iconproject2024"
    Then I should be logged in successfully

  Scenario: Create a note and verify it after logout and login
    Given I am logged in with valid email "valid@example.com" and password "validPassword"
    When I create a note with title "Test Note" and content "This is a test note"
    And I logout
    And I login again with valid email "valid@example.com" and password "validPassword"
    Then I should see the note with title "Test Note" and content "This is a test note"