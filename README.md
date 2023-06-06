# O que é REST (Representational State Transfer)?

É um estilo de arquitetura de software utilizado para criação de serviços web.
Define um conjunto de princípios e restrições que permitem que o sistemas se comuniquem de forma padronizada e escalável.

Em uma arquitetura REST, os recursos são identificados por URLs e utiliza métodos padrão do protocolo HTTP, como GET, POST, PUT e DELETE, para manipula-los.

Princípios-chave do REST incluem:

1. Cliente-Servidor: As duas partes são separadas.

2. Comunicação Stateless: Cada solicitação de um cliente para o servidor contém todas as informações necessárias para entender e processar a requisição. 
O servidor não mantém nenhum estado da sessão do cliente entre as solicitações.

3. Cacheable: O cliente deve ser informado sobre as propriedades de cache de um recurso para que possa decidir quando deve ou não utilizar cache.

4. Interface Uniforme e Baseado em Recursos: Uma interface uniforme entre cliente e servidor onde cada recurso é identificado por uma URI e são a entidade central no REST, recursos são manipulados por sua representação, mensagens auto-descritivas e HATEOAS. 
Recusos podem ser objetos/coleções de dados ou serviços.

5. Sitemas em camadas: O REST suporta a arquitetura em camadas, onde os componentes podem estar distribuidos em diferentes camadas, isso permite uma maior escalabilidade, separação de preocupações e flexibilidade na implementação. Podendo suportar load balancer, proxies, firewalls, etc.

# O que são Microserviços?

Microserviços são um estilo arquitetural de design de software em que uma aplicação é dividida em pequenos serviços independentes e autônomos. Cada serviço é responsável por uma função específica e pode ser desenvolvido, implantado e dimensionado de forma independente dos outros serviços.

Em contraste com uma abordagem monolítica, em que todas as funcionalidades são agrupadas em um único código-base e implantadas juntas, os microserviços permitem que os diferentes componentes da aplicação sejam desenvolvidos e mantidos separadamente.

Cada microserviço é construído em torno de um contexto de negócio delimitado, o que significa que ele se concentra em uma tarefa específica, como gerenciamento de usuários, processamento de pagamentos ou envio de e-mails. Os serviços podem se comunicar entre si por meio de APIs (Interfaces de Programação de Aplicativos) e trocar dados usando protocolos como HTTP/REST, mensagens assíncronas ou outros mecanismos.

Benefícios de microserviços incluem:

1. Escalabilidade e Flexibilidade: Esse estilo arquitetural permite escalar e implantar cada serviço individualmente, o que possiblita ajustar a capacidade e o desempenho de cada parte da aplicação de forma independente.

2. Manutenção Simplificada: As equipes de desenvolvimento podem trabalhar de forma mais independente, atualizando, corrigindo bugs ou adicionando novos recursos em serviços específicos sem afetar o restante da aplicação.

3. Resiliência e Tolerância a Falhas: Se um microserviço falha, os outros podem continuar funcionando normalmente, garantindo que a aplicação como um todo permaneça operacional.

4. Tecnologias e linguagens diversas: Cada microserviço pode ser implementado utilizando a tecnologia ou linguagem mais adequada para sua tarefa específica, permitindo utilizar a melhor ferramenta para cada necessidade.

No entanto, usar microserviços apresenta desafios como a complexidade entre serviços, a necessidade de gerenciar a consistência de dados distribuídos e a sobrecarga adicional na infraestrutura de implantação.


# Como rodar o projeto?

Precisa rodar um docker container com o MySQL e criar no banco de dados as Databases "shopping_cart_api", "shopping_product_api" e "shopping_identity_server"
Comando para rodar o docker container "docker run -p 3306:3306 --name mysql -e MYSQL_ROOT_PASSWORD=rootpwd -d mysql"
Executar "dotnet ef database update" para aplicar as migrations nos projetos correspondentes

RabbitMQ docker container "docker run -d hostname shopping-rabbit --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management"