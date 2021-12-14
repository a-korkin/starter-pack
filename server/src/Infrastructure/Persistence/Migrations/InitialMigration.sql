CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE SCHEMA IF NOT EXISTS admin;

CREATE SCHEMA IF NOT EXISTS common;

CREATE TABLE common.cs_entity_types (
    id uuid NOT NULL,
    c_name text NOT NULL,
    c_slug text NOT NULL,
    c_schema text NOT NULL,
    c_tablename text NOT NULL,
    CONSTRAINT pk_cs_entity_types PRIMARY KEY (id)
);

CREATE TABLE common.cd_entities (
    id uuid NOT NULL,
    f_type uuid NOT NULL,
    CONSTRAINT pk_cd_entities PRIMARY KEY (id, f_type),
    CONSTRAINT fk_cd_entities_cs_entity_types_f_type FOREIGN KEY (f_type) REFERENCES common.cs_entity_types (id) ON DELETE CASCADE
) PARTITION BY LIST(f_type);

CREATE TABLE admin.cd_roles (
    id uuid NOT NULL,
    f_type uuid NOT NULL,
    c_title text NULL,
    CONSTRAINT pk_cd_roles PRIMARY KEY (id),
    CONSTRAINT fk_cd_roles_cd_entities_id FOREIGN KEY (id, f_type) REFERENCES common.cd_entities (id, f_type) ON DELETE CASCADE
);

CREATE TABLE admin.cd_users (
    id uuid NOT NULL,
    f_type uuid NOT NULL,
    c_username text NOT NULL,
    c_password text NOT NULL,
    c_refresh_token text NULL,
    CONSTRAINT pk_cd_users PRIMARY KEY (id),
    CONSTRAINT fk_cd_users_cd_entities_id FOREIGN KEY (id, f_type) REFERENCES common.cd_entities (id, f_type) ON DELETE CASCADE
);

CREATE TABLE admin.cd_claims (
    id uuid NOT NULL,
    f_type uuid NOT NULL,
    f_role uuid NOT NULL,
    b_create boolean NOT NULL DEFAULT FALSE,
    b_read boolean NOT NULL DEFAULT FALSE,
    b_update boolean NOT NULL DEFAULT FALSE,
    b_delete boolean NOT NULL DEFAULT FALSE,
    CONSTRAINT pk_cd_claims PRIMARY KEY (id),
    CONSTRAINT fk_cd_claims_cd_roles_f_role FOREIGN KEY (f_role) REFERENCES admin.cd_roles (id) ON DELETE CASCADE,
    CONSTRAINT fk_cd_claims_cs_entity_types_f_type FOREIGN KEY (f_type) REFERENCES common.cs_entity_types (id) ON DELETE CASCADE
);

CREATE TABLE admin.cd_user_roles (
    id uuid NOT NULL,
    f_user uuid NOT NULL,
    f_role uuid NOT NULL,
    CONSTRAINT pk_cd_user_claims PRIMARY KEY (f_user, f_role),
    CONSTRAINT fk_cd_user_roles_cd_users_f_role FOREIGN KEY (f_role) REFERENCES admin.cd_roles (id) ON DELETE CASCADE,
    CONSTRAINT fk_cd_user_roles_cd_roles_f_user FOREIGN KEY (f_user) REFERENCES admin.cd_users (id) ON DELETE CASCADE
);

CREATE INDEX "IX_cd_claims_f_role" ON admin.cd_claims (f_role);

CREATE INDEX "IX_cd_claims_f_type" ON admin.cd_claims (f_type);

CREATE INDEX "IX_cd_roles_id_f_type" ON admin.cd_roles (id, f_type);

CREATE INDEX "IX_cd_user_roles_f_role" ON admin.cd_user_roles (f_role);

CREATE INDEX "IX_cd_users_id_f_type" ON admin.cd_users (id, f_type);

CREATE INDEX "IX_cd_entities_f_type" ON common.cd_entities (f_type);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211214040852_InitialMigration', '3.1.3');

