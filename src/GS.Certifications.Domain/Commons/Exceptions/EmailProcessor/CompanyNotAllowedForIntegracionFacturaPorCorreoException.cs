using GSF.Domain.Entities.Companies;
using System;

namespace GS.Certifications.Domain.Commons.Exceptions.EmailProcessor;

public class CompanyNotAllowedForIntegracionFacturaPorCorreoException : InvalidOperationException
{
    public readonly string StatusCode = "CNY_N_ALW";
    public Company Company { get; }

    public CompanyNotAllowedForIntegracionFacturaPorCorreoException(Company company)
        : base($"La empresa: {company.Id}, no está habilitada para la integración de facturas por correo.")
    {
        Company = company;
    }
}
