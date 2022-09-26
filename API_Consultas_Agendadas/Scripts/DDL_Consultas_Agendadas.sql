CREATE DATABASE Consultas_Agendadas;
GO

USE Consultas_Agendadas;
GO

CREATE TABLE TipoUsuario(
	Id INT PRIMARY KEY IDENTITY,
	Tipo NVARCHAR(MAX)
);
GO

CREATE TABLE Especialidade(
	Id INT PRIMARY KEY IDENTITY,
	Categoria NVARCHAR(MAX)
);
GO

CREATE TABLE Usuario(
	Id INT PRIMARY KEY IDENTITY,
	Nome NVARCHAR(MAX),
	Email NVARCHAR(MAX),
	Senha NVARCHAR(MAX),

	-- FK's
	IdTipoUsuario INT NOT NULL
	FOREIGN KEY (IdTipoUsuario) REFERENCES TipoUsuario(Id)
);

CREATE TABLE Paciente(
	Id INT PRIMARY KEY IDENTITY,
	Carteirinha NVARCHAR(MAX),
	DataNascimento DATETIME,
	Ativo BIT,

	-- FK's
	IdUsuario INT NOT NULL
	FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id)
);
GO

CREATE TABLE Medico(
	Id INT PRIMARY KEY IDENTITY,
	CRM NVARCHAR(MAX),

	-- FK's
	IdUsuario INT NOT NULL
	FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id),

	IdEspecialidade INT NOT NULL
	FOREIGN KEY (IdEspecialidade) REFERENCES Especialidade(Id)
);
GO

CREATE TABLE Consulta(
	Id INT PRIMARY KEY IDENTITY,
	DataHora DATETIME,

	-- FK's
	IdPaciente INT NOT NULL
	FOREIGN KEY (IdPaciente) REFERENCES Paciente(Id),

	IdMedico INT NOT NULL
	FOREIGN KEY (IdMedico) REFERENCES Medico(Id)	
);
GO

INSERT INTO TipoUsuario (Tipo) VALUES ('Paciente'),
									  ('Medico');
GO