using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class heartbeatlogMap : EntityTypeConfiguration<heartbeatlog>
    {
        public heartbeatlogMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_log)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("heartbeatlog", "icmdb");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.C_log).HasColumnName("_log");
            this.Property(t => t.C_logtime).HasColumnName("_logtime");
        }
    }
}
