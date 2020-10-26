using AndroidLanches.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AndroidLanches.API.Infra.EF.Configs
{
    public class PedidoConfig : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(e => e.Numero);
        }
    }
}
