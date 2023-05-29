# microservices-dotnet6

Precisa rodar um docker container com o MySQL e criar no banco de dados as Databases "shopping_cart_api", "shopping_product_api" e "shopping_identity_server"
Comando para rodar o docker container "docker run -p 3306:3306 --name mysql -e MYSQL_ROOT_PASSWORD=rootpwd -d mysql"
Executar "dotnet ef database update" para aplicar as migrations nos projetos correspondentes

RabbitMQ docker container "docker run -d hostname shopping-rabbit --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management"