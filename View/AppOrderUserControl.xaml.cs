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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmaginMA_2025_11_27.View
{
    /// <summary>
    /// Логика взаимодействия для AppOrderUserControl.xaml
    /// </summary>
    public partial class AppOrderUserControl : UserControl
    {
        public AppOrderUserControl(AppUserModel appUser, OrderProductModel orderProduct)
        {
            InitializeComponent();

            AppUser = appUser;
            AppOrderProduct = orderProduct;
        }

        public AppUserModel AppUser { get; set; }
        public OrderProductModel AppOrderProduct { get; set; }
    }
}
