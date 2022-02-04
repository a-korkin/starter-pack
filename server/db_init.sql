create database sp_db;
create user sp_user with encrypted password 'sp_user_pwd';
grant all privileges on database sp_db to sp_user;

create schema common;
grant all privileges on schema common to group sp_user;
grant all privileges on all tables in schema common to group sp_user;

create schema admin;
grant all privileges on schema admin to group sp_user;
grant all privileges on all tables in schema admin to group sp_user;