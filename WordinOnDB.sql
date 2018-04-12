create database WordinOnDB
go

use WordinOnDB
go

------------------------------------------------------------------------------------------------

--drop table Usuario;
--drop table Tema;
--drop table Sala;
--drop table Redacao;
--drop table Avaliacao;
--drop table salaXestudante;
--drop table salaXprofessor;

--select * from Usuario;
--select * from Tema;
--select * from Sala;
--select * from Redacao;
--select * from Avaliacao;
--select * from salaXestudante;
--select * from salaXprofessor

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

--select u.nome, t.nome, r.data from Redacao r 
--	inner join Usuario u on u.cod = r.codEstudante
--	inner join Tema t on t.cod = r.codTema;
	
--select top 1 * from Usuario order by newid()


--select * from tema;
--select * from Redacao;

--select * from usuario;

insert into tema (nome, descricao) values ('Política', 'Política do Brasil');

insert into usuario (nome, sobrenome, senha, email, chave, perfil_usuario) values ('Nathan', 'Jesus', '123', 'nathan.jesus@gmail.com', 456, 1);

select * from sala;
insert into sala (nome) values ('lab 02');


insert into redacao (texto, tempo, codTema, codEstudante, codSala, data) values ('minha primeira redacao', 10, 1, 1, 1, getdate());

select * from redacao;


