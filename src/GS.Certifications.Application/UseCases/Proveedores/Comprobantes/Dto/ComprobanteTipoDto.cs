using AutoMapper;
using GSF.Application.Common.Mappings;
using GSF.Domain.Entities.Global;
using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;

public class ComprobanteTipoDto : IMapFrom<ComprobanteTipo>
{
    public short Idm { get; set; }
    public string Descripcion { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ComprobanteTipo, ComprobanteTipoDto>();
        }
    }
}
