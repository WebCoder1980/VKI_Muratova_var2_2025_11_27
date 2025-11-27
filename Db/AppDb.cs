using Microsoft.EntityFrameworkCore;
using SmaginMA_2025_11_27.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaginMA_2025_11_27.Db
{
    internal class AppDb : DbContext
    {
        private AppDb()
        {
            Database.EnsureCreated();
        }

        private static AppDb? instance;

        public static AppDb GetInstance()
        {
            if (instance == null)
            {
                instance = new AppDb();
            }

            return instance;
        }

        public virtual DbSet<PickupAddressModel> AddressModel { get; set; }
        public virtual DbSet<UserModel> EmployeeModel { get; set; }
        public virtual DbSet<OrderProductModel> OrderItemModel { get; set; }
        public virtual DbSet<OrderModel> OrderModel { get; set; }
        public virtual DbSet<ProductModel> ProductModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SmaginMA_2207d2_Muratova_var2;User Id=sa;Password=Qwe12345!;TrustServerCertificate=True;Connect Timeout=90");
        }
    }
}
