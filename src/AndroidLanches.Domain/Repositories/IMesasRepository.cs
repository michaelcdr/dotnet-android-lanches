using AndroidLanches.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidLanches.API.Domain.Repositories
{
    public interface IMesasRepository : IDisposable
    {
        Task<List<Mesa>> ObterDesocupadas();
        Task Adicionar(Mesa mesa);
        Task<bool> TemAoMenosUma();
        Task<bool> Existe(int numero);
        Task<Mesa> ObterDesocupadaPorNumero(int numeroMesa);
        Task<Mesa> ObterDesocupadaPorId(int mesaId);
    }
}
