using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndroidLanches.API.Domain.Repositories
{
    public interface IMesas
    {
        List<Mesa> ObterDesocupadas();
    }
}
