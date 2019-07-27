using Sales.ViewModels;

namespace Sales.Infrastructure
{
    /// <summary>
    /// Expone principal ViewModel al comenzar la app
    /// </summary>
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
