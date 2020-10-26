using AndroidLanches.API.Domain;
using AndroidLanches.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndroidLanches.API.Infra.Repositories
{
    public class Mesas : IMesas
    {
        public Mesas()
        {

        }
        public List<Mesa> ObterDesocupadas()
        {
            return new List<Mesa>();
        }
    }
}
