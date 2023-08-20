using Business;
using Business.Data;
using Business.Model;
using Business.Services;
using Moq;
using System;
using Xunit;

namespace Tests
{
    public class AppraisalControllerTests
    {
        private Mock<IRequestDataService> requestDataServiceMock;
        private Mock<IPermissionsService> permissionsServiceMock;
        private Mock<IDatabase> dataBaseMock;
        
        public AppraisalControllerTests()
        {
            this.requestDataServiceMock = new Mock<IRequestDataService>();
            this.permissionsServiceMock = new Mock<IPermissionsService>();
            this.dataBaseMock = new Mock<IDatabase>();
        }

        [Fact]
        public void When_PermissionService_Returns_True_Then_AddAppraisal_Returns_SuccessResult()
        {
            //arrange
            Appraisal newAppraisal = DummyAppraisal();

            var user = new User()
            {
                IsAdmin = true
            };

            this.requestDataServiceMock.Setup(x => x.CurrentUser).Returns(user);
            this.permissionsServiceMock.Setup(x => x.UserHasAddAppraisalPermissions(user)).Returns(true);

            AppraisalController appraisalsController = new AppraisalController(this.requestDataServiceMock.Object, this.permissionsServiceMock.Object, this.dataBaseMock.Object);

            //act
            JsonResultDummy result = appraisalsController.Add(newAppraisal);

            //assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Appraisal successfully added.", result.Message);
        }

        [Fact]
        public void When_PermissionService_Returns_False_Then_AddAppraisal_Returns_FailureResult()
        {
            //arrange
            Appraisal newAppraisal = DummyAppraisal();

            var user = new User()
            {
                IsAdmin = true
            };

            this.requestDataServiceMock.Setup(x => x.CurrentUser).Returns(user);
            this.permissionsServiceMock.Setup(x => x.UserHasAddAppraisalPermissions(user)).Returns(false);

            AppraisalController appraisalsController = new AppraisalController(this.requestDataServiceMock.Object, this.permissionsServiceMock.Object, this.dataBaseMock.Object);

            //act
            JsonResultDummy result = appraisalsController.Add(newAppraisal);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Appraisal was not added as current user does not have appropriate admin permissions. Please check account and database settings.", result.Message);
        }

        [Fact]
        public void When_Exception_Thrown_Then_AddAppraisal_Returns_FailureResult()
        {
            //arrange
            Appraisal newAppraisal = DummyAppraisal();

            var user = new User()
            {
                IsAdmin = true
            };

            this.requestDataServiceMock.Setup(x => x.CurrentUser).Returns(user);
            this.permissionsServiceMock.Setup(x => x.UserHasAddAppraisalPermissions(user)).Throws(new Exception());

            AppraisalController appraisalsController = new AppraisalController(this.requestDataServiceMock.Object, this.permissionsServiceMock.Object, this.dataBaseMock.Object);

            //act
            JsonResultDummy result = appraisalsController.Add(newAppraisal);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("An unexpected error occurred please try again, if the problem persists please contact support.", result.Message);
        }

        private Appraisal DummyAppraisal()
        {
            return new Appraisal()
            {
                AppraisalID = 0,
                AppraiserID = 123,
                bAutoClosed = false,
                bDeleted = false,
                bHiddenFromAppraisee = false,
                dAutoClosed = null,
                ProcessstepID = 434,
                SignOffID = 87652,
                dDue = DateTime.UtcNow.AddDays(24),
                dCycleDate = DateTime.UtcNow.AddDays(-5),
                Title = "a third appraisal, but updated",
                UseGroupID = 4587,
                UserID = 7895
            };
        }
    }
}
