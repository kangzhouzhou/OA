using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    public class DictUser
    {
        public DictUser()
        {
            //避免潜在的未进行实例化而抛出的异常问题。
            DictRole = new List<DictRole>();
        }

        public int ID { get; set; }

        public string UID { get; set; }

        public string UName { get; set; }

        public string Upwd { get; set; }

        public string Remark { get; set; }

        public DateTime CreateTime { get; set; }

        public System.DateTime? ModifiedTime { get; set; }

        public virtual DictUserInfo DictUserInfo { get; set; }

        public virtual ICollection<DictRole> DictRole { get; set; }
    }

    public class DictUserMap : EntityTypeConfiguration<DictUser>
    {
        public DictUserMap()
        {
            ToTable("DictUser").HasKey(u => u.ID).Property(u => u.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.UID).IsRequired().HasColumnType("Varchar").HasMaxLength(8);
            Property(u => u.UName).IsRequired().HasColumnType("Varchar").HasMaxLength(32);
            Property(u => u.Upwd).IsRequired().HasColumnType("Varchar").HasMaxLength(128);
            Property(u => u.CreateTime).IsRequired();

            //配置多对多关系
            HasMany<DictRole>(u => u.DictRole).WithMany(r => r.DictUser).Map(m =>
            {
                m.ToTable("UserRole");
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
            });

            HasOptional(u => u.DictUserInfo).WithRequired(i => i.DictUser);
        }
    }
}
