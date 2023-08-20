using Business.Data;
using Business.Model;
using Business.Services;
using System;

namespace Business
{
    /// <summary>
    /// Squint your eyes and you could imagine this was a real MVC controller
    /// </summary>
    public class AppraisalController
    {
        private readonly IRequestDataService _requestDataService;
        private readonly IPermissionsService _permissionsService;
        private readonly IDatabase _database;

        public AppraisalController(IRequestDataService requestDataService, IPermissionsService permissionsService, IDatabase database)
        {
            this._requestDataService = requestDataService;
            this._permissionsService = permissionsService;
            this._database = database;
        }

        public JsonResultDummy Add(Appraisal newApparisal)
        {
            try
            {
                var currentUser = _requestDataService.CurrentUser;

                if (this._permissionsService.UserHasAddAppraisalPermissions(currentUser))
                {
                    _database.AddNewAppraisal(newApparisal);
                    
                    return new JsonResultDummy(true, "Appraisal successfully added.");
                }

                return new JsonResultDummy(false, "Appraisal was not added as current user does not have appropriate admin permissions. Please check account and database settings.");
            }

            catch(Exception ex) 
            {
                //log exception details then show user generic message. 
                return new JsonResultDummy(false, "An unexpected error occurred please try again, if the problem persists please contact support.");
            }
        }
    }
}
