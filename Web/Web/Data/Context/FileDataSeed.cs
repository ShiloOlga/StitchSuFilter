using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.Data.Entities;
using Web.Models.V2;

namespace Web.Data.Context
{
    public class FileDataSeed
    {
        private const string FileName = "backup.json";
        private ICollection<KitModel> _kits;
        private ICollection<string> _kitManufacturers;
        private ICollection<string> _patternAuthors;
        private ICollection<PatternModel> _patterns;
        private ICollection<string> _fabricTypes;
        private ICollection<string> _fabricContentTypes;
        private ICollection<string> _threadManufacturers = new[]{"ПНК", "DMC"};
        private ICollection<FabricModel> _fabrics;
        private ICollection<ThreadColorModel> _threads;

        public FileDataSeed()
        {
        }

        public void Execute()
        {
            if (File.Exists(FileName))
            {
                var result = File.ReadAllText(FileName);
                _kits = JsonConvert.DeserializeObject<IEnumerable<KitModel>>(result).ToArray();

                _kitManufacturers = GetKitManufacturers(_kits);
                _patternAuthors = GetPatternAuthors(_kits);
                _patterns = GetPatterns(_kits);
            }

            _fabricTypes = GetFabricTypes();
            _fabricContentTypes = GetFabricContentTypes();
            _fabrics = GetFabrics(_fabricTypes, _fabricContentTypes);
            //AddKits();
            var threads = new List<ThreadColorModel>(GetPnkPalette());
            threads.AddRange(GetDmcPalette());
            _threads = threads;
            //AddPatternThreads();
        }

        private ICollection<ThreadColorModel> GetPnkPalette()
        {
            var threads = new List<ThreadColorModel>();
            foreach (var thread in Data.PnkColors.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var model = new ThreadColorModel();
                model.Manufacturer = "ПНК";
                var parts = thread.Split(',').Select(t => t.Trim()).ToArray();
                model.ColorId = parts[0];
                model.ColorName = parts[1];
                model.RgbColor = parts[2];
                model.Length = 10;
                model.Sku = "";
                threads.Add(model);
            }
            return threads;
        }

        private ICollection<ThreadColorModel> GetDmcPalette()
        {
            var threads = new List<ThreadColorModel>();
            foreach (var thread in Data.DmcColors.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var model = new ThreadColorModel();
                model.Manufacturer = "DMC";
                var parts = thread.Split(',').Select(t => t.Trim()).ToArray();
                model.ColorId = parts[0];
                model.ColorName = parts[1];
                model.RgbColor = parts[2];
                model.Length = 8;
                model.Sku = "117S";
                threads.Add(model);
            }
            return threads;
        }

        //    private void AddKits()
        //    {
        //        _dbContext.PatternAuthors.Load();
        //        _dbContext.KitManufacturers.Load();
        //        _dbContext.ThreadManufacturers.Load();
        //        _dbContext.Fabrics.Load();
        //        _dbContext.FabricItems.Load();
        //        var dtoKits = new List<Kit>();
        //        foreach (var seedKit in _kits.Skip(1))
        //        {
        //            var dto = new Kit((_lazyLoader));
        //            dto.Title = seedKit.Title;
        //            var author = _dbContext.PatternAuthors.Local.FirstOrDefault(a => a.Name == seedKit.Author);
        //            if (author == null && !string.IsNullOrEmpty(seedKit.Author))
        //            {
        //                author = new PatternAuthor((_lazyLoader)) { Name = seedKit.Author };
        //                _dbContext.PatternAuthors.Add(author);
        //            }
        //            dto.Author = author;
        //            dto.ColorsCount = seedKit.ColorsCount;
        //            dto.Item = seedKit.Item;
        //            var manufacturer = _dbContext.KitManufacturers.Local.FirstOrDefault(a => a.Name.ToLowerInvariant() == seedKit.Manufacturer.ToLowerInvariant());
        //            if (manufacturer == null)
        //            {
        //                manufacturer = new KitManufacturer((_lazyLoader)) { Name = seedKit.Manufacturer };
        //                _dbContext.KitManufacturers.Add(manufacturer);
        //            }
        //            dto.Manufacturer = manufacturer;
        //            dto.Image = seedKit.Image;
        //            dto.Link = seedKit.Link;
        //            dto.WidthSm = seedKit.WidthSm;
        //            dto.HeightSm = seedKit.HeightSm;
        //            dto.WidthStitches = seedKit.WidthStitches;
        //            dto.HeightStitches = seedKit.HeightStitches;
        //            var threadManufacturer = _dbContext.ThreadManufacturers.Local.FirstOrDefault(m => m.Name.ToLowerInvariant() == seedKit.ThreadManufacturer.ToLowerInvariant());
        //            if (threadManufacturer == null)
        //            {
        //                threadManufacturer = new ThreadManufacturer(_lazyLoader) { Name = seedKit.ThreadManufacturer };
        //                _dbContext.ThreadManufacturers.Add(threadManufacturer);
        //            }
        //            dto.ThreadManufacturer = threadManufacturer;
        //            var fabricItem = _dbContext.FabricItems.Local.FirstOrDefault(f => f.Fabric != null && f.Fabric.Name == seedKit.FabricItem);
        //            if (fabricItem == null)
        //            {
        //                fabricItem = new FabricItem((_lazyLoader)) { Fabric = _dbContext.Fabrics.First(f => f.Name == seedKit.FabricItem), Sku = "KitModel", ColorId = "-", ColorName = "-" };
        //                _dbContext.FabricItems.Add(fabricItem);
        //            }
        //            dto.FabricItem = fabricItem;
        //            dtoKits.Add(dto);
        //        }

        //        _dbContext.Kits.AddRange(dtoKits);
        //    }

        private ICollection<FabricModel> GetFabrics(ICollection<string> fabricTypes, ICollection<string> contentTypes)
        {
            var fabricModels = new List<FabricModel>(Data.Fabrics.Count);
            foreach (var fabric in Data.Fabrics)
            {
                var item = new FabricModel();
                item.Name = fabric.Name;
                item.Count = fabric.Count;
                item.Priority = fabric.Priority;
                item.FabricType = fabricTypes.First(f => f == fabric.Type);
                item.ContentType = contentTypes.First(f => f == fabric.Content);
                fabricModels.Add(item);
            }

            return fabricModels;
        }

        private ICollection<string> GetFabricContentTypes()
        {
            return Data.Fabrics
                .Select(k => k.Content)
                .Distinct()
                .ToArray();
        }

        private ICollection<string> GetFabricTypes()
        {
            return Data.Fabrics
                .Select(k => k.Type)
                .Distinct()
                .ToArray();
        }

        private ICollection<PatternModel> GetPatterns(IEnumerable<KitModel> kits)
        {
            var patterns = new List<PatternModel>();
            foreach (var pattern in kits.Where(k => k.KitType == KitType.DesignerPattern))
            {
                var model = new PatternModel
                {
                    Title = pattern.Title,
                    Author = pattern.Manufacturer,
                    Item = pattern.Item,
                    ColorsCount = 0,
                    Size = pattern.Size,
                    ImageUrl = pattern.ImageUrl,
                    Link = string.Empty
                };
                patterns.Add(model);
            }

            return patterns;
        }

        private ICollection<string> GetPatternAuthors(IEnumerable<KitModel> kits)
        {
            return kits
                .Where(k => k.KitType == KitType.DesignerPattern)
                .Select(k => k.Manufacturer)
                .Distinct()
                .ToArray();
        }

        private ICollection<string> GetKitManufacturers(IEnumerable<KitModel> kits)
        {
            return kits
                .Where(k => k.KitType == KitType.ManufacturerKit)
                .Select(k => k.Manufacturer)
                .Distinct()
                .ToArray();
        }
    }
}
