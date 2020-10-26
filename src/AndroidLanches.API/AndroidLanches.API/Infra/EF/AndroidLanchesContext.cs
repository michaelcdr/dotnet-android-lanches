using AndroidLanches.API.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndroidLanches.API.Infra.EF
{
    public class AndroidLanchesContext : DbContext
    {
        public DbSet<Mesa> Mesas { get; set; }
    }
}
