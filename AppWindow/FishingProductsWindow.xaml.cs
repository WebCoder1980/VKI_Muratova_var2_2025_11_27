using Microsoft.EntityFrameworkCore;
using SmaginMA_2025_11_27.Db;
using SmaginMA_2025_11_27.Model;
using SmaginMA_2025_11_27.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Printing;
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

            Refresh();

            HideByRole();
        }

        private void HideByRole()
        {
            List<string> roles = ["Гость", "Клиент", "Менеджер", "Администратор"];

            string currentRole = CurrentUser?.AppRole.Name ?? roles[0];

            for (int i = roles.IndexOf(currentRole); i < roles.Count; i++)
            {
                switch (roles[i])
                {
                    case "Гость":
                        SortL.Visibility
                        = FilterL.Visibility
                        = SearchTB.Visibility
                        = FilterProducerCB.Visibility
                        = SortPropertyCB.Visibility
                        = SortOrderCB.Visibility
                        = Visibility.Hidden;
                        break;
                    case "Клиент":
                        OrdersB.Visibility = Visibility.Hidden;
                        break;
                    case "Менеджер":
                        CreateB.Visibility = Visibility.Visible;
                        foreach (AppProductUserControl j in ItemsLB.Items)
                        {
                            j.HideForRole(roles[i]);
                        }
                        break;
                    case "Администратор":

                        break;
                }
            }
        }

        private void Refresh()
        {
            if (Db == null)
            {
                return;
            }

            IQueryable<AppProductModel> query = Db.AppProduct
                .Include(i => i.Manufacturer)
                .Include(i => i.Supplier)
                .Include(i => i.Category);

            if (!FilterProducerCB.Items.IsEmpty)
            {
                string filter = (string)FilterProducerCB.SelectedItem;
                if (filter != "Все производители")
                {
                    query = query.Where(i => i.Manufacturer.Name == filter);
                }
            }

            string search = SearchTB.Text.ToLower();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(i =>
                    i.Article.ToLower().Contains(search)
                    || i.Name.ToLower().Contains(search)
                    || i.Unit.ToLower().Contains(search)
                    || i.Cost.ToString().ToLower().Contains(search)
                    || i.MaxDiscount.ToString().ToLower().Contains(search)
                    || i.Manufacturer.Name.ToLower().Contains(search)
                    || i.Supplier.Name.ToLower().Contains(search)
                    || i.Category.Name.ToLower().Contains(search)
                    || i.CurrentDiscount.ToString().ToLower().Contains(search)
                    || i.StockQuantity.ToString().ToLower().Contains(search)
                    || i.Description.ToLower().Contains(search)
                );
            }

            switch (((ComboBoxItem)SortPropertyCB.SelectedItem).Content)
            {
                case "Артикул":
                    query = query.OrderBy(i => i.Article);
                    break;

                case "Название":
                    query = query.OrderBy(i => i.Name);
                    break;

                case "Единицы измерения":
                    query = query.OrderBy(i => i.Unit);
                    break;

                case "Стоимость (без скидки)":
                    query = query.OrderBy(i => i.Cost);
                    break;

                case "Максимальная скидка":
                    query = query.OrderBy(i => i.MaxDiscount);
                    break;

                case "Производитель":
                    query = query.OrderBy(i => i.Manufacturer.Name);
                    break;

                case "Поставщик":
                    query = query.OrderBy(i => i.Supplier.Name);
                    break;

                case "Категория":
                    query = query.OrderBy(i => i.Category.Name);
                    break;

                case "Текущая скидка":
                    query = query.OrderBy(i => i.CurrentDiscount);
                    break;

                case "Кол-во на складе":
                    query = query.OrderBy(i => i.StockQuantity);
                    break;

                case "Описание":
                    query = query.OrderBy(i => i.Description);
                    break;

                default:
                    MessageBox.Show(SortPropertyCB.SelectedItem.ToString());
                    break;
            }

            if ((string)((ComboBoxItem)SortOrderCB.SelectedItem).Content == "По убыванию")
            {
                query = query.Reverse();
            }

            var currentWindow = this;

            ItemsLB.ItemsSource = query.Select(i => new AppProductUserControl(i, currentWindow))
                .ToList();

            ObservableCollection<string> filterProducers = ["Все производители"];
            foreach (string i in Db.Manufacturer.Select(i => i.Name)) {
                filterProducers.Add(i);
            }

            FilterProducerCB.ItemsSource = filterProducers;
        }

        private void Refresh(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void Refresh(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        public void UpdateItem(AppProductUserControl item)
        {
            var window = new CreateAppWindow(item);
            window.ShowDialog();

            if (window.DialogResult == true)
            {
                Refresh();
            }
        }

        public void DeleteItem(AppProductUserControl item)
        {
            Db.AppProduct.Remove(
                Db.AppProduct.First(i => i.Article == item.Article)
            );

            Db.SaveChanges();

            Refresh();
        }

        private void OrdersB_Click(object sender, RoutedEventArgs e)
        {
            var window = new AppOrdersWindow();
            window.ShowDialog();
        }

        private void CreateB_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateAppWindow();
            window.ShowDialog();

            if (window.DialogResult == true)
            {
                Refresh();
            }
        }
    }
}
