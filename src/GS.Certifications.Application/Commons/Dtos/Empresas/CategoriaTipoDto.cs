using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Application.Commons.Dtos.Empresas;

public class CategoriaTipoDto : IMapFrom<CategoriaTipo>
{
    public short Idm { get; set; }
    public string Descripcion { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoriaTipo, CategoriaTipoDto>();
        }
    }
}
