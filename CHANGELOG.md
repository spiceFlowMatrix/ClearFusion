# Change Log

## [ Version  [2.1](https://gitlab.com/edgsolutions-engineering/clear-fusion/-/milestones/11) ] - `(20-Feb-2017)`

## Bug Fixes

## 1. Editing employee requires to re-enter the Password and select City/Village/District [#402](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/401)

#### Issue Transcript
> #### If the password and city/village/district are not entered/selected, then it should require us to fill it up, but if they are added and saved, it should not ask us to enter them every time while editing employee details.

#### commits
>  #### [ce38a011d71b90f8d44517914873f9e53f901616](https://gitlab.com/edgsolutions-engineering/clear-fusion/-/merge_requests/520/diffs?commit_id=ce38a011d71b90f8d44517914873f9e53f901616)

## 2. Cannot select more than one day as weekend [#400](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/400)

#### Issue Transcript
> #### All the selected days for the weekend should be saved as weekend

#### commits
> #### [27c3ad1bca15565ee3631b0abe1092c5a9b38a75](https://gitlab.com/edgsolutions-engineering/clear-fusion/-/commit/27c3ad1bca15565ee3631b0abe1092c5a9b38a75)

## 3. The district dropdown are not getting cascaded from the province dropdown in the production system of the end-user [#399](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/399)

#### Issue Transcript
> #### The district dropdown should be cascaded from the province dropdown correctly. When we select provinces, all the districts of the selected province should be listed in the district dropdown.

#### commits
> #### No commits  

#### Data mapping
> #### Due to wrong data mapping in province with some district in databse in production corrected that.






## 4. It shows two edit buttons in purchase procurement control panel [#393](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/393)

#### Issue Transcript
> #### There should be one edit button for each existing purchases.

#### commits
> #### [a789d0e1ffd659b01685af93f9f4783417c51a31](https://gitlab.com/edgsolutions-engineering/clear-fusion/-/merge_requests/509/diffs?commit_id=a789d0e1ffd659b01685af93f9f4783417c51a31)

## 5. Timeout on HTTP requests for file uploads should be increased. [#376](https://gitlab.com/edgsolutions-engineering/clear-fusion/issues/376)

#### Issue Transcript
> #### Due to the internet limitations on end-users, they need a longer time to be able to successfully complete uploads for larger files. However, increasing the timeout on such requests can add performance problems for the API. The development team needs to do some experiments on-site with end-users so as to effectively identify a good timeout to set for file uploads. After an acceptable timeout is identified and agreed upon, the API will be updated accordingly..

#### commits
> #### [912a7b54a9815a56d0036c97f3dd084d0b01e811](https://gitlab.com/edgsolutions-engineering/clear-fusion/-/merge_requests/525/diffs?commit_id=912a7b54a9815a56d0036c97f3dd084d0b01e811)
