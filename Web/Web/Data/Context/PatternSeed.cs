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
            public string Sku { get; set; }
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
            context.Patterns
                .Include(p => p.FabricOptions)
                .ThenInclude(f => f.FabricItem)
                .Include(p => p.ThreadColorOptions)
                .Load();
            var patterns = new List<Func<PatternModel>> { Pattern12, Pattern313, Pattern397, Pattern1018 };
            foreach (var patternFunc in patterns)
            {
                var patternModel = patternFunc();
                var pattern = context.Patterns.First(x => x.Item == patternModel.Id);
                if (pattern.FabricOptions.Count != patternModel.Canvases.Count() || pattern.FabricOptions.Any(fo))
            }
        }

        private PatternModel Pattern12()
        {
            return new PatternModel("12")
            {
                Canvases = new[]
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

        private PatternModel Pattern313()
        {
            return new PatternModel("313")
            {
                Canvases = new[]
                {
                    new CanvasModel{Name ="Aida 14", ColorId = "100"},
                    new CanvasModel{Name ="Aida 14", Sku = "3706", ColorId = "589", ColorName = "Navy"},
                },
                Threads = new Dictionary<string, IEnumerable<ThreadModel>>()
                {
                    {Dmc, new []
                    {
                        new ThreadModel {Name = "310", Length = 0m},
                        new ThreadModel {Name = "611", Length = 0m},
                        new ThreadModel {Name = "775", Length = 0m},
                        new ThreadModel {Name = "823", Length = 0m},
                        new ThreadModel {Name = "827", Length = 0m},
                        new ThreadModel {Name = "828", Length = 0m},
                        new ThreadModel {Name = "839", Length = 0m},
                        new ThreadModel {Name = "840", Length = 0m},
                        new ThreadModel {Name = "841", Length = 0m},
                        new ThreadModel {Name = "924", Length = 0m},
                        new ThreadModel {Name = "926", Length = 0m},
                        new ThreadModel {Name = "927", Length = 0m},
                        new ThreadModel {Name = "930", Length = 0m},
                        new ThreadModel {Name = "931", Length = 0m},
                        new ThreadModel {Name = "932", Length = 0m},
                        new ThreadModel {Name = "939", Length = 0m},
                        new ThreadModel {Name = "3325", Length = 0m},
                        new ThreadModel {Name = "3371", Length = 0m},
                        new ThreadModel {Name = "3750", Length = 0m},
                        new ThreadModel {Name = "3756", Length = 0m},
                        new ThreadModel {Name = "3768", Length = 0m},
                        new ThreadModel {Name = "3799", Length = 0m},
                        new ThreadModel {Name = "B5200", Length = 0m},
                    } }
                }

            };
        }

        private PatternModel Pattern397()
        {
            return new PatternModel("397")
            {
                Canvases = new[]
                {
                    new CanvasModel{Name ="Murano",Sku = "3984", ColorId = "705", ColorName = "Pearl Grey"},
                    new CanvasModel{Name ="Murano",Sku = "3984", ColorId = "7011", ColorName = "Silvery Moon"},
                    new CanvasModel{Name ="Murano",Sku = "3984", ColorId = "5106", ColorName = "Blue Cloud"},
                },
                Threads = new Dictionary<string, IEnumerable<ThreadModel>>()
                {
                    {Dmc, new []
                    {
                        new ThreadModel {Name = "209", Length = 0.1m},
                        new ThreadModel {Name = "210", Length = 0.9m},
                        new ThreadModel {Name = "310", Length = 2.9m},
                        new ThreadModel {Name = "341", Length = 0.6m},
                        new ThreadModel {Name = "368", Length = 1.7m},
                        new ThreadModel {Name = "369", Length = 1.9m},
                        new ThreadModel {Name = "744", Length = 2.7m},
                        new ThreadModel {Name = "760", Length = 0.2m},
                        new ThreadModel {Name = "761", Length = 1.7m},
                        new ThreadModel {Name = "869", Length = 0.5m},
                        new ThreadModel {Name = "945", Length = 0.4m},
                        new ThreadModel {Name = "948", Length = 0.9m},
                        new ThreadModel {Name = "951", Length = 1.7m},
                        new ThreadModel {Name = "3756", Length = 0.8m},
                        new ThreadModel {Name = "3770", Length = 2.5m},
                        new ThreadModel {Name = "3823", Length = 3.3m},
                        new ThreadModel {Name = "3853", Length = 0.6m},
                        new ThreadModel {Name = "3855", Length = 1.7m},
                        new ThreadModel {Name = "3865", Length = 0.2m},
                        new ThreadModel {Name = "B5200", Length = 0.5m},
                    } }
                }

            };
        }

        private PatternModel Pattern1018()
        {
            return new PatternModel("1018")
            {
                Canvases = new[]
                {
                    new CanvasModel{Name ="Murano",Sku = "3984", ColorId = "99", ColorName = "Soft Cream"},
                },
                Threads = new Dictionary<string, IEnumerable<ThreadModel>>()
                {
                    {Dmc, new []
                    {
                        new ThreadModel {Name = "301", Length = 1.1m},
                        new ThreadModel {Name = "350", Length = 1.7m},
                        new ThreadModel {Name = "307", Length = 1m},
                        new ThreadModel {Name = "351", Length = 1.9m},
                        new ThreadModel {Name = "310", Length = 0.1m},
                        new ThreadModel {Name = "352", Length = 0.4m},
                        new ThreadModel {Name = "349", Length = 5.3m},
                        new ThreadModel {Name = "353", Length = 0.2m},
                        new ThreadModel {Name = "402", Length = 2.1m},
                        new ThreadModel {Name = "413", Length = 0.4m},
                        new ThreadModel {Name = "433", Length = 0.5m},
                        new ThreadModel {Name = "434", Length = 0.6m},
                        new ThreadModel {Name = "435", Length = 1.6m},
                        new ThreadModel {Name = "436", Length = 2.3m},
                        new ThreadModel {Name = "437", Length = 3.4m},
                        new ThreadModel {Name = "445", Length = 1m},
                        new ThreadModel {Name = "498", Length = 2.8m},
                        new ThreadModel {Name = "680", Length = 0.1m},
                        new ThreadModel {Name = "720", Length = 1.2m},
                        new ThreadModel {Name = "722", Length = 0.9m},
                        new ThreadModel {Name = "729", Length = 0.2m},
                        new ThreadModel {Name = "730", Length = 1.1m},
                        new ThreadModel {Name = "732", Length = 1m},
                        new ThreadModel {Name = "733", Length = 3.2m},
                        new ThreadModel {Name = "738", Length = 4.3m},
                        new ThreadModel {Name = "743", Length = 2.6m},
                        new ThreadModel {Name = "746", Length = 0.2m},
                        new ThreadModel {Name = "794", Length = 0.2m},
                        new ThreadModel {Name = "815", Length = 1.9m},
                        new ThreadModel {Name = "817", Length = 5.5m},
                        new ThreadModel {Name = "832", Length = 0.3m},
                        new ThreadModel {Name = "833", Length = 0.6m},
                        new ThreadModel {Name = "918", Length = 0.7m},
                        new ThreadModel {Name = "919", Length = 1.8m},
                        new ThreadModel {Name = "920", Length = 3.9m},
                        new ThreadModel {Name = "921", Length = 1m},
                        new ThreadModel {Name = "922", Length = 0.8m},
                        new ThreadModel {Name = "931", Length = 0.6m},
                        new ThreadModel {Name = "937", Length = 0.3m},
                        new ThreadModel {Name = "945", Length = 10.5m},
                        new ThreadModel {Name = "951", Length = 11.5m},
                        new ThreadModel {Name = "972", Length = 4.2m},
                        new ThreadModel {Name = "3031", Length = 3.1m},
                        new ThreadModel {Name = "3064", Length = 0.1m},
                        new ThreadModel {Name = "3340", Length = 2.1m},
                        new ThreadModel {Name = "3341", Length = 0.3m},
                        new ThreadModel {Name = "3770", Length = 1.5m},
                        new ThreadModel {Name = "3774", Length = 0.8m},
                        new ThreadModel {Name = "3782", Length = 4.5m},
                        new ThreadModel {Name = "3790", Length = 2.7m},
                        new ThreadModel {Name = "3819", Length = 0.7m},
                        new ThreadModel {Name = "3820", Length = 4.6m},
                        new ThreadModel {Name = "3821", Length = 0.9m},
                        new ThreadModel {Name = "3822", Length = 0.4m},
                        new ThreadModel {Name = "3826", Length = 1.2m},
                        new ThreadModel {Name = "3829", Length = 0.2m},
                        new ThreadModel {Name = "3853", Length = 4.6m},
                        new ThreadModel {Name = "3856", Length = 8.3m},
                        new ThreadModel {Name = "Ecru", Length = 0.1m},
                    } }
                }

            };
        }
    }
}
