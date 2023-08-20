using Business.Model;

namespace Business.Services
{
    public interface IPermissionsService
    {
        bool UserHasAddAppraisalPermissions(User currentUser);
    }

    public class PermissionsService : IPermissionsService
    {
        private readonly ISettingsService _settingsService;

        public PermissionsService(ISettingsService settingsService)
        {
            this._settingsService = settingsService;
        }

        public bool UserHasAddAppraisalPermissions(User currentUser)
        {
            if (this._settingsService.Settings.OnlySuperAdminsCanAddAppraisals)
            {
                if (currentUser.IsSuperAdmin)
                {
                    return true;
                }

                return false;

                //this could probably just be 'return currentUser.IsSuperAdmin'
                // but it's cyclomatic complexity is already pretty low and i think this way is a bit more readable.
            }

            if (currentUser.IsAdmin)
            {
                return true;
            }

            return false;
        }
    }
}
