﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211206035422_RefreshToken")]
    partial class RefreshToken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Domain.Entities.Admin.Claim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<bool>("Create")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("b_create")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("Delete")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("b_delete")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("Read")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("b_read")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<Guid>("RoleId")
                        .HasColumnName("f_role")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TypeId")
                        .HasColumnName("f_type")
                        .HasColumnType("uuid");

                    b.Property<bool>("Update")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("b_update")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.HasKey("Id")
                        .HasName("pk_cd_claims");

                    b.HasIndex("RoleId");

                    b.HasIndex("TypeId");

                    b.ToTable("cd_claims","admin");
                });

            modelBuilder.Entity("Domain.Entities.Admin.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnName("c_title")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_cd_roles");

                    b.ToTable("cd_roles","admin");
                });

            modelBuilder.Entity("Domain.Entities.Admin.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("c_password")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnName("c_refresh_token")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("c_username")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_cd_users");

                    b.ToTable("cd_users","admin");
                });

            modelBuilder.Entity("Domain.Entities.Common.Entity", b =>
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

            modelBuilder.Entity("Domain.Entities.Common.EntityType", b =>
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

                    b.ToTable("cs_entity_types","common");
                });

            modelBuilder.Entity("Domain.Entities.Admin.Claim", b =>
                {
                    b.HasOne("Domain.Entities.Admin.Role", "Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_cd_claims_cd_roles_f_role")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Common.EntityType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .HasConstraintName("fk_cd_claims_cs_entity_types_f_type")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Admin.User", b =>
                {
                    b.HasOne("Domain.Entities.Common.Entity", null)
                        .WithMany()
                        .HasForeignKey("Id")
                        .HasConstraintName("fk_cd_users_cd_entities_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Common.Entity", b =>
                {
                    b.HasOne("Domain.Entities.Common.EntityType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .HasConstraintName("fk_cd_entities_cs_entity_types_f_type")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
