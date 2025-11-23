# Saucedemo E2E Tests

## Project Overview
This project contains end-to-end (E2E) tests for the [saucedemo.com](https://www.saucedemo.com/) application.  
It is implemented in C# using Playwright and NUnit, with Allure for reporting.

The project is organized as follows:

- **Pages**:  
  Contains page objects with locators and methods representing actions that can be performed on the site (e.g., login, add items to cart, checkout).

- **TestData**:  
  Contains static data used for assertions, such as product names, descriptions, prices, and images.

- **Models**:  
  Contains data models used across the project (e.g., `Product`).

- **Tests**:  
  Contains test scenarios organized by functionality, such as `LoginTests`, `ItemsTests`, `CartTests`, and `OrderTests`.
---

## Technical Decisions
### Test Framework
NUnit was chosen for its simplicity, strong integration with C#, and compatibility with Visual Studio and CI/CD pipelines. Its rich feature set (attributes, assertions, test lifecycle management) makes it ideal for structuring automated tests.

### E2E Automation Tool
Playwright was selected to perform end-to-end browser automation. Key advantages include:
  - Cross-browser support (Chromium, Firefox, WebKit) within the same test.
  - Built-in support for screenshots and video recording.
  - Easy handling of asynchronous actions in modern web apps.
  - Seamless integration with C# projects.

### Test Reporting
Allure was chosen for generating detailed and interactive reports. Allure allows:
   - Step-by-step tracking of test execution.
   - Attaching screenshots, videos, and other artifacts.
   - Parameterized test reporting for better clarity.
   - Clear visualization of test failures and execution trends over time.

### Project Structure
The project is organized according to Page Object Model (POM) principles:
  - Pages: Contains locators and methods representing actions on individual pages.
  - Tests: Contains test scenarios grouped by feature (LoginTests, ItemsTests, CartTests, OrderTests).
  - TestData: Holds static data used in assertions to avoid hardcoding values in tests.
  - Models: Contains reusable data models (e.g., Product) for passing structured data between layers.
   
### Test Data Management
Centralized in the ItemsData class to ensure:
  - Reusability across multiple tests.
  - Easy maintenance when product details change.
  - Improved readability and clarity in test cases.

### Performance & Reliability Considerations
  - Asynchronous programming (async/await) is used consistently to ensure tests are reliable and non-blocking.
  - Playwright’s WaitForSelector and robust locators prevent flaky tests due to dynamic page content.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 (or later)
- NUnit
- Playwright
- Allure Report

### Installing Dependencies
```powershell
dotnet restore
dotnet tool install --global Microsoft.Playwright.CLI
playwright install
```

### Running Tests

1. Using Visual Studio Test Explorer
  - Open the solution in Visual Studio.
  - Navigate to Test Explorer.
  - Run all tests or select individual tests to run.

2. Using Command Line (PowerShell / Terminal)
Run all tests:
```powershell
dotnet test
```
Run a single test:
```powershell
dotnet test --filter "TestName"
```

### Generating Allure Reports

After running tests, you can generate and view the Allure report.  
**Note:** To use Allure commands (`allure serve`, `allure generate`, `allure open`), Allure CLI must be installed on your local machine.
To install Allure, follow the instructions on the official website: [https://allurereport.org/docs/install/](https://allurereport.org/docs/install/)

To serve the report interactively:

```powershell
allure serve bin/Debug/net8.0/allure-results
```
This command will:
  - Start a local server
  - Open an interactive Allure report in your default browser
  - Display test results, steps, attachments (screenshots, videos), and parameters

Alternatively, you can generate a static report and open it manually:
```powershell
allure generate bin/Debug/net8.0/allure-results -o allure-report
allure open allure-report
```

Example report:

<img width="2556" height="1274" alt="image" src="https://github.com/user-attachments/assets/962a9310-e9e4-428a-80b7-c6150d5ad6cb" />

