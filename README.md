# Коротко о приложении
Прототип-симулятор маркетплейса ит-решений имеет следующие сервисы:
1. Сервис авторизации(включая внешнюю) на основе IdentityServer
2. Сервис решений реализующий логику работы с моделями программных решений(CRUD, фильтрация, система отзывов и тарифных планов)
3. Сервис биллинга, который отвечает за обработку заказов обычных пользователей (пополнение/обновление баланса пользователя, покупка решения и получение 
доступа к облаку(симуляция) )
4. Сервис тематический подборок от владельца маркетплейса.

# REST

Приложение на основе .NET Core реализует RESTFul апи для всех своих сервисов, что значительно упрощает интеграцию. К программному интерфейсу
прилагается документация на Swagger.

# Клиентская часть приложения

Для реализации клиентской части использовался MVC для экономии времени. В дальнейшем приложения будет SPA на Angular.

# Масштабируемость для деплоймента решений пользователей

Чтобы иметь возможность реализовать автоматическое развертывание продуктов пользователей мы используем 2 пути: 
1. нашу собственную спецификацию по разработке собственного провайдера для доступа к своему облаку(последующий редирект на сервисы владельца решения)
2. Docker контейнер с решением для дальнейшего развертывания в нашем облаке

# Структура директорий

Приложение разбито на 2 солюшна (Маркетплейс и IdentityServer)

# Маркетплейс

1. Уровень доступа к базе данных(DataAccessLayer): code first БД на EntityFramework CORE.
2. Уровень бизнес-логиги(BusinnessLogicLayer): основные сервисы для обработки данных
3. API для доступа к сервисам BLL
4. UI на MVC для взаимодействия с API приложения.

# IdentityServer

Обычное MVC приложение, которое содержит в себе IdentityServer, контекст БД и вспомогательное API для клиентской части
