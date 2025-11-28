using Microsoft.EntityFrameworkCore;
using SmaginMA_2025_11_27.AppWindow;
using SmaginMA_2025_11_27.Db;
using SmaginMA_2025_11_27.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmaginMA_2025_11_27
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private readonly AppDb Db;
        public AuthWindow()
        {
            InitializeComponent();

            Db = AppDb.GetInstance();
        }

        private void LoginB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTB.Text) || string.IsNullOrWhiteSpace(PasswordPB.Password)) {
                MessageBox.Show("Не все поля заполнены");
                return;
            }

            AppUserModel? user = Db.AppUser
                .Include(i => i.AppRole)
                .FirstOrDefault(i => i.Login == LoginTB.Text && i.Password == PasswordPB.Password);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            var window = new FishingProductsWindow(user);
            window.ShowDialog();
        }

        private void LoginGuestB_Click(object sender, RoutedEventArgs e)
        {
            var window = new FishingProductsWindow();
            window.ShowDialog();
        }
    }
}