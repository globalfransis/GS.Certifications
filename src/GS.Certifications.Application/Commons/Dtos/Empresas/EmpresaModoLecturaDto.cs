using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.ModosLecturas;
using GS.Certifications.Domain.Entities.Empresas.ModosLecturas;

namespace GS.Certifications.Application.Commons.Dtos.Empresas
{
    public class EmpresaModoLecturaDto
    {
        public int Id { get; set; }
        public int EmpresaPortalId { get; set; }
        public short ModoLecturaIdm { get; set; }
        public ModoLecturaDto ModoLectura { get; set; }
    }

    public class MappingProfileEmpresaModoLectura : Profile
    {
        public MappingProfileEmpresaModoLectura()
        {
            CreateMap<EmpresaModoLectura, EmpresaModoLecturaDto>();
        }
    }
}
