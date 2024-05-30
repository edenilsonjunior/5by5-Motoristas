use DBMotorista; 

if exists (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'U' AND NAME = 'Penalidade')
	drop table Penalidade;
GO

if exists (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'U' AND NAME = 'ControleProcessamento')
	drop table ControleProcessamento;
GO

CREATE TABLE Penalidade(

    razao_social VARCHAR(50),
    cnpj VARCHAR(18) NOT NULL,
    nome_motorista VARCHAR(40),
    cpf VARCHAR(14),
    vigencia_do_cadastro DATETIME
);
    
CREATE TABLE ControleProcessamento(

    id INT IDENTITY(1, 1) NOT NULL,
    descricao VARCHAR(50),
    data_processamento DATETIME,
    quantidade_insercoes INT,

    CONSTRAINT pkcontroleprocessamento PRIMARY KEY (id)
);

GO
CREATE OR ALTER PROC InserirPenalidade

    @cnpj VARCHAR(18),
    @razao_social VARCHAR(40),
    @nome_motorista VARCHAR(30),
    @cpf VARCHAR(14),
    @vigencia_do_cadastro VARCHAR(10)
    AS
    BEGIN

        DECLARE @data_criacao DATE = CONVERT(DATE, @vigencia_do_cadastro, 103);

        INSERT INTO Penalidade
            VALUES (@cnpj, @razao_social, @nome_motorista, @cpf, @data_criacao);
END;


GO
CREATE OR ALTER PROC InserirControle

    @descricao VARCHAR(50),
    @data_processamento DATETIME,
    @quantidade_insercoes INT
    AS
    BEGIN

        INSERT INTO ControleProcessamento (descricao, data_processamento, quantidade_insercoes)
            VALUES (@descricao, @data_processamento, @quantidade_insercoes);
END;

select * from ControleProcessamento;
select count(*) from Penalidade;
select * from Penalidade;

