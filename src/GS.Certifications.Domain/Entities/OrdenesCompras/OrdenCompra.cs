using GS.Certifications.Domain.Entities.Empresas;
using GSF.Domain.Common;
using GSF.Domain.Entities.Companies;
using GSF.Domain.Entities.Global;
using System;

namespace GS.Certifications.Domain.Entities.OrdenesCompras
{
    public class OrdenCompra : BaseIntEntity
    {
        public string NumeroOrden { get; set; }
        public DateTime Fecha { get; set; }
        public EmpresaPortal EmpresaPortal { get; set; }
        public int? EmpresaPortalId { get; set; }
        public OrdenCompraTipo OrdenCompraTipo { get; set; }
        public int? OrdenCompraTipoId { get; set; }
        public string CodigoHES { get; set; }
        public string CodigoQAD { get; set; }
        public OrdenCompraEstado OrdenCompraEstado { get; set; }
        public int? OrdenCompraEstadoIdm { get; set; }
        public decimal Importe { get; set; } = default;
        public Currency Moneda { get; set; }
        public long? MonedaId { get; set; }
        public string Observaciones { get; set; }
        public Company Company { get; set; }
        public long CompanyId { get; set; }
    }
}
