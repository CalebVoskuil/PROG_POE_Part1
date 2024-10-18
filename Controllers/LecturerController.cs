using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PROG_POE1.Controllers
{
    [Authorize(Roles = "Lecturer")]
    public class LecturerController : Controller
    {
        public IActionResult Submit()
        {
            return View();
        }
    }

}
// ~~~~~~~~^^~~^^~~^^~[ End of File ]~^^~~^^~~^^~~~~~~~~
