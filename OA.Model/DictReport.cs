using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class DictReport
    {
        public DictReport()
        {
            DictReportArgument = new List<DictReportArgument>();
        }
        public int ID { get; set; }

        public string ReportName { get; set; }

        public string FuncName { get; set; }

        public string ReportFile { get; set; }

        public string Remark { get; set; }

        public ICollection<DictReportArgument> DictReportArgument { get; set; }
    }

    public class DictReportMap : EntityTypeConfiguration<DictReport>
    {
        public DictReportMap()
        {
            ToTable("DictReport").HasKey(r => r.ID);
            Property(r => r.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(r => r.ReportFile).IsRequired().HasColumnType("Varchar").HasMaxLength(128);
            Property(r => r.FuncName).IsRequired().HasColumnType("Varchar").HasMaxLength(64);
            Property(r => r.ReportName).IsRequired();
            Property(r => r.Remark).HasColumnType("Varchar").HasMaxLength(128);
        }
    }
}
