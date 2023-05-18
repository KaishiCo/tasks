using System.Data;

namespace Application.Data;

public interface IDbConnectionFactory
{
    public Task<IDbConnection> CreateConnectionAsync();
}
