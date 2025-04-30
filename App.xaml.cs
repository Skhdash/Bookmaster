using Bookmaster.Model;
using System.Windows;

namespace Bookmaster
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static BookmasterEntities context = new BookmasterEntities();
    }
}
