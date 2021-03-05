# Desafio - Criar uma API REST com C# 
# Autor: Lucas Macedo da Silva

O projeto foi desenvolvido no Visual Studio Community 2019.
Para os testes foi utilizado o Postman

Obs.: Optou-se por passar os dados via parâmetros, junto com a URL, porém o processo poderia ser feito de outra forma.
Obs.: A pasta packages foi deletada para diminuir o tamnho do arquivo

As rotas criadas são:

1. Rota para autenticação: /Token
	O usuário é "lucas" e a senha é "123456".
	Note que, esta parte requer melhorias para facilitar o acesso. Para os testes, era solicitado o token, depois o token era salvo na aba Authorization do Postman.
	A melhoria seria realizar esse processo automaticamente.
	Para a autorização foi utilizada a anotação (annotation) [Authorize]

2. Rota para consulta de todas as pessoas cadastradas: /Consultar - Método GET

3. Rota de consulta de uma pessoa pelo código: /ConsultarCodigo - Método GET
	Espera-se que o código seja passado como parâmetro na URL

4. Rota para consulta de pessoas pelo estado: /ConsultarEstado - Método GET
	Espera-se que o estado seja pessado como parâmetro na URL

5. Rota para salvar uma pessoa: /SalvarPessoa - Método POST
	Espera-se que os dados sejam passados como parâmetro na URL
	Os dados foram:
		- Se o código era maior que zero;
		- Se o nome não estava vazio;
		- Se o CPF era válido;
		- Se o UF não estava vazio. Outra validação não implementada foi a de verificar se o UF era válido, comparando-o com uma lista de UFs válidos
	

6. Rota para atualizar os dados de uma pessoa: /AtualizarPessoa - Método PATCH
	Foi considerado que a pessoa só pode atulizar seu nome e/ou estado.
	Espera-se que os dados sejam passados como parâmetro na URL

7. Rota para excluir uma pessoa: /DeletarPessoa - Método DELETE
	Foi considerado que a pessoa é excluída a partir de seu código
	Espera-se que o código seja passado como parâmetro na URL