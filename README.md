# Car Rental (API Rest)
## Descripción
API Rest que permite la gestión del alquiler de vehículos. Entre sus principales funciones se encuentran:

- Funciones CRUD para la gestión de tipos de vehículos y precios, vehículos, clientes y alquileres.
- Gestión de los puntos de fidelidad de los clientes.
- Gestión del inventario de vehículos.
- Cálculo del precio de los alquileres, devolución del vehículo y sobrecostes.

## Desarrollo (v1.0)
El desarrollo se ha centrado en crear un esqueleto funcional de la aplicación, que cubra las necesidades esenciales, permitiendo añadir de forma sencilla cambios a futuro.

## Arquitectura y tecnología
La solución hace uso de la arquitectura DDD (Domain Driven Design) + CQRS. El framework utilizado es .NET 6.0. Se mantiene la configuración por defecto de Swagger para generar de forma automática la documentación de los endpoints y esquemas.

- **Domain:** Recoge las entidades necesarias para el funcionamiento de la aplicación.
- **Infrastructure:** Configuración de la capa de datos (EF Core + MSSQLLocalDB) y de los repositorios.
- **Application:** CQRS + Servicios (gestión y cálculos de la aplicación)
- **Presentation:** Api Rest



## Posibles mejoras en el desarrollo
Se proponen las siguientes mejoras en el desarrollo utilizando el esqueleto proporcionado como base:

- Creación de pruebas unitarias
- Modificación del desarrollo para ser utilizado como parte de un sistema de microservicios que gestionen una conjunto más complejo.
- Refactorización de los repositorios a una nueva librería para un mejor mantinimiento del código (CarRental.Infrastructure.Repositories).

## Postman Collection
[CarRental.API.postman_collection.zip](https://github.com/ncobianm/CarRental/files/10715630/CarRental.API.postman_collection.zip)
