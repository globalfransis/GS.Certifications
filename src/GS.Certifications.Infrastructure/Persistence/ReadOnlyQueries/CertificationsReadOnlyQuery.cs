using GS.Certifications.Application.CQRS.ReadOnlyQueries;
using GSF.Infrastructure.Persistence.CQRS.ReadOnlyQueries.Dapper;

namespace GS.Certifications.Infrastructure.Persistence.ReadOnlyQueries;

public class CertificationsReadOnlyQuery : DapperReadOnlyQuery, ICertificationsReadOnlyQuery
{
    public CertificationsReadOnlyQuery(ICertificationsReadOnlyQueryDataBaseConfiguration options) : base(options)
    {
    }
}
