using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Web.Data.Context
{
    public class PatternSeed
    {
        private const string Dmc = "DMC";
        private const string Pnk = "ПНК им. Кирова";

        private class CanvasModel
        {
            public string Name { get; set; }
            public string ColorId { get; set; }
            public string ColorName { get; set; }
        }

        private class ThreadModel
        {
            public string Name { get; set; }
            public decimal Length { get; set; }
        }

        private class PatternModel
        {
            public PatternModel(string id)
            {
                Id = id;
            }
            public string Id { get; }
            public IEnumerable<CanvasModel> Canvases { get; set; }
            public Dictionary<string, IEnumerable<ThreadModel>> Threads { get; set; }
        }

        public void Execute(MariaDbContext context)
        {
            //=CONCATENATE("new ThreadModel {Name = ", """",A1, """, Length = ",B1,"m},")
            context.Fabrics.Load();
            var patterns = new List<Func<PatternModel>> {Pattern12};
            foreach (var patternFunc in patterns)
            {
                var patternModel = patternFunc();
            }
        }

        private PatternModel Pattern12()
        {
            return new PatternModel("12")
            {
                Canvases = new []
                {
                    new CanvasModel{Name ="Brittney", ColorId = "100"},
                    new CanvasModel{Name ="Linda", ColorId = "100"},
                },
                Threads = new Dictionary<string, IEnumerable<ThreadModel>>()
                {
                    {Dmc, new []
                    {
                        new ThreadModel {Name = "153", Length = 3.4m},
                        new ThreadModel {Name = "168", Length = 0.8m},
                        new ThreadModel {Name = "211", Length = 5.6m},
                        new ThreadModel {Name = "225", Length = 12.3m},
                        new ThreadModel {Name = "310", Length = 2.7m},
                        new ThreadModel {Name = "317", Length = 6.1m},
                        new ThreadModel {Name = "414", Length = 1.2m},
                        new ThreadModel {Name = "433", Length = 2.7m},
                        new ThreadModel {Name = "434", Length = 2m},
                        new ThreadModel {Name = "435", Length = 0.9m},
                        new ThreadModel {Name = "648", Length = 0.1m},
                        new ThreadModel {Name = "800", Length = 46.3m},
                        new ThreadModel {Name = "801", Length = 0.1m},
                        new ThreadModel {Name = "819", Length = 16.6m},
                        new ThreadModel {Name = "828", Length = 5.9m},
                        new ThreadModel {Name = "945", Length = 10.4m},
                        new ThreadModel {Name = "951", Length = 7.1m},
                        new ThreadModel {Name = "963", Length = 0.9m},
                        new ThreadModel {Name = "3072", Length = 1.1m},
                        new ThreadModel {Name = "3716", Length = 0.7m},
                        new ThreadModel {Name = "3779", Length = 0.1m},
                        new ThreadModel {Name = "3799", Length = 2.5m},
                        new ThreadModel {Name = "B5200", Length = 9.3m},
                    } }
                }

            };
        }
    }
}
