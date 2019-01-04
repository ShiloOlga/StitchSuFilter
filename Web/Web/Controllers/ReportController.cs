using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Context;
using Web.Data.Repositories;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController
    {
        private readonly ICrossStitchKitsRepository _repository;

        public ReportController(ICrossStitchKitsRepository repository, MariaDbContext dbContext)
        {
            _repository = repository;
        }

        [HttpGet]
        public string Get()
        {
            var sb = new StringBuilder();
            var reportTask = _repository.GetColorReport();
            reportTask.Wait();
            foreach (var model in reportTask.Result.OrderByDescending(p => p.Color.ColorId.Length).ThenBy(p => p.Color.ColorId))
            {
                sb.AppendLine($"{model.Color.ColorId,-5} - {model.TotalLength,5}m");
            }
            return sb.ToString();
        }

        private void AddPattern()
        {
            var canvas = new[]
            {
                new {Name = "Brittney", Color = "White"},
                new {Name = "Linda", Color = "White"},
            };
            var dmcColors = new[]
            {
                new {Name = "", Length = }
            }
        }

    }
}
