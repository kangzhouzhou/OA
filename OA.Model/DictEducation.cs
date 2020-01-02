using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class DictEducation
    {
        public int ID { get; set; }

        public string EducationName { get; set; }
    }

    public class DictEducationMap : EntityTypeConfiguration<DictEducation>
    {
        public DictEducationMap()
        {
            ToTable("DictEducation").HasKey(e=>e.ID);
            Property(e => e.EducationName).IsRequired().HasMaxLength(32);
        }
    }
}
