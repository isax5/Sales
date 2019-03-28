using System.Globalization;

namespace Sales.Interfaces
{
    public interface ILocalize
    {
        /// <summary>
        /// Retorna idioma del telefono
        /// </summary>
        /// <returns></returns>
        CultureInfo GetCurrentCultureInfo();

        /// <summary>
        /// Cambiar configuracion de telefono
        /// </summary>
        /// <param name="ci"></param>
        void SetLocale(CultureInfo ci);
    }
}
