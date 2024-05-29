use DBMotorista; 

if exists (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'U' AND NAME = 'Penalidade')
	drop table Penalidade;
GO

CREATE TABLE Penalidade(

    cnpj VARCHAR NOT NULL,
    razao_social VARCHAR,
    nome_motorista VARCHAR,
    cpf VARCHAR,
    vigencia_do_cadastro DATE
);
    
GO
CREATE OR ALTER PROC InserirPenalidade

    @cnpj VARCHAR,
    @razao_social VARCHAR,
    @nome_motorista VARCHAR,
    @cpf VARCHAR,
    @vigencia_do_cadastro VARCHAR(10)
    AS
    BEGIN

        DECLARE @data_criacao DATE = CONVERT(DATE, @vigencia_do_cadastro, 103);

        INSERT INTO Penalidade
            VALUES (@cnpj, @razao_social, @nome_motorista, @cpf, @data_criacao);
END;

select * from Penalidade;