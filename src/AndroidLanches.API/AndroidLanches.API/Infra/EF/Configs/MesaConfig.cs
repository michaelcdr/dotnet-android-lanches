//using AndroidLanches.API.Domain;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace AndroidLanches.API.Infra.EF.Configs
//{
//    public class MesaConfig : IEntityTypeConfiguration<Mesa>
//    {
//        public void Configure(EntityTypeBuilder<Mesa> builder)
//        {
//            builder.ToTable("Mesas");
//            builder.HasKey(e => e.MesaId);
//            builder.Property(e => e.Numero).IsRequired();
//        }
//    }
//}
