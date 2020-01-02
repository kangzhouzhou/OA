using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class DictArgument
    {
        public int ID {get;set;}

        public string Name { get; set; }

        public string Format { get; set; }

        public string Remark { get; set; }

        public string Script { get; set; }
    }

    public class DictArgumentMap : EntityTypeConfiguration<DictArgument>
    {

        public DictArgumentMap()
        {
            ToTable("DictArgument").HasKey(a => a.ID);
            Property(a => a.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(a => a.Name).IsRequired().HasColumnType("Varchar").HasMaxLength(32);
            Property(a => a.Format).IsRequired().HasColumnType("Varchar").HasMaxLength(32);
            Property(a => a.Remark).HasColumnType("Varchar").HasMaxLength(64);
            Property(a => a.Remark).HasColumnType("Varchar");
        }
    }
}
