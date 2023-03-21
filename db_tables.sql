/*
    drop table if exists token;
    drop table if exists post;
    drop table if exists topic;
    drop table if exists forum;
    drop table if exists section;
    drop table if exists account;
    drop table if exists role;
    

    for creating models and context from console:
    dotnet ef dbcontext scaffold "Host=localhost:5432;Database=asp_forum;Username=postgres;Password=.Qwerty1%" Npgsql.EntityFrameworkCore.PostgreSQL -o ./temp
*/

-- role right level from 0 to inf (0 is the lowest right level(user))
create table role (
	id serial primary key,
	name text not null
);

insert into role(id, name)
values
	(0, 'user'),
    (1, 'moderator'),
	(2, 'admin');

create table account(
    id serial primary key,
    role_id integer not null constraint fk_account_role_id references role(id),
    username text not null constraint account_username_unique unique,
    password_hash text not null,
    email text default null,
    register_date timestamp default now(),
    image_path text default null
);

create table section (
    id serial primary key,
    title text not null,
    order_number integer default 0
);

create table forum (
    id serial primary key,
    title text not null,
    order_number integer default 0,
    section_id integer not null constraint fk_forum_section_id references section(id),
    image_path text default null
);

create table topic(
    id serial primary key,
    title text not null,
    create_date timestamp default now(),
    is_pinned boolean default false,
    is_closed boolean default false,
    author_id integer not null constraint fk_topic_author_id references account(id),
    forum_id integer not null constraint fk_topic_forum_id references forum(id)
);

create table post(
    id serial primary key,
    content text not null,
    create_date timestamp default now(),
    last_edit_date timestamp default now(),
    author_id integer not null constraint fk_post_author_id references account(id),
    topic_id integer not null constraint fk_post_topic_id references topic(id)
);

create table token(
    id serial primary key,
    account_id integer not null constraint fk_token_account_id references account(id),
    token text not null,
    expire_date timestamp not null
);