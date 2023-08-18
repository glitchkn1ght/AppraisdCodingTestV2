﻿# Appraisd coding test

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


Jams Notes
added readonly property to class properties
put changes on master which i wouldn't normally do
skinny controllers fat services
constructor null checks and appropriate tests.
logs.
validate appraisal content

Problems
shouldn't be checking the dependancies, e.g. if appraisal has been added to the database as that's not what a unit test is for
shouldn't be initalizing actual instances of dependencies, use mocks. 
