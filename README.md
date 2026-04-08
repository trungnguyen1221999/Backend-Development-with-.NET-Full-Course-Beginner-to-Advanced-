# Backend Development with .NET — Full Course (Beginner to Advanced)

Full course on YouTube: [Watch here](https://www.youtube.com/watch?v=Aa5ARJRWaEs&t=205s)

---

## Key Concepts

### Routing
- Use `{id:int}` or `{id:guid}` instead of plain `{id}` string — framework validates automatically, wrong format returns 404 immediately
- Guid is safer than int because it cannot be guessed
- Most common route constraints: `:int`, `:guid`, `:min(1)`

### HTTP Methods & Status Codes
| Method | Use case | Status code |
|--------|----------|-------------|
| GET | Retrieve data | 200 OK |
| POST | Create resource | 201 Created |
| PUT | Replace entire resource | 200 OK + data |
| PATCH | Partial update | 200 OK + data |
| DELETE | Remove resource | 204 No Content |

- PATCH uses `JsonPatchDocument<T>` — client only sends fields to change
- PUT and PATCH should return updated data in response body

### Middleware
- Registration order = execution order — wrong order causes bugs
- `await next.Invoke()` — waits for the entire downstream pipeline to finish before continuing
- `before` runs top to bottom, `after` runs bottom to top (onion model)
- Standard order: `UseAuthentication` → `UseAuthorization` → `MapControllers`

### Controller vs Minimal API
| | Minimal API | Controller |
|---|---|---|
| Use case | Small projects | Large projects, production |
| Syntax | `app.MapGet(...)` | `[HttpGet]` attribute |

- `[Route]` — required, without it endpoints return 404
- `[ApiController]` — optional but recommended
- `ActionResult<T>` — use for GET (returns data)
- `IActionResult` — use for DELETE (returns status code only)

### Unit Testing
- Framework: **xUnit** + **WebApplicationFactory**
- Pattern AAA: Arrange → Act → Assert
- Naming: `Method_Scenario_ExpectedResult`
- Test project sits alongside the app project, push to GitHub normally
- CI/CD runs tests on every push — fail means blocked from merging

### CI/CD
- File `.github/workflows/test.yml` — GitHub Actions runs automatically on push
- Flow: push → build → test → Pass ✅ / Fail ❌

---

## Projects

| Project | Description |
|---------|-------------|
| `WebApplication` | Minimal API CRUD with in-memory Blog list |
| `AdvanceRouter` | Route constraints: guid, int |
| `ConfiguringMiddleware` | Custom middleware pipeline, HttpLogging |
| `ProductCatalogApi` | Controller-based CRUD API with JsonPatch |

---

## Course Outline

### Module 1: Course Overview & .NET Fundamentals

| Time | Topic |
|------|-------|
| 00:00:07 | Introduction to Backend Development |
| 00:02:11 | Introduction to .NET & Architecture |
| 00:03:28 | Expert Insight: .NET at Microsoft (Real-world scaling) |
| 00:12:02 | History & Evolution of the .NET Platform |
| 00:18:46 | Cross-Platform Development |
| 00:24:45 | Essential Tools: VS Code, CLI, & NuGet |
| 00:37:12 | C# Syntax Basics & OOP Concepts |
| 00:50:10 | Coding Demo: Writing Basic C# Programs |
| 01:01:20 | Package Management with NuGet |
| 01:09:41 | Popular Libraries: Newtonsoft, Dapper, Serilog |
| 01:16:24 | Lab: Serializing & Deserializing JSON |

### Module 2: Building Web APIs with ASP.NET Core

| Time | Topic |
|------|-------|
| 01:24:10 | Introduction to Web APIs |
| 01:30:30 | Benefits of ASP.NET Core |
| 01:35:05 | Creating a Simple Web API Project |
| 01:41:40 | Implementing Basic API Endpoints (GET, POST, PUT, DELETE) |
| 02:03:25 | Building a CRUD API (In-Memory Data) |
| 02:21:42 | Understanding Routing & Route Templates |
| 02:48:02 | Configuring Middleware |
| 03:05:17 | Lab: Creating a Product Catalog API |
| 03:13:46 | Dependency Injection (DI) Explained |
| 03:18:40 | Implementing DI: Singleton, Scoped, Transient |
| 03:29:47 | Unit Testing with Mocking & DI |
| 03:39:59 | Error Handling Best Practices |
| 03:47:07 | Logging Best Practices & Providers |
| 03:52:42 | Lab: Error Handling & Serilog Integration |

### Module 3: Serialization & Data Handling

| Time | Topic |
|------|-------|
| 04:05:25 | Introduction to Serialization & Deserialization |
| 04:06:45 | Concepts: JSON vs XML vs Binary |
| 04:13:39 | How to Serialize Objects |
| 04:17:08 | Hands-on: Implementing Serialization |
| 04:41:17 | Deserializing Objects in .NET |
| 04:59:20 | Lab: Deserialization Demo |
| 05:07:40 | Performance Considerations & Optimization |
| 05:16:06 | Security Risks & Best Practices |
| 05:22:45 | Lab: Securing Serialization |

### Module 4: Middleware & OpenAPI (Swagger)

| Time | Topic |
|------|-------|
| 05:32:04 | Introduction to Middleware & OpenAPI |
| 05:33:31 | The HTTP Request Pipeline |
| 05:41:40 | Intro to OpenAPI & Swagger |
| 05:46:16 | Integrating Swagger UI |
| 06:13:26 | Generating API Clients (SDKs) with NSwag |
| 06:37:44 | Lab: Swagger Integration |
| 06:47:44 | Designing Middleware for Performance |
| 06:52:26 | Securing Middleware |
| 06:56:18 | Lab: Security Implementation |

### Module 5: AI-Assisted Development (Microsoft Copilot)

| Time | Topic |
|------|-------|
| 07:04:01 | Introduction to AI-Assisted Development |
| 07:10:25 | Generating API Code with Copilot |
| 07:22:09 | Improving & Refactoring Code with AI |
| 07:36:18 | Detecting & Fixing Bugs with Copilot |
| 07:47:18 | AI-Assisted Debugging Techniques |
| 08:06:23 | Implementing Middleware using Copilot |
| 08:22:42 | Final Project: Integrating Everything with AI Assistance |
| 08:35:10 | Course Conclusion |
