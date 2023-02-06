Feature: Elemets category

As a user 
I want to do some stuff through the Elements category
So I can see the results.

Background:
Given user navigate to the Elements category

@TextBoxSection
Scenario Outline: Submit user's personal info
  Given user is on the Text Box Section
  When user enter '<name>' , '<email>' , '<currentAddress>' and '<permanentAddress>'
  And click on the submit button
  Then just entered info '<name>' , '<email>' , '<currentAddress>' and '<permanentAddress>'

  Examples: 
  | name | email          | currentAddress   | permanentAddress |
  | Kate | kate1@mail.com | 123 Privet drive | 123 red street   |

@CheckBoxSection
Scenario: Selected folders are displayed
    Given user is on Check Box Section
	When  user unfolds the 'home' folder
	And user select 'desktop' folder without unfolding it
	And user unfolds 'documents' than 'workspace' folder and select 'angular' and 'veu' items
	And user unfolds 'office' folder and select one by one 'public' , 'private' , 'classified' and 'general' 
	And user unfolds 'downloads' folder and select whole folder
	Then result of selecting folders equals to ' You have selected : desktop notes commands angular veu office public private classified general downloads wordFile excelFile'


@WebTables
Scenario: Sorting Salaries by increasing
    Given user is on Web Tables Section
	When click on 'Salary' column 
	Then values are sorted by increasing in 'Salary' column

@WebTables
Scenario: Delete the second row from the table
    Given user is on Web Tables Section
	When click on Trash Can Icon in Action column in '2' row
	Then there are 2 records in the table 
    And  the 'Department' column does not have the 'Compliance' value

@ButtonsSection
Scenario Outline: Buttons are clicked by different ways
    Given user is on Button Section
	When click on '<button name>' 
	Then message:'<displayed message>' is displayed
Examples: 
| button name     | displayed message             |
| Double Click Me | You have done a double click  |
| Right Click Me  | You have done a right click   |
| Click Me        | You have done a dynamic click |