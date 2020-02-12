## Milestone planning details

| **Planning Detail Type** | Planning Detail Value |
| ------------------------ | --------------------- |
| **Version**              | v2.0                  |
| **Start Date**           | 4th February, 2020    |
| **Due Date**             | 13th February, 2020   |

## Issue transcripts

Following are all the issues that had work done in the milestone and transcripts for which are maintained here:

### Payroll administration in HRM Resources Control Panel - 370

| **Planning Detail Type** | Planning Detail Value                       |
| ------------------------ | ------------------------------------------- |
| **Labels**               | ~enhancement ~"module:hrm" ~"release:major" |
| **Start Date**           | 6 Feb, 2020                                 |
| **Due Date**             | 13 Feb, 2020                                |

#### Transcript

> As specified in [this proposal section](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/212#payroll-administration), we need to add payroll administration functionality in the employees listing page.
>
> ### Current proposal transcript
>
> Users can approve or recall monthly salary for a selection of employees here. This is done through a form which they can access by first selecting the employees they want to administer salaries for and then clicking the **ADMINISTER SALARIES** button. The form will ask users to first select the salary month before showing them the salary summaries of each of the selected employees and allowing them to approve or revoke their salaries.
>
> The form will throw an error if **Payroll Daily Hours** are not configured for any of the selected employees' attendance groups on the selected month. This error would also be a call to action for them to ensure the **Payroll Daily Hours** are configured.
>
> The form will only show users the NET and Gross salaries for the selected employees. If more details are needed, the user can easily click on the Name/Code for that employee and check their Salary Config tab.

### The weekends get counted toward the salary of an employee - 358

| **Planning Detail Type** | Planning Detail Value                            |
| ------------------------ | ------------------------------------------------ |
| **Labels**               | ~enhancement ~"module:hrm" ~bug ~"release:patch" |
| **Start Date**           | 4 Feb, 2020                                      |
| **Due Date**             | 13 Feb, 2020                                     |

#### Transcript

> ### Steps to reproduce
>
> 1. Mark all the days of the month as present.
> 2. Open Salary Config, check Monthly Salary Breakdown
>
> ### What is the current *bug* behavior?
>
> 1. The employee salary gets calculated for all of the days of the month included weekend.
>
> ### What is the expected *correct* behavior?
>
> 1. The employee salary should not include the weekend while calculating the salary.
> 2. This is how the gross salary should be calculated: **Gross salary** = (basic per hour salary \* no of worked hours + Allowances - Fines) - (basic per hour salary \* no of weekend hours)
>
> ### Test Cases:
>
> 1. The Gross Salary Calculation should be **Gross salary** = (basic per hour salary \* no of worked hours + Allowances - Fines) - (basic per hour salary \* no of weekend hours)
> 2. Adding fine should add a deductible amount equal to the fine amount to the gross salary.
> 3. Adding bonus should Add the allowance amount equal to bonus  amount to the gross salary.
> 4. Deleting fine should remove the deductible amount equal to the original fine amount from the gross salary.
> 5. Deleting bonus should remove the allowance amount equal to the original bonus amount from the gross salary.
> 6. If the weekend days are changed then the amount of the gross salary should be calculated according the specified weekends.
> 7. Marking Employee as absent should **reduce** the gross salary.
> 8. Marking Employee as present should **increase** the gross salary.
> 9. Monthly salary breakdown values for approved salary months of an employee must never change no matter what changes are made to related configurations (Payroll daily hours, active base salary) and data (attendance).
> 10. Revoked salaries' breakdown values should present it's values according to the current values of related configurations (payroll daily hours, active base salary) and data (attendance).
>
> ### Relevant logs and/or screenshots
>
> ![image](/uploads/7c0fa22d6bfcf876498b688178a44e5e/image.png)

#### Change history

Below is a list of all the changes made to the scope of this issue and the exact source where the change originated from:

1. Description was updated by Abdul Salam on 11 Feb, 2020, [here](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/358#note_286119687) in order to add test cases and clearer problem specification
2. Description was updated by Hamza on 12 Feb, 2020, [here](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/358#note_286862866) in order to fix faulty test case for gross salary calculation

### Reworked offices and departments configuration functionality - 357

| **Planning Detail Type** | Planning Detail Value                       |
| ------------------------ | ------------------------------------------- |
| **Labels**               | ~enhancement ~"module:hrm" ~"Release:Minor" |
| **Start Date**           | 4 Feb, 2020                                 |
| **Due Date**             | 13 Feb, 2020                                |

#### Transcript

> As specified in [this proposal section](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/212#offices-and-departments) we need to implement reworked configuration functionality for offices and departments.
>
> ### Current proposal transcript
>
> Administrators can add offices and since each office can have multiple departments, they can add a distinct set of departments for each office. When adding a new office, the administrator must provide the same office details as before, however, the form must also allow them to provide the list of departments for the office so it can all be added in one go.
>
> Once an office is created, they will still have the option to edit any existing office or add, remove, or edit their departments at any time.
>
> All offices will be displayed as a collapsible list, meaning that you can click on any office to expand it and see all of its departments.

### Changes to employee data structure - 351

| **Detail**     | value                                       |
| -------------- | ------------------------------------------- |
| **Labels**     | ~enhancement ~"module:hrm" ~"release:patch" |
| **Start Date** | 4 Feb, 2020                                 |
| **Due Date**   | 13 Feb, 2020                                |

#### Transcript

> Change to fields for employee details was requested by the HR department is service desk ticket #344. The ticket asks specifically for changes to the employee data structure as detailed by our support team as:
>
> 1. Make *Birth Place* field of employee details a dropdown. **[DEPRECATED]**
> 2. Make *Experience Year* and *Experience Month* fields of employee details into a numeric field instead of a dropdown.

#### Change history

Below is a list of all the changes made to the scope of this issue and the exact source where the change originated from:

1. Task scope was approved by Hamza to deprecate the *Birth Place* field change due to it's effects on employee imported data as described [here](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/351#note_286214352)

### Rework attendance groups and monthly payroll hours configuration functionality - 347

| **Planning Detail Type** | Planning Detail Value                         |
| ------------------------ | --------------------------------------------- |
| **Labels**               | ~"enhancement" ~"module:hrm" ~"release:major" |
| **Start Date**           | Feb 4, 2020                                   |
| **Due Date**             | Feb 13, 2020                                  |

#### Transcript

> As a result of reworks specified in [HRM Reworks Feature Proposal](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/212) to **Attendance Groups** and **Payroll Daily Hours**, we need to add a configuration panel for **Attendance Groups** and allow **Payroll Daily Hours** to be managed within the attendance group similar to what has been done for **Designations** and **Interview Technical Questions**
>
> ### Current proposal transcript
>
> Administrators can now configure attendance groups and their related payroll daily hours within a dedicated tab in this control panel. They can add new attendance groups by providing a name and a description for them. Once added, the new attendance group will be available in a collapsible list.
>
> Any existing attendance group can be expanded to view a Payroll Daily Hours toolbar and the list of existing payroll daily hours for that attendance group. The toolbar will allow the administrator to add new payroll daily hours. Since payroll daily hours are office based, the toolbar will have two buttons for two different methods of adding payroll daily hours:
>
> 1. Add for Selected Office - As the name suggests, this will allow them to add for a single, selected office.
> 2. Add for All Offices
>
> Once payroll daily hours are added, they can only be viewed and edited. No delete functionality will be provided for payroll daily hours or attendance groups


### Fines are also part of the gross salary calculation - 380

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~"bug" ~"confirmed" ~"critical" ~"module:hrm" ~"release:patch" |
| **Start Date** | Feb 11, 2020                                                 |
| **Due Date**   | Feb 13, 2020                                                 |

#### Transcript

> ### Reproduce steps
>
> 1. Go to the salary config details of an employee.
> 2. Select a month in which the employee has attendance.
> 3. Add Fines.
>
> ### What is the current bug behavior?
>
> 1. The fine does not affect the gross salary.
>
> ### What is the correct expected behavior?
>
> 1. The fine should also get calculated in the gross salary calculation. If there were any fines, it should be deducted from the gross salary calculation.
>
> ### Test Cases:
>
> 1. The Gross Salary Calculation should be **Gross salary** = (basic per hour salary \* no of worked hours + Allowances - Fines)
> 2. Adding fine should add a deductible amount equal to the fine amount to the gross salary.
> 3. Deleting fine should remove the deductible amount equal to the original fine amount from the gross salary.

#### Change history

Below is a list of all the changes made to the scope of this issue and the exact source where the change originated from:

1. Abdul Salam updated the issue description on Feb 11, 2020 [here](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/380#note_286141981) in order to clarify further the problem and add test cases.
2. Description was updated by Hamza on 12 Feb, 2020, [here](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/358#note_286862866) in order to fix faulty test case for gross salary calculation


### Getting error in income expense report - 372

| **Detail**     | Value                                                 |
| -------------- | ----------------------------------------------------- |
| **Labels**     | ~"bug" ~"confirmed" ~"module:finance ~"release:patch" |
| **Start Date** | Feb 10, 2020                                          |
| **Due Date**   | Feb 13, 2020                                          |

#### Transcript

> ### Reproduce steps 
>
> 1.  Opened the income expense report through financial reports.
> 2.  Clicked on view report.
>
> ### What is the current bug behavior?
>
> 1.  Clicked on view report of the income expense report it generates an error.
>
> ### What is the expected correct behavior?
>
> 1.  While viewing the income expense it should show all the reports of income expense report without getting any error.
>
> ### Relevant logs or/screenshots
>
> ![image](/uploads/06759be244665302469381ba97227131/image.png)

#### Change history

Below is a list of all the changes made to the scope of this issue and the exact source where the change originated from:

1. Sarwar updated the task description on 12th February, 2020, [here](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/372#note_286770585) due to missing information in the report.


### Getting error while selecting new candidate - 371

| **Detail**     | Value                                                 |
| -------------- | ----------------------------------------------------- |
| **Labels**     | ~"bug" ~"confirmed" ~"module:project ~"release:patch" |
| **Start Date** | Feb 10, 2020                                          |
| **Due Date**   | Feb 13, 2020                                          |

#### Transcript

> ### Reproduce steps
>
> 1.  Added new candidate.
> 2.  Fill out all the interview date and save it.
> 3.  Clicked on the select candidate
>
> ### What is the current bug behavior?
>
> 1.  While clicking on the select candidate it generates an error and it shows the employee candidate id (undefined-undefined)
> 2.  The candidate status is selected but while clicking on the candidate id link the candidate is not available in HR.
>
> ### What is the correct expected behavior?
>
> 1.  While selecting a new candidate it should not generate error.
> 1.  It should show the candidate id link and while clicking on the link it should open the clicked candidate details form.
>
> ### Relevant logs or/screenshots
>
> ![Humanitarian__16_](/uploads/7ed135995da51b8a1d84961f2a93faef/Humanitarian__16_.mp4)

### Add new file types for Project Proposals - 226

| **Detail**     | Value                          |
| -------------- | ------------------------------ |
| **Labels**     | ~"enhancement" ~module:project |
| **Start Date** | Feb 6, 2020                    |
| **Due Date**   | Feb 13, 2020                   |

#### Transcript

> End users had requested to add these two new file type options for Project Proposals:
>
> 1. Reporting
> 2. Contract

### Major design problem in employee attendance administration - 388

| **Planning Detail Type** | Planning Detail Value                                    |
| ------------------------ | -------------------------------------------------------- |
| **Labels**               | ~bug ~confirmed ~critical ~"module:hrm" ~"release:patch" |
| **Start Date**           | 12th February, 2020                                      |
| **Due Date**             | 13th February, 2020                                      |

#### Transcript

> ### Steps to reproduce
>
> 1. Select a number of employees in the HRM Resources Control Panel.
> 2. Click on **SET ATTENDANCE** and select a full month's worth of days in the calendar form.
>
> ### What is the current *bug* behavior?
>
> After selecting the days, I am presented with the Set Attendance form where all I can see are the days. I'm unable to scroll down to see the actual form controls.
>
> ### What is the desired *correct* behavior?
>
> Instead of showing a list of dates, it's going to be easier to read and manage if we just show the date range in bold text at the top of the form beneath where it says "Please submit the attendance for the selected employees". The date range can be displayed simply in the format of ***You are managing attendance for* [First Date] till [Last Date]**
>
> ### Relevant logs/screenshots
>
> ![image](/uploads/1be48bb61cc1cbe0b920fd1d0bba5259/image.png)

### Editing employee takes me back to employee listing page - 387

| **Planning Detail Type** | Planning Detail Value                          |
| ------------------------ | ---------------------------------------------- |
| **Labels**               | ~bug ~confirmed ~"module:hrm" ~"release:patch" |
| **Start Date**           | 12th February, 2020                            |
| **Due Date**             | 13th February, 2020                            |

#### Transcript

> ### Steps to reproduce
>
> 1. Enter the **EDIT DETAILS** form for an existing employee.
> 2. Make changes and save it.
>
> ### What is the current *bug* behavior?
>
> After the save operation is successful, I am redirected back to the employee listing page.
>
> ### What is the desired *correct* behavior?
>
> After save operation is successful, I am redirected back to the exact tab I was on in the employee details page before I clicked the **EDIT DETAILS** button.

### Reworked Vouchers Control Panel - 368

| **Planning Detail Type** | Planning Detail Value                           |
| ------------------------ | ----------------------------------------------- |
| **Labels**               | ~enhancement ~"module:finance" ~"release:major" |
| **Start Date**           | 4 Feb, 2020                                     |
| **Due Date**             | 13 Feb, 2020                                    |

#### Transcript

> Below is the transcript of the proposal taken from [wiki page](https://gitlab.com/edgsolutions-engineering/clear-fusion/-/wikis/Proposals/Reworked-Voucher-Control-Panel) at commit - ea958d5c
>
> ### Problems to solve
>
> Vouchers with large numbers of transactions take too long to load and this is further exacerbated by the fact that the typical end-user has access to very slow internet bandwidth.
>
> The vouchers management page currently uses old design patterns that have many navigation problems and uses outdated UI components. UI components that deal with loading or presenting data such as selection dropdowns have basic implementations that are not equipped to deal with loading and presenting very large numbers of entries at a time. These components have basic implementations like this in order to save time, however, it is becoming problematic now and they need to be enhanced.
>
> The overall UI design pattern is also outdated and does not match the pattern currently being used across the board for all new functionality.
>
> ### Intended users
>
> * Finance manager
> * Finance clerk
> * Finance administrator
>
> ### Further details
>
> #### Use cases
>
> 1. View all vouchers
> 2. Search and find vouchers
> 3. View all debits of a voucher
> 4. View all credits of a voucher
> 5. Add new voucher
> 6. Add debit to a voucher
> 7. Add credit to a voucher
> 8. Edit an existing voucher
> 9. Edit an existing voucher debit
> 10. Edit an existing voucher credit
> 11. Verify a voucher
> 12. Revoke voucher verification
> 13. Delete an existing voucher
> 14. Delete an existing voucher credit
> 15. Delete an existing voucher debit
> 16. Generate PDF export for a single voucher
> 17. Generate PDF export for a selection of vouchers
> 18. Add document to voucher
> 19. Remove a document from voucher
> 20. Download existing voucher document
>
> #### Goals
>
> 1. Reduce voucher transaction load times to less than 10 seconds regardless of the number of transactions within.
> 2. Bring the UI for voucher management functionality to match the standard UI design pattern currently being used across the application.
> 3. Provide a smooth data entry experience for vouchers and transactions.
>
> ### Proposal
>
> 1. A new control panel that replaces the current voucher listing page and adds some bulk operation functions. [Prototyped and specified here](#voucher-management-control-panel)
> 2. [Prototyped and specified here](#voucher-control-panel). The new control panel that replaces current voucher details view and adds functions for:
>    1. Editing.
>       Done through a separate form with specialized components optimized for searching and loading large lists of - projects, budget lines (cascaded), employees, accounts.
>    2. Verifying
>    3. Deleting
>    4. Creating PDF Exports
>    5. Managing transactions
>
> #### Voucher Management Control Panel
>
> ![voucher_cp_ui_components](uploads/504de97fa6a83d7e737b3fb8f4194f81/voucher_cp_ui_components.jpg)
>
> > Above is the current prototype for this control panel.
>
> New page with a paginated listing for vouchers. Listing can be filtered (filter fields to be specified). Users can add new single or bulk vouchers here and it will appear on the list. Voucher fields when adding new vouchers are going to be the same as they are now.
>
> Form for adding a new voucher navigates the user to page dedicated to the form. This is so that we can add an optimized load time in the middle. The dropdown UI components in the form will use autocomplete search dropdowns.
>
> **IMPORTANT:** Once the form is submitted, the user is navigated back to the voucher listing page with the newly added voucher highlighted and at the top of the voucher listing page.
>
> Users can then enter vouchers and perform operations specified in the Voucher Control Panel below.
>
> Voucher listing also allows users to select a set of vouchers (with select all also available) and perform operations - delete, verify, PDF Export - on all of them. These bulk operations can be done through popup menus/forms if additional information is needed for them.
>
> > Filters not applied by default. Once the filter is applied, it can be cleared too.
> > **NOTE** Operational voucher types such as Purchase Order vouchers cannot be deleted.
>
> Voucher listing only shows vouchers for offices that are selected through a control option provided in the toolbar of the control panel. They can also select multiple offices at a time.
>
> #### Voucher Control Panel
>
> ![voucher_details_cp_ui_components](uploads/3483c8f340ce319c6e71348051546f27/voucher_details_cp_ui_components.jpg)
>
> > Current prototype image of the voucher control panel. You get here by clicking on a voucher in the voucher listing within the voucher management control panel.
>
> ![purord_voucher_details_cp_ui_components](uploads/bcc5cf4afa53dbea9ff900c01eb3cda6/purord_voucher_details_cp_ui_components.jpg)
>
> > Current prototype image of the voucher control panel for a Purchase Order voucher. You get here by opening a voucher from the listing that is a Purchase Order voucher.
> > **NOTE** Purchase Order vouchers cannot be deleted.
>
> ![voucher_form_ui_components](uploads/6a54f1de9fd66074190ed6d3fd56c1d0/voucher_form_ui_components.jpg)
>
> > Current prototype image of the voucher form. You get here either by clicking the **ADD VOUCHER** button in the voucher management control panel or the **EDIT DETAILS** button in the voucher control panel.
>
> ![transactions_base_form_ui_components](uploads/24e8a4a8370d0026b4c517f661cf9492/transactions_base_form_ui_components.jpg)
>
> > Current prototype image of the voucher transactions form without form for adding new debit/credit. You get here by clicking the **MODIFY TRANSACTIONS** button in the voucher control panel.
>
> ![transactions_add_form_ui_components](uploads/5fb317cb8f5e4fe712e646cb07d50b72/transactions_add_form_ui_components.jpg)
>
> > Current prototype image of the voucher transactions form with the form for adding new debit/credit. You do this by clicking the **ADD DEBIT** or **ADD CREDIT** buttons next to the transactions section label.
>
> New page with control options and features for:
>
> 1. View voucher details. We could add a tab for this
>
> 2. View the current details of all transactions. Debits and credits must be visually separated and provide easily accessible buttons (check user requests about enhancement) for performing operations on them.
>
>    > View and Edit voucher transactions can no longer happen at the same place/page.
>
> 3. Delete a selection of transactions
>
> 4. Editing voucher transactions with dedicated form. This is for adding new transactions or removing existing ones. This is needed in order to do back-end verifications on the voucher update request.
>
> 5. Easily accessible/viewable voucher summary - balance, total debit, total credit.
>
> 6. Verify that voucher
>
> 7. Create PDF export for the voucher
>
> 8. Delete that voucher
>
> 9. Manage the voucher's documents
>
> ##### Transactions presentation improvement
>
> The list of transactions is now all displayed as a single list. Each entry in the list will either show a Credit amount or a Debit amount depending on what the user submitted through the form. This method of viewing transactions will be easily readable by users as well because their current management application displays voucher transactions in the same way.
>
> ##### Transaction form features and optimizations
>
> Buttons for Adding and Deleting transactions are available at the top and bottom of the transactions list to make it easy to access.
>
> New sub-form for adding transactions to the voucher that works mostly the same as it currently does except for the amount field. The current implementation uses different forms for credits and debits, but with this enhancement, users can enter debits or credits using the same form by either filling out a Credit Amount or Debit Amount for the transaction. Users can only enter a debit or a credit amount in any one transaction. They cannot enter both. This method of entering transactions is also the same as how the users enter transactions in their current management system.
>
> The form control for selecting transaction accounts, projects, and budget lines are autocomplete search dropdowns. The search dropdowns will not show any options until at least three (3) characters are entered into the search box. The dropdown options for Account, Project and Budget Line must show the Code and Name of the object, the same as it does in the current implementation. The search string (that user inputs) checks against the actual text of what appears in the dropdown options, the same as it does now.
>
> The *Total Credits* and *Total Debits* values in the voucher details section of this form will update as transactions are added/removed.
>
> Users can freely add and remove transactions but they will only get saved once they click the **SAVE** button at the main toolbar (at the top) of the form.
>
> ### Testing
>
> We must ensure that the following test cases pass in order to meet acceptance criteria:
>
> 1. Voucher transaction amount values must only accept a maximum of two decimal points.
> 2. Voucher transaction amount presentation components (lists and summaries) must only display a maximum of two decimal points.
> 3. Voucher Control Panel for a voucher that has more than 500 transactions must load in less than 5 seconds.
> 4. Voucher Transaction Form for a voucher that has more than 500 transactions must load in less than 5 seconds.
> 5. Voucher total debits and total credits summary values must only display a maximum of two decimal points.
> 6. Vouchers PDF export document format must match the format that must be provided by end-users as a pre-condition of approving this proposal.
> 7. Newly added vouchers appear at the top of the vouchers list and are highlighted.
> 8. In Voucher Management Control Panel if at least one voucher in your selection is a Purchase Order voucher, the delete function must throw an error saying that you cannot delete a Purchase Order voucher. The error must also show the exact codes of the Purchase Order vouchers in your selection.
> 9. Adding and removing transactions in the Voucher Transactions Modification form update the total credit and total debit values in the voucher details reactively (as and when transactions are added/removed)
> 10. Voucher listing search filter checks against the voucher code and description.
> 11. Voucher code string generation follows the same implementation as it currently does. 
> 12. The voucher form throws a warning if the exchange rate does not exist on the voucher date.
> 13. Voucher Control Panel (details view) shows error labels when the exchange rate does not exist for it's selected date. This is important because exchange rates can be deleted/changed.
> 14. Voucher transactions form throws an error if the total debits and total credits are not equal.
> 15. Full account codes and descriptions can be seen in the transaction listing.
> 16. Full account codes and descriptions can be seen in the account selection search dropdown in the new transaction form.
> 17. Full project codes and descriptions can be seen in the transaction listing.
> 18. Full project codes and descriptions can be seen in the project selection search dropdown in the new transaction form.
> 19. Full budget line codes and descriptions can be seen in the transaction listing.
> 20. Full budget line codes and descriptions can be seen in the budget line selection search dropdown in the new transaction form.
> 21. Full project job codes and descriptions can be seen in the transaction listing.

#### Change history

Below is a list of all the changes made to the scope of this issue and the exact source where the change originated from:

1. Hamza updated the proposal transcript in the description so as to bring in changes to the requirements that were approved by Finance department on 8 Feb, 2020, [here](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/368#note_284668531)

### Remove employee delete function - 366

| **Planning Detail Type** | Planning Detail Value                       |
| ------------------------ | ------------------------------------------- |
| **Labels**               | ~enhancement ~"module:hrm" ~"release:patch" |
| **Start Date**           | 4 Feb, 2020                                 |
| **Due Date**             | 13 Feb, 2020                                |

#### Transcript

> Users had requested through service desk issue #344 to remove the delete functionality for employees in all places. They only terminate or resign employees, never deleting them.

### Autofill new employee contracts - 364

| **Planning Detail Type** | Planning Detail Value                       |
| ------------------------ | ------------------------------------------- |
| **Labels**               | ~enhancement ~"module:hrm" ~"release:minor" |
| **Start Date**           | 4 Feb, 2020                                 |
| **Due Date**             | 13 Feb, 2020                                |

#### Transcript

> Users had requested through service desk issue #344 to enhance contract Add functionality so that it automatically fill out all the contract details.
>
> ### Related functionality and problems
>
> We have to consider that some of the fields in contracts might not actually be available in employee details at the time of adding a new contract. The following fields cannot be auto-filled since there is no reference point specified that the application can use to fetch that information for the employee's contract:
>
> 1. Contract start date
> 2. Contract end date
> 3. Contract duration
> 4. Project
> 5. Budget line
> 6. Job
> 7. Workday
> 8. Work time

#### Change history

Below is a list of all the changes made to the scope of this issue and the exact source where the change originated from:

1. Hamza updated the issue description so as to list out the exact contract fields for which we have no reference point specified to fetch the fields' data for on 12 Feb, 2020, [here](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/364#note_286923598)

### Dedicated audit log coverage and presentation feature in employee details - 356

| **Planning Detail Type** | Planning Detail Value                       |
| ------------------------ | ------------------------------------------- |
| **Labels**               | ~enhancement ~"module:hrm" ~"release:minor" |
| **Start Date**           | 4 Feb, 2020                                 |
| **Due Date**             | 13 Feb, 2020                                |

#### Transcript

> As specified in [this proposal section](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/212#audit-logs) and [this proposal section](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/212#audit-logging) we need to ensure all employee-related operations are audit logged and presented within a dedicated tab in Employee Control Panel (employee details page)
>
> ### Current proposal transcript
>
> #### Audit logging minor feature
>
> Our REST API backend will be upgraded with logging functionality that stores a record of every single operation/event that the API runs. Each logged audit event record must store:
>
> 1. The exact date and time of the event
> 2. The type of entity that the event is for (e.g Employee, Pension config, etc)
> 3. The entity id of the object on which the event took place
> 4. The user who initiated the event
> 5. The type of action that event was (e.g, DELETE, UPDATE, ADD, etc)
> 6. A description of the event that shows at least the value or state of the entity before and after it was changed
>
> #### Presentation feature
>
> Audit log events generated by the API when performing different actions on the employee can be viewed as a list in a dedicated **Audit Logs** tab.

#### Change history

Below is a list of all the changes made to the scope of this issue and the exact source where the change originated from:

1. Since we could not get 100% coverage for audit events to be logged for HRM, Hamza issued new task #390 to handle that in a later milestone. Done on 12 Feb, 2020, [here](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/356#note_286896036)

### Reworked analytical info report - 354

| **Planning Detail Type** | Planning Detail Value                       |
| ------------------------ | ------------------------------------------- |
| **Labels**               | ~enhancement ~"module:hrm" ~"release:minor" |
| **Start Date**           | 4 Feb, 2020                                 |
| **Due Date**             | 13 Feb, 2020                                |

#### Transcript

> Need to implement a reworked analytical info report as specified in [this proposal section](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/212#analytical-info-report). 
>
> ### Current proposal transcript
>
> All employees' are hired into the organization through **Project Hiring Requests** and they may receive portions (or all) of their monthly salary through project budgets from projects that they have been hired into.
>
> The analytical info report shows the exact percentages of an employee's salary that they receive from the project budgets that they were selected for as candidates. There is no data entry involved for analytical info in this control panel and this report is presented in a dedicated tab.

### Reworked appraisals feature - 353

| **Planning Detail Type** | Planning Detail Value                       |
| ------------------------ | ------------------------------------------- |
| **Labels**               | ~enhancement ~"module:hrm" ~"release:minor" |
| **Start Date**           | 4 Feb, 2020                                 |
| **Due Date**             | 13 Feb, 2020                                |

#### Transcript

> Need to implement a reworked appraisals feature as specified in [this proposal section](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/212#appraisals).
>
> ### Current proposal transcript
>
> Users can add appraisal applications for an employee by submitting an appraisal evaluation form. Appraisal applications are managed through a dedicated tab in this control panel. Once an appraisal application is successfully added, a user with necessary permissions can view the full evaluation form and choose to approve or reject it.
>
> > The appraisal evaluation form has many fields and functional components to it that are defined in the knowledgebase.
>
> Approval or rejection of an appraisal application has no functional effect on anything outside of the appraisal status itself.

### Search functionality is not working for user - 377

| **Planning Detail Type** | Planning Detail Value                         |
| ------------------------ | --------------------------------------------- |
| **Labels**               | ~enhancement ~"module:admin" ~"release:patch" |
| **Start Date**           | 8 Feb, 2020                                   |
| **Due Date**             | 13 Feb, 2020                                  |

#### Transcript

> ### Reproduce steps
>
> 1. Go to users
> 2. Search a username
>
> ### What is the current bug behavior?
>
> 1. When we search a user, the app is not showing anything.
>
> ### What is the correct expected behavior?
>
> 1. When a user is searched, it should give us the result according to the search.
>
> ### Testing Cases:
>
> 1. Users should be searched based on their names, last name, email, and status.
>
> ### Logs/Screenshots:
>
> ![image](/uploads/51b40be3bfa15ba7bef6be15e0b6b64b/image.png)
