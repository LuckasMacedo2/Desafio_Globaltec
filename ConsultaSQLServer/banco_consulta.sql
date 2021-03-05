-- ----------------------------------------------------------------------
create database DesafioAPI

use DesafioAPI

-- ----------------------------------------------------------------------
create table Pessoas (
	Codigo bigint not null,
	Nome varchar(110),
	CpfCnpj varchar(14),

	primary key (Codigo)
);

create table ContasAPagar (
	Numero bigint not null,
	CodigoFornecedor bigint,
	DataVencimento datetime,
	DataProrrogacao datetime,
	Valor numeric(18,6),
	Acrescimneto numeric(18,6),
	Desconto numeric(18,6),

	primary key (Numero),
	foreign key (CodigoFornecedor) references Pessoas(Codigo)
);

create table ContasPagas (
	Numero bigint not null,
	CodigoFornecedor bigint,	
	DataPagamento datetime,
	DataVencimento datetime,
	Valor numeric(18,6),
	Acrescimneto numeric(18,6),
	Desconto numeric(18,6),

	primary key (Numero),
	foreign key (CodigoFornecedor) references Pessoas(Codigo)
);

-- ----------------------------------------------------------------------
insert into Pessoas values (1, 'José', 30800821000173),
					(2, 'João', 24992493000151),
					(3, 'Pelé', 82828257000109),
					(4, 'Chris', 63975353000127),
					(5, 'Manuel', 76486786000160);

insert into ContasAPagar values
(123, 1, convert(datetime,'03-03-2021 22:22:22 PM',103), convert(datetime,'04-03-2021 22:22:22 PM',103), 1.500, 30, 25),
(456, 1, convert(datetime,'03-04-2021 22:22:22 PM',103), convert(datetime,'04-08-2021 22:22:22 PM',103), 2.500, 500, 100),
(789, 5, convert(datetime,'05-03-2021 22:22:22 PM',103), convert(datetime,'04-05-2021 22:22:22 PM',103), 500, 10, 15);

insert into ContasPagas values
(159, 3, convert(datetime,'21-03-2021 22:22:22 PM',103), convert(datetime,'24-03-2021 22:22:22 PM',103), 1.500, 30, 25),
(753, 4, convert(datetime,'26-04-2021 22:22:22 PM',103), convert(datetime,'24-08-2021 22:22:22 PM',103), 2.500, 500, 100),
(486, 2, convert(datetime,'01-03-2021 22:22:22 PM',103), convert(datetime,'04-05-2021 22:22:22 PM',103), 500, 10, 15),
(426, 2, convert(datetime,'15-03-2021 22:22:22 PM',103), convert(datetime,'06-05-2021 22:22:22 PM',103), 500, 10, 15);

select * from Pessoas
select * from ContasAPagar
select * from ContasPagas

drop table Pessoas

-- drop database DesafioAPI
SELECT *
FROM (
		SELECT	ContasAPagar.CodigoFornecedor, 
				ContasAPagar.Numero, 
				Convert(varchar, ContasAPagar.DataVencimento, 103) as 'Data Vencimento/Data Pagamento',
				'Conta a Pagar' AS tipo, 
				format(ContasAPagar.Valor + ContasAPagar.Acrescimneto - ContasAPagar.Desconto, 'C', 'pt-br') as 'Valor Liquido'
		FROM ContasAPagar
		UNION ALL 
		SELECT	ContasPagas.CodigoFornecedor, 
				ContasPagas.Numero, 
				Convert(varchar, ContasPagas.DataPagamento, 103) as 'Data Vencimento/Data Pagamento', 
				'Conta Paga' AS tipo, 
				format(ContasPagas.Valor + ContasPagas.Acrescimneto - ContasPagas.Desconto, 'C', 'pt-br') as 'Valor Liquido'
		FROM ContasPagas
     ) res
INNER JOIN Pessoas ON Pessoas.Codigo = res.CodigoFornecedor