using Microsoft.EntityFrameworkCore;
using SmaginMA_2025_11_27.Db;
using SmaginMA_2025_11_27.Model;
using SmaginMA_2025_11_27.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AppOrdersWindow.xaml
    /// </summary>
    public partial class AppOrdersWindow : Window
    {
        private AppDb Db { get; set; }
        public AppOrdersWindow()
        {
            InitializeComponent();

            Db = AppDb.GetInstance();

            Refresh();
        }

        private void Refresh()
        {
            List<AppOrderModel> orders = Db.AppOrder
                .Include(i => i.Client)
                .Include(i => i.OrderProducts)
                .OrderBy(i => i.Client.FullName)
                .ToList();

            ObservableCollection<AppOrderUserControl> ordersDataSource = new ObservableCollection<AppOrderUserControl>();

            foreach (AppOrderModel order in orders)
            {
                foreach (OrderProductModel product in order.OrderProducts)
                {
                    ordersDataSource.Add(new AppOrderUserControl(
                        order.Client,
                        product
                    ));
                }
            }

            ItemsLB.ItemsSource = ordersDataSource;
        }
    }
}
