using System.Threading.Tasks;

namespace AndroidLanches.Infra.DBConfiguration
{
    public interface ICriadorBancoDeDados
    {
        Task Criar();
    }
}
