use DBMotorista; 

if exists (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'U' AND NAME = 'Penalidade')
	drop table Penalidade;
GO

CREATE TABLE Penalidade(

    cnpj VARCHAR(18) NOT NULL,
    razao_social VARCHAR(40),
    nome_motorista VARCHAR(30),
    cpf VARCHAR(14),
    vigencia_do_cadastro DATE
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

select count(*) from Penalidade;
select * from Penalidade;