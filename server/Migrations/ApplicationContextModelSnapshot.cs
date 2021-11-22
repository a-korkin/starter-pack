﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using server.DbContexts;

namespace server.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("server.Entities.Admin.EntityType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("c_name")
                        .HasColumnType("text");

                    b.Property<string>("Schema")
                        .IsRequired()
                        .HasColumnName("c_schema")
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnName("c_slug")
                        .HasColumnType("text");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnName("c_tablename")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_cs_entity_types");

                    b.ToTable("cs_entity_types","admin");
                });

            modelBuilder.Entity("server.Entities.Common.Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TypeId")
                        .HasColumnName("f_type")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("pk_cd_entities");

                    b.HasIndex("TypeId");

                    b.ToTable("cd_entities","common");
                });

            modelBuilder.Entity("server.Entities.Common.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("c_first_name")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("c_last_name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_cd_persons");

                    b.ToTable("cd_persons","common");
                });

            modelBuilder.Entity("server.Entities.Common.Entity", b =>
                {
                    b.HasOne("server.Entities.Admin.EntityType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .HasConstraintName("fk_cd_entities_cs_entitie_types_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("server.Entities.Common.Person", b =>
                {
                    b.HasOne("server.Entities.Common.Entity", null)
                        .WithMany()
                        .HasForeignKey("Id")
                        .HasConstraintName("fk_cd_persons_cd_entities_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
