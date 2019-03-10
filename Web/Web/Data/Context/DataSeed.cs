using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.Data.Entities;
using Web.Models;
using FabricItem = Web.Data.Entities.FabricItem;
using Kit = Web.Data.Entities.Kit;

namespace Web.Data.Context
{
    public class DataSeed
    {
        private readonly MariaDbContext _dbContext;
        private readonly ILazyLoader _lazyLoader;
        private const string FileName = "backup.json";

        #region Private classes

        private class F
        {
            public string Type { get; }
            public sbyte Count { get; }
            public string Name { get; }
            public sbyte Priority { get; }
            public string Content { get; }

            public F(string name, sbyte count, string type, sbyte priority, string content)
            {
                Type = type;
                Count = count;
                Name = name;
                Priority = priority;
                Content = content;
            }
        }

        private class SeedKit
        {
            public string Manufacturer { get; set; }
            public string Title { get; set; }
            public string Item { get; set; }
            public string ThreadManufacturer { get; set; }
            public string FabricItem { get; set; }
            public int ColorsCount { get; set; }
            public decimal WidthSm { get; set; }
            public decimal HeightSm { get; set; }
            public short WidthStitches { get; set; }
            public short HeightStitches { get; set; }
            public string Author { get; set; }
            public string Image { get; set; }
            public string Link { get; set; }
        }

        #endregion
        #region Data
       
        private List<F> _fabrics = new List<F>
        {
                new F("Aida 16", 16, "Blockweave", 1, "100% cotton"),
                new F("Aida 14", 14, "Blockweave", 1, "100% cotton"),
                new F("Aida 18", 18, "Blockweave", 2, "100% cotton"),
                new F("Aida 11", 11, "Blockweave", 3, "100% cotton"),
            new F("Aida 10", 10, "Blockweave", 3, "100% cotton"),
                new F("Linda", 27, "Evenweave", 1, "100% cotton"),
                new F("Jubilee", 28, "Evenweave", 3, "100% cotton"),
                new F("Annabelle", 28, "Evenweave", 3, "100% cotton"),
                new F("Dublin", 20, "Evenweave", 3, "100% linen"),
                new F("Cashel", 28, "Evenweave", 2, "100% linen"),
                new F("Belfast", 32, "Evenweave", 2, "100% linen"),
                new F("Permin", 32, "Evenweave", 2, "100% linen"),
                new F("Edinburgh", 36, "Evenweave", 3, "100% linen"),
                new F("Newcastle", 40, "Evenweave", 3, "100% linen"),
                new F("Lugana", 25, "Evenweave", 1, "52% cotton & 48% rayon"),
                new F("Murano", 32, "Evenweave", 1, "52% cotton & 48% rayon"),
                new F("Bellana", 20, "Evenweave", 3, "52% cotton & 48% rayon"),
                new F("Perlleinen80", 20, "Evenweave", 3, "60% polyester & 40% linen"),
                new F("Perlleinen100", 25, "Evenweave", 3, "60% polyester & 40% linen"),
                new F("Perlleinen", 32, "Evenweave", 3, "52% cotton & 48% rayon"),
                new F("Brittney", 28, "Evenweave", 2, "52% cotton & 48% rayon"),
                new F("Lucan", 32, "Evenweave", 2, "48% cotton & 52% linen")
            };
        private List<SeedKit> _kits = new List<SeedKit>
        {
            new SeedKit{Title = "Русская усадьба. Чай под яблоней", Manufacturer = "Riolis", Item = "1140", Author = "Юлия Красавина", ColorsCount = 24, WidthSm = 30, HeightSm = 40,
                ThreadManufacturer = "Riolis", FabricItem = "Aida 14", WidthStitches = 124, HeightStitches = 183, Image = "http://www.riolis.ru/zoom/photos/2177.jpg",
                Link = "http://www.riolis.ru/catalog/details_2177.html"
            },
            new SeedKit{Title = "Одуванчики", Manufacturer = "Riolis", Item = "807", Author = "Юлия Красавина", ColorsCount = 11, WidthSm = 30, HeightSm = 21,
                ThreadManufacturer = "Riolis", FabricItem = "Aida 16", WidthStitches = 170, HeightStitches = 116, Image = "http://www.riolis.ru/zoom/photos/1038.jpg",
                Link = "http://www.riolis.ru/catalog/details_1038.html"
            },
            new SeedKit{Title = "Фуксия", Manufacturer = "Riolis", Item = "1398", Author = "Юлия Красавина", ColorsCount = 25, WidthSm = 40, HeightSm = 30,
                ThreadManufacturer = "Riolis", FabricItem = "Aida 14", WidthStitches = 187, HeightStitches = 144, Image = "http://www.riolis.ru/zoom/photos/2878.jpg",
                Link = "http://www.riolis.ru/catalog/details_2878.html"
            },
            new SeedKit{Title = "Озеро в горах", Manufacturer = "Riolis", Item = "1235", Author = "Светлана Сидорова", ColorsCount = 29, WidthSm = 60, HeightSm = 40,
                ThreadManufacturer = "Riolis", FabricItem = "Aida 10", WidthStitches = 230, HeightStitches = 160, Image = "http://www.riolis.ru/zoom/photos/2450.jpg",
                Link = "http://www.riolis.ru/catalog/details_2450.html"
            },
            new SeedKit{Title = "Снегири", Manufacturer = "Золотое Руно", Item = "РС-018", Author = "Крумина Елена", ColorsCount = 33, WidthSm = 24.7m, HeightSm = 32.2m,
                ThreadManufacturer = "Madeira", FabricItem = "Aida 16", WidthStitches = 210, HeightStitches = 120, Image = "https://www.rukodelie.ru/upload/iblock/9c4/7a92849b_52b5_11e4_9997_bcaec538956e_2acde4e7_53a2_11e4_9997_bcaec538956e.resize1.jpg",
                Link = "https://www.rukodelie.ru/catalog/nabory_dlya_vyshivaniya/nabory_dlya_vyshivaniya_krestom/rayskiy_sad/rs_018_snegiri"
            },
            new SeedKit{Title = "Волшебный лес", Manufacturer = "Золотое Руно", Item = "Ф-028", Author = "Крумина Елена", ColorsCount = 41, WidthSm = 36.9m, HeightSm = 46.7m,
                ThreadManufacturer = "Madeira", FabricItem = "Aida 16", WidthStitches = 300, HeightStitches = 239, Image = "https://www.rukodelie.ru/upload/iblock/bb3/5ad3767a_5757_11e3_a080_485b3970aae4_5ad3767b_5757_11e3_a080_485b3970aae4.resize1.jpg",
                Link = "https://www.rukodelie.ru/catalog/nabory_dlya_vyshivaniya/nabory_dlya_vyshivaniya_krestom/fentezi/f_028_volshebnyy_les"
            },
            new SeedKit{Title = "Зимняя фантазия", Manufacturer = "Золотое Руно", Item = "ЧМ-032", Author = "Есенова Индира", ColorsCount = 30, WidthSm = 44.2m, HeightSm = 36.9m,
                ThreadManufacturer = "Madeira", FabricItem = "Aida 16", WidthStitches = 240, HeightStitches = 280, Image = "https://www.rukodelie.ru/upload/iblock/d67/7a9284d5_52b5_11e4_9997_bcaec538956e_a94f10c2_538b_11e4_9997_bcaec538956e.resize1.jpg",
                Link = "https://www.rukodelie.ru/catalog/nabory_dlya_vyshivaniya/nabory_dlya_vyshivaniya_krestom/chudesnoe_mgnovenie/chm_032_zimnyaya_fantaziya"
            },
            new SeedKit{Title = "Уголок России", Manufacturer = "Золотое Руно", Item = "ВМ-021", Author = "Есенова Индира", ColorsCount = 42, WidthSm = 46.4m, HeightSm = 30.2m,
                ThreadManufacturer = "Madeira", FabricItem = "Aida 16", WidthStitches = 300, HeightStitches = 194, Image = "https://www.rukodelie.ru/upload/iblock/f3a/e3e48288_f8ef_11e2_af16_485b3970aae4_e3e48289_f8ef_11e2_af16_485b3970aae4.resize1.jpg",
                Link = "https://www.rukodelie.ru/catalog/nabory_dlya_vyshivaniya/nabory_dlya_vyshivaniya_krestom/vremena_goda/vm_021_ugolok_rossii/"
            },
            new SeedKit{Title = "Мимоза", Manufacturer = "Золотое Руно", Item = "Т-004", Author = "Сафонова Надежда", ColorsCount = 26, WidthSm = 62m, HeightSm = 34.7m,
                ThreadManufacturer = "Madeira", FabricItem = "Aida 14", WidthStitches = 300, HeightStitches = 200, Image = "https://www.rukodelie.ru/upload/iblock/52d/e3e4828a_f8ef_11e2_af16_485b3970aae4_e3e4828b_f8ef_11e2_af16_485b3970aae4.resize1.jpg",
                Link = "https://www.rukodelie.ru/catalog/nabory_dlya_vyshivaniya/nabory_dlya_vyshivaniya_krestom/triptikh/t_004_mimoza/"
            },
            new SeedKit{Title = "Тюльпаны", Manufacturer = "Алиса", Item = "2-29", Author = "Левашов Игорь", ColorsCount = 35, WidthSm = 40, HeightSm = 30,
                ThreadManufacturer = "Gamma", FabricItem = "Aida 14", WidthStitches = 221, HeightStitches = 165, Image = "https://www.alisa-collection.ru/_data/objects/0001/3186/icon.jpg",
                Link = "https://www.alisa-collection.ru/?id=13186"
            },
            new SeedKit{Title = "Февральский домик", Manufacturer = "Алиса", Item = "3-22", Author = null, ColorsCount = 24, WidthSm = 18, HeightSm = 14,
                ThreadManufacturer = "Gamma", FabricItem = "Aida 16", WidthStitches = 112, HeightStitches = 90, Image = "https://www.alisa-collection.ru/_data/objects/0001/9206/icon.jpg",
                Link = "https://www.alisa-collection.ru/?id=19206"
            },
            new SeedKit{Title = "Венок сновидений", Manufacturer = "Овен", Item = "919", Author = "Левашов Игорь", ColorsCount = 26, WidthSm = 36, HeightSm = 36,
                ThreadManufacturer = "ПНК им. Кирова", FabricItem = "Aida 16", WidthStitches = 230, HeightStitches = 230, Image = "http://ooo-oven.ru/upload/iblock/bee/bee22d3c003d59f279ba209a8b9fec18.jpg",
                Link = "https://www.stitch.su/oven/919"
            },
            new SeedKit{Title = "Tiger chilling out", Manufacturer = "Dimensions", Item = "35222", Author = "Matthew Hillier", ColorsCount = 20, WidthSm = 23, HeightSm = 36,
                ThreadManufacturer = "Dimensions", FabricItem = "Aida 18", WidthStitches = 160, HeightStitches = 250, Image = "https://www.dimshop.ru/images/big/35222.jpg",
                Link = "https://www.dimshop.ru/dimensions/35222/"
            },
            new SeedKit{Title = "Аромат сирени", Manufacturer = "Чудесная игла", Item = "40-64", Author = null, ColorsCount = 41, WidthSm = 40, HeightSm = 37,
                ThreadManufacturer = "Gamma", FabricItem = "Aida 14", WidthStitches = 220, HeightStitches = 210, Image = "http://chudo-igla.ru/thumb/2/_6AtVZioQC4eCqJE3kVblQ/c/d/6aromat-sireni.jpg",
                Link = "https://www.stitch.su/chudesnaya_igla/40-64"
            },
            new SeedKit{Title = "Моя отрада!", Manufacturer = "Чудесная игла", Item = "90-01", Author = null, ColorsCount = 45, WidthSm = 32, HeightSm = 41,
                ThreadManufacturer = "Gamma", FabricItem = "Aida 14", WidthStitches = 164, HeightStitches = 225, Image = "http://chudo-igla.ru/thumb/2/bIled3wWBnJ_74DyzsJVnw/c/d/90-01_baget.jpg",
                Link = "https://www.stitch.su/chudesnaya_igla/90-01"
            },
            new SeedKit{Title = "Узоры зимы", Manufacturer = "М.П. Студия", Item = "Р-162", Author = "Слесарева Дарья", ColorsCount = 9, WidthSm = 9, HeightSm = 9,
                ThreadManufacturer = "Bestex", FabricItem = "Aida 16", WidthStitches = 55, HeightStitches = 55, Image = "https://mpstudia.ru/upload/iblock/437/4379779c252b82a61e37ccdc0683f48d.jpg",
                Link = "https://mpstudia.ru/shop/57/1732/"
            },
            new SeedKit{Title = "Новогодний изумруд", Manufacturer = "М.П. Студия", Item = "Р-165", Author = "Слесарева Дарья", ColorsCount = 10, WidthSm = 9, HeightSm = 9,
                ThreadManufacturer = "Bestex", FabricItem = "Aida 16", WidthStitches = 55, HeightStitches = 55, Image = "https://mpstudia.ru/upload/iblock/7b3/7b385b5832d3d10e9d6d4891584aff89.jpg",
                Link = "https://mpstudia.ru/shop/57/1722/"
            },
            new SeedKit{Title = "Янтарное великолепие", Manufacturer = "М.П. Студия", Item = "Р-168", Author = "Слесарева Дарья", ColorsCount = 8, WidthSm = 9, HeightSm = 9,
                ThreadManufacturer = "Bestex", FabricItem = "Aida 16", WidthStitches = 55, HeightStitches = 55, Image = "https://mpstudia.ru/upload/iblock/688/6884d9f609d1bd7d07a78afd9ed280b9.jpg",
                Link = "https://mpstudia.ru/shop/57/1750/"
            },
            new SeedKit{Title = "Морозный рубин", Manufacturer = "М.П. Студия", Item = "Р-167", Author = "Слесарева Дарья", ColorsCount = 11, WidthSm = 9, HeightSm = 9,
                ThreadManufacturer = "Bestex", FabricItem = "Aida 16", WidthStitches = 55, HeightStitches = 55, Image = "https://mpstudia.ru/upload/iblock/2a2/2a292cf6efd1660a3c1bc75a45d225f8.jpg",
                Link = "https://mpstudia.ru/shop/57/1731/"
            },
            new SeedKit{Title = "Морозный рубин", Manufacturer = "RTO", Item = "М553", Author = "Сотникова Ольга", ColorsCount = 25, WidthSm = 25.5m, HeightSm = 21.5m,
                ThreadManufacturer = "DMC", FabricItem = "Aida 14", WidthStitches = 140, HeightStitches = 120, Image = "https://rto21.by/upload/iblock/9e3/9e350a6127e755752aec5c6482fe0ed0.jpg",
                Link = "https://rto21.by/catalog/rto/m553_nabor_dlya_vyshivaniya_koshachya_gratsiya/"
            },
        };

        private string _threadsUniverse = @"301 9210 0,9
775 2602 0,3
350 0810 0,1
828 3302 0,7
351 9222 0,2 
839 6512 6,2
352 9220 0,7
840 6006 2,1
353 7501 4,1
841 6203 10,3
400 9212 0,6
842 6501 5,6
402 9208 6,0
921 5408 0,2
420 9104 0,2
938 5711 1,3
437 5903 2,7
945 6300 14,7
518 3304 0,4
948 9000 19,6
519 2902 0,8
3078 0300 6,4
712 0103  14,3
3371 6516 1,0
738 5902  26,7
3770 9207 17,5
739 5901  28,6
3776 9210 2,1
743 0508  6,1
3821 7306   3,2
744 0504  12,3
3823 0501 33,0
745 0502  4,8
3865 0104 30,6
754 1602  1,6
B5200 0101 1,1";
        #endregion
        public DataSeed(MariaDbContext dbContext)
        {
            _dbContext = dbContext;
            _lazyLoader = null;
        }

        public void Execute()
        {
            //if (File.Exists(FileName))
            //{
            //    var result = File.ReadAllText(FileName);
            //    var kits = JsonConvert.DeserializeObject<IEnumerable<KitModel>>(result).ToArray();

            //    AddKitManufacturers(kits);
            //    AddPatternAuthors(kits);
            //    AddPatterns(kits);
            //}

            //AddFabricTypes();
            //AddFabricContentTypes();
            //AddFabrics();
            //AddKits();
            //AddDmcPalette();
            //AddPnkPalette();
            //AddPatternThreads();
            //_dbContext.SaveChanges();

            //AddPatternColors();
           // _dbContext.SaveChanges();
            new PatternSeed().Execute(_dbContext);
        }

        private void AddPatternColors()
        {
            string s = @"154 7,2
211 1,8
316 1,3
369 3,5
413 2,2 
554 1,8
562 1,8
563 2,2
564 0,7
597 1,1 
598 0,9
648 1,7
677 0,3
712 0,9
727 0,4 
746 0,3
760 0,2
761 1
775 2,3
778 4,6 
796 0,1
799 0,2
809 0,5
819 8,2
832 0,3 
833 0,4
834 0,5
905 0,8
906 1,4
907 1,9 
963 1,6
964 0,9
966 0,5
987 0,5
988 0,6 
3072 1,1
3078 0,5
3348 0,4
3687 1,8
3688 2,2 
3689 1,5
3713 6,2
3726 2,8
3727 1,1
3756 2,7 
3770 1,1
3810 1,0
3834 9
3835 3,5
3836 3,6 
3838 0,9
3840 0,8
3841 1,7
B5200 0,6
Blanc 5,3  ";
            var datalines = s.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            var dmcArray = new List<ThreadColorOption>(datalines.Length);
            var pattern = _dbContext.Patterns.First(p => p.Item == "21749");
            foreach (var line in datalines)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()).ToArray();
                if (parts.Length != 2)
                {
                    throw new Exception();
                }

                var dmc = _dbContext.ThreadColors.First(c => c.ColorId == parts[0]);
                if (dmc == null)
                {
                    throw new Exception();
                }

                var length = decimal.Parse(parts[1]);
                dmcArray.Add(new ThreadColorOption(_lazyLoader)
                    {Pattern = pattern, ThreadColor = dmc, RequiredLength = length});
            }

            _dbContext.ThreadColorOptions.AddRange(dmcArray);
        }

        private void AddPatternThreads()
        {
            _dbContext.ThreadColors.Load();
               var datalines = _threadsUniverse.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            var dmcArray = new List<ThreadColorOption>(datalines.Length);
            var pnkArray = new List<ThreadColorOption>(datalines.Length);
            var pattern = _dbContext.Patterns.First(p => p.Title == "Вселенная");
            foreach (var line in datalines)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
                var dmc = _dbContext.ThreadColors.Local.First(c => c.ColorId == parts[0]);
                var pnk = _dbContext.ThreadColors.Local.First(c => c.ColorId == parts[1]);
                var length = decimal.Parse(parts[2]);
                dmcArray.Add(new ThreadColorOption(_lazyLoader){Pattern = pattern, ThreadColor = dmc, RequiredLength = length });
                var existingPnk = pnkArray.FirstOrDefault(p => p.ThreadColor == pnk);
                if (existingPnk == null)
                {
                    pnkArray.Add(new ThreadColorOption(_lazyLoader) { Pattern = pattern, ThreadColor = pnk, RequiredLength = length });
                }
                else
                {
                    existingPnk.RequiredLength += length;
                }
            }
            _dbContext.ThreadColorOptions.AddRange(dmcArray);
            foreach (var option in pnkArray)
            {
                _dbContext.ThreadColorOptions.Add(option);
            }
        }

        private void AddPnkPalette()
        {

            var dtoThreads = new List<ThreadColor>();
            var content = File.ReadAllText("pnk.json");
            var jsonArray = JsonConvert.DeserializeObject(content) as JArray;
            var manufacturer = _dbContext.ThreadManufacturers.First(m => m.Name.StartsWith("ПНК"));
            foreach (var token in jsonArray)
            {
                var dto = new ThreadColor(_lazyLoader);
                dto.Manufacturer = manufacturer;
                var name = token["Name"].Value<string>();
                dto.ColorId = name;
                dto.ColorName = name;
                var rgb = token["Rgb"].Value<string>();
                dto.RgbColor = rgb;
                dto.Length = 10;
                dto.Sku = "";
                dtoThreads.Add(dto);
            }
            _dbContext.ThreadColors.AddRange(dtoThreads);
        }

        private void AddDmcPalette()
        {
            var dtoThreads = new List<ThreadColor>();
            var manufacturer = _dbContext.ThreadManufacturers.First(m => m.Name == "DMC");
            foreach (var thread in Data.DmcColors.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var parts = thread.Split(',').Select(t => t.Trim()).ToArray();
                var dto = new ThreadColor(_lazyLoader);
                dto.Manufacturer = manufacturer;
                dto.ColorId = parts[0];
                dto.ColorName = parts[1];
                dto.RgbColor = parts[2];
                dto.Length = 8;
                dto.Sku = "117S";
                dtoThreads.Add(dto);
            }

            _dbContext.ThreadColors.AddRange(dtoThreads);
        }

        private void AddKits()
        {
            _dbContext.PatternAuthors.Load();
            _dbContext.KitManufacturers.Load();
            _dbContext.ThreadManufacturers.Load();
            _dbContext.Fabrics.Load();
            _dbContext.FabricItems.Load();
            var dtoKits = new List<Kit>();
            foreach (var seedKit in _kits.Skip(1))
            {
                var dto = new Kit((_lazyLoader));
                dto.Title = seedKit.Title;
                var author = _dbContext.PatternAuthors.Local.FirstOrDefault(a => a.Name == seedKit.Author);
                if (author == null && !string.IsNullOrEmpty(seedKit.Author))
                {
                    author = new PatternAuthor((_lazyLoader)) { Name = seedKit.Author };
                    _dbContext.PatternAuthors.Add(author);
                }
                dto.Author = author;
                dto.ColorsCount = seedKit.ColorsCount;
                dto.Item = seedKit.Item;
                var manufacturer = _dbContext.KitManufacturers.Local.FirstOrDefault(a => a.Name.ToLowerInvariant() == seedKit.Manufacturer.ToLowerInvariant());
                if (manufacturer == null)
                {
                    manufacturer = new KitManufacturer((_lazyLoader)) { Name = seedKit.Manufacturer };
                    _dbContext.KitManufacturers.Add(manufacturer);
                }
                dto.Manufacturer = manufacturer;
                dto.Image = seedKit.Image;
                dto.Link = seedKit.Link;
                dto.WidthSm = seedKit.WidthSm;
                dto.HeightSm = seedKit.HeightSm;
                dto.WidthStitches = seedKit.WidthStitches;
                dto.HeightStitches = seedKit.HeightStitches;
                var threadManufacturer = _dbContext.ThreadManufacturers.Local.FirstOrDefault(m => m.Name.ToLowerInvariant() == seedKit.ThreadManufacturer.ToLowerInvariant());
                if (threadManufacturer == null)
                {
                    threadManufacturer = new ThreadManufacturer(_lazyLoader) { Name = seedKit.ThreadManufacturer };
                    _dbContext.ThreadManufacturers.Add(threadManufacturer);
                }
                dto.ThreadManufacturer = threadManufacturer;
                var fabricItem = _dbContext.FabricItems.Local.FirstOrDefault(f => f.Fabric != null && f.Fabric.Name == seedKit.FabricItem);
                if (fabricItem == null)
                {
                    fabricItem = new FabricItem((_lazyLoader)) { Fabric = _dbContext.Fabrics.First(f => f.Name == seedKit.FabricItem), Sku = "KitModel", ColorId = "-", ColorName = "-" };
                    _dbContext.FabricItems.Add(fabricItem);
                }
                dto.FabricItem = fabricItem;
                dtoKits.Add(dto);
            }

            _dbContext.Kits.AddRange(dtoKits);
        }

        private void AddFabrics()
        {
            var fabricsDto = new List<Entities.Fabric>(_fabrics.Count);
            foreach (var fabric in _fabrics)
            {
                var dto = new Entities.Fabric(_lazyLoader);
                dto.Name = fabric.Name;
                dto.Count = fabric.Count;
                dto.Priority = fabric.Priority;
                dto.FabricTypeId = _dbContext.FabricTypes.First(f => f.Name == fabric.Type).Id;
                dto.ContentTypeId = _dbContext.ContentTypes.First(f => f.Name == fabric.Content).Id;
                fabricsDto.Add(dto);
            }

            _dbContext.Fabrics.AddRange(fabricsDto);
        }

        private void AddFabricContentTypes()
        {
            var contentTypes = _fabrics
                .Select(k => k.Content)
                .Distinct()
                .Select(a => new ContentType(_lazyLoader) { Name = a })
                .ToArray();
            _dbContext.ContentTypes.AddRange(contentTypes);
        }

        private void AddFabricTypes()
        {
            var fabricTypes = _fabrics
                .Select(k => k.Type)
                .Distinct()
                .Select(a => new FabricType(_lazyLoader) { Name = a })
                .ToArray();
            _dbContext.FabricTypes.AddRange(fabricTypes);
        }

        private void AddPatterns(IEnumerable<KitModel> kits)
        {
            var dtoPatterns = new List<Pattern>();
            foreach (var kit in kits.Where(k => k.KitType == KitType.DesignerPattern))
            {
                var dto = new Pattern((_lazyLoader));
                dto.Title = kit.Title;
                dto.AuthorId = _dbContext.PatternAuthors.First(f => f.Name == kit.Manufacturer).Id;
                dto.Item = kit.Item;
                dto.ColorsCount = 0;
                dto.Width = (short)kit.Size.Width;
                dto.Height = (short)kit.Size.Height;
                dto.Image = kit.ImageUrl;
                dto.Link = String.Empty;
                dtoPatterns.Add(dto);
            }

            _dbContext.Patterns.AddRange(dtoPatterns);
        }

        private void AddPatternAuthors(IEnumerable<KitModel> kits)
        {
            var authors = kits
                .Where(k => k.KitType == KitType.DesignerPattern)
                .Select(k => k.Manufacturer)
                .Distinct()
                .Select(a => new PatternAuthor(_lazyLoader) { Name = a })
                .ToArray();
            _dbContext.PatternAuthors.AddRange(authors);
        }

        private void AddKitManufacturers(IEnumerable<KitModel> kits)
        {
            var manufacturers = kits
                .Where(k => k.KitType == KitType.ManufacturerKit)
                .Select(k => k.Manufacturer)
                .Distinct()
                .Select(a => new KitManufacturer(_lazyLoader) { Name = a })
                .ToArray();
            _dbContext.KitManufacturers.AddRange(manufacturers);
        }
    }
}
