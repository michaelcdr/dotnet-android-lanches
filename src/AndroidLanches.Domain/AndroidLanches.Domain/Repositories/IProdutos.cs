using AndroidLanches.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidLanches.Domain.Repositories
{
    public interface IProdutos
    {
        Task AdicionarBebida(Bebida produto);
        Task AdicionarPrato(Prato produto);
        Task<List<Bebida>> ObterBebidas();
        Task<List<Prato>> ObterPratos();
    }
}
