create database WordinOnDB
use WordinOnDB

-----------------------------------------------------------------------------------------------

drop table Estudante;
drop table Professor;
drop table Sala;
drop table Tema;
drop table Avaliacao;
drop table Nota;
drop table Redacao;

select * from Estudante;
select * from Professor;
select * from Sala;
select * from Tema;
select * from Avaliacao;
select * from Nota;
select * from Redacao;

-----------------------------------------------------------------------------------------------

create table Usuario
(
	cod int primary key identity (1,1),
	nome varchar (200) not null,
	sobrenome varchar (200) not null,
	senha varchar (200) not null,
	email varchar (250) not null
);

create table Estudante
(
	cod int primary key identity (1,1),
	codUsusario int foreign key references Usuario (cod)
);

create table Professor
(
	cod int primary key identity (1,1),
	codUsusario int foreign key references Usuario (cod),
	chave int not null
);

create table Tema
(
	cod int primary key identity (1,1),
	nome varchar (200) not null,
	descricao varchar (max) not null
);

create table Avaliacao
(
	cod int primary key identity (1,1),
	texto varchar (max) not null,
	codProfessor int foreign key references Professor(cod) not null 
);

create table Nota
(
	cod int primary key identity (1,1),
	valor int not null,
	codProfessor int foreign key references Professor (cod) not null
);

create table Redacao
(
	cod int primary key identity (1,1),
	texto varchar (max) not null,
	tempo int not null,
	codTema int foreign key references Tema (cod) not null,
	codAvaliacao int foreign key references Avaliacao (cod) null,
	codNota int foreign key references Nota (cod) null,
	codEstudante int foreign key references Estudante (cod) not null,
	data date
);

create table Sala
(
	cod int primary key identity (1,1),
	nome varchar (200) not null,
	codProf int foreign key references Professor (cod) not null,
	codEstudante int foreign key references Estudante (cod) null,
	codRedacao int foreign key references Redacao (cod) null
);

-----------------------------------------------------------------------------------------------

create table salaXestudante
(
	codEst int references Estudante (cod) not null,
	codSala int references sala (cod) not null,
	constraint pkSxE primary key (codEst, codSala)z
);
