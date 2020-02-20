## Original planning details

These are all the planning details at the start date of the milestone.

| Planning Detail Type | Planning Detail Value |
| -------------------- | --------------------- |
| **Version**          | v2.1                  |
| **Start Date**       | 18th February, 2020   |
| **Due Date**         | 20th February, 2020   |

## Closing planning details

These are all the planning related details at the end of the milestone when it was completed and delivered.

| Planning Detail Type | Planning Detail Value                                        |
| -------------------- | ------------------------------------------------------------ |
| **Version**          | v2.1                                                         |
| **Start Date**       | 18th February, 2020                                          |
| **Due Date**         | N/A                                                          |
| **Change history**   | [See this section of report](#issues-manifest-history) for detailed log of issue tracking throughout the milestone |

## Planned issues/tasks

These are all the issues/tasks that were planned for delivery at the beginning/start date of the release milestone:

### Editing employee requires to re-enter the Password and select City/Village/District - 401

| **Detail**     | Value                                          |
| -------------- | ---------------------------------------------- |
| **Labels**     | ~bug ~Confirmed ~"module:hrm" ~"release:minor" |
| **Start Date** | Feb 18, 2020                                   |
| **Due Date**   | Feb 20, 2020                                   |

#### Transcript

> ### Steps to reproduce
>
> 1. Open Employee Control Panel
> 2. Open an employee details
> 3. Edit an employee
> 4. Click on save button without changing anything
>
> ### What is the current bug behavior?
>
> Every time when we are trying to edit the employee details, it requires us to enter the password and select City/Village/District.
>
> ### What is the expected correct behavior?
>
> If the password and city/village/district are not entered/selected, then it should require us to fill it up, but if they are added and saved, it should not ask us to enter them every time while editing employee details.
>
> ### Relevant logs and/or screenshots
>
> (image.png)

### Cannot select more than one day as weekend - 400

| **Planning Detail Type** | Planning Detail Value                          |
| ------------------------ | ---------------------------------------------- |
| **Labels**               | ~bug ~Confirmed ~"module:hrm" ~"release:minor" |
| **Start Date**           | Feb 18, 2020                                   |
| **Due Date**             | Feb 20, 2020                                   |

#### Transcript

> 1. ### Steps to reproduce
>
>    1. Go to the HR Configuration page
>    2. Go to the holiday
>    3. Click on configure weekend.
>    4. Select two days as the weekend of the day
>    5. Click save
>
>    ### What is the current bug behavior?
>
>    When we select multiple days for the weekend and save them, only one day gets selected.
>
>    ### What is the expected correct behavior?
>
>    All the selected days for the weekend should be saved as weekend.
>
>    ### Relevant logs and/or screenshots
>
>    (image.png)

### The district dropdown are not getting cascaded from the province dropdown in the production system of the end-user - 399

| **Planning Detail Type** | Planning Detail Value                              |
| ------------------------ | -------------------------------------------------- |
| **Labels**               | ~bug ~Confirmed ~"module:project" ~"release:minor" |
| **Start Date**           | Feb 18, 2020                                       |
| **Due Date**             | Feb 20, 2020                                       |

#### Transcript

> This has been checked with the end-user in a meeting held on 2/16/2020.
>
> ### Steps to reproduce
>
> 1. Go to manage.cha-net.org.
> 2. Open a project.
> 3. Open the advance details of the project.
> 4. Select Provinces
> 5. Check districts
>
> ### What is the current *bug* behavior?
>
> When we select a province or provinces, the districts of the selected provinces are not getting listed in the district dropdown. However, for a few of the provinces, some of their districts are getting listed, but a number of the United States' districts are also among them.
>
> ### What is the expected *correct* behavior?
>
> The district dropdown should be cascaded from the province dropdown correctly. When we select provinces, all the districts of the selected province should be listed in the district dropdown.

### It shows two edit buttons in purchase procurement control panel - 393

| **Planning Detail Type** | Planning Detail Value                                |
| ------------------------ | ---------------------------------------------------- |
| **Labels**               | ~bug ~Confirmed ~"module:inventory" ~"release:patch" |
| **Start Date**           | Feb 18, 2020                                         |
| **Due Date****           | Feb 20, 2020                                         |

#### Transcript

> ### Reproduce steps
>
> 1. Opened purchase procurement control panel
> 2. There are tow edit buttons for editing purchases.
>
> ### What is the current bug behavior?
>
> It shows tow edit button for the existing purchases.
>
> ### What is the correct expected behavior?
>
> There should be one edit button for each existing purchases.
>
> (image.png)

## Closing issues/tasks

### Cannot select more than one day as weekend - 400

| **Planning Detail Type** | Planning Detail Value                          |
| ------------------------ | ---------------------------------------------- |
| **Labels**               | ~bug ~Confirmed ~"module:hrm" ~"release:minor" |
| **Start Date**           | Feb 18, 2020                                   |
| **Due Date**             | Feb 20, 2020                                   |

#### Transcript

> 1. ### Steps to reproduce
>
>    1. Go to the HR Configuration page
>    2. Go to the holiday
>    3. Click on configure weekend.
>    4. Select two days as the weekend of the day
>    5. Click save
>
>    ### What is the current bug behavior?
>
>    When we select multiple days for the weekend and save them, only one day gets selected.
>
>    ### What is the expected correct behavior?
>
>    All the selected days for the weekend should be saved as weekend.
>
>    ### Relevant logs and/or screenshots
>
>    (image.png)

### The district dropdown are not getting cascaded from the province dropdown in the production system of the end-user - 399

| **Planning Detail Type** | Planning Detail Value                              |
| ------------------------ | -------------------------------------------------- |
| **Labels**               | ~bug ~Confirmed ~"module:project" ~"release:minor" |
| **Start Date**           | Feb 18, 2020                                       |
| **Due Date**             | Feb 20, 2020                                       |

#### Transcript

> This has been checked with the end-user in a meeting held on 2/16/2020.
>
> ### Steps to reproduce
>
> 1. Go to manage.cha-net.org.
> 2. Open a project.
> 3. Open the advance details of the project.
> 4. Select Provinces
> 5. Check districts
>
> ### What is the current *bug* behavior?
>
> When we select a province or provinces, the districts of the selected provinces are not getting listed in the district dropdown. However, for a few of the provinces, some of their districts are getting listed, but a number of the United States' districts are also among them.
>
> ### What is the expected *correct* behavior?
>
> The district dropdown should be cascaded from the province dropdown correctly. When we select provinces, all the districts of the selected province should be listed in the district dropdown.

### It shows two edit buttons in purchase procurement control panel - 393

| **Planning Detail Type** | Planning Detail Value                                |
| ------------------------ | ---------------------------------------------------- |
| **Labels**               | ~bug ~Confirmed ~"module:inventory" ~"release:patch" |
| **Start Date**           | Feb 18, 2020                                         |
| **Due Date****           | Feb 20, 2020                                         |

#### Transcript

> ### Reproduce steps
>
> 1. Opened purchase procurement control panel
> 2. There are tow edit buttons for editing purchases.
>
> ### What is the current bug behavior?
>
> It shows tow edit button for the existing purchases.
>
> ### What is the correct expected behavior?
>
> There should be one edit button for each existing purchases.
>
> (image.png)

### Make Office selection in HRM Resources CP into multi-select dropdown - 362

| **Planning Detail Type** | Planning Detail Value                       |
| ------------------------ | ------------------------------------------- |
| **Labels**               | ~Enhancement ~"module:hrm" ~"release:minor" |
| **Start Date**           | Feb 18, 2020                                |
| **Due Date****           | Feb 20, 2020                                |

#### Transcript

> Users had requested in service desk ticket [#344]() to be able to select multiple offices when they are in the HRM Resources Control Panel so that they can perform administrative and operational activities on large numbers of employees at a time.
>
> ### Related functionality
>
> This change can most definitely affect the different actions available to users in the control panel. We must see if this change can break any of the other functionality. If anything may break by this change, we must devise a solution for it and add it to the scope of this issue.

### Contract clauses configuration reworks - 365

| **Planning Detail Type** | Planning Detail Value                       |
| ------------------------ | ------------------------------------------- |
| **Labels**               | ~Enhancement ~"module:hrm" ~"release:major" |
| **Start Date**           | Feb 18, 2020                                |
| **Due Date****           | Feb 20, 2020                                |

#### Transcript

> Need to implement a reworked version of the contract clauses configuration panel in new UI as specified in [this proposal section](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/212#employee-contract-clauses).
>
> ### Current proposal transcript
>
> The organization has three different types of contracts for employees, each of which has different clauses. This control panel will have a dedicated tab for configuring all three types of contract clauses:
>
> 1. Permanent
> 2. Part-time
> 3. Probationary
>
> The clauses for each type will be held within collapsible panels that can expand to show the current values of the clauses for that type of contract within **rich text input** boxes. Administrators can change the clauses for a contract type by providing updated values in the text boxes and clicking a **Save** button that will become available in the panel once changes are made.

### Editing employee requires to re-enter the Password and select City/Village/District - 401

| **Detail**     | Value                                          |
| -------------- | ---------------------------------------------- |
| **Labels**     | ~bug ~Confirmed ~"module:hrm" ~"release:minor" |
| **Start Date** | Feb 18, 2020                                   |
| **Due Date**   | Feb 20, 2020                                   |

#### Transcript

> ### Steps to reproduce
>
> 1. Open Employee Control Panel
> 2. Open an employee details
> 3. Edit an employee
> 4. Click on save button without changing anything
>
> ### What is the current bug behavior?
>
> Every time when we are trying to edit the employee details, it requires us to enter the password and select City/Village/District.
>
> ### What is the expected correct behavior?
>
> If the password and city/village/district are not entered/selected, then it should require us to fill it up, but if they are added and saved, it should not ask us to enter them every time while editing employee details.
>
> ### Relevant logs and/or screenshots
>
> (image.png)

### Timeout on HTTP requests for file uploads should be increased - 376

| **Detail**     | Value                                                        |
| -------------- | ------------------------------------------------------------ |
| **Labels**     | ~discussion ~enhancement ~"module:project" ~"release:minor" ~"release:support" |
| **Start Date** | Feb 18, 2020                                                 |
| **Due Date**   | Feb 20, 2020                                                 |

#### Transcript

> As requested and confirmed in service desk ticket #374, end-users need to be able to upload large files and their current internet bandwidth does not allow the upload of files greater than 50MB to be completed within the HTTP Request timeout for the file upload.
> Due to the internet limitations on end-users, they need a longer time to be able to successfully complete uploads for larger files. However, increasing the timeout on such requests can add performance problems for the API. The development team needs to do some experiments on-site with end-users so as to effectively identify a good timeout to set for file uploads. After an acceptable timeout is identified and agreed upon, the API will be updated accordingly.

### Adding a new entry in the appraisal form brings back the removed entries - 405

| **Detail**     | Value                                          |
| -------------- | ---------------------------------------------- |
| **Labels**     | ~bug ~confirmed ~"module:hrm" ~"release:patch" |
| **Start Date** | Feb 18, 2020                                   |
| **Due Date**   | Feb 20, 2020                                   |

#### Transcript

> As requested and confirmed in service desk ticket #374, end-users need to be able to upload large files and their current internet bandwidth does not allow the upload of files greater than 50MB to be completed within the HTTP Request timeout for the file upload.
> Due to the internet limitations on end-users, they need a longer time to be able to successfully complete uploads for larger files. However, increasing the timeout on such requests can add performance problems for the API. The development team needs to do some experiments on-site with end-users so as to effectively identify a good timeout to set for file uploads. After an acceptable timeout is identified and agreed upon, the API will be updated accordingly.

## Issues manifest history

[Download issues manifest for milestone version v2.1](https://drive.google.com/open?id=1C8WUIZMhJEppBTpdzgY61fBD13hVJhhp)

## Currently outstanding items (at the time of release)

This section lists out transcripts of all the remaining open tasks that must be completed in further milestones. For the sake of keeping this document brief, all transcripts will be provided as download links.

### Proposals

1. [Operations voucher filtering in Journal - #130](https://drive.google.com/open?id=1DD6YDRi9kov27N4wc9kvxB_UuAe_31iU)
2. [[FEATURE PROPOSAL] Reworked accounting panels and dashboards - #190](https://drive.google.com/open?id=1CBIpgeQCPTL7CnM9JFJkPj3HtXu9wTXi)
3. [Inventory asset tracking enhancements - #181](https://drive.google.com/open?id=1C9xY-VvbTlIjSH4Ba6m8l0YiEbQcnyJ5)
4. [Improved forms for transport related items in logistics requested items - #180](https://drive.google.com/open?id=1C9oyNtSN0Ba7Z4-Oy5mtPUUFD0QI6K3E)
5. [Enhancement request for Logistic request currency selection - #171](https://drive.google.com/open?id=1C94R_Ks5hCuItlX10pt01MfV6RPd4jNQ)
6. [[CHANGE PROPOSAL REQUEST] Exchange gain/loss consolidation enhancements - #160](https://drive.google.com/open?id=1CEuoS4rIxtpyJ2MXoG2jjrVmu9GklMiq)
7. [Financial Operations Integration with related operations - #389](https://drive.google.com/open?id=1D9HdZOdXzilgLynVGgKBhAxMJfcCCUo_)

### Approved enhancements

1. [HRM audit event logging coverage - #390](https://drive.google.com/open?id=1CJC2afJb3uEiOurm6IZNX_dPVqKovBFT)
2. [New table structure for employee listing - #363](https://drive.google.com/open?id=1CUBqiSbaWjkHiYvXp__LuLmZz3AX00G5)
3. [Updated implementation of technical questions total mark in interview form - #229](https://drive.google.com/open?id=1CfBRAYiS68otTsDmPStQpI5Z6gqyLCql)
4. [Salary slip PDF export for approved salaries - #228](https://drive.google.com/open?id=1Cl9W43CEIp01KazW-yJRX2ghiuZou8zf)
5. [Make project advanced detail sector and program multi-select dropdown - #227](https://drive.google.com/open?id=1D4LN3qb-sl5EtOIVYo-jgB_KWqJoKLbS)

### Bug fixes

1. [Salary Pension PDF export is broken - #360](https://drive.google.com/open?id=1COr6kVz4anqVb4GDTb1BkBDrvFiN-nMd)
2. [Editing voucher date is not updating the date in the voucher details control panel - #408](https://drive.google.com/open?id=1-kCOzTAc3MuBlnSC1vE0iXDJbFE2PfRV)
3. [When the transactions are saved, all the existing transactions disappeared in the production system of the end-user - #404](https://drive.google.com/open?id=1-khmpMmhV5Hrgs86AuRZhxfs5ti_FJ6as)
