using Sales.Domain.Models;

namespace Sales.Backend.Models
{
    /// <summary>
    /// Herencia de <see cref="DataContext"/> para facilitar las migraciones, los modelos ya estan en el datacontext
    /// desde la clase superior
    /// </summary>
    public class LocalDataContext : DataContext
    {

    }
}