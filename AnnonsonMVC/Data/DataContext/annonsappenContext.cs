using System;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.DataContext
{
    public partial class AnnonsappenContext : DbContext
    {
        public virtual DbSet<ApplicationLog> ApplicationLog { get; set; }
        public virtual DbSet<ApplicationSetting> ApplicationSetting { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleCategory> TArticleCategory { get; set; }
        public virtual DbSet<ArticleLog> ArticleLog { get; set; }
        public virtual DbSet<Bonus> Bonus { get; set; }
        public virtual DbSet<BonusStore> BonusStore { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyContract> CompanyContract { get; set; }
        public virtual DbSet<CompanyIndustry> CompanyIndustry { get; set; }
        public virtual DbSet<Consumer> Consumer { get; set; }
        public virtual DbSet<ConsumerBonus> ConsumerBonus { get; set; }
        public virtual DbSet<ConsumerBonusAchievementStats> ConsumerBonusAchievementStats { get; set; }
        public virtual DbSet<ConsumerBonusStats> ConsumerBonusStats { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplate { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Industry> Industry { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Mall> Mall { get; set; }
        public virtual DbSet<MallStore> MallStore { get; set; }
        public virtual DbSet<Municipality> Municipality { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<NotificationHub> NotificationHub { get; set; }
        public virtual DbSet<NotificationOutcome> NotificationOutcome { get; set; }
        public virtual DbSet<NotificationPlatform> NotificationPlatform { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Seller> Seller { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<StoreArticle> StoreArticle { get; set; }
        public virtual DbSet<StoreSubscription> StoreSubscription { get; set; }
        public virtual DbSet<Subscription> Subscription { get; set; }
        public virtual DbSet<SubscriptionPaymentMethod> SubscriptionPaymentMethod { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserCompany> UserCompany { get; set; }
        public virtual DbSet<UserPasswordReset> UserPasswordReset { get; set; }
        public virtual DbSet<UserStore> UserStore { get; set; }
        public virtual DbSet<UserStoreInvite> UserStoreInvite { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"Server=SAMUEL;Database=annonsappen;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-M702LBS;Database=annonsappen;Trusted_Connection=True;");
            }
        }
        public AnnonsappenContext(DbContextOptions<AnnonsappenContext> options)
        : base(options)
        {
        }
        public AnnonsappenContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationLog>(entity =>
            {
                entity.HasKey(e => e.ApplicationLogId);

                entity.ToTable("T_ApplicationLog");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Application)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ApplicationSetting>(entity =>
            {
                entity.HasKey(e => e.ApplicationSettingId);

                entity.ToTable("T_ApplicationSetting");

                entity.Property(e => e.ApplicationSettingId).ValueGeneratedNever();

                entity.Property(e => e.ImageUrlPath)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.VersionMajor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.VersionMinor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

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
                entity.HasKey(e => new { e.ArticleId, e.CategoryId, e.ArticleCategoryId });

                entity.ToTable("T_ArticleCategory");

                entity.Property(e => e.ArticleCategoryId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleCategory)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_T_ArticleCategory_T_Article");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ArticleCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_T_ArticleCategory_T_Category");
            });

            modelBuilder.Entity<ArticleLog>(entity =>
            {
                entity.HasKey(e => e.ArticleLogId);

                entity.ToTable("T_ArticleLog");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OperatingSystem).HasMaxLength(50);

                entity.Property(e => e.StoreId).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Bonus>(entity =>
            {
                entity.ToTable("T_Bonus");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.Info).HasMaxLength(250);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Product)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<BonusStore>(entity =>
            {
                entity.HasKey(e => new { e.BonusId, e.StoreId });

                entity.ToTable("T_BonusStore");

                entity.HasOne(d => d.Bonus)
                    .WithMany(p => p.BonusStore)
                    .HasForeignKey(d => d.BonusId)
                    .HasConstraintName("FK_T_BonusStore_T_Bonus");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.BonusStore)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_T_BonusStore_T_Store");
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

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.CompanyContract)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK_T_ContractCompany_T_Contract");

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
                    .WithMany(p => p.CompanyIndustry)
                    .HasForeignKey(d => d.IndustryId)
                    .HasConstraintName("FK_T_CompanyIndustry_T_Industry");
            });

            modelBuilder.Entity<Consumer>(entity =>
            {
                entity.ToTable("T_Consumer");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Modified).HasColumnType("datetime");
            });

            modelBuilder.Entity<ConsumerBonus>(entity =>
            {
                entity.ToTable("T_ConsumerBonus");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PayedOut).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ConsumerBonusAchievementStats>(entity =>
            {
                entity.ToTable("T_ConsumerBonusAchievementStats");

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<ConsumerBonusStats>(entity =>
            {
                entity.ToTable("T_ConsumerBonusStats");

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => e.ContractId);

                entity.ToTable("T_Contract");

                entity.Property(e => e.BeginDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('9999-12-31')");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_T_Contract_T_Country");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.ToTable("T_Country");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_T_Country_Name")
                    .IsUnique();

                entity.HasIndex(e => e.Slug)
                    .HasName("IX_T_Country_Slug")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

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

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.DiscountId);

                entity.ToTable("T_Discount");

                entity.Property(e => e.Amount).HasDefaultValueSql("((0))");

                entity.Property(e => e.BeginDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('9999-12-31')");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SellerId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.Discount)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("FK_T_Discount_T_Subscription");
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.ToTable("T_EmailTemplate");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CountryId).HasColumnName("countryId");

                entity.Property(e => e.TemplateBody).IsRequired();

                entity.Property(e => e.TemplateName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TemplateSubject)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("T_Event");

                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Organizer).HasMaxLength(255);

                entity.Property(e => e.TicketUrl).HasMaxLength(255);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.WebUrl).HasMaxLength(255);
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

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.LanguageId);

                entity.ToTable("T_Language");

                entity.HasIndex(e => e.Code)
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Mall>(entity =>
            {
                entity.HasKey(e => e.MallId);

                entity.ToTable("T_Mall");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MallStore>(entity =>
            {
                entity.HasKey(e => new { e.MallId, e.StoreId });

                entity.ToTable("T_MallStore");

                entity.HasOne(d => d.Mall)
                    .WithMany(p => p.MallStore)
                    .HasForeignKey(d => d.MallId)
                    .HasConstraintName("FK_T_MallStore_T_Mall");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.MallStore)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_T_MallStore_T_Store");
            });

            modelBuilder.Entity<Municipality>(entity =>
            {
                entity.HasKey(e => e.MunicipalityId);

                entity.ToTable("T_Municipality");

                entity.HasIndex(e => new { e.RegionId, e.Slug })
                    .HasName("IX_T_Municipality_RegionIdSlug")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Municipality)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_T_Municipality_T_Region");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);

                entity.ToTable("T_Notification");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_T_Notification_T_Article");

                entity.HasOne(d => d.NotificationHub)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.NotificationHubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Notification_T_NotificationHub");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Notification_T_User");
            });

            modelBuilder.Entity<NotificationHub>(entity =>
            {
                entity.HasKey(e => e.NotificationHubId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("T_NotificationHub");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SendAccessSignature)
                    .IsRequired()
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.NotificationHub)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_NotificationHub_T_Store");
            });

            modelBuilder.Entity<NotificationOutcome>(entity =>
            {
                entity.HasKey(e => new { e.NotificationId, e.NotificationPlatformId });

                entity.ToTable("T_NotificationOutcome");

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasMaxLength(260);

                entity.Property(e => e.TrackingId).HasMaxLength(50);

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.NotificationOutcome)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_NotificationOutcome_T_Notification");

                entity.HasOne(d => d.NotificationPlatform)
                    .WithMany(p => p.NotificationOutcome)
                    .HasForeignKey(d => d.NotificationPlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_NotificationOutcome_T_NotificationPlatform");
            });

            modelBuilder.Entity<NotificationPlatform>(entity =>
            {
                entity.HasKey(e => e.NotificationPlatformId);

                entity.ToTable("T_NotificationPlatform");

                entity.Property(e => e.NotificationPlatformId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.PaymentMethodId);

                entity.ToTable("T_PaymentMethod");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PaymentOperatorId).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegionId);

                entity.ToTable("T_Region");

                entity.HasIndex(e => new { e.CountryId, e.Slug })
                    .HasName("IX_T_Region_CountryIdSlug")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Region)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_T_Region_T_Country");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasKey(e => e.SellerId);

                entity.ToTable("T_Seller");

                entity.HasIndex(e => e.Code)
                    .HasName("IX_T_Seller_1")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("IX_T_Seller")
                    .IsUnique();

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

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Company)
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

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RegistrationNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SellerConfirmationEmail).HasMaxLength(255);

                entity.Property(e => e.Subscriptions).HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
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

                entity.HasOne(d => d.Municipality)
                    .WithMany(p => p.Store)
                    .HasForeignKey(d => d.MunicipalityId)
                    .HasConstraintName("FK_T_Store_T_Municipality");
            });

            modelBuilder.Entity<StoreArticle>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ArticleId, e.StoreArticleId });

                entity.ToTable("T_StoreArticle");

                entity.Property(e => e.StoreArticleId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.StoreArticle)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_T_StoreArticle_T_Article");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreArticle)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_T_StoreArticle_T_Store");
            });

            modelBuilder.Entity<StoreSubscription>(entity =>
            {
                entity.HasKey(e => e.StoreSubscriptionId);

                entity.ToTable("T_StoreSubscription");

                entity.Property(e => e.ArticleLimit).HasDefaultValueSql("((0))");

                entity.Property(e => e.BeginDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.CompanyId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Discount).HasDefaultValueSql("((0))");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('9999-12-31')");

                entity.Property(e => e.Length).HasDefaultValueSql("((0))");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.PaymentMethodId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PaymentMethodName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasDefaultValueSql("((0))");

                entity.Property(e => e.StripeId)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Vat).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.StoreSubscription)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK_T_StoreSubscription_T_Seller");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreSubscription)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_T_StoreSubscription_T_Store");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.StoreSubscription)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("FK_T_Subscription_T_StoreSubscription");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId);

                entity.ToTable("T_Subscription");

                entity.Property(e => e.ArticleLimit).HasDefaultValueSql("((0))");

                entity.Property(e => e.BeginDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01')");

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

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('9999-12-31')");

                entity.Property(e => e.Length).HasDefaultValueSql("((0))");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Price).HasDefaultValueSql("((0))");

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((0))");

                entity.Property(e => e.Vat).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Subscription)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Subscription_T_Country");
            });

            modelBuilder.Entity<SubscriptionPaymentMethod>(entity =>
            {
                entity.HasKey(e => new { e.SubscriptionId, e.PaymentMethodId });

                entity.ToTable("T_SubscriptionPaymentMethod");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.SubscriptionPaymentMethod)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK_T_SubscriptionPaymentMethod_T_PaymentMethod");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.SubscriptionPaymentMethod)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("FK_T_SubscriptionPaymentMethod_T_Subscription");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.TestId);

                entity.ToTable("T_Test");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ImageLargeUrl)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImageMediumUrl)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImageOriginalUrl)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImageSmallUrl)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");
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

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_User_T_Country");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_User_T_Language");
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

            modelBuilder.Entity<UserPasswordReset>(entity =>
            {
                entity.HasKey(e => e.UserPasswordResetId);

                entity.ToTable("T_UserPasswordReset");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.GuId)
                    .IsRequired()
                    .HasMaxLength(50);
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

            modelBuilder.Entity<UserStoreInvite>(entity =>
            {
                entity.HasKey(e => e.UserStoreInviteId);

                entity.ToTable("T_UserStoreInvite");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.GuId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RecipientEmail)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.UserStoreInvite)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_T_UserStoreInvite_T_Store");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserStoreInvite)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_T_UserStoreInvite_T_User");
            });
        }
    }
}
