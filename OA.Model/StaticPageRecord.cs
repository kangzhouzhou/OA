using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class StaticPageRecord
    {
        public StaticPageRecord()
        {
            ImgRecord = new List<StaticPageImgReocrd>();
        }

        public int ID { get; set; }

        public string PageName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual ICollection<StaticPageImgReocrd> ImgRecord { get; set; }
    }

    public class StaticPageRecordMap: EntityTypeConfiguration<StaticPageRecord>
    {
        public StaticPageRecordMap()
        {
            ToTable("StaticPageRecord").HasKey(s => s.ID);
            Property(s => s.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(s => s.PageName).HasColumnType("Varchar").HasMaxLength(128);
            Property(s => s.Title).IsRequired().HasColumnType("Varchar").HasMaxLength(64);
            Property(s => s.Content).IsRequired().HasColumnType("Varchar").HasMaxLength(2048);

            HasMany(s => s.ImgRecord).WithRequired(si => si.StaticPageRecord).HasForeignKey(s => s.StaticPageRecordID).WillCascadeOnDelete();
        }
    }
}
