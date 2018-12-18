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
        private const string FileName = "backup.json";

        public BackupController(ICrossStitchKitsRepository kitsRepository)
        {
            _kitsRepository = kitsRepository;
        }

        [Route("api/[controller]/Create")]
        public async Task<IActionResult> Create()
        {
            var kits = (await _kitsRepository.All()) ?? Enumerable.Empty<Kit>();
            var result = JsonConvert.SerializeObject(kits);
            System.IO.File.WriteAllText(FileName, result);
            TempData[Constants.Message] = "Backup finished successfully.";
            return RedirectToAction("Index", "Home");
        }

        [Route("api/[controller]/Restore")]
        public async Task<IActionResult> Restore()
        {
            if (System.IO.File.Exists(FileName))
            {
                var result = System.IO.File.ReadAllText(FileName);
                var kits = JsonConvert.DeserializeObject<IEnumerable<Kit>>(result);
                await _kitsRepository.AddRange(kits);
                TempData[Constants.Message] = "Restore finished successfully.";
            }
            else
            {
                TempData[Constants.Message] = "Restore file not found.";
            }
            return RedirectToAction("Index", "Home");
        }

        [Route("api/[controller]/Clear")]
        public async Task<IActionResult> Clear()
        {
            await _kitsRepository.Clear();
            TempData[Constants.Message] = "Cleanup finished successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}