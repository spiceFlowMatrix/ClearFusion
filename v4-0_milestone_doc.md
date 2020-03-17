## Original planning details

These are all the planning details at the start date of the milestone.

| Planning Detail Type | Planning Detail Value |
| -------------------- | --------------------- |
| **Version**          | v3.0                  |
| **Start Date**       | 25th February, 2020   |
| **Due Date**         | 27th February, 2020   |

## Closing planning details

These are all the planning related details at the end of the milestone when it was completed and delivered.

| Planning Detail Type | Planning Detail Value                                        |
| -------------------- | ------------------------------------------------------------ |
| **Version**          | v3.0                                                         |
| **Start Date**       | 25th February, 2020                                          |
| **Due Date**         | 27th February, 2020                                          |
| **Change history**   | [See this section of report](#issues-manifest-history) for detailed log of issue tracking throughout the milestone |

## Planned issues/tasks

These are all the issues/tasks that were planned for delivery at the beginning/start date of the release milestone:

### No province is selected in employee form, but it showing two City/Village/Districts in the production system - 413

| **Detail**     | Value                                            |
| -------------- | ------------------------------------------------ |
| **Labels**     | ~bug ~Confirmed ~"module:hrm" ~"release:[Patch]" |
| **Start Date** | Feb 25, 2020                                     |
| **Due Date**   | Feb 27, 2020                                     |

#### Transcript

> ### Reproduce steps
>
> 1. Opened HRM resources control panel
> 2. Click on add new employee
> 3. Click on city/village /district
>
> ### What is the current bug behavior?
>
> It shows two city/village/district without selecting country or province end user had requested through this service desk [#367 (comment 292961312)](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/367#note_292961312)
>
> ### What is the correct expected behavior?
>
> It should not show the city/village/district without selecting one of the provinces.
>
> ### Relevant log or/screenshots
>
> image.jpg

### Additional Edit buttons in the Advances tab - 410

| **Detail**     | Value                                            |
| -------------- | ------------------------------------------------ |
| **Labels**     | ~bug ~Confirmed ~"module:hrm" ~"release:[Patch]" |
| **Start Date** | Feb 25, 2020                                     |
| **Due Date**   | Feb 27, 2020                                     |

#### Transcript

> ### Steps to reproduce
>
> 1. Go to the HR, Employee List.
> 2. Go to one of the Employee Control panel.
> 3. Go to the ADVANCES tab
> 4. Add Advance Request
>
> ### What is the current bug behavior?
>
> There are four extra edit buttons in the Advance table which are not required to be here. we already have one edit button in this advances tab Such can be used.
>
> ### What is the expected correct behavior?
>
> needed to remove these four additional buttons from this Advance tab. It doesn't have to be here,
>
> ### Relevant logs and/or screenshots
>
> image.jpg

### Logging and error reporting infrastructure middleware - 409

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~discussion ~enhancement ~"release:[Major]" ~specification:approved ~suggesions= |
| **Start Date** | Feb 25, 2020                                                 |
| **Due Date**   | Feb 27, 2020                                                 |

#### Transcript

> Functional requirements
>
> All commands and queries are logged automatically by the middleware behind the scenes, setting up an environment where a maximum of 1 function call is needed per command or query in order to perform logging and error reporting business. The NorthwindTraders GitHub repo already presents a great example of this in their application layer Common Behaviors.
> Application commands and queries are logged directly into our GCP hosted Stackdriver Logging and Error Reporting. See official google documentation here for .NET Stackdriver SDK for both Logging and Error Reporting.
>
> All logged/reported errors must store and allow project maintainers to see the full stack-trace where the error occurred.
>
>
> Application MUST allow sysadmins and DevOps engineers to provide Stackdriver configuration values (as per google documentation) via System Environment Variables.
>
> Engineers and Sysadmins must collaborate to decide on a good configuration structure to allow our middleware to support.
>
>
> All logging and error reporting middleware can easily be ported to new monorepo project which follows same project structure as current version of NorthwindTraders cleanarch repo on github
>
> 
>
> Test cases
>
> Any HTTP request to our REST API is logged and appears in GCP stack-driver logs
> Any FAILED Command or Query appears in GCP Error Reporting control panel
> Log and error reporting users must be able to filter log entries based on:
>
> Client (end-user for which an instance of the application is deployed)
> User (who tried to run a command or query)
> Created date/time range
> Environment (production/staging/devtest)
> Log level
> Application/Service ID
> Application/Service name
> Release/Version ID
> Service Workload cluster name
> Service Workload cluster region
> Service Workload Name/ID
> Transaction/Command/Query Request Payload
> Transaction/Command/Query Response Payload

### Editing voucher date is not updating the date in the voucher details control panel - 408

| **Detail**     | Value                                            |
| -------------- | ------------------------------------------------ |
| **Labels**     | ~bug ~confirmed ~module:finance ~release:[patch] |
| **Start Date** | Feb 25, 2020                                     |
| **Due Date**   | Feb 27, 2020                                     |

#### Transcript

> Steps to reproduce
>
> Go to review.cha-net.org.
> Open voucher Control Panel
> Open a voucher details
> edit voucher date.
>
>
> What is the current bug behavior?
> user complain in the service desk #406 (closed).
> Editing voucher date is updating the date in the voucher control panel but it is not updating the voucher date in the voucher details control panel.
>
> What is the expected correct behavior?
> Editing voucher date should show the edited date in the voucher control panel and in the voucher details control panel.
>
> Relevant log/or screenshots
>
> video.mp4

### When the transactions are saved, all the existing transactions disappeared in the production system of the end-user - 404

| **Detail**     | Value                                                     |
| -------------- | --------------------------------------------------------- |
| **Labels**     | ~bug ~confirmed ~module:finance ~release:[patch] ~support |
| **Start Date** | Feb 25, 2020                                              |
| **Due Date**   | Feb 27, 2020                                              |

#### Transcript

> Steps to reproduce
>
> Go to manage.cha-net.org.
> Open voucher Control Panel
> Open a voucher details
> add a transaction.
>
>
> What is the current bug behavior?
> End-user complaint,
> The Transactions were added in Voucher #802 in manage.cha-net.org. When the Save button is clicked, all the transactions disappeared. However, when we export the voucher, all the transactions are there in the exported PDF. This has been checked with Abdul Salam in the manage.cha-net.org.
>
> What is the expected correct behavior?
> It should show all the existing transactions and the newly added transaction in the transaction list and pdf.

### Validation and UI state bugs in appraisals - 398

| **Detail**     | Value                                                     |
| -------------- | --------------------------------------------------------- |
| **Labels**     | ~bug ~confirmed ~module:finance ~release:[patch] ~support |
| **Start Date** | Feb 25, 2020                                              |
| **Due Date**   | Feb 27, 2020                                              |

#### Transcript

> Issue 1 reproduce steps
>
> Open the appraisal tab.
> Click on add appraisal then click the save button.
>
>
> What is the current bug behavior?
> No validation fired on fields Score and Remarks.
>
> What is the correct expected behavior?
> There should be a form validation message.
>
> Issue 2 reproduce steps
>
> Click on edit appraisal detail
> Delete appraisal training, strong points, weak points. then add new Training, add strong points, add appraisal team members and add weak points.
>
>
> What is the current bug behavior?
> Once we delete the existing records of Appraisal training, Strong points, Weak points, and appraisal team members. and then adding new records in Appraisal training, Strong points, Weak points, and appraisal team members, the deleted records also displayed in the list.
>
> What is the correct expected behavior?
> For each deleted Appraisal training, Strong points, Weak points and Appraisal team members, the deleted records should not be displayed in the list.

### New table structure for employee listing - 363

| **Detail**     | Value                                                   |
| -------------- | ------------------------------------------------------- |
| **Labels**     | ~doing ~To Do ~enhancement ~release:[patch] ~module:hrm |
| **Start Date** | Feb 25, 2020                                            |
| **Due Date**   | Feb 27, 2020                                            |

#### Transcript

> Users had requested via [this service desk issue #344](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/344) to change the fields that are displayed for employee listing in HRM Resources Control Panel.
>
> Screenshot:
>
> image.png

### Make sector and program selection in project advance details into multi-select dropdown - 227

| **Detail**     | Value                                         |
| -------------- | --------------------------------------------- |
| **Labels**     | ~enhancement ~release:[major] ~module:project |
| **Start Date** | Feb 25, 2020                                  |
| **Due Date**   | Feb 27, 2020                                  |

#### Transcript

> The project management department had requested via service desk ticket [#374]() to make the sector and program multi-select dropdown.
>
> image.png
>
> ### Current Implementation
>
> Right now, the sectors and programs can be added in the Advance Project Details, and can select only one of the added sectors, and programs. For those project which are related to multiple sectors and programs, they cannot select more than one sectors and programs.
>
> ### Requested Changes/Enhancement
>
> The end-user is stating that each project can be related to multiple sectors, and programs. Thus, making the field of Programs and Sector as multiple selection will solve the problem.
>
> ### Test Cases
>
> 1. Add Sectors
> 2. Add Programs
> 3. Select Multiple Programs
> 4. Deselect Multiple Programs
> 5. Select Multiple Sectors
> 6. Deselect Multiple Sectors

### Develop infrastructure needed to create Dari PDF exports - 414

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~discussion ~release:[major] ~documentation ~specification:approved ~proposal:approved |
| **Start Date** | Feb 25, 2020                                                 |
| **Due Date**   | Feb 27, 2020                                                 |

#### Transcript

> ### Problems to solve
>
> As of now, we cannot progress the development of functionality that requires the generation of PDF export documents with Dari/Farsi text in them. We need to first **develop** a dedicated infrastructure layer middleware that allows effective creation of PDF documents with Dari text in them. Once we develop such infrastructure, we will be able to continue progress on the outstanding PDF export document generators that need this, namely (as of now):
>
> 1. Salary pension PDF export tracked via issue [#360]()
> 2. Salary slip PDF export tracked via issue [#228]()
>
> ### Goals
>
> - Reusable infrastructure that can allow us to easily create PDF Export documents with Dari/Farsi text in them
> - Continue development on outstanding PDF Generator functionality
>
> ### Proposal
>
> Addition of new infrastructure middleware service that can be injected into any distinct **command** or **query** that deals with generating PDF files.

## Closing issues/tasks

### No province is selected in employee form, but it showing two City/Village/Districts in the production system - 413

| **Detail**     | Value                                            |
| -------------- | ------------------------------------------------ |
| **Labels**     | ~bug ~Confirmed ~"module:hrm" ~"release:[Patch]" |
| **Start Date** | Feb 25, 2020                                     |
| **Due Date**   | Feb 27, 2020                                     |

#### Transcript

> ### Reproduce steps
>
> 1. Opened HRM resources control panel
> 2. Click on add new employee
> 3. Click on city/village /district
>
> ### What is the current bug behavior?
>
> It shows two city/village/district without selecting country or province end user had requested through this service desk [#367 (comment 292961312)](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/367#note_292961312)
>
> ### What is the correct expected behavior?
>
> It should not show the city/village/district without selecting one of the provinces.
>
> ### Relevant log or/screenshots
>
> image.jpg

### Additional Edit buttons in the Advances tab - 410

| **Detail**     | Value                                            |
| -------------- | ------------------------------------------------ |
| **Labels**     | ~bug ~Confirmed ~"module:hrm" ~"release:[Patch]" |
| **Start Date** | Feb 25, 2020                                     |
| **Due Date**   | Feb 27, 2020                                     |

#### Transcript

> ### Steps to reproduce
>
> 1. Go to the HR, Employee List.
> 2. Go to one of the Employee Control panel.
> 3. Go to the ADVANCES tab
> 4. Add Advance Request
>
> ### What is the current bug behavior?
>
> There are four extra edit buttons in the Advance table which are not required to be here. we already have one edit button in this advances tab Such can be used.
>
> ### What is the expected correct behavior?
>
> needed to remove these four additional buttons from this Advance tab. It doesn't have to be here,
>
> ### Relevant logs and/or screenshots
>
> image.jpg

### Logging and error reporting infrastructure middleware - 409

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~discussion ~enhancement ~"release:[Major]" ~specification:approved ~suggesions= |
| **Start Date** | Feb 25, 2020                                                 |
| **Due Date**   | Feb 27, 2020                                                 |

#### Transcript

> Functional requirements
>
> All commands and queries are logged automatically by the middleware behind the scenes, setting up an environment where a maximum of 1 function call is needed per command or query in order to perform logging and error reporting business. The NorthwindTraders GitHub repo already presents a great example of this in their application layer Common Behaviors.
> Application commands and queries are logged directly into our GCP hosted Stackdriver Logging and Error Reporting. See official google documentation here for .NET Stackdriver SDK for both Logging and Error Reporting.
>
> All logged/reported errors must store and allow project maintainers to see the full stack-trace where the error occurred.
>
>
> Application MUST allow sysadmins and DevOps engineers to provide Stackdriver configuration values (as per google documentation) via System Environment Variables.
>
> Engineers and Sysadmins must collaborate to decide on a good configuration structure to allow our middleware to support.
>
>
> All logging and error reporting middleware can easily be ported to new monorepo project which follows same project structure as current version of NorthwindTraders cleanarch repo on github
>
> 
>
> Test cases
>
> Any HTTP request to our REST API is logged and appears in GCP stack-driver logs
> Any FAILED Command or Query appears in GCP Error Reporting control panel
> Log and error reporting users must be able to filter log entries based on:
>
> Client (end-user for which an instance of the application is deployed)
> User (who tried to run a command or query)
> Created date/time range
> Environment (production/staging/devtest)
> Log level
> Application/Service ID
> Application/Service name
> Release/Version ID
> Service Workload cluster name
> Service Workload cluster region
> Service Workload Name/ID
> Transaction/Command/Query Request Payload
> Transaction/Command/Query Response Payload

### Editing voucher date is not updating the date in the voucher details control panel - 408

| **Detail**     | Value                                            |
| -------------- | ------------------------------------------------ |
| **Labels**     | ~bug ~confirmed ~module:finance ~release:[patch] |
| **Start Date** | Feb 25, 2020                                     |
| **Due Date**   | Feb 27, 2020                                     |

#### Transcript

> Steps to reproduce
>
> Go to review.cha-net.org.
> Open voucher Control Panel
> Open a voucher details
> edit voucher date.
>
>
> What is the current bug behavior?
> user complain in the service desk #406 (closed).
> Editing voucher date is updating the date in the voucher control panel but it is not updating the voucher date in the voucher details control panel.
>
> What is the expected correct behavior?
> Editing voucher date should show the edited date in the voucher control panel and in the voucher details control panel.
>
> Relevant log/or screenshots
>
> video.mp4

### When the transactions are saved, all the existing transactions disappeared in the production system of the end-user - 404

| **Detail**     | Value                                                     |
| -------------- | --------------------------------------------------------- |
| **Labels**     | ~bug ~confirmed ~module:finance ~release:[patch] ~support |
| **Start Date** | Feb 25, 2020                                              |
| **Due Date**   | Feb 27, 2020                                              |

#### Transcript

> Steps to reproduce
>
> Go to manage.cha-net.org.
> Open voucher Control Panel
> Open a voucher details
> add a transaction.
>
>
> What is the current bug behavior?
> End-user complaint,
> The Transactions were added in Voucher #802 in manage.cha-net.org. When the Save button is clicked, all the transactions disappeared. However, when we export the voucher, all the transactions are there in the exported PDF. This has been checked with Abdul Salam in the manage.cha-net.org.
>
> What is the expected correct behavior?
> It should show all the existing transactions and the newly added transaction in the transaction list and pdf.

### Validation and UI state bugs in appraisals - 398

| **Detail**     | Value                                                     |
| -------------- | --------------------------------------------------------- |
| **Labels**     | ~bug ~confirmed ~module:finance ~release:[patch] ~support |
| **Start Date** | Feb 25, 2020                                              |
| **Due Date**   | Feb 27, 2020                                              |

#### Transcript

> Issue 1 reproduce steps
>
> Open the appraisal tab.
> Click on add appraisal then click the save button.
>
>
> What is the current bug behavior?
> No validation fired on fields Score and Remarks.
>
> What is the correct expected behavior?
> There should be a form validation message.
>
> Issue 2 reproduce steps
>
> Click on edit appraisal detail
> Delete appraisal training, strong points, weak points. then add new Training, add strong points, add appraisal team members and add weak points.
>
>
> What is the current bug behavior?
> Once we delete the existing records of Appraisal training, Strong points, Weak points, and appraisal team members. and then adding new records in Appraisal training, Strong points, Weak points, and appraisal team members, the deleted records also displayed in the list.
>
> What is the correct expected behavior?
> For each deleted Appraisal training, Strong points, Weak points and Appraisal team members, the deleted records should not be displayed in the list.

### Make sector and program selection in project advance details into multi-select dropdown - 227

| **Detail**     | Value                                         |
| -------------- | --------------------------------------------- |
| **Labels**     | ~enhancement ~release:[major] ~module:project |
| **Start Date** | Feb 25, 2020                                  |
| **Due Date**   | Feb 27, 2020                                  |

#### Transcript

> The project management department had requested via service desk ticket [#374]() to make the sector and program multi-select dropdown.
>
> image.png
>
> ### Current Implementation
>
> Right now, the sectors and programs can be added in the Advance Project Details, and can select only one of the added sectors, and programs. For those project which are related to multiple sectors and programs, they cannot select more than one sectors and programs.
>
> ### Requested Changes/Enhancement
>
> The end-user is stating that each project can be related to multiple sectors, and programs. Thus, making the field of Programs and Sector as multiple selection will solve the problem.
>
> ### Test Cases
>
> 1. Add Sectors
> 2. Add Programs
> 3. Select Multiple Programs
> 4. Deselect Multiple Programs
> 5. Select Multiple Sectors
> 6. Deselect Multiple Sectors

## Excel Export functionality for Employee Payroll - 418

| **Detail**     | Value                                     |
| -------------- | ----------------------------------------- |
| **Labels**     | ~enhancement ~release:[minor] ~module:hrm |
| **Start Date** | Feb 25, 2020                              |
| **Due Date**   | Feb 27, 2020                              |

#### Transcript

> ### Functional requirements
>
> 1. The Employee Payroll Export Functionality should be in the Employee Control Panel.
>
> 2. When the employees are selected, and the button of the Payroll Export is clicked, then the payroll information of the selected employee will be exported. **You cannot export the employee payroll of different offices at once.**
>
> 3. Following is the table information:
>
>    | S#          | Emp#   | Name                   | Designation       | Sex  | Curr | Office | BasicPay  | Atd  | Abs  | Salary    | Bonus | Gross     | Cap.B   | Security | S.Tax    | Fine | Adv  | Pension   | NetSalary | Project | Job   | BLine | Percentage |
>    | ----------- | ------ | ---------------------- | ----------------- | ---- | ---- | ------ | --------- | ---- | ---- | --------- | ----- | --------- | ------- | -------- | -------- | ---- | ---- | --------- | --------- | ------- | ----- | ----- | ---------- |
>    | 1           | E00023 | Abdul  Rahman          | Transport  Member | M    | AFG  | KBL    | 36471     | 166  | 0    | 36471     | 0     | 35971     | 789.67  | 394.83   | 2447.0   | 500  | 200  | 1596.195  | 30043.3   | 00475   | 00100 | G3171 | 100        |
>    | 2           | E00047 | Mohammad  Kabir Jalali | Finance  Manager  | M    | AFG  | KBL    | 36250.32  | 160  | 0    | 36250.32  | 0     | 36250.32  | 189.52  | 94.76    | 4586.06  | 0    |      | 1631.26   | 29748.7   | 00429   | 00001 | FH005 | 24         |
>    | 3           | E00047 | Mohammad  Kabir Jalali | Finance  Manager  | M    | AFG  | KBL    | 114792.68 | 160  | 0    | 114792.68 | 0     | 114792.68 | 600.15  | 300.07   | 14522.54 | 0    |      | 5165.67   | 94204.3   | 00475   | 00100 | G3171 | 76         |
>    | 4           | E12222 | Abdul  Mateen          | Accountant        | M    | AFG  | KBL    | 11500     | 160  | 0    | 11500     | 0     | 11500     | 181.62  | 90.81    | 897      | 0    |      | 517.5     | 9813.1    | 00429   | 00001 | FH003 | 23         |
>    | 5           | E12222 | Abdul  Mateen          | Accountant        | M    | AFG  | KBL    | 12500     | 160  | 0    | 12500     | 0     | 12500     | 197.42  | 98.71    | 975      | 0    |      | 562.5     | 10666.4   | 00470   |       | CG091 | 25         |
>    | 6           | E12222 | Abdul  Mateen          | Accountant        | M    | AFG  | KBL    | 26000     | 160  | 0    | 26000     | 0     | 26000     | 410.63  | 205.31   | 2028     | 0    |      | 1170      | 22186.1   | 00475   | 00100 | G3171 | 52         |
>    | Grand Total |        |                        |                   |      |      |        | 237514    | 966  | 0    | 237514    | 0     | 237014    | 2369.01 | 1184.49  | 25455.6  | 500  |      | 10643.125 | 196661.78 |         |       |       |            |
>
> 4. If an employee is getting percentages of his/her salary from different project, for each percentage one row should be created.
>
> 5. Field descriptions:
>
>    1. **BasicPay** = Active Base Salary * Percentage / 100
>    2. **Atd** = The number of hours which is marked as present
>    3. **Abs** = The number of hours which is marked as absent
>    4. **Salary** = BasicPay - [(atd * Active Hourly Rate) * Percentage / 100]
>    5. **Bonus** = Total number of bonuses
>    6. **Gross** = Total Gross Salary
>    7. **Cap.B** = Capacity Building * Percentage / 100
>    8. **Security** = Security * Percentage / 100
>    9. **S.Tax** = S.Tax * Percentage / 100
>    10. **Fine** = Total Number of fines
>    11. **Adv** = Advance Recovery * Percentage / 100
>    12. **Pension** = Pension * Percentage / 100
>    13. **Net Salary** = Total Net Salary * Percentage / 100
>    14. **Project** = Project Code which the employee is getting salary from
>    15. **BLine** = Budget line which the employee is getting salary from
>    16. **Percentage** = The amount of percentage of his salary which is getting from the project
>
> 6. The currency of the payroll will be AFG
>
> 7. The office is the office which you exported the payroll
>
> 8. Payment Date is the date on which the payroll got exported
>
> 9. Attached is a sample of exported payroll format. [Payroll_format__1_.xlsx]()
>
> ### Test cases
>
> 1. Payroll gets exported for the selected employees.
> 2. The calculations are correct according to the specifications.
> 3. For each of analytical info percentages that currently exists for the employee, one row gets created.
> 4. The Format of the payroll is according to the attached payroll format.

## Currently outstanding items (at the time of release)

This section lists out transcripts of all the remaining open tasks that must be completed in further milestones. For the sake of keeping this document brief, all transcripts will be provided as download links.

### Proposals

1. [Operations voucher filtering in Journal - #130](https://drive.google.com/open?id=10PssklfbnJSroGUokgM6Sb2k9JfaOIZ2)
2. [[FEATURE PROPOSAL] Reworked accounting panels and dashboards - #190](https://drive.google.com/open?id=10gFzfW74r9W4ssFOBVI586aUWpryqW7E)
3. [Inventory asset tracking enhancements - #181](https://drive.google.com/open?id=117CaXWN8mDtmmaNGa8UFpa1-Cr4g4tgZ)
4. [Improved forms for transport related items in logistics requested items - #180](https://drive.google.com/open?id=10aoO3CTNyrDaREiZx0V3NliFUPSx_mls)
5. [Enhancement request for Logistic request currency selection - #171](https://drive.google.com/open?id=10Yp-aLephq4pd8m9k6GIoU1_wWGr3g9_)
6. [[CHANGE PROPOSAL REQUEST] Exchange gain/loss consolidation enhancements - #160](https://drive.google.com/open?id=10d_aEDC7kC8LKETOzTEhMuuAjDOimaw5)
7. [Financial Operations Integration with related operations - #389](https://drive.google.com/open?id=10WdZ6bZK6ZAslKUgpDNnYVr263O-DR1I)

### Approved enhancements

1. [HRM audit event logging coverage - #390](https://drive.google.com/open?id=10R62htr9bQkO10u1UipNdEow65QqrZ-x)
2. [Updated implementation of technical questions total mark in interview form - #229](https://drive.google.com/open?id=10eU0Mnib-1zg5Rul6KVc1dgjEnTzyimX)
3. [Salary slip PDF export for approved salaries - #228](https://drive.google.com/open?id=10xFmN7A_e-NQlT_YyUpnpjg1mR9Vs9o6)
4. [Enhancement to transactions - #416](https://drive.google.com/open?id=11HYOkXJu6-1_992Rqi2k-pGP0IgG8dyn)
5. [Some of the fields in the Purchase Form should be made searchable - #417](https://drive.google.com/open?id=11IKGTuFLisSFuth-vdIXToz75PpCMdF9)
6. [Enhancement request in purchase advanced filter - #418](https://drive.google.com/open?id=11MZhniCgDtjl0-wuGoioyJgSOmm7RfHh&authuser=salam.faeq@edgsolutions.com&usp=drive_fs)
7. [Enhancement to the deibt and credit columns order - #427](https://drive.google.com/open?id=11NqTfAeZJXxcN1Mtt5mHdqwNY9Ke30jJ)
8. [Enhancement request to the search dropdown of account, project and budget line in the transaction form - #426](https://drive.google.com/open?id=11RPucP1payk-8RTLMSYjGItu9qD9vOnh)
9. [Wrong Calculation for Salary, and Gross Columns in the Excel Payroll Export - #428](https://drive.google.com/open?id=1rM1PdfCWbZ8WDd97p2tK-RxIuFX_1HN0)

### Bug fixes

1. [Salary Pension PDF export is broken - #360](https://drive.google.com/open?id=10eYG_67kHt39uZsuHcqYWdwWoPIah2YM)
2. [Cannot upload any files to the proposal in project details control panel - #420](https://drive.google.com/open?id=11CE_uPlGexS_KopTAOaqSXI4nMNTR7SC)
