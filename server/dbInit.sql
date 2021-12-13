-- заменить название бд, пользователя и пароль!!!
create database start_proj_db;
create user start_user with encrypted password 'start_user_pwd';
grant all privileges on database start_proj_db to start_user;

create schema common;
grant all privileges on schema common to group start_user;
grant all privileges on all tables in schema common to group start_user;

create schema admin;
grant all privileges on schema admin to group start_user;
grant all privileges on all tables in schema admin to group start_user;
