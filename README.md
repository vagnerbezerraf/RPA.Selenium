# RPA.Selenium

O primeiro passo foi definir a camada de acesso a dados, como o objetivo era ter uma arquitetura de demonstração, optei pelo micro ORM Dapper para demonstrar o conhecimento em SQL.
Logo em seguida a escolha do mecanismo de acesso a dados se fez bem particular, pois como se tratava de um projeto para ser rodado de forma fácil, decidi pelo uso do SQLite para simplificar o startup da aplicação, usando um drive em memória para armazenar os dados de teste.

Seguindo a orientação, foi criada então o repositório de dados que iria então compor a primeira parte da camada de Infraestrutura, com:


SQLite -> Dapper -> Repository

Selenium -> CursoService -> Repository

E para completar a segunda e ultima parte da camada de Infraestrutura, foi então criado o encapsulamento do Webdriver do Selenium, com o objetivo de protefer e tratar as exceções comuns da feramenta durante a captura de dados. 

Desa forma quando um campo não é achado, conseguimos seguir o processamento se subida de exceções que interrompam a captura da página que já foi rendenrizada.

E por ultima, representando uma integração dentro do nosso modelo DDD, temos o Crawler reponsável por manter os contratos que conhecem o mundo exterior da aplicação, neste caso o site a ser capturado.

Na nossa camada de domínio foi ultilizada uma classe POCO para simbolizar o curso a ser salvo na nossa estrutura de dados e o desenho da interface de repositório necessário para tratar o domínio.


Na camada de Aplicação, foi criado o serviço responsável por carregar a regra de negócio soficiente para operacionar no robo de captura, tenho então os métodos simples relacionados a entidade de cursos, e e o executos capaz e trabalhar com a estratégia de logs, iniciar o Webdriver no momento certo, tratar os dados de retorno e salvá-los através dos repositório, e listar os cursos.

Como projeto de execução foi feito um Console, que faz a injeção das depedências do nosso projeto, e dispara o serviço para buscar os cursos.
