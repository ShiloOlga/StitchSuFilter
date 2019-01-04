using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Web.Data.Entities;
using Web.Models;

namespace Web.Data.Mappings
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Pattern, KitModel>()
                .ForMember(p => p.Size, e => e.MapFrom(p => new Size { Width = p.Width, Height = p.Height }))
                .ForMember(p => p.ImageUrl, expr => expr.MapFrom(p => p.Image))
                .ForMember(p => p.Manufacturer, expr => expr.MapFrom(p => p.Author.Name))
                .ForMember(p => p.KitType, expr => expr.MapFrom(p => KitType.DesignerPattern));

            CreateMap<PatternAuthor, AuthorModel>();
            CreateMap<FabricOption, FabricModel>()
                .ForMember(p => p.Name, expr => expr.MapFrom(p => p.FabricItem.Fabric.Name))
                .ForMember(p => p.Count, expr => expr.MapFrom(p => p.FabricItem.Fabric.Count))
                .ForMember(p => p.Sku, expr => expr.MapFrom(p => $"{p.FabricItem.Sku}/{p.FabricItem.ColorId} - {p.FabricItem.ColorName}"));
            CreateMap<ICollection<ThreadColorOption>, Dictionary<PaletteModel, IEnumerable<ColorModel>>>()
                .ConvertUsing<DictConverter>();

            CreateMap<Pattern, PatternModel>()
                .ForMember(p => p.Size, e => e.MapFrom(p => new Size { Width = p.Width, Height = p.Height }))
                .ForMember(p => p.Author, expr => expr.MapFrom(p => p.Author))
                .ForMember(p => p.ImageUrl, expr => expr.MapFrom(p => p.Image))
                .ForMember(p => p.FabricItems, expr => expr.MapFrom(p => p.FabricOptions))
                .ForMember(p => p.ColorsMap, expr => expr.MapFrom(p => p.ThreadColorOptions));

            CreateMap<FabricItem, FabricItemModel>();
        }
    }

    internal class DictConverter : ITypeConverter<ICollection<ThreadColorOption>, Dictionary<PaletteModel, IEnumerable<ColorModel>>>
    {
        public Dictionary<PaletteModel, IEnumerable<ColorModel>> Convert(ICollection<ThreadColorOption> source, Dictionary<PaletteModel,
            IEnumerable<ColorModel>> destination, ResolutionContext context)
        {
            var result = new Dictionary<PaletteModel, IEnumerable<ColorModel>>();
            if (source != null && source.Count > 0)
            {
                var groups = source.GroupBy(s => s.ThreadColor.Manufacturer);
                foreach (var g in groups)
                {
                    result[new PaletteModel {Name = g.Key.Name}] = g.Select(i => new ColorModel
                    {
                        Color = $"{i.ThreadColor.ColorId} - {i.ThreadColor.ColorName}", Length = i.RequiredLength ?? 0
                    });
                }
            }
            return result;
        }
    }
}
