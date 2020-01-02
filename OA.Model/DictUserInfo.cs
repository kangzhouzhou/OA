using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class DictUserInfo
    {
        public int DictUserID { get; set; }

        public virtual DictUser DictUser { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        //public int DictEducation { get; set; }


        public int DictEducationID { get; set; }
        
        /// <summary>
        /// 学历
        /// </summary>
        public DictEducation DictEducation { get; set; }

        /// <summary>
        /// 工资
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string NativePlace { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime EntryTime { get; set; }

    }
    public class DictUserInfoMap : EntityTypeConfiguration<DictUserInfo>
    {
        public DictUserInfoMap()
        {
            ToTable("DictUserInfo").HasKey(u => u.DictUserID);
            Property(u => u.Birthday).IsRequired().HasColumnType("Date");
            Property(u => u.DictEducationID).IsRequired();
            Property(u => u.Salary).IsRequired().HasColumnType("Decimal").HasPrecision(10, 2);
            Property(u => u.IdentityCard).IsRequired().HasColumnType("Varchar").HasMaxLength(32);
            Property(u => u.Tel).IsRequired().HasColumnType("Varchar").HasMaxLength(32);
            Property(u => u.NativePlace).IsRequired().HasColumnType("Varchar").HasMaxLength(32);
            Property(u => u.EntryTime).IsRequired().HasColumnType("Date");
            Property(u => u.Sex).HasColumnType("Char").HasMaxLength(2);

            HasRequired(u => u.DictUser).WithOptional(u => u.DictUserInfo).WillCascadeOnDelete();
            HasRequired(u => u.DictEducation).WithMany().HasForeignKey(u => u.DictEducationID);
        }
    }
}
