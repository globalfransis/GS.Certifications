using AutoMapper;
using GSF.Domain.Entities.Global;
using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Dtos.Empresas
{
    public class EmpresaCurrencyDto
    {
        public int Id { get; set; }
        public int EmpresaPortalId { get; set; }
        public Currency Currency { get; set; }
        public long CurrencyId { get; set; }
        public bool MonedaPorDefecto { get; set; }
    }

    public class MappingProfileEmpresaCurrency : Profile
    {
        public MappingProfileEmpresaCurrency()
        {
            CreateMap<EmpresaCurrency, EmpresaCurrencyDto>();
        }
    }
}
