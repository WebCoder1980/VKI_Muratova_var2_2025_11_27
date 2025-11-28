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
    /// Логика взаимодействия для AppProductUserControl.xaml
    /// </summary>
    public partial class AppProductUserControl : UserControl
    {
        public string Article { get; set; }

        public string ProductName { get; set; }

        public string Unit { get; set; }

        public decimal Cost { get; set; }

        public int MaxDiscount { get; set; }

        public string Manufacturer { get; set; }

        public string Supplier { get; set; }

        public string Category { get; set; }

        public int CurrentDiscount { get; set; }

        public int StockQuantity { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public AppProductUserControl(AppProductModel model)
        {
            InitializeComponent();

            Article = model.Article;
            ProductName = model.Name;
            Unit = model.Unit;
            Cost = model.Cost;
            MaxDiscount = model.MaxDiscount;
            Manufacturer = model.Manufacturer;
            Supplier = model.Supplier;
            Category = model.Category;
            CurrentDiscount = model.CurrentDiscount;
            StockQuantity = model.StockQuantity;
            Description = model.Description;
            Image = model.Image;

            Refresh();
        }

        public void Refresh()
        {
            
        }
    }
}
