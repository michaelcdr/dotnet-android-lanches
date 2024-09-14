using AndroidLanches.Domain.Entities;
using AndroidLanches.Domain.Filtros;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidLanches.Domain.Repositories
{
    public interface IProdutosRepository
    {
        Task AdicionarBebida(Bebida produto);
        Task AdicionarPrato(Prato produto);
        Task<List<Bebida>> ObterBebidas(FiltrosBebida filtros);
        Task<List<Prato>> ObterPratos(FiltrosPrato filtros);
        void Dispose();
    }
}
