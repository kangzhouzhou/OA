using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class DictConfig
    {
        public string Key { get; set; }
        
        public string Value { get; set; }

        public string Memo { get; set; }

    }

    public class DictConfigMap : EntityTypeConfiguration<DictConfig>
    {
        public DictConfigMap()
        {
            ToTable("DictConfig").HasKey(c => c.Key);
            Property(c => c.Key).IsRequired().HasColumnType("Varchar").HasMaxLength(8);
            Property(c=>c.Value).IsRequired().HasColumnType("Varchar").HasMaxLength(8);
            Property(c => c.Memo).IsRequired().HasColumnType("Varchar").HasMaxLength(64);
        }
    }
}
