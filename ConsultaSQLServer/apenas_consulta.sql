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