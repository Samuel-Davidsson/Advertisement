using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Data.DataContext
{
    public partial class annonsappenContext : DbContext
    {
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategory { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyContract> CompanyContract { get; set; }
        public virtual DbSet<CompanyIndustry> CompanyIndustry { get; set; }
        public virtual DbSet<Industry> Industry { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<StoreArticle> StoreArticle { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserCompany> UserCompany { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-M702LBS;Database=annonsappen;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer(@"Server = SAMUEL; Database = annonsappen; Trusted_Connection = True;");
            }
        }

        public annonsappenContext(DbContextOptions<annonsappenContext> options)
        : base(options)
        {
        }

        public annonsappenContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.ArticleId);

                entity.ToTable("T_Article");

                entity.HasIndex(e => new { e.ArticleId, e.Name, e.Slug, e.Description, e.ImagePath, e.ImageFileName, e.ImageFileFormat, e.ImageWidths, e.Price, e.PriceText, e.PriceUnit, e.PublishBegin, e.PublishEnd, e.Modified, e.Created, e.Deleted, e.IsDeleted })
                    .HasName("IX_T_Article_DeletedArticles");

                entity.Property(e => e.CompanyId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("nvarchar(max)")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImageFileFormat)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImageFileName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                entity.Property(e => e.ImageWidths)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Price).HasDefaultValueSql("((-1))");

                entity.Property(e => e.PriceText)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PriceUnit)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PublishBegin)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.PublishEnd)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('9999-12-31')");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Article_T_Company");
            });

            modelBuilder.Entity<ArticleCategory>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.CategoryId });

                entity.ToTable("T_ArticleCategory");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleCategory)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_T_ArticleCategory_T_Article");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ArticleCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_T_ArticleCategory_T_Category");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("T_Category");

                entity.HasIndex(e => new { e.ParentId, e.Name })
                    .HasName("IX_T_Category_Name")
                    .IsUnique();

                entity.HasIndex(e => new { e.ParentId, e.Slug })
                    .HasName("IX_T_Category_Slug");

                entity.Property(e => e.Childs).HasDefaultValueSql("((0))");

                entity.Property(e => e.ChildsClosestPublished).HasDefaultValueSql("((0))");

                entity.Property(e => e.ChildsPublished).HasDefaultValueSql("((0))");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Depth).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.HierarcyIndex).HasDefaultValueSql("((0))");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ParentId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PathSlug)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Category_T_Category");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.ToTable("T_Company");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_T_Company_Name")
                    .IsUnique();

                entity.HasIndex(e => e.RegistrationNumber)
                    .HasName("IX_T_Company_RegistrationNumber")
                    .IsUnique();

                entity.HasIndex(e => e.Slug);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AddressExtra)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CountryId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InvoiceAddress)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InvoiceAddressExtra)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InvoiceCity)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InvoiceCountry)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InvoiceZipCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RegistrationNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StripeId)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CompanyContract>(entity =>
            {
                entity.HasKey(e => e.CompanyContractId);

                entity.ToTable("T_CompanyContract");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyContract)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_T_ContractCompany_T_Company");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CompanyContract)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_T_ContractCompany_T_User");
            });

            modelBuilder.Entity<CompanyIndustry>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.IndustryId });

                entity.ToTable("T_CompanyIndustry");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyIndustry)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_T_CompanyIndustry_T_Company");

                entity.HasOne(d => d.Industry)
                    .WithMany(p => p.TCompanyIndustry)
                    .HasForeignKey(d => d.IndustryId)
                    .HasConstraintName("FK_T_CompanyIndustry_T_Industry");
            });

            modelBuilder.Entity<Industry>(entity =>
            {
                entity.HasKey(e => e.IndustryId);

                entity.ToTable("T_Industry");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_T_Industry")
                    .IsUnique();

                entity.HasIndex(e => e.Slug)
                    .HasName("IX_T_Industry_Slug")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(50);
            });


            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.ToTable("T_Store");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AddressExtra)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AndroidId).HasMaxLength(50);

                entity.Property(e => e.AppIcon).HasMaxLength(250);

                entity.Property(e => e.AppleId).HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FridayBegin)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.FridayEnd)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.Homepage)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImageFileFormat)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImageFileName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImageWidths)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Latitude)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Longitude)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.MondayBegin)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.MondayEnd)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.OpeningHoursDescription)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PlayLink).HasMaxLength(250);

                entity.Property(e => e.PublishBegin)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.PublishEnd)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('9999-12-31')");

                entity.Property(e => e.SaturdayBegin)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.SaturdayEnd)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StoreImageUrl).HasMaxLength(255);

                entity.Property(e => e.StoreLink).HasMaxLength(250);

                entity.Property(e => e.SundayBegin)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.SundayEnd)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.ThursdayBegin)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.ThursdayEnd)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.TuesdayBegin)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.TuesdayEnd)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.WednesdayBegin)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.WednesdayEnd)
                    .HasColumnType("time(0)")
                    .HasDefaultValueSql("('00:00:00')");

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Store)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_T_Store_T_Company");


            });

            modelBuilder.Entity<StoreArticle>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ArticleId });

                entity.ToTable("T_StoreArticle");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.StoreArticle)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_T_StoreArticle_T_Article");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreArticle)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_T_StoreArticle_T_Store");
            });


            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("T_User");

                entity.HasIndex(e => e.Email)
                    .HasName("IX_T_User_Email")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("IX_T_User")
                    .IsUnique();

                entity.Property(e => e.CountryId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LanguageId).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserCompany>(entity =>
            {
                entity.HasKey(e => e.UserCompanyId);

                entity.ToTable("T_UserCompany");

                entity.HasIndex(e => new { e.UserId, e.CompanyId })
                    .HasName("IX_T_UserCompany_UserCompany")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_T_UserCompany_T_Company");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCompany)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_T_UserCompany_T_User");
            });

            modelBuilder.Entity<UserStore>(entity =>
            {
                entity.HasKey(e => e.UserStoreId);

                entity.ToTable("T_UserStore");

                entity.HasIndex(e => new { e.UserId, e.StoreId })
                    .HasName("IX_T_UserStore_UserStore")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.UserStore)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_T_UserStore_T_Store");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserStore)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_T_UserStore_T_User");
            });
        }
    }
}
