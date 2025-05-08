using AutoMapper;
using GS.Certifications.Domain.Entities.Impuestos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Dtos.Impuestos
{
    public class ImpuestoTipoDto
    {
        public short Idm { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public bool General { get; set; }

        public class MappingImpuestoProfile : Profile
        {
            public MappingImpuestoProfile()
            {
                CreateMap<ImpuestoTipo, ImpuestoTipoDto>();
            }
        }
    }
}
