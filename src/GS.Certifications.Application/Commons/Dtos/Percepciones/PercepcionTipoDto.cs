using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Impuestos;
using GS.Certifications.Domain.Entities.Impuestos;
using GS.Certifications.Domain.Entities.Percepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Dtos.Percepciones
{
    public class PercepcionTipoDto
    {
        public short Idm { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public bool General { get; set; }

        public class MappingImpuestoProfile : Profile
        {
            public MappingImpuestoProfile()
            {
                CreateMap<PercepcionTipo, PercepcionTipoDto>();
            }
        }
    }
}
