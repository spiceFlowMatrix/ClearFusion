## Original planning details

These are all the planning details at the start date of the milestone.

| Planning Detail Type | Planning Detail Value |
| -------------------- | --------------------- |
| **Version**          | v4.0                  |
| **Start Date**       | 2th March, 2020       |
| **Due Date**         | 16th March, 2020      |

## Closing planning details

These are all the planning related details at the end of the milestone when it was completed and delivered.

| Planning Detail Type | Planning Detail Value                                        |
| -------------------- | ------------------------------------------------------------ |
| **Version**          | v4.0                                                         |
| **Start Date**       | 2th March, 2020                                              |
| **Due Date**         | 17th March, 2020                                             |
| **Change history**   | [See this section of report](#issues-manifest-history) for detailed log of issue tracking throughout the milestone |



## Planned issues/tasks

These are all the issues/tasks that were planned for delivery at the beginning/start date of the release milestone:

### Enhancement request to supplier offer grid field addition - 434

| **Detail**     | Value                                              |
| -------------- | -------------------------------------------------- |
| **Labels**     | ~enhancement ~"module: project" ~"release:[minor]" |
| **Start Date** | Mar 03, 2020                                       |
| **Due Date**   | Mar 16, 2020                                       |

#### Transcript

> ### Current implementation
>
> Currently, After adding the supplier it shows the supplier and item in the supplier grid.
>
> ### Requested changes/Enhancement
>
> End-user had requested in [service desk](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/234) that the quantity and total amount should be added in the supplier grid.
>
> 1. Quantity
> 2. Total amount

### Enhancement request to submit a bid tender form - 433

| **Detail**     | Value                                              |
| -------------- | -------------------------------------------------- |
| **Labels**     | ~enhancement ~"module: project" ~"release:[patch]" |
| **Start Date** | Mar 03, 2020                                       |
| **Due Date**   | Mar 16, 2020                                       |

#### Transcript

> ### Current implementation
>
> Currently, In the submit a bit form, there is security date.
>
> ### Requested changes/Enhancement
>
> End-user had requested through [service](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/234) desk that the Security Date **label** should be changed to say **Delivery Date**.

### Enhancement to the Payroll Salary - 429

| **Detail**     | Value                                         |
| -------------- | --------------------------------------------- |
| **Labels**     | ~enhancement ~"module:hrm" ~"release:[minor]" |
| **Start Date** | Mar 03, 2020                                  |
| **Due Date**   | Mar 16, 2020                                  |

#### Transcript

> ### Current implementation
>
> According to the [service desk received](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/367#note_294036007). We couldn't do these in the [previous task](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/415), so I am adding it as an enhancement to be implemented.
>
> ### Requested changes/Enhancement
>
> 1. If no employee is selected then the payroll should be exported for all employees according to the filters which are applied.
> 2. The end-user should be able to export the payroll of multiple offices at the once.
> 3. There should be a selection of the month for which they want the payroll export. It can multiple selections. The exported payroll will be for selected months.
> 4. Payroll should be project base as well. [For this I think we can consider a filtering base on the project, so they can filter the employees on project and export the payroll]
> 5. The payment date will be changed to Payroll Month, so only the month which the payroll is exported for will be shown there.
> 6. Another column will be added to the payroll format called: "Payroll Month", As stated above the payroll can be exported for multiple months, so the month which the salary is for will be mentioned there.
> 7. Field descriptions: 
>    1.  **Gross Salary** = Total Gross Salary * Percentage / 100
> 8. Attached is a sample of exported payroll format. [Payroll_format__2_.xlsx](https://gitlab.com/edgsolutions-engineering/clear-fusion/uploads/2a025390af5f8f4d50e0ac5ff93543c1/Payroll_format__2_.xlsx) 

### Wrong Calculation for Salary, and Gross Columns in the Excel Payroll Export - 428

| **Detail**     | Value                                                 |
| -------------- | ----------------------------------------------------- |
| **Labels**     | ~"bug" ~ "confirmed" ~"module:hrm" ~"release:[patch]" |
| **Start Date** | Mar 03, 2020                                          |
| **Due Date**   | Mar 16, 2020                                          |

#### Transcript

> ### Reproduce steps
>
> 1. Open RM resources control panel
> 2. Select employees
> 3. Export payroll
>
> ###  What is the current bug behavior?
>
> Right now it shows the wrong calculation for the salary and the gross salary
>
> ###  What is the correct expected behavior?
>
> The calculation should be as follow: **Salary** = BasicPay - [(atd * Active Hourly Rate) * Percentage / 100]

### Enhancement to the debit and credit columns order - 427

| **Detail**     | Value                                                 |
| -------------- | ----------------------------------------------------- |
| **Labels**     | ~ "enhancement" ~"module: finance" ~"release:[minor]" |
| **Start Date** | Mar 03, 2020                                          |
| **Due Date**   | Mar 16, 2020                                          |

#### Transcript

> ### Current Implementation
>
> Currently, in the Voucher Control Panel, Voucher Details Control Panel, and Exported PDF of the voucher, the column of the credit is before the debit.
>
> ###  Requested Changes/Enhancement
>
> According to the request in the [Service Desk](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/395), In all of the pages and PDF export format, the **Debit amount** should be shown **before Credit**, because this is standard practice.

### The formatting of the voucher pdf export is too bold - 425

| **Detail**     | Value                                                 |
| -------------- | ----------------------------------------------------- |
| **Labels**     | ~ "enhancement" ~"module: finance" ~"release:[minor]" |
| **Start Date** | Mar 03, 2020                                          |
| **Due Date**   | Mar 16, 2020                                          |

#### Transcript

> ### Current Implementation
>
> In the current implementation, the size of the font is big, and the border is thick.
>
> ###  Requested Changes/Enhancement
>
> As end-user requested through the [Service Desk](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/340) and during [the meeting](https://drive.google.com/open?id=1kLcWd2TtZy-1Lo-xL1hPzpidll3_LeS0) the formatting of the voucher pdf export needs changes.
>
> 1. The font size needs to be reduced to 9.
> 2. Only the header should be bold, the rest of the text should be normal formatting.
> 3. The top header (COORDINATION OF HUMANITARIAN ASSISTANCE) size should be 14 and bolded.
> 4. The top header (TRANSACTION VOUCHER) size should be 13 and bold.
> 5. The border needs to be a normal border, **NOT THICK**.
> 6. Increase the margin of the exported format so that more transactions fit in one page.
> 7. If we export multiple vouchers, between each voucher a page break should be inserted. If one voucher ends, the other voucher should be started on a new page.

### Cannot upload any files to the proposal in project details control panel - 420

| **Detail**     | Value                                                       |
| -------------- | ----------------------------------------------------------- |
| **Labels**     | ~ "bug" ~"confirmed" ~"module:[project]" ~"release:[patch]" |
| **Start Date** | Mar 03, 2020                                                |
| **Due Date**   | Mar 16, 2020                                                |

#### Transcript

> This bug has been reported through [the service desk](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/419).
>
> ###  Prerequisites (if any)
>
> ###  Steps to reproduce
>
> **Note: Please don't upload anything to the production system of the end-user. You can check out the video attached**
>
> 1. Go to manage.cha-net.org
> 2. Open a project
> 3. Go to Proposal
> 4. Upload a file to the proposal
>
> ###  What is the current *bug* behavior?
>
> When we upload any file to the proposal of the production system of the end-user, it throws an error: "[Obejct Object]," and the file doesn't get uploaded.
>
> ###  What is the expected *correct* behavior?
>
> When we upload the file to the proposal, it should not throw any error and get uploaded.

### Enhancement to transactions - 416

| **Detail**     | Value                                                  |
| -------------- | ------------------------------------------------------ |
| **Labels**     | ~ "enhancement" ~"module:[finance]" ~"release:[minor]" |
| **Start Date** | Mar 03, 2020                                           |
| **Due Date**   | Mar 16, 2020                                           |

#### Transcript

> ### Current Implementation
>
> Currently, any user can delete the transactions if they have permission to edit the transaction.
>
> ###  Requested Changes/Enhancement
>
> End-user has requested [here](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/395) to change the delete functionality in the transaction details to edit functionality, and it should be permission-based. The entry officer should only be able to edit a transaction if they have not saved the voucher. Once the voucher is saved, only the authorized user should be able to edit the transactions.

### HRM audit event logging coverage - 390

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~ "enhancement" ~"module:[hrm]" ~"release:[major]" ~"[specification: pending]" |
| **Start Date** | Mar 03, 2020                                                 |
| **Due Date**   | Mar 16, 2020                                                 |

#### Transcript

> ### Problem to solve
>
> Very detailed logging required for all HRM functionality. Very tight time budget. We only have very basic logging logging and log reporting features implemented in our application code-base. Notifications are also very urgently needed.
>
> ###  Goals
>
> - Decouple logging and notifications infrastructure.
> - Simplify development of application functionality and infrastructure layer for notifications
> - Provide comprehensive logging and log filtering for all HRM activities for: 
>   - All events (API transactions) that take place against a group of employees
>   - All events (API transactions) that take place against an individual employee
>
> ###  Proposal
>
> 1. API endpoints and UI/UX features in **Employee Control Panel** that provide detailed filtering and listing for all events (CQRS commands) that have changed or affected the employee in any way.
>
> 2. API endpoints and UI/UX features in 
>
>    HRM Resources Control Panel
>
>     that provide detailed filtering and listing for all events (CQRS commands) that have changed or affected a 
>
>    selection of employees
>
>    . 
>
>    - Log filters must include **at least** - command type (edit, delete, attendance_update, etc)
>    - Log output must show **at least** - **HTTP Request Body/Payload** | **CQRS command metadata (id, connection, etc)** | **the exact kubernetes cluster, ingress host, and pod of the service serving the command** 
>
> ####  Proposal work breakdown
>
> - Develop and design prototypes
> - Develop and design a testing plan
> - Have designed prototypes approved
> - Implement proposed functionality according to approved prototypes and testing plan

### Salary Pension PDF export is broken - 360

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~ "bug" ~"enhancement" ~"module:[hrm]" ~"proposal:[pending]" ~"release:[patch]" |
| **Start Date** | Mar 03, 2020                                                 |
| **Due Date**   | Mar 16, 2020                                                 |

#### Transcript

> ### Steps to reproduce
>
> 1. Create a new employee or select an existing one from the employees listing page
> 2. Go to the Pension & Salary Tax details for the selected employee, click on the PDF Export
>
> ###  What is the current *bug* behavior?
>
> When PDF EXPORT button is clicked, the salary pension is not getting exported.
>
> ###  What is the expected *correct* behavior?
>
> The salary pension should get exported when the PDF EXPORT button is clicked in TAX & PENSION details.

### Updated implementation of technical questions total mark in interview form - 229

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~"enhancement" ~"module:[hrm]" ~"module:[project]" ~"release:[minor]" ~"specification:[requested]" |
| **Start Date** | Mar 03, 2020                                                 |
| **Due Date**   | Mar 16, 2020                                                 |

#### Transcript

> ### What is the current bug behavior
>
> 1. the totals are a certain number of questions had that score and it shows on the total mark obtain.
>
> ###  What is the expected correct behavior
>
> 1. It should sum up all the technical questions and show their average on the total mark the total average mark should not increase from 30.

### Salary slip PDF export for approved salaries - 228

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~"enhancement" ~"module:[hrm]" ~"proposal:[pending]" ~"release:[minor]" |
| **Start Date** | Mar 03, 2020                                                 |
| **Due Date**   | Mar 16, 2020                                                 |

#### Transcript

> 1.  Create PDF export of salary slip for an approved salary
>
> ###  Test Cases:
>
> 1. Salary slip should be exported.
> 2. The format of Salary Slip should be according to the specification specified [here](https://edgsolutionse-sbo7486.slack.com/archives/GS2EE9WQJ/p1581341919223600?thread_ts=1581337543.220900&cid=GS2EE9WQJ) and [here](https://edgsolutionse-sbo7486.slack.com/archives/GS2EE9WQJ/p1581341978223900?thread_ts=1581337543.220900&cid=GS2EE9WQJ) 
>
> ###  Format document of salary slip.

### Enhancement request to the search dropdown of account, project and budget line in the transaction form - 426

| **Detail**     | Value                                                 |
| -------------- | ----------------------------------------------------- |
| **Labels**     | ~"enhancement" ~"module:[finance]" ~"release:[minor]" |
| **Start Date** | Mar 03, 2020                                          |
| **Due Date**   | Mar 16, 2020                                          |

#### Transcript

> ### Current Implementation
>
> In the search dropdown of account, budget line, and project, it is not showing any option until at least three (3) charterers are entered into the search box.
>
> ### Requested Changes/Enhancement
>
> The end-user had requested through the [service desk](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/395) to show the list of the account, budget line, and project before typing it because this will make the end-user life easier. They don't need to memorize the codes.

### IDP Infrastructure layers - 438

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~"proposal: approved" ~"specification: pending" ~"proposal: approved" ~"release:[major]" |
| **Start Date** | Mar 03, 2020                                                 |
| **Due Date**   | Mar 16, 2020                                                 |

#### Transcript

> ### Problem to solve
>
> The development team is getting close to reaching 100% passing (manual) test coverage on all official use-cases in circulation. However, the IAM infrastructure is very under-developed and almost none of the passing functionality can be enforced any IAM rules upon. At the same time, reaching full IAM coverage on all functionality with self-implemented and hosted IAM infrastructure is both costly (especially in the long run) and can be taxing to develop in the first place primarily due to the QA effort required to get all of it tested and working reliably.
>
> ### Goals
>
> - Delegate all IAM and user-administration infrastructure to Auth0
> - Implement a simple middleware library that interfaces with the Auth0 Authorization and Admin APIs
> - Produce a simple CLI tool that can be used by our CI/CD scripts to automate operations with Auth0 tenant.
>   - CLI tool should be able to read environment variables configured for its environment in GitLab.
>   - Operations that need automating - Mirroring of roles and permissions | Updating and maintaining mission-critical auth0 applications and APIs.
> - Produce a CI/CD pipeline that ensures all scopes and roles **maintained in this repository** are mirrored into our Auth0 tenant using our internally developed Auth0 Admin CLI tool.
>
> ### Proposal
>
> 1. Infrastructure middleware for making sure commands and queries can only be accessed or run by authorized **users** or **machine applications**
>
> 2. Write a CI/CD script that runs a pre-deployment (to production) procedure to ensure all current users are transferred to Auth0.
>
> 3. [OPTIONAL] Implement infrastructure middleware that performs IAM based on our own application logic for project-group, logistics-group, etc use-cases.
>
> 4. Prepare training material for user and role management via Auth0 (to be used by end-users).
>
> 5. Specify a comprehensive set of roles and permissions that will be mirrored and kept in-sync with Auth0 by our automation tools.
>
> 6. [REFLECTIVE ACTION] Must update all current data structure in order to remove foreign key dependencies and constraints on our current users/roles tables. **Decouple current user data structure from other data structures and application functionality**.
>
> 7. Provide permission-based authorization coverage 
>
>    ONLY
>
>     for voucher management features.
>
>    - Prepare specification of exact role-to-scope-cqrs mappings for all voucher management commands and queries.
>
> 8. Prepare Auth0 configuration guidelines for developers to ensure they are possible in the application.
>
>    - Provide relevant auth0 API and Client configurations to the dev team
>
> #### Not doing
>
> - Provide full permission-based authorization validation coverage for all CQRS commands and queries.

### Service Desk (from admin-kabul@cha-net.org): about CHA logistic system - 234

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~"enhancement" ~"module: inventory" ~"module: project" ~"suggestions" |
| **Start Date** | Mar 03, 2020                                                 |
| **Due Date**   | Mar 16, 2020                                                 |

#### Transcript

> به امید صحت و سلامتی شما سیستم انلاین بخش لوژستیک دفتر CHA،  قراریکه مشاهده گردید در بعضی جای ها مشکلات دارد که قرار ذیل خدمت تان معلومات داده میشود.
>
> 1-      از فورم مقایسته (Comparative statements) آپشن ورنتی دیلیت گردد بخاطریکه در 3 کوتیشن ورنتی موجود نیست.
>
> 2-      در فورم مقایسته (Comparative statements) نام، تعداد و قیمت مجموعی جنس قابل دید باشد.
>
> 3-      در آپشن submit a bids-tinder bid contact details شماره تیلفون راسیستم قبول نمیکند، به همین دلیل نتوانستیم که از این جلوتر برویم و در همین جا متوقف مانیدم.
>
> 4-      آپشن International Tender فعال نگردیده است.
>
> 5-       درصفحه Tinder bid، به عوض date security- ذکر گردد       Delivery date
>
> 6-      در صفحه purchase submission form آپشن Requested Units ادیتیبل باشد.
>
> ضمیمه ایمیل هذا شارت اسکرین آنها برای تان ارسال گردید.
>
> > In the email the following enhancement were requested:
> >
> > 1. In comparative statement, warranty option should be deleted because in 3 quotation, warranty is not there.
> > 2. In comparative statement, name, quantity, and total price should be viewable.
> > 3. In submit a bids-tinder bid contact details, telephone number is not accepted by the system, so I couldn't go ahead further.
> > 4. International tender is activated.
> > 5. Date security should be changed to Delivery date in tinder bid.
> > 6. In purchase submission form, requested items should be editable.

### Service Desk (from nadeem@cha-net.org): RE: error in system - 337

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~"enhancement" ~"module: finance" ~"support" ~"suggestions" ~"bug" |
| **Start Date** | Mar 03, 2020                                                 |
| **Due Date**   | Mar 16, 2020                                                 |

#### Transcript

> Please design the same font and design  and same format for cha
>
> From: Nadeem Mahmood [mailto:[nadeem@cha-net.org](mailto:nadeem@cha-net.org)] Sent: Tuesday, January 28, 2020 8:15 PM To: '[incoming+edgsolutions-engineering-clear-fusion-13186895-issue-@incoming.git](mailto:incoming+edgsolutions-engineering-clear-fusion-13186895-issue-@incoming.git) lab.com' <[incoming+edgsolutions-engineering-clear-fusion-13186895-issue-@incoming.git](mailto:incoming+edgsolutions-engineering-clear-fusion-13186895-issue-@incoming.git) lab.com> Cc: 'Edgsolutions Engineering / Clear Fusion' [incoming+82e523c98d4596fda853b044d309d894@incoming.gitlab.com](mailto:incoming+82e523c98d4596fda853b044d309d894@incoming.gitlab.com); 'Abdul Rahman Faeq' [abdulrahmanfaeq1@gmail.com](mailto:abdulrahmanfaeq1@gmail.com) Subject: error in system
>
> Dear Hamza,
>
> During data entry of new system M[anagehttps://manage.cha-net.org/](anagehttps://manage.cha-net.org/) and found some errors.
>
> - ```
>   The system is not showing the USD transaction in the journal, in
>   ```
>
> ```
> 
> ```
>
> ```
> Ledgers and trail balance.
> 
> - ```
> During data entry we found the system is showing credit and debit
> ```
>
> ```
> balances more the five decimals, creating issues during saving the vouchers, reduce the decimals from five to two decimals. In voucher, Journal Ledgers and trail balance.
> 
> - ```
> In Badghis Office during data entry,  shut down of Electricity,
> ```
>
> ```
> system Skipped voucher number 9 of badhgis office and create the voucher no 10 in the system.
> 
> - ```
> Vouchers Lines are very bold, please check the old system format
> ```
>
> and design same, font, etc
>
> Regards,
>
> Nadeem Mahmood
>
> [SBU07015-19.pdf]()
>
> > For this the following response was provided:
> >
> > Dear Nadeem Sb,
> >
> > I am writing this reply to include all the reported issues for finance and provide you with details on them.
> >
> > Here are all the issues reported for the Finance Module. You can follow-up on all of the following issues by replying to this ticket. You can find our input for them under each item.
> >
> > 1. The text of the Account and budget lines are not getting shown completely. This will cause problem for the data entry officers while doing the entry. This enhancement has been implemented with the new voucher control panel implementation.
> > 2. The Add button for the transaction should be shifted to the bottom of the transaction section in the voucher details control panel. This enhancement has been implemented with the new voucher control panel implementation.
> > 3. The document attached to the vouchers are not getting uploaded. This issue also got fixed with the latest voucher implementation,
> > 4. The formatting of the voucher pdf export is not good. The font and border are too bold, and the size of the border should be reduced. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 5. The system is slow during the data entry. This enhancement has been implemented with the new voucher control panel implementation.
> > 6. Add one column in excel for the attachment status. Can you please further clarify this? For which export excel do you want this functionality to be added?
> > 7. In the budget line summary report, the system is exporting trail reports. We will check this issue, and will get back to you with an issue#.
> > 8. The Transactions were added in Voucher #802 in manage.cha-net.org. When the Save button is clicked, all the transactions disappeared. However, when we export the voucher, all the transactions are there in the exported PDF. This has been checked with Abdul Salam in the manage.cha-net.org. This bug will be investigated by us, and we will get back to you with the Issue# once it got planned.
> > 9. As we have checked the prototype, the fields for Account, Project and Budget Line were dropdown. Right now they seem to be dropdown, but we have to type something to see the list. This will cause a problem for the entry users because they will have to memorize all the Accounts, Projects, and Budget Lines to be able to do the entry. According to the approved proposal of the voucher, The search dropdowns will not show any options until at least three characters are entered into the search box. This issue is not a bug and will be treated as an enhancement.
> > 10. Right now we can select to show up to 20 items per page in the transactions section of the voucher details control panel. The number should be increased to 100. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 11. The delete option in the transaction details should be changed to edit a transaction, and it should be permission-based. The entry officer should only be able to edit a transaction if they have not saved the voucher. Once the voucher is saved, only the authorized user should be able to edit the transactions. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 12. In all of the pages and export format, the Debit amount should be shown before Credit. This is standard practice. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 13. Balance of the Debit and Credit should be shown in the Voucher Details. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 14. When we Select All of the Offices or Journals in the Journal Control Panel, the label in the dropdown field should be changed to "All items are selected." Right now, it is showing the name of the Selected Offices and Journals. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 15. In the journals, all accounts should be selected by default, and we should not be able to de-select any of the accounts in the journal. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 16. In the voucher it only shows the Job Name, the job code should also be shown there. This enhancement has been implemented with the new voucher control panel implementation.
> >
> > I hope you find this email informative and that it covers all your concerns. Please let us know if we missed anything by replying to this email.
> >
> > Best regards,
> >
> > Abdul Salam
> >
> > The end-user replied with the following response to one of the questions:
> >
> > Dear Salam,
> >
> > Thanks for the detailed email, till now what we have shared with you all covering your following email.
> >
> > 1. `Add one column in excel for the attachment status. Can you please further clarify this? For which export excel do you want this functionality to be added?`
> >
> > The system we are right now using, when we export the journal in excel, vouchers with attachment show 1 and without attachment show 0. But in new system to find out vouchers without attachment we don’t have no tool.
> >
> > Regards,
> >
> > Nadeem Mahmood

### Some of the fields in the Purchase Form should be made searchable - 417

| **Detail**     | Value                                                 |
| -------------- | ----------------------------------------------------- |
| **Labels**     | ~"enhancement" ~"module:inventory" ~"release:[minor]" |
| **Start Date** | Mar 03, 2020                                          |
| **Due Date**   | Mar 16, 2020                                          |

#### Transcript

> ### Current Implementation
>
> Currently the options which the client is requested to be made searchable are not searchable.
>
> ### Requested Changes/Enhancement
>
> As the user had requested in the [Service Desk](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/199#note_276939726). The selected options in the screenshot which are **Inventory(Master), Item Group, Item, Project, Budget Line, Received From Location, and Received from employee** should be made searchable based on codes and names.

### Service Desk (from faeq@cha-net.org): Cannot upload files to the proposal - 419

| **Detail**     | Value                                 |
| -------------- | ------------------------------------- |
| **Labels**     | ~"bug" ~"module:project" ~"confirmed" |
| **Start Date** | Mar 03, 2020                          |
| **Due Date**   | Mar 16, 2020                          |

#### Transcript

> Dear Hamza & Salam,
>
> I am sending you out this issue on behalf of the PMU department because Nadeem Sb is on a trip. We have tried to upload files to the Proposal, but the system is throwing an error. This error is different than the previous one. We have taken the logs for your further actions. You can see the logs in this video(https://drive.google.com/file/d/1RXAqtzb4SmMYGSYYk93jLZu4cu145MNF). I need to mention that the issue is not only with the large sizes but with all the files which are uploading to the Proposal.
>
> Regards
>
> Abdul Rahman Faeq
>
> General Admin & Finance Manager  CHA
>
> Email: [Faeq@cha-net.org](mailto:Faeq@cha-net.org) mailto:Faeq@cha-net.org
>
> ```
>       <mailto:Abdulrahmanfaeq@gmail.com> Abdulrahmanfaeq@gmail.com
> ```
>
> Web site: [www.cha-net.org](http://www.cha-net.org) http://www.cha-net.org/
>
> Phone : +93(0) 729128699
>
> Whatsapp:+93(0) 796306999
>
> Skype: abdulrahmanfaeq2013
>
> > Followings are the finding after the discussion:
> >
> > The issue was created for this service desk, furthermore, the same issue is in the voucher uploading files as well.

### Service Desk (from asifsalik@cha-net.org): HR system bugs - 436

| **Detail**     | Value                                   |
| -------------- | --------------------------------------- |
| **Labels**     | ~"enhancement" ~"module:hrm" ~"support" |
| **Start Date** | Mar 03, 2020                            |
| **Due Date**   | Mar 16, 2020                            |

#### Transcript

> Dear Hamza and Abdul Salam,
>
> Hope you are all doing well,
>
> Here is a list of bugs in different sections of the HR system that has the higher priority to be fixed.
>
> 1-      Employee salary configuration page, while selecting a month to mark attendance for an employee the system is not working.
>
> 2-      In the manage while we are selecting the Saturday as weekend the system is not calculating it as a weekend, need to be fixed and has high priority.
>
> 3-      TIN# in PDF tax report format is not correct, as we have two places for TIN# in the format; one for the organization and one specific for employ, the system is not working correctly and replacing the correct TIN# in their places, for details please refer to PDF tax report format screenshot.
>
> 4-      In the payroll daily hours, we need an enhancement, for details please refer to the attached screenshot.
>
> 5-      In leave application section, in case an employ apply for two hours leave in a day, the system does not have this functionality to calculate two hours leave.
>
> 6-      Employees code generation, while adding new employee in the manage system the code generation is not working correctly, for example after code
>
> ##### E19421 code# E19422 is missing, meanwhile, after code # E19436 the system
>
> has generated Emp Code#  E19461.
>
> 7-      Employee control panel, please set active status in this section as a default.
>
> 8-      There is no place for adding employees documentation.
>
> 9-      Please add PDF export in exit interview and appraisal section according the format we have shared.
>
> 10-   Exit interview, Please move the question; Are there any Unresolved Issues or Additional Comments? in the end of the format, for details please see the shared format.
>
> Always appreciate your hard work and hope you fixe all the above issues very soon.
>
> Best Regards
>
> Mohammad Asif Salik
>
> CHA Main Office HR Acting Manager
>
> Official email:  mailto:asifsalik@cha-net.org [asifsalik@cha-net.org](mailto:asifsalik@cha-net.org)
>
> Skype: salik2014
>
> Phone#: (+93) 729128430

## Closing issues/tasks

### Enhancement request to supplier offer grid field addition - 434

| **Detail**     | Value                                               |
| -------------- | --------------------------------------------------- |
| **Labels**     | ~"enhancement" ~"module:project" ~"release:[minor]" |
| **Start Date** | Mar 03, 2020                                        |
| **Due Date**   | Mar 17, 2020                                        |

#### Transcript

> ### Current implementation
>
> Currently, After adding the supplier it shows the supplier and item in the supplier grid.
>
> ###  Requested changes/Enhancement
>
> End-user had requested in [service desk](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/234) that the quantity and total amount should be added in the supplier grid.
>
> 1. Quantity
> 2. Total Amount(CURR). This is to be a calculated field as described [here](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/434#note_296857471)

### Enhancement to the debit and credit columns order - 427

| **Detail**     | Value                                               |
| -------------- | --------------------------------------------------- |
| **Labels**     | ~"enhancement" ~"module:finance" ~"release:[minor]" |
| **Start Date** | Mar 03, 2020                                        |
| **Due Date**   | Mar 17, 2020                                        |

#### Transcript

> ### Current Implementation
>
> Currently, in the Voucher Control Panel, Voucher Details Control Panel, and Exported PDF of the voucher, the column of the credit is before the debit.
>
> ###  Requested Changes/Enhancement
>
> According to the request in the [Service Desk](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/395), In all of the pages and PDF export format, the **Debit amount** should be shown **before Credit**, because this is standard practice.

### Cannot upload any files to the proposal in project details control panel- 420

| **Detail**     | Value                                                      |
| -------------- | ---------------------------------------------------------- |
| **Labels**     | ~"bug" ~"confirmed" ~"module:[project]" ~"release:[patch]" |
| **Start Date** | Mar 03, 2020                                               |
| **Due Date**   | Mar 17, 2020                                               |

#### Transcript

> This bug has been reported through [the service desk](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/419).
>
> ###  Prerequisites (if any)
>
> ###  Steps to reproduce
>
> **Note: Please don't upload anything to the production system of the end-user. You can check out the video attached**
>
> 1. Go to manage.cha-net.org
> 2. Open a project
> 3. Go to Proposal
> 4. Upload a file to the proposal
>
> ###  What is the current *bug* behavior?
>
> When we upload any file to the proposal of the production system of the end-user, it throws an error: "[Obejct Object]," and the file doesn't get uploaded.
>
> ###  What is the expected *correct* behavior?
>
> When we upload the file to the proposal, it should not throw any error and get uploaded.

### Service Desk (from faeq@cha-net.org): Cannot upload files to the proposal - 419

| **Detail**     | Value                                   |
| -------------- | --------------------------------------- |
| **Labels**     | ~"bug" ~"confirmed" ~"module:[project]" |
| **Start Date** | Mar 03, 2020                            |
| **Due Date**   | Mar 17, 2020                            |

#### Transcript

> Dear Hamza & Salam,
>
> I am sending you out this issue on behalf of the PMU department because Nadeem Sb is on a trip. We have tried to upload files to the Proposal, but the system is throwing an error. This error is different than the previous one. We have taken the logs for your further actions. You can see the logs in this video(https://drive.google.com/file/d/1RXAqtzb4SmMYGSYYk93jLZu4cu145MNF). I need to mention that the issue is not only with the large sizes but with all the files which are uploading to the Proposal.
>
> Regards
>
> Abdul Rahman Faeq
>
> General Admin & Finance Manager  CHA
>
> Email: [Faeq@cha-net.org](mailto:Faeq@cha-net.org) mailto:Faeq@cha-net.org
>
> ```
>       <mailto:Abdulrahmanfaeq@gmail.com> Abdulrahmanfaeq@gmail.com
> ```
>
> Web site: [www.cha-net.org](http://www.cha-net.org) http://www.cha-net.org/
>
> Phone : +93(0) 729128699
>
> Whatsapp:+93(0) 796306999
>
> Skype: abdulrahmanfaeq2013
>
> > Followings are the finding after the discussion:
> >
> > The issue was created for this service desk, furthermore, the same issue is in the voucher uploading files as well.

### Updated implementation of technical questions total mark in interview form - 229

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~"enhancement" ~"module:[project]" ~"module:[hrm]"~ "release:[minor]"      ~"specification: requested" |
| **Start Date** | Mar 03, 2020                                                 |
| **Due Date**   | Mar 17, 2020                                                 |

#### Transcript

> work break down  updated
>
> ###  What is the current bug behavior
>
> 1. the totals are a certain number of questions had that score and it shows on the total mark obtain.
>
> ###  What is the expected correct behavior
>
> 1. It should sum up all the technical questions and show their average on the total mark the total average mark should not increase from 30.
>
> **Work Break Down**
>
> -  Maximum score for technical question should be 30. formula used(sum_of_question_score/No of question)
> -   Maximum score for rating base criteria should be 10. formula used(sum_of_question_score/No of question)
> -  Maximum score for Written Test form should be 60.
> -  Maximum score for Interview Form should be 100. formula used(technical question marks+rating_b_c marks+written text marks)

### Enhancement request to submit a bid tender form - 433

| **Detail**     | Value                                                  |
| -------------- | ------------------------------------------------------ |
| **Labels**     | ~"enhancement" ~"module:[project]" ~ "release:[patch]" |
| **Start Date** | Mar 03, 2020                                           |
| **Due Date**   | Mar 17, 2020                                           |

#### Transcript

> ### Current implementation
>
> Currently, In the submit a bit form, there is security date.
>
> ###  Requested changes/Enhancement
>
> End-user had requested through [service](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/234) desk that the Security Date **label** should be changed to say **Delivery Date**.

### Wrong Calculation for Salary, and Gross Columns in the Excel Payroll Export - 428

| **Detail**     | Value                                                    |
| -------------- | -------------------------------------------------------- |
| **Labels**     | ~"bug" ~"confirmed" ~ "module:[hrm]" ~ "release:[patch]" |
| **Start Date** | Mar 03, 2020                                             |
| **Due Date**   | Mar 17, 2020                                             |

#### Transcript

> ### Reproduce steps
>
> 1. Open RM resources control panel
> 2. Select employees
> 3. Export payroll
>
> ###  What is the current bug behavior?
>
> Right now it shows the wrong calculation for the salary and the gross salary
>
> ###  What is the correct expected behavior?
>
> The calculation should be as follow: **Salary** = BasicPay - [(abs * Active Hourly Rate) * Percentage / 100]

### Enhancement request to the search dropdown of account, project and budget line in the transaction form - 426

| **Detail**     | Value                                                   |
| -------------- | ------------------------------------------------------- |
| **Labels**     | ~"enhancement" ~ "module:[finance]" ~ "release:[minor]" |
| **Start Date** | Mar 03, 2020                                            |
| **Due Date**   | Mar 17, 2020                                            |

#### Transcript

> ### Current Implementation
>
> In the search dropdown of account, budget line, and project, it is not showing any option until at least three (3) charterers are entered into the search box.
>
> ###  Requested Changes/Enhancement
>
> The end-user had requested through the [service desk](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/395) to show the list of the account, budget line, and project before typing it because this will make the end-user life easier. They don't need to memorize the codes.

### Salary Pension PDF export is broken - 360

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~"bug" ~"enhancement" ~ "module:[hrm]" ~ "proposal:[pending]" ~"release:[patch]" |
| **Start Date** | Mar 03, 2020                                                 |
| **Due Date**   | Mar 17, 2020                                                 |

#### Transcript

> 1. Create a new employee or select an existing one from the employees listing page
> 2. Go to the Pension & Salary Tax details for the selected employee, click on the PDF Export
> 3. Also, export the Salary Tax export.
>
> ###  What is the current *bug* behavior?
>
> 1. When PDF EXPORT button is clicked, the salary pension is not getting exported.
> 2. The details of the authorized officers are not getting shown in the Salary Tax export.
>
> ###  What is the expected *correct* behavior?
>
> 1. The salary pension should get exported when the PDF EXPORT button is clicked in TAX & PENSION details.
> 2. The details of the authorized officers should also get generated.
>
> **Work breakdown**
>
> -   Create Service  to implement jsPdf export functionality
> -   Use jsPdf Service for salary pension pdf export

### Service Desk (from asifsalik@cha-net.org): HR system bugs - 436

| **Detail**     | Value                                       |
| -------------- | ------------------------------------------- |
| **Labels**     | ~"enhancement" ~ "module:[hrm]" ~ "support" |
| **Start Date** | Mar 03, 2020                                |
| **Due Date**   | Mar 17, 2020                                |

#### Transcript

> Dear Hamza and Abdul Salam,
>
> Hope you are all doing well,
>
> Here is a list of bugs in different sections of the HR system that has the higher priority to be fixed.
>
> 1-      Employee salary configuration page, while selecting a month to mark attendance for an employee the system is not working.
>
> 2-      In the manage while we are selecting the Saturday as weekend the system is not calculating it as a weekend, need to be fixed and has high priority.
>
> 3-      TIN# in PDF tax report format is not correct, as we have two places for TIN# in the format; one for the organization and one specific for employ, the system is not working correctly and replacing the correct TIN# in their places, for details please refer to PDF tax report format screenshot.
>
> 4-      In the payroll daily hours, we need an enhancement, for details please refer to the attached screenshot.
>
> 5-      In leave application section, in case an employ apply for two hours leave in a day, the system does not have this functionality to calculate two hours leave.
>
> 6-      Employees code generation, while adding new employee in the manage system the code generation is not working correctly, for example after code
>
> ####  E19421 code# E19422 is missing, meanwhile, after code # E19436 the system
>
> has generated Emp Code#  E19461.
>
> 7-      Employee control panel, please set active status in this section as a default.
>
> 8-      There is no place for adding employees documentation.
>
> 9-      Please add PDF export in exit interview and appraisal section according the format we have shared.
>
> 10-   Exit interview, Please move the question; Are there any Unresolved Issues or Additional Comments? in the end of the format, for details please see the shared format.
>
> Always appreciate your hard work and hope you fixe all the above issues very soon.
>
> Best Regards
>
> Mohammad Asif Salik
>
> CHA Main Office HR Acting Manager

### The formatting of the voucher pdf export is too bold - 425

| **Detail**     | Value                                                   |
| -------------- | ------------------------------------------------------- |
| **Labels**     | ~"enhancement" ~ "module:[finance]" ~ "release:[minor]" |
| **Start Date** | Mar 03, 2020                                            |
| **Due Date**   | Mar 17, 2020                                            |

#### Transcript

> ### Current Implementation
>
> In the current implementation, the size of the font is big, and the border is thick.
>
> ###  Requested Changes/Enhancement
>
> As end-user requested through the [Service Desk](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/340) and during [the meeting](https://drive.google.com/open?id=1kLcWd2TtZy-1Lo-xL1hPzpidll3_LeS0) the formatting of the voucher pdf export needs changes.
>
> 1. The font size needs to be reduced to 9.
> 2. Only the header should be bold, the rest of the text should be normal formatting.
> 3. The top header (COORDINATION OF HUMANITARIAN ASSISTANCE) size should be 14 and bolded.
> 4. The top header (TRANSACTION VOUCHER) size should be 13 and bold.
> 5. The border needs to be a normal border, **NOT THICK**.
> 6. Increase the margin of the exported format so that more transactions fit in one page.
> 7. If we export multiple vouchers, between each voucher a page break should be inserted. If one voucher ends, the other voucher should be started on a new page.
>
> Please refer to this format for more details: [VoucherReport.pdf]()

### Some of the fields in the Purchase Form should be made searchable - 417

| **Detail**     | Value                                                     |
| -------------- | --------------------------------------------------------- |
| **Labels**     | ~"enhancement" ~ "module:[inventory]" ~ "release:[minor]" |
| **Start Date** | Mar 03, 2020                                              |
| **Due Date**   | Mar 17, 2020                                              |

#### Transcript

> ### Current Implementation
>
> Currently the options which the client is requested to be made searchable are not searchable.
>
> ###  Requested Changes/Enhancement
>
> As the user had requested in the [Service Desk](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/199#note_276939726). The selected options in the screenshot which are **Inventory(Master), Item Group, Item, Project, Budget Line, Received From Location, and Received from employee** should be made searchable based on codes and names.

### Salary slip PDF export for approved salaries - 228

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~"enhancement" ~ "module:[hrm]" ~ "proposal:[pending]" ~ "release:[minor]" |
| **Start Date** | Mar 03, 2020                                                 |
| **Due Date**   | Mar 17, 2020                                                 |

#### Transcript

> 1.  Create PDF export of salary slip for an approved salary
>
> ###  Test Cases:
>
> 1. Salary slip should be exported.
> 2. The format of Salary Slip should be according to the specification specified [here](https://edgsolutionse-sbo7486.slack.com/archives/GS2EE9WQJ/p1581341919223600?thread_ts=1581337543.220900&cid=GS2EE9WQJ) and [here](https://edgsolutionse-sbo7486.slack.com/archives/GS2EE9WQJ/p1581341978223900?thread_ts=1581337543.220900&cid=GS2EE9WQJ) 
>
> ###  Format document of salary slip.
>
> [Salary_Slip.doc]()
>
> ###  Work breakdown
>
> 1.   Need to change in cshtml file to update format of pdf
> 2.   Bind the data with pdf model in salary slip API with testing

> ments) نام، تعداد و قیمت مجموعی جنس قابل دید باشد.
>
> 3-      در آپشن submit a bids-tinder bid contact details شماره تیلفون راسیستم قبول نمیکند، به همین دلیل نتوانستیم که از این جلوتر برویم و در همین جا متوقف مانیدم.
>
> 4-      آپشن International Tender فعال نگردیده است.
>
> 5-       درصفحه Tinder bid، به عوض date security- ذکر گردد       Delivery date
>
> 6-      در صفحه purchase submission form آپشن Requested Units ادیتیبل باشد.
>
> ضمیمه ایمیل هذا شارت اسکرین آنها برای تان ارسال گردید.

### Service Desk (from nadeem@cha-net.org): RE: error in system - 337

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~"bug" ~"enhancement" ~ "module:[finance]" ~ "suggestion" ~ "support" |
| **Start Date** | Mar 03, 2020                                                 |
| **Due Date**   | Mar 17, 2020                                                 |

#### Transcript

> Please design the same font and design  and same format for cha
>
> From: Nadeem Mahmood [mailto:[nadeem@cha-net.org](mailto:nadeem@cha-net.org)] Sent: Tuesday, January 28, 2020 8:15 PM To: '[incoming+edgsolutions-engineering-clear-fusion-13186895-issue-@incoming.git](mailto:incoming+edgsolutions-engineering-clear-fusion-13186895-issue-@incoming.git) lab.com' <[incoming+edgsolutions-engineering-clear-fusion-13186895-issue-@incoming.git](mailto:incoming+edgsolutions-engineering-clear-fusion-13186895-issue-@incoming.git) lab.com> Cc: 'Edgsolutions Engineering / Clear Fusion' [incoming+82e523c98d4596fda853b044d309d894@incoming.gitlab.com](mailto:incoming+82e523c98d4596fda853b044d309d894@incoming.gitlab.com); 'Abdul Rahman Faeq' [abdulrahmanfaeq1@gmail.com](mailto:abdulrahmanfaeq1@gmail.com) Subject: error in system
>
> Dear Hamza,
>
> During data entry of new system M[anagehttps://manage.cha-net.org/](anagehttps://manage.cha-net.org/) and found some errors.
>
> - ```
>      The system is not showing the USD transaction in the journal, in
>   ```
>
> Ledgers and trail balance.
>
> - ```
>      During data entry we found the system is showing credit and debit
>   ```
>
> balances more the five decimals, creating issues during saving the vouchers, reduce the decimals from five to two decimals. In voucher, Journal Ledgers and trail balance.
>
> - ```
>      In Badghis Office during data entry,  shut down of Electricity,
>   ```
>
> system Skipped voucher number 9 of badhgis office and create the voucher no 10 in the system.
>
> - ```
>      Vouchers Lines are very bold, please check the old system format
>   ```
>
> and design same, font, etc
>
> Regards,
>
> Nadeem Mahmood
>
> [SBU07015-19.pdf]()
>
> > For this the following response was provided:
> >
> > Dear Nadeem Sb,
> >
> > I am writing this reply to include all the reported issues for finance and provide you with details on them.
> >
> > Here are all the issues reported for the Finance Module. You can follow-up on all of the following issues by replying to this ticket. You can find our input for them under each item.
> >
> > 1. The text of the Account and budget lines are not getting shown completely. This will cause problem for the data entry officers while doing the entry. This enhancement has been implemented with the new voucher control panel implementation.
> > 2. The Add button for the transaction should be shifted to the bottom of the transaction section in the voucher details control panel. This enhancement has been implemented with the new voucher control panel implementation.
> > 3. The document attached to the vouchers are not getting uploaded. This issue also got fixed with the latest voucher implementation,
> > 4. The formatting of the voucher pdf export is not good. The font and border are too bold, and the size of the border should be reduced. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 5. The system is slow during the data entry. This enhancement has been implemented with the new voucher control panel implementation.
> > 6. Add one column in excel for the attachment status. Can you please further clarify this? For which export excel do you want this functionality to be added?
> > 7. In the budget line summary report, the system is exporting trail reports. We will check this issue, and will get back to you with an issue#.
> > 8. The Transactions were added in Voucher #802 in manage.cha-net.org. When the Save button is clicked, all the transactions disappeared. However, when we export the voucher, all the transactions are there in the exported PDF. This has been checked with Abdul Salam in the manage.cha-net.org. This bug will be investigated by us, and we will get back to you with the Issue# once it got planned.
> > 9. As we have checked the prototype, the fields for Account, Project and Budget Line were dropdown. Right now they seem to be dropdown, but we have to type something to see the list. This will cause a problem for the entry users because they will have to memorize all the Accounts, Projects, and Budget Lines to be able to do the entry. According to the approved proposal of the voucher, The search dropdowns will not show any options until at least three characters are entered into the search box. This issue is not a bug and will be treated as an enhancement.
> > 10. Right now we can select to show up to 20 items per page in the transactions section of the voucher details control panel. The number should be increased to 100. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 11. The delete option in the transaction details should be changed to edit a transaction, and it should be permission-based. The entry officer should only be able to edit a transaction if they have not saved the voucher. Once the voucher is saved, only the authorized user should be able to edit the transactions. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 12. In all of the pages and export format, the Debit amount should be shown before Credit. This is standard practice. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 13. Balance of the Debit and Credit should be shown in the Voucher Details. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 14. When we Select All of the Offices or Journals in the Journal Control Panel, the label in the dropdown field should be changed to "All items are selected." Right now, it is showing the name of the Selected Offices and Journals. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 15. In the journals, all accounts should be selected by default, and we should not be able to de-select any of the accounts in the journal. This enhancement will be planned, and we will then provide you with a due date and Issue# once it got planned. We will get back to you if there was any question regarding the enhancement.
> > 16. In the voucher it only shows the Job Name, the job code should also be shown there. This enhancement has been implemented with the new voucher control panel implementation.
> >
> > I hope you find this email informative and that it covers all your concerns. Please let us know if we missed anything by replying to this email.
> >
> > Best regards,
> >
> > Abdul Salam
> >
> > The end-user replied with the following response to one of the questions:
> >
> > Dear Salam,
> >
> > Thanks for the detailed email, till now what we have shared with you all covering your following email.
> >
> > 1. `Add one column in excel for the attachment status. Can you please further clarify this? For which export excel do you want this functionality to be added?`
> >
> > The system we are right now using, when we export the journal in excel, vouchers with attachment show 1 and without attachment show 0. But in new system to find out vouchers without attachment we don’t have no tool.
> >
> > Regards,
> >
> > Nadeem Mahmood

### Enhancement to the Payroll Salary - 429

| **Detail**     | Value                                         |
| -------------- | --------------------------------------------- |
| **Labels**     | ~enhancement ~"module:hrm" ~"release:[minor]" |
| **Start Date** | Mar 03, 2020                                  |
| **Due Date**   | Mar 17, 2020                                  |

#### Transcript

> ### Current implementation
>
> According to the [service desk received](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/367#note_294036007). We couldn't do these in the [previous task](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/415), so I am adding it as an enhancement to be implemented.
>
> ### Requested changes/Enhancement
>
> 1. If no employee is selected then the payroll should be exported for all employees according to the filters which are applied.
> 2. The end-user should be able to export the payroll of multiple offices at the once.
> 3. There should be a selection of the month for which they want the payroll export. It can multiple selections. The exported payroll will be for selected months.
> 4. Payroll should be project base as well. [For this I think we can consider a filtering base on the project, so they can filter the employees on project and export the payroll]
> 5. The payment date will be changed to Payroll Month, so only the month which the payroll is exported for will be shown there.
> 6. Another column will be added to the payroll format called: "Payroll Month", As stated above the payroll can be exported for multiple months, so the month which the salary is for will be mentioned there.
> 7. Field descriptions: 
>    1.  **Gross Salary** = Total Gross Salary * Percentage / 100
> 8. Attached is a sample of exported payroll format. [Payroll_format__2_.xlsx](https://gitlab.com/edgsolutions-engineering/clear-fusion/uploads/2a025390af5f8f4d50e0ac5ff93543c1/Payroll_format__2_.xlsx) 
>
> ### Test cases
>
> 1. Payroll gets exported for the selected employees.
> 2. The calculations are correct according to the specifications.
> 3. The Format of the payroll is according to the attached payroll format.



## Currently outstanding items (at the time of release)

This section lists out transcripts of all the remaining open tasks that must be completed in further milestones. For the sake of keeping this document brief, all transcripts will be provided as download links.

### Proposals

1. [[FEATURE PROPOSAL] Reworked accounting panels and dashboards - #190](https://drive.google.com/open?id=10gFzfW74r9W4ssFOBVI586aUWpryqW7E)
2. [[CHANGE PROPOSAL REQUEST] Exchange gain/loss consolidation enhancements - #160](https://drive.google.com/open?id=10d_aEDC7kC8LKETOzTEhMuuAjDOimaw5)
3. [Financial Operations Integration with related operations - #389](https://drive.google.com/open?id=10WdZ6bZK6ZAslKUgpDNnYVr263O-DR1I)
4. [[CONSOLIDATED FEATURES PROPOSAL] Upgraded/Enhanced inventory and logistics - #437](https://drive.google.com/open?id=12urvj8Nm4SmebKeKUG4QhXp3aIR9z958)

### Approved enhancements

4. [Enhancement request in purchase advanced filter - #418](https://drive.google.com/open?id=11MZhniCgDtjl0-wuGoioyJgSOmm7RfHh&authuser=salam.faeq@edgsolutions.com&usp=drive_fs)
5. [Enhancement request to procurement grid location field needs to be added - #421](https://drive.google.com/open?id=12iqSkszJx-8hHKlbz2FNyvcxvGCIqxGx&authuser=salam.faeq@edgsolutions.com&usp=drive_fs)
6. [PDF Export Functionality for the Configuration page of the Store - #422](https://drive.google.com/open?id=131qmcoU2zka1f-VLA7Xqqq6r7dT6FYs7)
7. [Assets details form for general, vehicle, motorcycle, and furniture asset information - #423](https://drive.google.com/open?id=1zdkWg1SIVEjqXRbXmeP0ASA6gkV6_nVY )
8. [Enhancement to voucher no in the procurement form - 424](https://drive.google.com/open?id=1-5qi61uKFotunGA76Dsl7UCHaQEjgiXJ )
9. [Enhancement request to the employee exit interview dashboard - 430](https://drive.google.com/open?id=1-7MXnS1c-gUVKOH7QryF5fTONKfb4GBd )
10. [Enhancement request to the depreciation of none-expendable items - 432](https://drive.google.com/open?id=13AANWpB5MnkdHNmeiSkhUadRLdVCOcps )
11. [Enhancement request, add project status dropdown in the project listing and project details control panel - 439](https://drive.google.com/open?id=1-D3-Y2ByC7ZJRAH9jPpntrK_g2YUvLJh )
12. [Enhancement request it should show the debit and credit balance in the voucher details control panel - 440](https://drive.google.com/open?id=1-DRFJ_t3ztQxxBT2csMdnpJSdakefcXe )
13. [The search option should be added for the Store Configuration Page - 441](https://drive.google.com/open?id=1-FOFMZsOuz-_W-NvSom1vRcnFAy4SOob )
14. [Selecting all the offices and journals in the journal control panel the label should be changed to (All items are selected - 442](https://drive.google.com/open?id=1-GASINUsUQqpKOjw8EDmnXOblvnQVnGb )
15. [All accounts should be selected by default, they should not be able to de-select in the journal - 443 ](https://drive.google.com/open?id=1-IGV46VjRaCizmpuvOdsbIZ_qf2CR83f )
16. [Increase showing items per page number to 100 in the transaction lists of voucher details control panel - 444 ](https://drive.google.com/open?id=1-KAPTDcrytsZKXVaoAzRyXz32XUYr8DK )
17. [In the budget line summary report it is exporting the trail report - 445](https://drive.google.com/open?id=10Zw3fI0fhOEYalLqUBwyb9-hPDMSeOOv )
18. [The Purchases & Procurements name should be changed to the Purchases and issues - 446](https://drive.google.com/open?id=1-NHwbloavdZVmt-sgmfHjZqn9YVFCVoh )
19. [Remove associated wanrrenty document option from suplier offer form - 447 ](https://drive.google.com/open?id=1-U79OHnE43dWWPJMqct11ZanJNexDOPA )
20. [Enhancement request add employee documentation - 449 ](https://drive.google.com/open?id=1-UWRLWFpjE7QLxeRDH-LZwg2LYowicnA )
21. [Enhancement Request in the payroll daily hour - 450 ](https://drive.google.com/open?id=1-lFWvFkOxgTp1I5pij7QhqeYnM6-nBZh )
22. [The Active status should be set by default in the Employees page - 451 ](https://drive.google.com/open?id=1-pfL-RTXx-kakbE72-gnAy_yX-FsPxgP )
23. [Enhancement to employee PDF tax report format TIN - 452 ](https://drive.google.com/open?id=1-rNnUtsqwA5Prv3MRK0QOXh9QiyqMYaY )
24. [Enhancement request to the Employee Add Leave Application form - 453 ](https://drive.google.com/open?id=1-ujjPK6yY8wQ8tG87UP5h8phf9HCrXWJ )
25. [Can not select Saturday or more than one day as weekend - 454 ](https://drive.google.com/open?id=10e3vBkGsXta-njvVm2L8woSwhbEQh10B )
26. [Not showing the monthly salary breakdown - 455 ](https://drive.google.com/open?id=10fEZctIDrFccaiKi_p5QQVs__Dmgpjfj )
27. [Enhancement request to Purchases & Procurements filter option - 456 ](https://drive.google.com/open?id=1-ylPgApndYPA8qmVUgCiQcmXbA_KrxV9 )
28. [Enhancement request to Purchase Documents-Image - 457 ](https://drive.google.com/open?id=101IctIGYLXxwltztLUr-FTN_Z2SmSSA0 )
29. [Enhancement requests new form addition for available items, sold items, unavailable items, and lost items - 458 ](https://drive.google.com/open?id=102n3hYDG0xqGFQBtv80nU9RitZmvRbLp )
30. [New column for the attachment status in the journal excel export - 459  ](https://drive.google.com/open?id=107CDcx7dJNj8nbWDySqwVOqomOvH7zq_ )
31. [It shows wrong email address for employee ID#E4093 in the user section in the production system - 460 ](https://drive.google.com/open?id=10lmGTuPiv8iI0uIRBW1BRKRCPqUtkm8W )
32. [Enhancement to the employee codes - 461 ](https://drive.google.com/open?id=10GH4sXo3WoSHFXtBBTRzc2WcGYc87Fpp )
33. [Move, Are there any Unresolved Issues or Additional Comments? question to the bottom of the exit interview form - 462 ](https://drive.google.com/open?id=10GjoKJAzGCC7QB9oaeHZRsBCNUJVhwc4 )
34. [Saving the education detail form the system automatically change the date - 563 ](https://drive.google.com/open?id=10pP5BfXye4SJA2lf3Ua1OyhTrZaRnJCp )
35. [It shows duplicate attendance date for employee E32 - 465 ](https://drive.google.com/open?id=10xR7-mpk6JzhIb9PlgxEIpmDzVaEiAGS )
36. [Create PDF export for the employee appraisal form - 466 ](https://drive.google.com/open?id=10Hjns4wKL6lAPBw7UlFwZ3gIYy6HE_13 )
37. [Add PDF export for the exit interview form - 467 ](https://drive.google.com/open?id=10KXR4RSD-V_DpxeqV9awUuq5HlM_MAi-)

### Bug fixes

1. [In the budget line summary report it is exporting the trail report - 445](https://drive.google.com/open?id=10Zw3fI0fhOEYalLqUBwyb9-hPDMSeOOv)
2. [Can not select Saturday or more than one day as weekend - 454](https://drive.google.com/open?id=10e3vBkGsXta-njvVm2L8woSwhbEQh10B)
3. [It shows wrong email address for employee ID#E4093 in the user section in the production system - 460](https://drive.google.com/open?id=10lmGTuPiv8iI0uIRBW1BRKRCPqUtkm8W )
4. [Saving the education detail form the system automatically change the date - 463](https://drive.google.com/open?id=10pP5BfXye4SJA2lf3Ua1OyhTrZaRnJCp )
5. [It shows duplicate attendance date for employee E32 - #465](It shows duplicate attendance date for employee E32)

