using AndroidLanches.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndroidLanches.Domain.Repositories
{
    public interface IProdutos
    {
        Task AdicionarBebida(Bebida produto);
        Task AdicionarPrato(Prato produto);
    }
}
