using GSF.Domain.Common;
using GSF.Domain.Entities.Companies;

namespace GS.Certifications.Domain.Entities.Percepciones;

public class PercepcionCompany : BaseIntEntity
{
    public short PercepcionId { get; set; }
    public Percepcion Percepcion { get; set; }
    public long CompanyId { get; set; }
    public Company Company { get; set; }
}
