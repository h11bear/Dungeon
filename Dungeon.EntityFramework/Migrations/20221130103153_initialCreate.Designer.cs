﻿// <auto-generated />
using Dungeon.EntityFramwork.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dungeon.EntityFramework.Migrations
{
    [DbContext(typeof(DungeonContext))]
    [Migration("20221130103153_initialCreate")]
    partial class initialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Dungeon.Logic.Model.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Narrative")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Dungeon.Logic.Model.RoomCatalog", b =>
                {
                    b.Property<int>("RoomCatalogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomCatalogId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoomCatalogId");

                    b.ToTable("RoomCatalogs");
                });

            modelBuilder.Entity("Dungeon.Logic.Model.Room", b =>
                {
                    b.OwnsMany("Dungeon.Logic.Model.RoomExit", "Exits", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Keyword")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("RoomId")
                                .HasColumnType("int");

                            b1.Property<string>("RoomName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Id");

                            b1.HasIndex("RoomId");

                            b1.ToTable("RoomExit");

                            b1.WithOwner()
                                .HasForeignKey("RoomId");
                        });

                    b.Navigation("Exits");
                });
#pragma warning restore 612, 618
        }
    }
}
