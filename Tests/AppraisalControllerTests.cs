using Business;
using Business.Data;
using Business.Model;
using Business.Services;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Tests
{
    public class AppraisalControllerTests
    {
        [Fact]
        public void when_adding_an_appraisal_as_an_admin__then_it_is_passed_to_the_database_for_storage()
        {
            //arrange
            var newAppraisal = DummyAppraisal();

            var user = new User
            {
                IsAdmin = true
            };

            var mockRequest = new Mock<IRequestDataService>();
            mockRequest.Setup(x => x.CurrentUser).Returns(user);

            var mockDatabase = new Mock<IDatabase>();
            var controller = new AppraisalController(mockDatabase.Object, mockRequest.Object, new SettingsService());

            //act
            controller.Add(newAppraisal);

            //assert
            mockDatabase.Verify(x => x.AddNewAppraisal(It.Is<Appraisal>(y => y == newAppraisal)));
        }

        [Fact]
        public void when_adding_an_appraisal_as_a_non_admin__then_an_error_is_returned_to_the_user()
        {
            //arrange
            var newAppraisal = DummyAppraisal();

            var user = new User
            {
                IsAdmin = false
            };

            var mockRequest = new Mock<IRequestDataService>();
            mockRequest.Setup(x => x.CurrentUser).Returns(user);

            var mockDatabase = new Mock<IDatabase>();
            var controller = new AppraisalController(mockDatabase.Object, mockRequest.Object, new SettingsService());

            //act
            var result = controller.Add(newAppraisal);

            //assert
            Assert.Equal(result.IsSuccess, false);
            Assert.Equal(result.Message, "Permission denied, user must be an admin to add an appraisal");
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

        private Database Database()
        {
            return new Database();
        }
    }
}
