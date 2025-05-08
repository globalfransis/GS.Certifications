using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Dtos.Empresas
{
    public class TipoCuentaDto
    {
        public short Idm { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class TipoCuentaDtoProfile : Profile
    {
        public TipoCuentaDtoProfile()
        {
            CreateMap<TipoCuenta, TipoCuentaDto>();
        }
    }
}
