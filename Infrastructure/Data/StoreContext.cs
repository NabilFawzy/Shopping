using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEntities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext( DbContextOptions<StoreContext> options) :
         base(options)
        {
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> productTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
              base.OnModelCreating(modelBuilder);
              modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

              if(Database.ProviderName=="Microsoft.EntityFrameworkCore.Sqlite"){
                   foreach (var entityModel in modelBuilder.Model.GetEntityTypes())
                   {
                       var properties=entityModel.ClrType.GetProperties().Where(p=>p.PropertyType==typeof(decimal));

                       foreach(var prop in properties){
                           modelBuilder.Entity(entityModel.Name).Property(prop.Name).HasConversion<double>();
                       }
                   }
              }
        }
    }

}