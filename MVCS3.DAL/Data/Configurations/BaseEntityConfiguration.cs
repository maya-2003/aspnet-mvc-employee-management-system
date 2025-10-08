using MVCS3.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.DAL.Data.Configurations
{
    public class BaseEntityConfiguration<T>:IEntityTypeConfiguration<T> where T : BaseEntity
    {
        

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(d => d.LastModifiedon).HasComputedColumnSql("GETDATE()");
        }
    }
}
