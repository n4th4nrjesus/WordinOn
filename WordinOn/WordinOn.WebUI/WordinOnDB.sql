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
--select * from salaXprofessor;

------------------------------------------------------------------------------------------------

create table Usuario
(
	cod int primary key identity (1,1),
	nome varchar (200) not null,
	sobrenome varchar (200) not null,
	senha varchar (200) not null,
	email varchar (250) not null,
	chave varchar (100),
	perfil_usuario int not null
);

create table Tema
(
	cod int primary key identity (1,1),
	nome varchar (200) not null,
	descricao varchar (max) not null,
	removido bit default 0 
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
	codRedacao int foreign key references Redacao(cod) not null,
	data datetime not null default getdate() 
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

insert into Usuario (nome, sobrenome, senha, email, chave, perfil_usuario) 
values ('Nathan', 'Jesus', '123', 'nathan.jesus@gmail.com', 456, 1);
insert into Usuario (nome, sobrenome, senha, email, chave, perfil_usuario) 
values ('Eliseu', 'Messias', '321', 'eliseu.messias@gmail.com', 457, 1);

insert into Usuario (nome, sobrenome, senha, email, chave, perfil_usuario) 
values ('Tiago', 'Andrade', '123', 'tgnandrade@gmail.com', 999, 2);
insert into Usuario (nome, sobrenome, senha, email, chave, perfil_usuario) 
values ('Wagner', 'Oliveira', '321', 'wsoliveira@live.com', 1000, 2);

insert into Tema (nome, descricao) 
values 
('Política', 'Política do Brasil'), 
('Desigualdade', 'Desigualdade racial')

insert into Sala (nome)
values
('Metamorfose'),
('Raízes Urbanas')

insert into Redacao (texto, tempo, codTema, codEstudante, codSala)
values
('blablablabla1', 70, 1, 1, 1),
('beruberuberu1', 50, 2, 2, 2)

insert into Avaliacao (texto, valor, codProfessor, codRedacao)
values
('Muito bom', 750, 3, 1),
('Muito bom mesmo', 800, 3, 1)

insert into salaXestudante (codEstudante, codSala)
values
(1, 1),
(2, 2)

insert into salaXprofessor (codProfessor, codSala)
values
(3, 1),
(4, 2)
