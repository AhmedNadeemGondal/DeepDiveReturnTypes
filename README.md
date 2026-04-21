# DeepDiveReturnTypes

## Purpose
This repository explores the various **Return Types** available in **ASP.NET Core Web APIs**. It examines the trade-offs between different return patterns, focusing on how they impact API documentation (Swagger/OpenAPI), type safety, and the flexibility of HTTP response codes.

## Core Return Patterns

### 1. Specific Types
Demonstrates returning primitive or complex types (e.g., `Employee` or `IEnumerable<Employee>`).
* **Pros:** Simple, clear intent.
* **Cons:** Limited to a single HTTP status code ($200$ OK). Cannot easily return $404$ Not Found or $400$ Bad Request without throwing exceptions.

### 2. IActionResult
Explores the use of the `IActionResult` interface to return various `ActionResult` implementations.
* **Flexibility:** Allows returning `Ok()`, `NotFound()`, `BadRequest()`, or `CreatedAtAction()`.
* **Drawback:** Lacks type safety for the response body, requiring `[ProducesResponseType]` attributes for proper Swagger documentation.

### 3. ActionResult<T>
Showcases the hybrid approach that combines type safety with HTTP flexibility.
* **Best Practice:** Provides the benefits of `IActionResult` while explicitly declaring the return type, allowing the framework to automatically infer the response schema for documentation.

### 4. Results & TypedResults (Minimal APIs)
Focuses on the modern static `Results` and `TypedResults` classes used in Minimal APIs.
* **TypedResults:** Highlighted for its ability to facilitate unit testing by maintaining the underlying type of the result without needing to cast from an abstract interface.



## Implementation Examples
The project includes comparative implementations of a single endpoint using different return strategies to highlight the differences in:
* **Unit Testing:** How different types affect the ease of asserting response content.
* **API Documentation:** How return types influence the generated `swagger.json`.
* **Pipeline Performance:** The overhead (if any) of different wrapping mechanisms.

### Credits
Credit to **Frank Liu**. Check out his [video series](https://www.youtube.com/watch?v=F4dDe0SLjJM&list=PLgRlicSxjeMOXiYY7deqzO5qKdkg9wrqM&index=1&pp=iAQB) for the original walkthrough.