using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Empresas
{
    public class EmpresaCurrencyConfiguration : BaseWithInt32IdEntityConfiguration<EmpresaCurrency>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EmpresaCurrency> builder)
        {
            builder.ToTable("emp_EmpresaCurrency");
        }
    }
}
