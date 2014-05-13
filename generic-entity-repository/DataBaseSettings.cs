using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Entity.Repository
{
    public class DataBaseSettings : DbContext
    {
        public DataBaseSettings()
        {
            this.Configuration.LazyLoadingEnabled = false; //Set it to true for lazy loading of objects.
        }

        /// <summary>
        /// Getting all mapping files and add in to modelbuilder.
        /// </summary>
        /// <param name="modelBuilder"> </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                              .Where(type => !string.IsNullOrEmpty(type.Namespace))
                              .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                                  type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var assembly in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(assembly);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
