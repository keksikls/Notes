using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Models.RoleIdConst;

namespace Notes.Web.Areas.Admin.ApiControllers
{
    [Route("api/RoleApi")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = ConstRole.Admin)]
    public class RoleApiController : ControllerBase
    {

    }
}
