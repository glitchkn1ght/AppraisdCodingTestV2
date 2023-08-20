# Appraisd coding test

For this test you are asked to expand the functionality of `AppraisalController.Add`, and to add test code coverage for your expansion.  You should probably do TDD.

## The new requirement
A new setting has been added to the system - OnlySuperAdminsCanAddAppraisals.  You need to update the AppraisalController so that when this setting is set as `true` only users with the flag `IsSuperAdmin` will be allowed to add new appraisals.  

If a user tries to add an appraisal but is not a super admin then they should receive a `JsonDummyResult` with `IsSucces` as false and an appropriate error message and the database should not be changed.

The new behaviour should be as follows

| User.IsAdmin | User.IsSuperAdmin | Settings.OnlySuperAdminsCanAddAppraisals | Appraisal Is Added? |
-----------------------------------------------------------------------------------------------------
|  true        |  true             |          true                            |    true             |
|  false       |  true             |          true                            |    true             |
|  true        |  false            |          true                            |    false            |
|  false       |  false            |          true                            |    false            |
|  true        |  true             |          false                           |    true             |
|  false       |  true             |          false                           |    false            |
|  true        |  false            |          false                           |    true             |
|  false       |  false            |          false                           |    false            |


## General Notes
I'm aware i've put the changes on master. Normally i'd do a branch with the story number and raise a PR but didn't think that was part of this exercise.

## Notes on the Orginal Application Design
The controllers dependancies (requestDataService etc) should be readonly unless you intend them to be changed outside of the constructor.

The permissions checks were refactored into a separate service to follow the SRP and the general design goal to keep controllers as skinny as possible. 
It also made the behaviour easy to test via greater isolation. 

There's no logging which will make monitoring and bug tracing very difficult.

Appraisal content should have some validation before being added to the database.

It may be more a style choice but i like to have null checks arguments the constructor  eg. dependancy ?? throw new ArguementNullException(nameof(dependancy))
It just makes it a bit easier to see what dependancy bindings you have missed in complex situations.

Lack of exception handling e.g. try/catch.

## Issues with Original Unit Test Design

You're creating a external dependancy with the Database() method which goes against the principle of testing methods in isolation. 
However since there's not actual calls to this method the tests themselves don't violate this principle. 

In the first of the controller test your calling verify against the add method of the database mock. This isn't incorrect but you should be asserting the 
value of the JsonResultDummy as well. The second of the controller tests does assert on the JsonResultDummy result but doesn't verify the database mock. 
Really you should be testing the behaviour of a unit in a consistent manner. 

## Approach to the new design/tests
Im aware i could have combined some of my tests together by using more parameters but i didn't in order to keep them easy to understand going forward.
As you keep adding parameters it soon gets to the stage you're struggling to understand what scenario each one represents. It also makes it harder to keep the name
of the test meaningful.

As permissionsService simply returns true or false i've changed the failure message to something generic, this does make it slightly harder for users to diagnose the problem.
Another approach would be return a string or class from permissionsService which details of the error and would be one of the things i would change given more time. 

The database class has too much happening in it's methods. It should be seperated into a service layer for the business logic and a pure DAL layer that follows some sort of repository pattern.

Also the AddNewAppraisal method returns the appraisal but when the method is used in the controller no use is made of the returned value. I'd change this to return void if the 
appraisal object is not used for anything. 





