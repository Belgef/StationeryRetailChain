using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StationeryRetailChain.Shared.Models;

namespace StationeryRetailChain.Server.Data
{
    public partial class StationeryRetailChainContext : DbContext
    {
        public StationeryRetailChainContext()
        {
        }

        public StationeryRetailChainContext(DbContextOptions<StationeryRetailChainContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(Console.WriteLine);

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<DeliveryInvoice> DeliveryInvoices { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<Offer> Offers { get; set; } = null!;
        public virtual DbSet<OfferStationery> OfferStationeries { get; set; } = null!;
        public virtual DbSet<Receipt> Receipts { get; set; } = null!;
        public virtual DbSet<ShipmentInvoice> ShipmentInvoices { get; set; } = null!;
        public virtual DbSet<ShipmentSupply> ShipmentSupplies { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<StationeryProduct> StationeryProducts { get; set; } = null!;
        public virtual DbSet<StationerySale> StationerySales { get; set; } = null!;
        public virtual DbSet<StationeryShop> StationeryShops { get; set; } = null!;
        public virtual DbSet<StationerySupply> StationerySupplies { get; set; } = null!;
        public virtual DbSet<StockProduct> StockProducts { get; set; } = null!;
        public virtual DbSet<Subcategory> Subcategories { get; set; } = null!;
        public virtual DbSet<SupplierCompany> SupplierCompanies { get; set; } = null!;
        public virtual DbSet<Shared.Models.Type> Types { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryDescription)
                    .HasMaxLength(255)
                    .HasColumnName("category_description");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("category_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("cities");

                entity.Property(e => e.CityId).HasColumnName("city_id");
                entity.HasKey(e => e.CityId);

                entity.Property(e => e.CityName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.StateId).HasColumnName("state_id");
                entity.HasOne(e => e.State).WithMany()
                .HasForeignKey(e => e.StateId);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("colors");

                entity.Property(e => e.ColorId).HasColumnName("color_id");
                entity.HasKey(e => e.ColorId);

                entity.Property(e => e.ColorHex)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("color_hex")
                    .IsFixedLength();

                entity.Property(e => e.ColorName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("color_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");

                entity.Property(e => e.CountryId).HasColumnName("country_id");
                entity.HasKey(e => e.CountryId);

                entity.Property(e => e.CountryName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("country_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CityId).HasColumnName("city_id");
                entity.HasOne(e => e.City).WithMany()
                .HasForeignKey(e => e.CityId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("customer_name");

                entity.Property(e => e.CustomerPhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("customer_phone_number")
                    .IsFixedLength();

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(255)
                    .HasColumnName("street_address");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<DeliveryInvoice>(entity =>
            {
                entity.ToTable("delivery_invoices");

                entity.Property(e => e.DeliveryInvoiceId).HasColumnName("delivery_invoice_id");
                entity.HasKey(e => e.DeliveryInvoiceId);

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.DeliveryInvoiceNumber).HasColumnName("delivery_invoice_number");

                entity.Property(e => e.ShipmentInvoiceId).HasColumnName("shipment_invoice_id");

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("employee_name");

                entity.Property(e => e.EmployeePhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("employee_phone_number")
                    .IsFixedLength();

                entity.Property(e => e.JobId).HasColumnName("job_id");
                entity.HasOne(e => e.Job).WithMany()
                .HasForeignKey(e => e.JobId);

                entity.Property(e => e.ShopId).HasColumnName("shop_id");
                entity.HasOne(e => e.Shop).WithMany()
                .HasForeignKey(e => e.ShopId);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("jobs");

                entity.Property(e => e.JobId).HasColumnName("job_id");
                entity.HasKey(e => e.JobId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.JobName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("job_name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("manufacturers");

                entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
                entity.HasKey(e => e.ManufacturerId);

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.ManufacturerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("manufacturer_name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone_number")
                    .IsFixedLength();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("offers");

                entity.Property(e => e.OfferId).HasColumnName("offer_id");
                entity.HasKey(e => e.OfferId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.EndDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.OfferDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("offer_description");

                entity.Property(e => e.OfferName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("offer_name");

                entity.Property(e => e.StartDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("start_date");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<OfferStationery>(entity =>
            {
                entity.ToTable("offer_stationery");

                entity.Property(e => e.OfferStationeryId).HasColumnName("offer_stationery_id");
                entity.HasKey(e => e.OfferStationeryId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("discount");

                entity.Property(e => e.OfferId).HasColumnName("offer_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.ToTable("receipts");

                entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
                entity.HasKey(e => e.ReceiptId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.HasOne(e => e.Customer).WithMany()
                .HasForeignKey(e => e.CustomerId);

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("purchase_date");

                entity.Property(e => e.PurchaseSum)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("purchase_sum");

                entity.Property(e => e.ReceiptNumber).HasColumnName("receipt_number");

                entity.Property(e => e.SellerId).HasColumnName("seller_id");
                entity.HasOne(e => e.Seller).WithMany()
                .HasForeignKey(e => e.SellerId);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");

                entity.HasMany(e => e.Items).WithOne().HasForeignKey(e => e.ReceiptId);
            });

            modelBuilder.Entity<ShipmentInvoice>(entity =>
            {
                entity.ToTable("shipment_invoices");

                entity.Property(e => e.ShipmentInvoiceId).HasColumnName("shipment_invoice_id");
                entity.HasKey(e => e.ShipmentInvoiceId);

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.ShipmentInvoiceNumber).HasColumnName("shipment_invoice_number");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.TotalSum)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_sum");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<ShipmentSupply>(entity =>
            {
                entity.ToTable("shipment_supply");

                entity.Property(e => e.ShipmentInvoiceId).HasColumnName("shipment_invoice_id");
                entity.HasKey(e => e.ShipmentInvoiceId);

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("cost");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.ShipmentSupplyId).HasColumnName("shipment_supply_id");

                entity.Property(e => e.StationeryProductId).HasColumnName("stationery_product_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("states");

                entity.Property(e => e.StateId).HasColumnName("state_id");
                entity.HasKey(e => e.StateId);

                entity.Property(e => e.CountryId).HasColumnName("country_id");
                entity.HasOne(e => e.Country).WithMany()
                .HasForeignKey(e => e.CountryId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.StateName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("state_name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<StationeryProduct>(entity =>
            {
                entity.ToTable("stationery_products");

                entity.Property(e => e.StationeryProductId).HasColumnName("stationery_product_id");
                entity.HasKey(e => e.StationeryProductId);

                entity.Property(e => e.BarCode)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("bar_code")
                    .IsFixedLength();

                entity.Property(e => e.ColorId).HasColumnName("color_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.ItemsPerPackage).HasColumnName("items_per_package");

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");

                entity.Property(e => e.MinimumAge).HasColumnName("minimum_age");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PackagesPerBox).HasColumnName("packages_per_box");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Width).HasColumnName("width");
            });

            modelBuilder.Entity<StationerySale>(entity =>
            {
                entity.ToTable("stationery_sales");

                entity.Property(e => e.SaleId).HasColumnName("sale_id");
                entity.HasKey(e => e.SaleId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");

                entity.Property(e => e.SellPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("sell_price");

                entity.Property(e => e.SellQuantity).HasColumnName("sell_quantity");

                entity.Property(e => e.StockProductId).HasColumnName("stock_product_id");
                entity.HasOne(e => e.StockProduct).WithMany()
                .HasForeignKey(e => e.StockProductId);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<StationeryShop>(entity =>
            {
                entity.ToTable("stationery_shops");

                entity.Property(e => e.ShopId).HasColumnName("shop_id");
                entity.HasKey(e => e.ShopId);

                entity.Property(e => e.CityId).HasColumnName("city_id");
                entity.HasOne(e => e.City).WithMany()
                .HasForeignKey(e => e.CityId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone_number")
                    .IsFixedLength();

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(255)
                    .HasColumnName("street_address");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<StationerySupply>(entity =>
            {
                entity.ToTable("stationery_supply");

                entity.Property(e => e.StationerySupplyId).HasColumnName("stationery_supply_id");
                entity.HasKey(e => e.StationerySupplyId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeliveryInvoiceId).HasColumnName("delivery_invoice_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.ShipmentSupplyId).HasColumnName("shipment_supply_id");

                entity.Property(e => e.StockProductId).HasColumnName("stock_product_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<StockProduct>(entity =>
            {
                entity.ToTable("stock_products");

                entity.Property(e => e.StockProductId).HasColumnName("stock_product_id");
                entity.HasKey(e => e.StockProductId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.Property(e => e.StationeryProductId).HasColumnName("stationery_product_id");
                entity.HasOne(e => e.StationeryProduct).WithMany()
                .HasForeignKey(e => e.StationeryProductId);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Subcategory>(entity =>
            {
                entity.ToTable("subcategories");

                entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");
                entity.HasKey(e => e.SubcategoryId);

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.SubcategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subcategory_name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<SupplierCompany>(entity =>
            {
                entity.ToTable("supplier_companies");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
                entity.HasKey(e => e.SupplierId);

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone_number")
                    .IsFixedLength();

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(255)
                    .HasColumnName("street_address");

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("supplier_name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Shared.Models.Type>(entity =>
            {
                entity.ToTable("types");

                entity.Property(e => e.TypeId).HasColumnName("type_id");
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type_name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("updated_at");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
