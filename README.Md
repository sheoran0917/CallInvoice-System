This project exposes a GET endpoint at api/price/callprices (https://localhost:7290/api/price/callprices) that retrieves the price of each call.

To facilitate data retrieval, three distinct services were developed for clients, experts, and rates. While direct access to the data could have been implemented, I opted to keep the services modularized for improved maintainability. All services have been scoped to ensure appropriate lifetime management.

A generic function was implemented to retrieve data based on the specified type. This function allows for flexible and reusable data retrieval operations.

Logging functionality is achieved using the ILogging interface. By default, logs are written to the console, but the logging configuration can be customized according to specific requirements.

For testing purposes, the project employs the xUnit testing framework. Additionally, the Moq library is utilized to mock the behavior of services, enabling controlled and predictable test scenarios. Separate test cases have been developed for both controllers and business logic. Controller unit tests focus on validating API path functionality, while business logic tests ensure the correctness of the implemented logic.