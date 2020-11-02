using System.Data;

namespace AndroidLanches.Infra.DBConfiguration
{
    public interface IDatabaseFactory
    {
        IDbConnection GetDbConnection { get; }
    }
}
