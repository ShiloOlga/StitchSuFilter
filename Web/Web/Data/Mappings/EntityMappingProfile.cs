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
                .ForMember(p => p.Size, e => e.MapFrom(p => new Size{ Width = p.Width, Height = p.Height}))
                .ForMember(p => p.ImageUrl, expr => expr.MapFrom(p => p.Image))
                .ForMember(p => p.Manufacturer, expr => expr.MapFrom(p => p.Author.Name))
                .ForMember(p => p.KitType, expr => expr.MapFrom(p => KitType.DesignerPattern));
        }
    }
}
