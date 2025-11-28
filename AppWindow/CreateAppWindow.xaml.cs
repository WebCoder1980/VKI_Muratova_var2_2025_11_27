using SmaginMA_2025_11_27.Db;
using SmaginMA_2025_11_27.Model;
using SmaginMA_2025_11_27.View;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для CreateAppWindow.xaml
    /// </summary>
    public partial class CreateAppWindow : Window
    {
        private AppDb Db { get; set; }
        private AppProductUserControl? Item { get; set; }

        public CreateAppWindow()
        {
            InitializeComponent();

            Db = AppDb.GetInstance();

            ManufacturerCB.ItemsSource = Db.Manufacturer.Select(i => i.Name).ToList();
            SupplierCB.ItemsSource = Db.Supplier.Select(i => i.Name).ToList();
            CategoryCB.ItemsSource = Db.Category.Select(i => i.Name).ToList();
        }

        public CreateAppWindow(AppProductUserControl item) : this()
        {
            Item = item;

            ArticleTB.Text = Item.Article;
            NameTB.Text = Item.ProductName;
            UnitTB.Text = Item.Unit;
            CostTB.Text = Item.Cost.ToString();
            MaxDiscountTB.Text = Item.MaxDiscount.ToString();
            ManufacturerCB.SelectedItem = Item.Manufacturer;
            SupplierCB.SelectedItem = Item.Supplier;
            CategoryCB.SelectedItem = Item.Category;
            CurrentDiscountTB.Text = Item.CurrentDiscount.ToString();
            StockQuantityTB.Text = Item.StockQuantity.ToString();
            DescriptionTB.Text = Item.Description;
            ImagePathTB.Text = Item.Image;

            Title = "Редактирование существующего товара";
        }

        private void SaveB_Click(object sender, RoutedEventArgs e)
        {
            if (
                string.IsNullOrWhiteSpace(ArticleTB.Text)
                || string.IsNullOrWhiteSpace(NameTB.Text)
                || string.IsNullOrWhiteSpace(UnitTB.Text)
                || string.IsNullOrWhiteSpace(CostTB.Text)
                || string.IsNullOrWhiteSpace(MaxDiscountTB.Text)
                || ManufacturerCB.SelectedIndex == -1
                || SupplierCB.SelectedIndex == -1
                || CategoryCB.SelectedIndex == -1
                || string.IsNullOrWhiteSpace(CurrentDiscountTB.Text)
                || string.IsNullOrWhiteSpace(StockQuantityTB.Text)
                || string.IsNullOrWhiteSpace(DescriptionTB.Text)
            ) {
                MessageBox.Show("Не все поля заполнены");
                return;
            }

            decimal cost;
                
            if (!decimal.TryParse(CostTB.Text, out cost)) {
                MessageBox.Show("Стоимость должна быть в виде числа с запятой");
                return;
            }

            if (cost < 0)
            {
                MessageBox.Show("Цена не может быть отрицательной!");
                return;
            }

            int maxDiscount;

            if (!int.TryParse(MaxDiscountTB.Text, out maxDiscount))
            {
                MessageBox.Show("Максимальная скидка должна быть в целого числа");
                return;
            }

            if (maxDiscount < 0 || maxDiscount > 100)
            {
                MessageBox.Show("Максимальная скидка не может быть вне диапазона [0, 100]!");
                return;
            }

            int currentDiscount;

            if (!int.TryParse(CurrentDiscountTB.Text, out currentDiscount))
            {
                MessageBox.Show("Текущая скидка должна быть в виде целого числа!");
                return;
            }

            if (currentDiscount < 0 || currentDiscount > 100)
            {
                MessageBox.Show("Текущая скидка не может быть вне диапазона [0, 100]!");
                return;
            }

            if (currentDiscount > maxDiscount)
            {
                MessageBox.Show("Текущая скидка не может быть больше максимальной скидки!");
                return;
            }

            int stockQuantity;

            if (!int.TryParse(StockQuantityTB.Text, out stockQuantity))
            {
                MessageBox.Show("Количество должно быть в виде целого числа!");
                return;
            }

            if (stockQuantity < 0)
            {
                MessageBox.Show("Количество не может быть отрицательно!");
                return;
            }

            if (!string.IsNullOrWhiteSpace(ImagePathTB.Text))
            {
                string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", ImagePathTB.Text);

                if (!File.Exists(fullPath))
                {
                    MessageBox.Show("Файла со следующим путём не существует!");
                    return;
                }
            }

            AppProductModel model;

            if (Item == null)
            {
                model = new AppProductModel();
                model.Article = ArticleTB.Text;
                Db.AppProduct.Add(model);
            }
            else
            {
                model = Db.AppProduct.FirstOrDefault(i => i.Article == Item.Article);
            }

            model.Article = ArticleTB.Text;
            model.Name = NameTB.Text;
            model.Unit = UnitTB.Text;
            model.Cost = cost;
            model.MaxDiscount = maxDiscount;
            model.Manufacturer = Db.Manufacturer.First(i => i.Name == ManufacturerCB.Text);
            model.Supplier = Db.Supplier.First(i => i.Name == SupplierCB.Text);
            model.Category = Db.Category.First(i => i.Name == CategoryCB.Text);
            model.CurrentDiscount = currentDiscount;
            model.StockQuantity = stockQuantity;
            model.Description = DescriptionTB.Text;
            model.Image = ImagePathTB.Text;

            Db.SaveChanges();

            DialogResult = true;
        }
    }
}
