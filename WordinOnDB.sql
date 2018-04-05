create database WordinOnDB
use WordinOnDB

------------------------------------------------------------------------------------------------

drop table Usuario;
drop table Sala;
drop table Tema;
drop table Avaliacao;
drop table Nota;
drop table Redacao;

select * from Usuario;
select * from Sala;
select * from Tema;
select * from Avaliacao;
select * from Nota;
select * from Redacao;

------------------------------------------------------------------------------------------------

create table Usuario
(
	cod int primary key identity (1,1),
	nome varchar (200) not null,
	sobrenome varchar (200) not null,
	senha varchar (200) not null,
	email varchar (250) not null,
	chave int,
	perfil_usuario int not null
);

create table Tema
(
	cod int primary key identity (1,1),
	nome varchar (200) not null,
	descricao varchar (max) not null
);

create table Sala
(
	cod int primary key identity (1,1),
	nome varchar (200) not null,
);

create table Redacao
(
	cod int primary key identity (1,1),
	texto varchar (max) not null,
	tempo int not null,
	codTema int foreign key references Tema (cod) not null,
	codEstudante int foreign key references Usuario (cod) not null,
	codSala int foreign key references Sala(cod),
	data datetime not null default getdate() 
);

create table Avaliacao
(
	cod int primary key identity (1,1),
	texto varchar (max) not null,
	valor int not null,
	codProfessor int foreign key references Usuario(cod) not null,
	codRedacao int foreign key references Redacao(cod) not null
);

------------------------------------------------------------------------------------------------

create table salaXestudante
(
	codEstudante int references Usuario(cod) not null,
	codSala int references sala (cod) not null,
	constraint pkSxE primary key (codEstudante, codSala)
);

create table salaXprofessor
(
	codProfessor int references Usuario(cod) not null,
	codSala int references sala (cod) not null,
	constraint pkSxP primary key (codProfessor, codSala)
);

------------------------------------------------------------------------------------------------

select u.nome, t.nome, r.data from Redacao r 
	inner join Usuario u on u.cod = r.codEstudante
	inner join Tema t on t.cod = r.codTema;
	
select top 1 * from Usuario order by newid()

