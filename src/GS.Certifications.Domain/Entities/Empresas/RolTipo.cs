using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Empresas;

public class RolTipo : BaseFixedShortEntity
{
    public const short PROV_IDM = 1;
    public const short CONT_IDM = 2;
    public const short CLI_IDM = 3;

    public const string PROV_DESCRIPCION = "Proveedor";
    public const string CONT_DESCRIPCION = "Contratista";
    public const string CLI_DESCRIPCION = "Cliente";

    public const string PROV_CODIGO = "PROV";
    public const string CONT_CODIGO = "CONT";
    public const string CLI_CODIGO = "PCLI";
    public string Descripcion { get; set; }
    public string Codigo { get; set; }
}
