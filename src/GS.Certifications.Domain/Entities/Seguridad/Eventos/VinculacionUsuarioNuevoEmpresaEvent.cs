using GS.Certifications.Domain.Entities.Empresas;
using GSF.Domain.Common;
using GSF.Domain.Entities.Security;

namespace GS.Certifications.Domain.Entities.Seguridad.Eventos
{
    public class VinculacionUsuarioNuevoEmpresaEvent : DomainEvent
    {
        public User User { get; }
        public long DomainFIdm { get; }

        public EmpresaPortal EmpresaPortal { get; }
        public VinculacionUsuarioNuevoEmpresaEvent(User usuario, long domainF, EmpresaPortal empresa)
        {
            User = usuario;
            DomainFIdm = domainF;
            EmpresaPortal = empresa;
        }
    }
}
