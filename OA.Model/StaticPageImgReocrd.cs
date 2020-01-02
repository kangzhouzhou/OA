using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class StaticPageImgReocrd
    {
        public int ID { get; set; }

        public string ImgPath { get; set; }

        public string ImgMemo { get; set; }

        public int StaticPageRecordID { get; set; }

        public StaticPageRecord StaticPageRecord { get; set; }
    }

    public class StaticPageImgReocrdMap : EntityTypeConfiguration<StaticPageImgReocrd>
    {
        public StaticPageImgReocrdMap()
        {
            ToTable("StaticPageImgReocrd").HasKey(i => i.ID);
            Property(i => i.ImgPath).IsRequired();
            Property(i => i.StaticPageRecordID).IsRequired();
            Property(i => i.ImgMemo).HasColumnType("Varchar").HasMaxLength(64);
            HasRequired(i => i.StaticPageRecord).WithMany(s => s.ImgRecord).HasForeignKey(s=>s.StaticPageRecordID).WillCascadeOnDelete();
        }
    }
}
