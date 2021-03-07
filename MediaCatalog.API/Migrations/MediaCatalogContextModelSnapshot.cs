﻿// <auto-generated />
using System;
using MediaCatalog.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MediaCatalog.API.Migrations
{
    [DbContext(typeof(MediaCatalogContext))]
    partial class MediaCatalogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Actors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Sylvester",
                            LastName = "Stallone"
                        });
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.ActorMovie", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("ActorId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("ActorMovie");

                    b.HasData(
                        new
                        {
                            ActorId = 1,
                            MovieId = 1
                        });
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Directors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Sylvestor",
                            LastName = "Stallone"
                        });
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.DirectorMovie", b =>
                {
                    b.Property<int>("DirectorId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("DirectorId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("DirectorMovie");

                    b.HasData(
                        new
                        {
                            DirectorId = 1,
                            MovieId = 1
                        });
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "featuring characters involved in exciting and usually dangerous activities and adventures The movie is closer to an action-adventure thriller than a journalistic account, but energetic acting and vigorous directing make it work harrowingly well on its own terms.",
                            Name = "Action Adventure"
                        });
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.GenreMovie", b =>
                {
                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("GenreId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("GenreMovie");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            MovieId = 1
                        });
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.MediaImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("MediaImages");
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.MediaType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("MediaType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "a format of DVD designed for the storage of high-definition video and data.",
                            Name = "Blu-ray"
                        });
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.MediaTypeMovie", b =>
                {
                    b.Property<int>("MediaTypeId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("MediaTypeId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MediaTypeMovie");

                    b.HasData(
                        new
                        {
                            MediaTypeId = 1,
                            MovieId = 1
                        });
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Favorite")
                        .HasColumnType("bit");

                    b.Property<string>("LongDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RatingId")
                        .HasColumnType("int");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<int>("RunTime")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("RatingId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Favorite = true,
                            LongDescription = "After spending several years in Northern Thailand operating a longboat on the Salween River, John Rambo reluctantly agrees to carry a group of Christian missionaries into war-torn Burma. But when the aid workers are captured by ruthless Nationalist Army soldiers, Rambo leads a group of battle-scarred, combat-hardened mercenaries on an epic last-ditch mission to rescue the prisoners - at all costs.",
                            RatingId = 1,
                            ReleaseYear = 2008,
                            RunTime = 91,
                            ShortDescription = "The ultimate American action hero returns - with a vengeance!",
                            Title = "Rambo: The Fight Continues"
                        });
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ratings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Under 17 requires accompanying parent or adult guardian. Contains some adult material. Parents are urged to learn more about the film before taking their young children with them.",
                            Name = "R"
                        });
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.Studio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Studios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The first major new studio in decades, Lionsgate is a global content leader with a reputation for innovation whose films, television series, location-based and live entertainment attractions and Starz premium pay platform reach next generation audiences around the world.",
                            Name = "Lionsgate"
                        });
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.StudioMovie", b =>
                {
                    b.Property<int>("StudioId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("StudioId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("StudioMovie");

                    b.HasData(
                        new
                        {
                            StudioId = 1,
                            MovieId = 1
                        });
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.ActorMovie", b =>
                {
                    b.HasOne("MediaCatalog.API.Data.Entities.Actor", "Actor")
                        .WithMany("Movies")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediaCatalog.API.Data.Entities.Movie", "Movie")
                        .WithMany("MovieActors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.DirectorMovie", b =>
                {
                    b.HasOne("MediaCatalog.API.Data.Entities.Director", "Director")
                        .WithMany("DirectorMovies")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediaCatalog.API.Data.Entities.Movie", "Movie")
                        .WithMany("MovieDirectors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.GenreMovie", b =>
                {
                    b.HasOne("MediaCatalog.API.Data.Entities.Genre", "Genre")
                        .WithMany("GenreMovies")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediaCatalog.API.Data.Entities.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.MediaImage", b =>
                {
                    b.HasOne("MediaCatalog.API.Data.Entities.Movie", null)
                        .WithMany("MediaImages")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.MediaTypeMovie", b =>
                {
                    b.HasOne("MediaCatalog.API.Data.Entities.MediaType", "MediaType")
                        .WithMany("MediaTypeMovies")
                        .HasForeignKey("MediaTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediaCatalog.API.Data.Entities.Movie", "Movie")
                        .WithMany("MovieMediaTypes")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.Movie", b =>
                {
                    b.HasOne("MediaCatalog.API.Data.Entities.Rating", "Rating")
                        .WithMany("RatingMovies")
                        .HasForeignKey("RatingId");
                });

            modelBuilder.Entity("MediaCatalog.API.Data.Entities.StudioMovie", b =>
                {
                    b.HasOne("MediaCatalog.API.Data.Entities.Movie", "Movie")
                        .WithMany("MovieStudios")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediaCatalog.API.Data.Entities.Studio", "Studio")
                        .WithMany("StudioMovies")
                        .HasForeignKey("StudioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
