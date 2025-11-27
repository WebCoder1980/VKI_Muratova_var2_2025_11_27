using SmaginMA_2025_11_27.Db;
using SmaginMA_2025_11_27.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmaginMA_2025_11_27.AppWindow
{
    /// <summary>
    /// Логика взаимодействия для FishingProductsWindow.xaml
    /// </summary>
    public partial class FishingProductsWindow : Window
    {
        private AppDb Db { get; set; }
        private AppUserModel? CurrentUser { get; set; }

        public FishingProductsWindow(AppUserModel? user = null)
        {
            InitializeComponent();

            Db = AppDb.GetInstance();

            CurrentUser = user;
        }
    }
}
