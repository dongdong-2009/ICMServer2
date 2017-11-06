using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class leavewordMap : EntityTypeConfiguration<leaveword>
    {
        public leavewordMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.filenames)
                .HasMaxLength(250);

            this.Property(t => t.src_addr)
                .HasMaxLength(20);

            this.Property(t => t.dst_addr)
                .HasMaxLength(20);

            this.Property(t => t.time)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("leaveword", "icmdb");
            this.Property(t => t.id).HasColumnName("_id");
            this.Property(t => t.filenames).HasColumnName("_filenames");
            this.Property(t => t.src_addr).HasColumnName("_src_addr");
            this.Property(t => t.dst_addr).HasColumnName("_dst_addr");
            this.Property(t => t.time).HasColumnName("_ttime");
            this.Property(t => t.readflag).HasColumnName("_readflag");
        }
    }
}
