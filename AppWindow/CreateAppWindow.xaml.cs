using SmaginMA_2025_11_27.Db;
using SmaginMA_2025_11_27.Model;
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
        public CreateAppWindow()
        {
            InitializeComponent();

            Db = AppDb.GetInstance();

            ManufacturerCB.ItemsSource = Db.Manufacturer.Select(i => i.Name).ToList();
            SupplierCB.ItemsSource = Db.Supplier.Select(i => i.Name).ToList();
            CategoryCB.ItemsSource = Db.Category.Select(i => i.Name).ToList();
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
                || string.IsNullOrWhiteSpace(ImagePathTB.Text)
            ) {
                MessageBox.Show("Не все поля заполнены");
                return;
            }

            decimal cost;
                
            if (!decimal.TryParse(CostTB.Text, out cost)) {
                MessageBox.Show("Стоимость должна быть в виде числа с запятой");
                return;
            }

            int maxDiscount;

            if (!int.TryParse(MaxDiscountTB.Text, out maxDiscount))
            {
                MessageBox.Show("Максимальная скидка должна быть в целого числа");
                return;
            }

            int currentDiscount;

            if (!int.TryParse(CurrentDiscountTB.Text, out currentDiscount))
            {
                MessageBox.Show("Текущая скидка должна быть в виде целого числа!");
                return;
            }

            int stockQuantity;

            if (!int.TryParse(StockQuantityTB.Text, out stockQuantity))
            {
                MessageBox.Show("Количество должно быть в виде целого числа!");
                return;
            }


            string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", ImagePathTB.Text);

            if (!File.Exists(fullPath))
            {
                MessageBox.Show("Файла со следующим путём не существует!");
                return;
            }

            var model = new AppProductModel()
            {
                Article = ArticleTB.Text,
                Name = NameTB.Text,
                Unit = UnitTB.Text,
                Cost = cost,
                MaxDiscount = maxDiscount,
                Manufacturer = Db.Manufacturer.First(i => i.Name == ManufacturerCB.Text),
                Supplier = Db.Supplier.First(i => i.Name == SupplierCB.Text),
                Category = Db.Category.First(i => i.Name == CategoryCB.Text),
                CurrentDiscount = currentDiscount,
                StockQuantity = stockQuantity,
                Description = DescriptionTB.Text,
                Image = ImagePathTB.Text
            };

            Db.AppProduct.Add(model);

            Db.SaveChanges();

            DialogResult = true;
        }
    }
}
