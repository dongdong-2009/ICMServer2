using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class eventwarnMap : EntityTypeConfiguration<eventwarn>
    {
        public eventwarnMap()
        {
            // Primary Key
            this.HasKey(t => new { t.srcaddr, t.time, t.channel, t.Action });

            // Properties
            this.Property(t => t.srcaddr)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.type)
                .HasMaxLength(50);

            this.Property(t => t.channel)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Action)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.handler)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("eventwarn", "icmdb");
            this.Property(t => t.srcaddr).HasColumnName("_srcaddr");
            this.Property(t => t.time).HasColumnName("_time");
            this.Property(t => t.handlestatus).HasColumnName("_handlestatus");
            this.Property(t => t.handletime).HasColumnName("_handletime");
            this.Property(t => t.type).HasColumnName("_type");
            this.Property(t => t.channel).HasColumnName("_channel");
            this.Property(t => t.Action).HasColumnName("_action");
            this.Property(t => t.handler).HasColumnName("_handler");
        }
    }
}
