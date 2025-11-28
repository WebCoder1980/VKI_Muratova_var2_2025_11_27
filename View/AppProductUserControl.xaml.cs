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
            Manufacturer = model.Manufacturer.Name;
            Supplier = model.Supplier.Name;
            Category = model.Category.Name;
            CurrentDiscount = model.CurrentDiscount;
            StockQuantity = model.StockQuantity;
            Description = model.Description;
            
            Image = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", model.Image);

            Refresh();
        }

        public void Refresh()
        {
            if (StockQuantity == 0)
            {
                GridUC.Background = GetBrush("#fc7c84");
            }
            else if (CurrentDiscount > 0)
            {
                DiscountTB.Foreground = new SolidColorBrush(Colors.DarkGreen);
                GridUC.Background = GetBrush("#73f6ff");

                OldCostR.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                OldCostR.TextDecorations = TextDecorations.Strikethrough;

                NewCostR.Text = ((100 - CurrentDiscount) * Cost / 100).ToString();
            }
        }

        private SolidColorBrush GetBrush(string rgb) => new SolidColorBrush((Color)ColorConverter.ConvertFromString(rgb));
    }
}
