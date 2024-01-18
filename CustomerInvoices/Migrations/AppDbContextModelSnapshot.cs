using CustomerInvoices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomerInvoices.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CustomerInvoices.Data.Customer", b =>
            {
                b.Property<int>("CustomerId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("Address")
                    .HasMaxLength(255)
                    .HasColumnType("nvarchar(255)");

                b.HasKey("CustomerId");

                b.ToTable("Customers");
            });

            modelBuilder.Entity("CustomerInvoices.Data.Service", b =>
            {
                b.Property<int>("ServiceId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"), 1L, 1);

                b.Property<string>("ServiceName")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("Description")
                    .HasMaxLength(255)
                    .HasColumnType("nvarchar(255)");

                b.Property<decimal>("Price")
                    .HasColumnType("decimal(18,2)");

                b.Property<int>("Duration")
                    .HasColumnType("int");

                b.HasKey("ServiceId");

                b.ToTable("Services");
            });

            modelBuilder.Entity("CustomerInvoices.Data.Invoice", b =>
            {
                b.Property<int>("InvoiceId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceId"), 1L, 1);

                b.Property<int>("CustomerId")
                    .HasColumnType("int");

                b.Property<int>("ServiceId")
                    .HasColumnType("int");

                b.Property<DateTime>("InvoiceDate")
                    .HasColumnType("datetime2");

                b.Property<decimal>("TotalAmount")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("InvoiceId");

                b.HasIndex("CustomerId");

                b.HasIndex("ServiceId");

                b.ToTable("Invoices");
            });

            modelBuilder.Entity("CustomerInvoices.Data.Invoice", b =>
            {
                b.HasOne("CustomerInvoices.Data.Customer", "Customer")
                    .WithMany()
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("CustomerInvoices.Data.Service", "Service")
                    .WithMany()
                    .HasForeignKey("ServiceId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Customer");

                b.Navigation("Service");
            });
#pragma warning restore 612, 618
        }
    }
}