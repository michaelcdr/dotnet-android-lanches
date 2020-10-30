using AndroidLanches.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidLanches.API.Domain.Repositories
{
    public interface IMesas
    {
        List<Mesa> ObterDesocupadas();
        Task Adicionar(Mesa mesa);
    }
}
