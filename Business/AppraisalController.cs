using Business.Data;
using Business.Model;
using Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// Squint your eyes and you could imagine this was a real MVC controller
    /// </summary>
    public class AppraisalController
    {
        public AppraisalController(IDatabase database, IRequestDataService requestDataService, ISettingsService settingsService)
        {
            this._requestDataService = requestDataService;
            this._settingsService = settingsService;
            this._database = database;
        }

        private IRequestDataService _requestDataService;
        private readonly ISettingsService _settingsService;
        private IDatabase _database;

        public JsonResultDummy Add(Appraisal newApparisal)
        {
            var currentUser = _requestDataService.CurrentUser;
            if(!currentUser.IsAdmin)
            {
                return new JsonResultDummy(false, "Permission denied, user must be an admin to add an appraisal");
            }
                                 
            _database.AddNewAppraisal(newApparisal);
            return new JsonResultDummy(true, "");
        }
    }
}
