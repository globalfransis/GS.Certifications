using GSF.Domain.Entities.Companies;

namespace GS.Certifications.Domain.Commons.Exceptions.EmailProcessor;

public class CuitCompanyConflictException : EmailProcessorInvalidOperationException
{
    public override string StatusCode { get; } = "CNY_CUIT_CNF";
    public Company Company { get; set; }
    public string Cuit { get; }

    public CuitCompanyConflictException(Company company, string cuit)
        : base($"El cuit de la compania {company.Id} no coincide con el del comprobante: {cuit}.")
    {
        Company = company;
        Cuit = cuit;
    }

}
