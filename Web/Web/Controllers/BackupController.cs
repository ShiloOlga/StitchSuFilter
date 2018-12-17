using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    public class BackupController : Controller
    {
        private readonly ICrossStitchKitsRepository _kitsRepository;

        public BackupController(ICrossStitchKitsRepository kitsRepository)
        {
            _kitsRepository = kitsRepository;
        }

        [Route("api/[controller]/Create")]
        public async Task<IActionResult> Create()
        {
            var sets = (await _kitsRepository.All()) ?? Enumerable.Empty<Kit>();
            var result = JsonConvert.SerializeObject(sets);
            System.IO.File.WriteAllText("backup.json", result);
            //var copy = JsonConvert.DeserializeObject<IEnumerable<Kit>>(result);
            TempData["Message"] = "Backup finished successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}