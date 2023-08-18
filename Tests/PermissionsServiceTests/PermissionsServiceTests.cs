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
    public class PermissionServiceTests
    {
        private Mock<ISettingsService> settingsServiceMock;
        private PermissionsService permissionService;
        
        
        public PermissionServiceTests()
        {
            this.settingsServiceMock = new Mock<ISettingsService>();
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, true)]
        public void When_Super_Admin_Setting_Enabled_And_User_IsSuperAdmin_Permission_Then_HasPermission_Returns_True(bool userIsAdmin, bool userIsSuperAdmin)
        {
            //arrange
            var settings = new Settings
            {
                OnlySuperAdminsCanAddAppraisals = true,
            };
            
            this.settingsServiceMock.Setup(x => x.Settings).Returns(settings);
            
            User user = new User
            {
                IsAdmin = userIsAdmin, 
                IsSuperAdmin = userIsSuperAdmin
            };

            this.permissionService = new PermissionsService(this.settingsServiceMock.Object);

            //act
            bool result = this.permissionService.UserHasAddAppraisalPermissions(user);

            //assert
            Assert.True(result);     
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, false)]
        public void When_Super_Admin_Setting_Enabled_And_User_IsNotSuperAdmin_Then_HasPermission_Returns_False(bool userIsAdmin, bool userIsSuperAdmin)
        {
            //arrange
            var settings = new Settings
            {
                OnlySuperAdminsCanAddAppraisals = true,
            };

            this.settingsServiceMock.Setup(x => x.Settings).Returns(settings);

            User user = new User
            {
                IsAdmin = userIsAdmin,
                IsSuperAdmin = userIsSuperAdmin
            };

            this.permissionService = new PermissionsService(this.settingsServiceMock.Object);

            //act
            bool result = this.permissionService.UserHasAddAppraisalPermissions(user);

            //assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        public void When_Super_Admin_Setting_Disabled_And_User_IsAdmin_Then_HasPermission_Returns_True(bool userIsAdmin, bool userIsSuperAdmin)
        {
            //arrange
            var settings = new Settings
            {
                OnlySuperAdminsCanAddAppraisals = false,
            };

            this.settingsServiceMock.Setup(x => x.Settings).Returns(settings);

            User user = new User
            {
                IsAdmin = userIsAdmin,
                IsSuperAdmin = userIsSuperAdmin
            };

            this.permissionService = new PermissionsService(this.settingsServiceMock.Object);

            //act
            bool result = this.permissionService.UserHasAddAppraisalPermissions(user);

            //assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void When_Super_Admin_Setting_Disabled_And_User_IsNotAdmin_Then_HasPermission_Returns_False(bool userIsAdmin, bool userIsSuperAdmin)
        {
            //arrange
            var settings = new Settings
            {
                OnlySuperAdminsCanAddAppraisals = false,
            };

            this.settingsServiceMock.Setup(x => x.Settings).Returns(settings);

            User user = new User
            {
                IsAdmin = userIsAdmin,
                IsSuperAdmin = userIsSuperAdmin
            };

            this.permissionService = new PermissionsService(this.settingsServiceMock.Object);

            //act
            bool result = this.permissionService.UserHasAddAppraisalPermissions(user);

            //assert
            Assert.False(result);
        }
    }
}
