using Microsoft.EntityFrameworkCore;

namespace Web.Domain
{
    public partial class MariaDbContext : DbContext
    {
        public MariaDbContext()
        {
        }

        public MariaDbContext(DbContextOptions<MariaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContentType> ContentTypes { get; set; }
        public virtual DbSet<FabricItem> FabricItems { get; set; }
        public virtual DbSet<FabricOption> FabricOptions { get; set; }
        public virtual DbSet<Fabric> Fabrics { get; set; }
        public virtual DbSet<FabricType> FabricTypes { get; set; }
        public virtual DbSet<KitManufacturer> KitManufacturers { get; set; }
        public virtual DbSet<Kit> Kits { get; set; }
        public virtual DbSet<PatternAuthor> PatternAuthors { get; set; }
        public virtual DbSet<Pattern> Patterns { get; set; }
        public virtual DbSet<ThreadColorOption> ThreadColorOptions { get; set; }
        public virtual DbSet<ThreadColor> ThreadColors { get; set; }
        public virtual DbSet<ThreadManufacturer> ThreadManufacturers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=TSql2005!;database=cross_stitch_web_copy");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ContentType>(entity =>
                {
                    entity.ToTable("content_types");

                    entity.Property(e => e.Id)
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    entity.Property(e => e.Name)
                            .IsRequired()
                            .HasColumnName("name")
                            .HasColumnType("varchar(64)");

                    entity.HasAlternateKey(e => e.Name);
                });

            modelBuilder.Entity<FabricItem>(entity =>
            {
                entity.ToTable("fabric_items");

                entity.HasIndex(e => e.FabricId)
                    .HasName("fk_fabric_id");

                entity.Property(e => e.Id)
                                .HasColumnName("id")
                                .HasColumnType("int(11)");

                entity.Property(e => e.ColorId)
                                .IsRequired()
                                .HasColumnName("color_id")
                                .HasColumnType("varchar(16)");

                entity.Property(e => e.ColorName)
                                .IsRequired()
                                .HasColumnName("color_name")
                                .HasColumnType("varchar(64)");

                entity.Property(e => e.FabricId)
                                .HasColumnName("fabric_id")
                                .HasColumnType("int(11)");

                entity.Property(e => e.Sku)
                                .IsRequired()
                                .HasColumnName("sku")
                                .HasColumnType("varchar(16)");

                entity.HasOne(d => d.Fabric)
                                .WithMany(p => p.FabricItems)
                                .HasForeignKey(d => d.FabricId)
                                .HasConstraintName("fk_fabric_id");
            });

            modelBuilder.Entity<FabricOption>(entity =>
            {
                entity.HasKey(e => new { e.PatternId, e.FabricItemId });

                entity.ToTable("fabric_options");

                entity.HasIndex(e => e.FabricItemId)
                    .HasName("fk_fabric_option_fabric_item_id");

                entity.Property(e => e.PatternId)
                                    .HasColumnName("pattern_id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.FabricItemId)
                                    .HasColumnName("fabric_item_id")
                                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FabricItem)
                                    .WithMany(p => p.FabricOptions)
                                    .HasForeignKey(d => d.FabricItemId)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("fk_fabric_option_fabric_item_id");

                entity.HasOne(d => d.Pattern)
                                    .WithMany(p => p.FabricOptions)
                                    .HasForeignKey(d => d.PatternId)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("fk_fabric_option_pattern_id");
            });

            modelBuilder.Entity<Fabric>(entity =>
            {
                entity.ToTable("fabrics");

                entity.HasAlternateKey(e => new {e.Name, e.Count});

                entity.HasIndex(e => e.ContentTypeId)
                    .HasName("fk_content_type_id");

                entity.HasIndex(e => e.FabricTypeId)
                                    .HasName("fk_fabric_type_id");

                entity.Property(e => e.Id)
                                    .HasColumnName("id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.ContentTypeId)
                                    .HasColumnName("content_type_id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.Count)
                                    .HasColumnName("count")
                                    .HasColumnType("tinyint(4)")
                                    .HasDefaultValueSql("'16'");

                entity.Property(e => e.FabricTypeId)
                                    .HasColumnName("fabric_type_id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                                    .IsRequired()
                                    .HasColumnName("name")
                                    .HasColumnType("varchar(64)");

                entity.Property(e => e.Priority)
                                    .HasColumnName("priority")
                                    .HasColumnType("tinyint(4)")
                                    .HasDefaultValueSql("'3'");

                entity.HasOne(d => d.ContentType)
                                    .WithMany(p => p.Fabrics)
                                    .HasForeignKey(d => d.ContentTypeId)
                                    .HasConstraintName("fk_content_type_id");

                entity.HasOne(d => d.FabricType)
                                    .WithMany(p => p.Fabrics)
                                    .HasForeignKey(d => d.FabricTypeId)
                                    .HasConstraintName("fk_fabric_type_id");
            });

            modelBuilder.Entity<FabricType>(entity =>
            {
                entity.ToTable("fabric_types");

                entity.HasAlternateKey(e => e.Name);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                                    .IsRequired()
                                    .HasColumnName("name")
                                    .HasColumnType("varchar(16)");
            });

            modelBuilder.Entity<KitManufacturer>(entity =>
            {
                entity.ToTable("kit_manufacturers");

                entity.HasAlternateKey(e => e.Name);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                                    .IsRequired()
                                    .HasColumnName("name")
                                    .HasColumnType("varchar(64)");
            });

            modelBuilder.Entity<Kit>(entity =>
            {
                entity.ToTable("kits");

                entity.HasAlternateKey(e => new {e.Title, e.ManufacturerId});

                entity.HasIndex(e => e.AuthorId)
                    .HasName("fk_kit_author_id");

                entity.HasIndex(e => e.FabricItemId)
                                    .HasName("fk_kit_fabric_item_id");

                entity.HasIndex(e => e.ManufacturerId)
                                    .HasName("fk_kit_manufacturer_id");

                entity.HasIndex(e => e.ThreadManufacturerId)
                                    .HasName("fk_kit_thread_manufacturer_id");

                entity.Property(e => e.Id)
                                    .HasColumnName("id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.AuthorId)
                                    .HasColumnName("author_id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.ColorsCount)
                                    .HasColumnName("colors_count")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.FabricItemId)
                                    .HasColumnName("fabric_item_id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.HeightSm)
                                    .HasColumnName("height_sm")
                                    .HasColumnType("decimal(7,2)");

                entity.Property(e => e.HeightStitches)
                                    .HasColumnName("height_stitches")
                                    .HasColumnType("smallint");

                entity.Property(e => e.Image)
                                    .IsRequired()
                                    .HasColumnName("image")
                                    .HasColumnType("varchar(256)");

                entity.Property(e => e.Item)
                                    .IsRequired()
                                    .HasColumnName("item")
                                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Link)
                                    .IsRequired()
                                    .HasColumnName("link")
                                    .HasColumnType("varchar(256)");

                entity.Property(e => e.ManufacturerId)
                                    .HasColumnName("manufacturer_id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.ThreadManufacturerId)
                                    .HasColumnName("thread_manufacturer_id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.Title)
                                    .IsRequired()
                                    .HasColumnName("title")
                                    .HasColumnType("varchar(64)");

                entity.Property(e => e.WidthSm)
                                    .HasColumnName("width_sm")
                                    .HasColumnType("decimal(7,2)");

                entity.Property(e => e.WidthStitches)
                                    .HasColumnName("width_stitches")
                                    .HasColumnType("smallint");

                entity.HasOne(d => d.Author)
                                    .WithMany(p => p.Kits)
                                    .HasForeignKey(d => d.AuthorId)
                                    .OnDelete(DeleteBehavior.Cascade)
                                    .HasConstraintName("fk_kit_author_id");

                entity.HasOne(d => d.FabricItem)
                                    .WithMany(p => p.Kits)
                                    .HasForeignKey(d => d.FabricItemId)
                                    .HasConstraintName("fk_kit_fabric_item_id");

                entity.HasOne(d => d.Manufacturer)
                                    .WithMany(p => p.Kits)
                                    .HasForeignKey(d => d.ManufacturerId)
                                    .HasConstraintName("fk_kit_manufacturer_id");

                entity.HasOne(d => d.ThreadManufacturer)
                                    .WithMany(p => p.Kits)
                                    .HasForeignKey(d => d.ThreadManufacturerId)
                                    .HasConstraintName("fk_kit_thread_manufacturer_id");
            });

            modelBuilder.Entity<PatternAuthor>(entity =>
            {
                entity.ToTable("pattern_authors");

                entity.HasAlternateKey(e => e.Name);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                                    .IsRequired()
                                    .HasColumnName("name")
                                    .HasColumnType("varchar(64)");
                entity.HasAlternateKey(author => author.Name);
            });

            modelBuilder.Entity<Pattern>(entity =>
            {
                entity.ToTable("patterns");

                entity.HasAlternateKey(e => new {e.AuthorId, e.Title});

                entity.HasIndex(e => e.AuthorId)
                    .HasName("fk_pattern_author_id");

                entity.Property(e => e.Id)
                                    .HasColumnName("id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.AuthorId)
                                    .HasColumnName("author_id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.ColorsCount)
                                    .HasColumnName("colors_count")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.Height)
                                    .HasColumnName("height")
                                    .HasColumnType("smallint");

                entity.Property(e => e.Width)
                    .HasColumnName("width")
                    .HasColumnType("smallint");

                entity.Property(e => e.Image)
                                    .IsRequired()
                                    .HasColumnName("image")
                                    .HasColumnType("varchar(256)");

                entity.Property(e => e.Item)
                                    .IsRequired()
                                    .HasColumnName("item")
                                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Link)
                                    .IsRequired()
                                    .HasColumnName("link")
                                    .HasColumnType("varchar(256)");

                entity.Property(e => e.Title)
                                    .IsRequired()
                                    .HasColumnName("title")
                                    .HasColumnType("varchar(64)");

                entity.HasOne(d => d.Author)
                                    .WithMany(p => p.Patterns)
                                    .HasForeignKey(d => d.AuthorId)
                                    .HasConstraintName("fk_pattern_author_id");
            });

            modelBuilder.Entity<ThreadColorOption>(entity =>
            {
                entity.HasKey(e => new { e.PatternId, e.ThreadColorId });

                entity.ToTable("thread_color_options");

                entity.HasIndex(e => e.ThreadColorId)
                    .HasName("fk_thread_color_option_thread_color_id");

                entity.Property(e => e.PatternId)
                                    .HasColumnName("pattern_id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.ThreadColorId)
                                    .HasColumnName("thread_color_id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.RequiredLength)
                                    .HasColumnName("required_length")
                                    .HasColumnType("decimal(5,2) unsigned zerofill");

                entity.HasOne(d => d.Pattern)
                                    .WithMany(p => p.ThreadColorOptions)
                                    .HasForeignKey(d => d.PatternId)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("fk_thread_color_option_pattern_id");

                entity.HasOne(d => d.ThreadColor)
                                    .WithMany(p => p.ThreadColorOptions)
                                    .HasForeignKey(d => d.ThreadColorId)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("fk_thread_color_option_thread_color_id");
            });

            modelBuilder.Entity<ThreadColor>(entity =>
            {
                entity.ToTable("thread_colors");

                entity.HasAlternateKey(e => new {e.ManufacturerId, e.ColorId});

                entity.HasIndex(e => e.ManufacturerId)
                    .HasName("fk_manufacturer_id");

                entity.Property(e => e.Id)
                                    .HasColumnName("id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.ColorId)
                                    .IsRequired()
                                    .HasColumnName("color_id")
                                    .HasColumnType("varchar(16)");

                entity.Property(e => e.ColorName)
                                    .IsRequired()
                                    .HasColumnName("color_name")
                                    .HasColumnType("varchar(64)");

                entity.Property(e => e.Length)
                                    .HasColumnName("length")
                                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.ManufacturerId)
                                    .HasColumnName("manufacturer_id")
                                    .HasColumnType("int(11)");

                entity.Property(e => e.RgbColor)
                                    .HasColumnName("rgb_color")
                                    .HasColumnType("varchar(8)");

                entity.Property(e => e.Sku)
                                    .IsRequired()
                                    .HasColumnName("sku")
                                    .HasColumnType("varchar(16)");

                entity.HasOne(d => d.Manufacturer)
                                    .WithMany(p => p.ThreadColors)
                                    .HasForeignKey(d => d.ManufacturerId)
                                    .HasConstraintName("fk_manufacturer_id");
            });

            modelBuilder.Entity<ThreadManufacturer>(entity =>
            {
                entity.ToTable("thread_manufacturers");

                entity.HasAlternateKey(e => e.Name);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                                    .IsRequired()
                                    .HasColumnName("name")
                                    .HasColumnType("varchar(64)");
            });
        }
    }
}
