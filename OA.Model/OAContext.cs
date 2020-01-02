using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class OAContext:DbContext
    {
        public OAContext()
            : base("name=conStr")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DictUserMap());
            modelBuilder.Configurations.Add(new DictRoleMap());
            modelBuilder.Configurations.Add(new DictMenuMap());
            modelBuilder.Configurations.Add(new DictUserInfoMap());
            modelBuilder.Configurations.Add(new DictEducationMap());
            modelBuilder.Configurations.Add(new DictReportMap());
            modelBuilder.Configurations.Add(new DictReportArgumentMap());
            modelBuilder.Configurations.Add(new DictArgumentMap());
            modelBuilder.Configurations.Add(new DictConfigMap());
            modelBuilder.Configurations.Add(new StaticPageRecordMap());
            modelBuilder.Configurations.Add(new StaticPageImgReocrdMap());
        }

        public DbSet<DictUser> DictUser { get; set; }

        public DbSet<DictRole> DictRole { get; set; }

        public DbSet<DictMenu> DictMenu { get; set; }

        public DbSet<DictUserInfo> DictUserInfo { get; set; }

        public DbSet<DictEducation> DictEducation { get; set; }

        public DbSet<DictReport> DictReport { get; set; }

        public DbSet<DictReportArgument> DictReportArgument { get; set; }

        public DbSet<DictArgument> DictArgument { get; set; }

        public DbSet<DictConfig> DictConfig { get; set; }

        public DbSet<StaticPageRecord> StaticPageRecord { get; set; }

        public DbSet<StaticPageImgReocrd> StaticPageImgReocrd { get; set; }
    }
}
