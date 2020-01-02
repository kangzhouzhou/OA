using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class DictMenu
    {
        public DictMenu()
        {
            DictRole = new List<DictRole>();
        }

        public int ID { get; set; }

        public string MenuName { get; set; }

        public string MenuPath { get; set; }

        public int ParentID { get; set; }

        public string ImagePath { get; set; }

        public int Remark { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual ICollection<DictRole> DictRole { get; set; }
    }

    public class DictMenuMap : EntityTypeConfiguration<DictMenu>
    {
        public DictMenuMap()
        {
            ToTable("DictMenu").HasKey(m => m.ID);
            Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.ParentID).IsRequired();
            Property(m => m.MenuPath).IsRequired().HasColumnType("Varchar").HasMaxLength(64);
            Property(m => m.MenuName).IsRequired().HasColumnType("Varchar").HasMaxLength(32);
            //Property(m => m.ImagePath).HasColumnType("Varchar").HasMaxLength(256);
            HasMany(m => m.DictRole).WithMany(r => r.DictMenu).Map(m =>
            {
                m.ToTable("RoleMenu");
                m.MapLeftKey("RoleId");
                m.MapRightKey("MenuId");
            });
        }
    }

}
