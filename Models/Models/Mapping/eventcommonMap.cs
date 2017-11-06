using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class eventcommonMap : EntityTypeConfiguration<eventcommon>
    {
        public eventcommonMap()
        {
            // Primary Key
            this.HasKey(t => new { t.srcaddr, t.time });

            // Properties
            this.Property(t => t.srcaddr)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.content)
                .HasMaxLength(250);

            this.Property(t => t.action)
                .HasMaxLength(250);

            this.Property(t => t.handler)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("eventcommon", "icmdb");
            this.Property(t => t.srcaddr).HasColumnName("_srcaddr");
            this.Property(t => t.time).HasColumnName("_time");
            this.Property(t => t.handlestatus).HasColumnName("_handlestatus");
            this.Property(t => t.handletime).HasColumnName("_handletime");
            this.Property(t => t.type).HasColumnName("_type");
            this.Property(t => t.content).HasColumnName("_content");
            this.Property(t => t.action).HasColumnName("_action");
            this.Property(t => t.handler).HasColumnName("_handler");
        }
    }
}
