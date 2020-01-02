using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    public class DictRole
    {
        public DictRole()
        {
            DictUser = new List<DictUser>();
            DictMenu = new List<DictMenu>();
        }

        public int ID { get; set; }

        public string RoleName { get; set; }

        public string Remark { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public virtual ICollection<DictUser> DictUser { get; set; }

        public virtual ICollection<DictMenu> DictMenu { get; set; }
    }

    public class DictRoleMap : EntityTypeConfiguration<DictRole>
    {
        public DictRoleMap()
        {
            ToTable("DictRole");
            HasKey(r => r.ID).Property(r => r.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(r => r.RoleName).IsRequired().HasColumnType("Varchar").HasMaxLength(32);
            Property(r => r.CreateTime).IsRequired().HasColumnType("Datetime");
            Property(r => r.CreateTime).HasColumnType("Datetime");
            HasMany(r => r.DictUser).WithMany(u => u.DictRole).Map(m =>
            {
                m.ToTable("UserRole");
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
            });

            HasMany(r => r.DictMenu).WithMany(m => m.DictRole).Map(m =>
            {
                m.ToTable("RoleMenu");
                m.MapLeftKey("RoleId");
                m.MapRightKey("MenuId");
            });
        }
    }
}
