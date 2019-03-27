using Sales.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sales.Backend.Models
{
    /// <summary>
    /// Herencia de <see cref="DataContext"/> para facilitar las migraciones
    /// </summary>
    public class LocalDataContext : DataContext
    {
        // TODO: Para API usamos el DataContext de Domain, entoces quiero ver que pasa con este que sobre
        // escribe algo que ya tienen pero se usaran migraciones
        public System.Data.Entity.DbSet<Sales.Common.Models.Product> Products { get; set; }
    }
}