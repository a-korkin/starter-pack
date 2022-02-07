CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migrationid character varying(150) NOT NULL,
    productversion character varying(32) NOT NULL,
    CONSTRAINT pk___efmigrationshistory PRIMARY KEY (migrationid)
);

START TRANSACTION;

CREATE SCHEMA IF NOT EXISTS common;

CREATE SCHEMA IF NOT EXISTS admin;

CREATE TABLE common.cs_entity_types (
    id uuid NOT NULL,
    c_name text NOT NULL,
    c_slug text NOT NULL,
    c_schema text NOT NULL,
    c_tablename text NOT NULL,
    CONSTRAINT pk_cs_entity_types PRIMARY KEY (id)
);
COMMENT ON TABLE common.cs_entity_types IS 'типы сущностей';
COMMENT ON COLUMN common.cs_entity_types.id IS 'идентификатор';
COMMENT ON COLUMN common.cs_entity_types.c_name IS 'название';
COMMENT ON COLUMN common.cs_entity_types.c_slug IS 'код';
COMMENT ON COLUMN common.cs_entity_types.c_schema IS 'схема';
COMMENT ON COLUMN common.cs_entity_types.c_tablename IS 'таблица';

CREATE TABLE common.cd_entities (
    id uuid NOT NULL,
    f_type uuid NOT NULL,
    CONSTRAINT pk_cd_entities PRIMARY KEY (id, f_type),
    CONSTRAINT fk_cd_entities_cs_entity_types_f_type FOREIGN KEY (f_type) REFERENCES common.cs_entity_types (id) ON DELETE CASCADE
) PARTITION BY (f_type);
COMMENT ON TABLE common.cd_entities IS 'сущности';
COMMENT ON COLUMN common.cd_entities.id IS 'идентификатор';
COMMENT ON COLUMN common.cd_entities.f_type IS 'тип';

CREATE TABLE admin.cd_users (
    id uuid NOT NULL,
    c_username text NOT NULL,
    c_lastname text NOT NULL,
    c_firstname text NOT NULL,
    c_middlename text NULL,
    f_type uuid NOT NULL,
    CONSTRAINT pk_cd_users PRIMARY KEY (id),
    CONSTRAINT fk_cd_users_cd_entities_entityid_entitytypeid FOREIGN KEY (id, f_type) REFERENCES common.cd_entities (id, f_type) ON DELETE CASCADE
);
COMMENT ON TABLE admin.cd_users IS 'пользователи';
COMMENT ON COLUMN admin.cd_users.id IS 'идентификатор';
COMMENT ON COLUMN admin.cd_users.c_username IS 'логин';
COMMENT ON COLUMN admin.cd_users.c_lastname IS 'фамилия';
COMMENT ON COLUMN admin.cd_users.c_firstname IS 'имя';
COMMENT ON COLUMN admin.cd_users.c_middlename IS 'отчество';
COMMENT ON COLUMN admin.cd_users.f_type IS 'тип';

CREATE INDEX ix_cd_entities_f_type ON common.cd_entities (f_type);

CREATE INDEX ix_cd_users_id_f_type ON admin.cd_users (id, f_type);

INSERT INTO "__EFMigrationsHistory" (migrationid, productversion)
VALUES ('20220207025459_InitialMigration', '5.0.1');

COMMIT;

