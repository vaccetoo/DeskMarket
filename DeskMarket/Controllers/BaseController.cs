using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskMarket.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
