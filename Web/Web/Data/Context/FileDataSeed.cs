using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Web.Models;

namespace Web.Data.Context
{
    public class FileDataSeed
    {
        private const string FileName = "backup.json";

        public FileDataSeed()
        {
        }

        public void Execute()
        {
            if (File.Exists(FileName))
            {
                var result = File.ReadAllText(FileName);
                var kits = JsonConvert.DeserializeObject<IEnumerable<KitModel>>(result).ToArray();

                //    AddKitManufacturers(kits);
                //    AddPatternAuthors(kits);
                //    AddPatterns(kits);
            }

                //AddFabricTypes();
                //AddFabricContentTypes();
                //AddFabrics();
                //AddKits();
                //AddDmcPalette();
                //AddPnkPalette();
                //AddPatternThreads();
            }

        //    private void AddPnkPalette()
        //    {

        //        var dtoThreads = new List<ThreadColor>();
        //        var content = File.ReadAllText("pnk.json");
        //        var jsonArray = JsonConvert.DeserializeObject(content) as JArray;
        //        var manufacturer = _dbContext.ThreadManufacturers.First(m => m.Name.StartsWith("ПНК"));
        //        foreach (var token in jsonArray)
        //        {
        //            var dto = new ThreadColor(_lazyLoader);
        //            dto.Manufacturer = manufacturer;
        //            var name = token["Name"].Value<string>();
        //            dto.ColorId = name;
        //            dto.ColorName = name;
        //            var rgb = token["Rgb"].Value<string>();
        //            dto.RgbColor = rgb;
        //            dto.Length = 10;
        //            dto.Sku = "";
        //            dtoThreads.Add(dto);
        //        }
        //        _dbContext.ThreadColors.AddRange(dtoThreads);
        //    }

        //    private void AddDmcPalette()
        //    {
        //        var dtoThreads = new List<ThreadColor>();
        //        var manufacturer = _dbContext.ThreadManufacturers.First(m => m.Name == "DMC");
        //        foreach (var thread in _dmcColors.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            var parts = thread.Split(',').Select(t => t.Trim()).ToArray();
        //            var dto = new ThreadColor(_lazyLoader);
        //            dto.Manufacturer = manufacturer;
        //            dto.ColorId = parts[0];
        //            dto.ColorName = parts[1];
        //            dto.RgbColor = parts[2];
        //            dto.Length = 8;
        //            dto.Sku = "117S";
        //            dtoThreads.Add(dto);
        //        }

        //        _dbContext.ThreadColors.AddRange(dtoThreads);
        //    }

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

        //    private void AddFabrics()
        //    {
        //        var fabricsDto = new List<Entities.Fabric>(_fabrics.Count);
        //        foreach (var fabric in _fabrics)
        //        {
        //            var dto = new Entities.Fabric(_lazyLoader);
        //            dto.Name = fabric.Name;
        //            dto.Count = fabric.Count;
        //            dto.Priority = fabric.Priority;
        //            dto.FabricTypeId = _dbContext.FabricTypes.First(f => f.Name == fabric.Type).Id;
        //            dto.ContentTypeId = _dbContext.ContentTypes.First(f => f.Name == fabric.Content).Id;
        //            fabricsDto.Add(dto);
        //        }

        //        _dbContext.Fabrics.AddRange(fabricsDto);
        //    }

        //    private void AddFabricContentTypes()
        //    {
        //        var contentTypes = _fabrics
        //            .Select(k => k.Content)
        //            .Distinct()
        //            .Select(a => new ContentType(_lazyLoader) { Name = a })
        //            .ToArray();
        //        _dbContext.ContentTypes.AddRange(contentTypes);
        //    }

        //    private void AddFabricTypes()
        //    {
        //        var fabricTypes = _fabrics
        //            .Select(k => k.Type)
        //            .Distinct()
        //            .Select(a => new FabricType(_lazyLoader) { Name = a })
        //            .ToArray();
        //        _dbContext.FabricTypes.AddRange(fabricTypes);
        //    }

        //    private void AddPatterns(IEnumerable<KitModel> kits)
        //    {
        //        var dtoPatterns = new List<Pattern>();
        //        foreach (var kit in kits.Where(k => k.KitType == KitType.DesignerPattern))
        //        {
        //            var dto = new Pattern((_lazyLoader));
        //            dto.Title = kit.Title;
        //            dto.AuthorId = _dbContext.PatternAuthors.First(f => f.Name == kit.Manufacturer).Id;
        //            dto.Item = kit.Item;
        //            dto.ColorsCount = 0;
        //            dto.Width = (short)kit.Size.Width;
        //            dto.Height = (short)kit.Size.Height;
        //            dto.Image = kit.ImageUrl;
        //            dto.Link = String.Empty;
        //            dtoPatterns.Add(dto);
        //        }

        //        _dbContext.Patterns.AddRange(dtoPatterns);
        //    }

        //    private void AddPatternAuthors(IEnumerable<KitModel> kits)
        //    {
        //        var authors = kits
        //            .Where(k => k.KitType == KitType.DesignerPattern)
        //            .Select(k => k.Manufacturer)
        //            .Distinct()
        //            .Select(a => new PatternAuthor(_lazyLoader) { Name = a })
        //            .ToArray();
        //        _dbContext.PatternAuthors.AddRange(authors);
        //    }

        //    private void AddKitManufacturers(IEnumerable<KitModel> kits)
        //    {
        //        var manufacturers = kits
        //            .Where(k => k.KitType == KitType.ManufacturerKit)
        //            .Select(k => k.Manufacturer)
        //            .Distinct()
        //            .Select(a => new KitManufacturer(_lazyLoader) { Name = a })
        //            .ToArray();
        //        _dbContext.KitManufacturers.AddRange(manufacturers);
        //    }
    }
}
