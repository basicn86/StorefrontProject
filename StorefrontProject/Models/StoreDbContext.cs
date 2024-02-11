using Avalonia.Platform;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.Models
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        private string dbPath;

        public StoreDbContext()
        {
            dbPath = System.IO.Path.Join(Environment.CurrentDirectory, "store.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        public void ResetDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            //Add Apple
            Products.Add(new Product
            {
                Name = "Apple",
                Price = 4.99m,
                ProductImage = LoadProductImage("apple.jpg")
            });

            
            //Add Banana
            Products.Add(new Product
            {
                Name = "Banana",
                Price = 1.99m,
                ProductImage = LoadProductImage("banana.jpg")
            });

            //Add carrot
            Products.Add(new Product
            {
                Name = "Carrot",
                Price = 2.99m,
                ProductImage = LoadProductImage("carrot.jpg")
            });

            //add durian
            Products.Add(new Product
            {
                Name = "Durian",
                Price = 7.99m,
                ProductImage = LoadProductImage("durian.webp")
            });

            //add eggplant
            Products.Add(new Product
            {
                Name = "Eggplant",
                Price = 14.99m,
                ProductImage = LoadProductImage("eggplant.webp")
            });

            //add figs
            Products.Add(new Product
            {
                Name = "Figs",
                Price = 0.99m,
                ProductImage = LoadProductImage("figs.jpg")
            });

            SaveChanges();
        }

        private byte[]? LoadProductImage(string name)
        {
            try
            {
                System.IO.Stream stream = AssetLoader.Open(new Uri("avares://StorefrontProject/Assets/" + name));
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
            catch
            {
                return null;
            }
        }
    }

    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }  
        public Byte[]? ProductImage { get; set; }
    }
}
