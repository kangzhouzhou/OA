using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class DictReportArgument
    {
        public int ID { get; set; }

        public string ArgumentText { get; set; }

        public string ArgumentName { get; set; }

        public int DictReportID { get; set; }

        public int DictArgumentID { get; set; }

        public DictReport DcitReport { get; set; }

        public DictArgument DictArgument { get; set; }

    }

    public class DictReportArgumentMap : EntityTypeConfiguration<DictReportArgument>
    {
        public DictReportArgumentMap()
        {
            ToTable("DictReportArgument").HasKey(a => a.ID);
            Property(a => a.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(a => a.ArgumentText).IsRequired().HasColumnType("Varchar").HasMaxLength(32);
            Property(a => a.ArgumentName).IsRequired().HasColumnType("Varchar").HasMaxLength(32);

            HasRequired(ra => ra.DcitReport).WithMany(r=>r.DictReportArgument).HasForeignKey(ra => ra.DictReportID).WillCascadeOnDelete();
            HasRequired(ra => ra.DictArgument).WithMany().HasForeignKey(ra => ra.DictArgumentID);
        }
    }
}
