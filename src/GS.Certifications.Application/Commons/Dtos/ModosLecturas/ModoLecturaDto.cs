using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Domain.Entities.Empresas.ModosLecturas;
using GS.Certifications.Domain.Entities.ModosLecturas;

namespace GS.Certifications.Application.Commons.Dtos.ModosLecturas
{
    public class ModoLecturaDto
    {
        public short Idm { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
    }
    public class MappingProfileModoLectura : Profile
    {
        public MappingProfileModoLectura()
        {
            CreateMap<ModoLectura, ModoLecturaDto>();
        }
    }
}
