# 📘 JIRA\[015\] - \[FINAL\] - UI Automated Testing with SELENIUM C# (Speclfow)

This project demonstrates a **Behavior-Driven Development (BDD)** test automation framework using **Reqnroll** (SpecFlow-compatible), **Selenium WebDriver**, and **ExtentReports**. It’s structured cleanly with separation of concerns, applying Page Object Model (POM) and layered architecture.

---

## 📁 Folder & File Structure Explanation

```
.
├── Core
│   ├── Drivers
│   │   └── BrowserFactory.cs             # Manages browser instances
│   ├── Element
│   │   ├── WebObject.cs                  # Wrapper for IWebElement abstraction
│   │   └── WebObjectExtension.cs         # Extension methods for interacting with WebObject
│   ├── Reports
│   │   └── ExtentReportHelper.cs         # Sets up and manages ExtentReports
│   ├── Utils
│   │   ├── ConfigurationUtils.cs         # Handles appsettings.json reading
│   │   ├── DriverUtils.cs                # General-purpose driver utilities
│   │   ├── FilePathExtension.cs          # Path utilities for test files
│   │   └── JsonFileUtils.cs              # Utilities for JSON operations
│   └── Core.csproj                       # Project definition for Core utilities
│
├── Service
│   ├── ApiServices
│   │   └── BookApiService.cs             # Service layer to call book-related apis
│   ├── Const
│   │   |── ApiEndpointConst.cs           # Api endpoints constants
│   │   |── ContextKeyConst.cs            # Scenario Context keys constants
│   │   └── FileConst.cs                  # File-related constants
│   ├── Models
│   │   ├── Data                          # Data model for test data
│   │   |   ├── RegisterData.cs        
│   │   |   └── RegisterData.cs        
│   │   ├── DTOs                          # Data model for common entities
│   │   |   ├── BookDto.cs       
│   │   |   ├── DeleteBookLaterDto.cs       
│   │   |   ├── IsbnDto.cs      
│   │   |   └── Login.cs        
│   │   ├── Response                      # Data model for api response body
│   │   |   ├── IsbnDto.cs      
│   │   |   └── Login.cs        
│   │   └── Request                       # Data model for api request body
│   │       └── AddBookRequest.cs        
│   └── Service.csproj                    # Project definition for Service layer
│
├── Test
│   ├── Configurations
│   │   ├── appsettings.json              # General configuration
│   │   └── ExtentReportConfig.xml        # Extent report XML (customizable)
│   ├── Const
│   │   ├── PageUrlConst.cs               # Pages' url constants
│   │   └── FilePathConst.cs              # Files path constants
│   ├── Extensions
│   │   ├── ReportContextExtension.cs     # Extent report helper extension
│   │   └── WebComponentExtensions.cs     # Custom WebComponent helpers
│   ├── PageObjects                       # Store POM pages
│   │   ├── LoginPage.cs           
│   │   ├── RegisterPage.cs        
│   │   ├── BookStorePage.cs       
│   │   └── ProfilePage.cs         
│   ├── WebComponents                     # Custom control component
│   │   |── CalendarWebObject.cs          
│   │   |── DeleteBookGridComponent.cs          
│   │   |── RegisterSuccessPopupComponent.cs          
│   │   └── SearchBookGridComponent.cs          
│   ├── Features                          
│   │   ├── DeleteBook.feature            # BDD scenario for delete book from collection
│   │   ├── Login.feature                 # BDD scenario for login
│   │   └── SearchBook.feature            # BDD scenario for book search
│   ├── StepDefinitions
│   │   ├── DeleteBookStepDefinitions.cs  # BDD step definition for delete book from collection
│   │   ├── LoginStepDefinitions.cs       # BDD step definition for login
│   │   ├── SearchBookStepDefinitions.cs  # BDD step definition for book search
│   │   └── Hooks.cs                      # Reqnroll hooks: before/after test/ scenarios/ steps
│   └── Test.csproj                       # Project file for test suite
│
└── TranKhaiMinhKhoi-PracticeCucumber.sln # Solution file
```

---

## ✅ Work Completed

### 🔧 Implemented Features:
- Automated scenarios for login, search, and project creation
- Page Object Model (POM) pattern applied
- Data binding from Gherkin DataTables
- Centralized reusable components
- Parallel Execution on Feature level

---

## ⚠️ Known Issues & Workarounds

### 1.Error: Extent Report XML config deserialization failed
  
`<extentreports xmlns=''> was not expected.`  
**Cause**: Mismatched XML schema or version.

**Resolution**:  
Used the [official sample XML config from ExtentReports GitHub repo](https://github.com/extent-framework/extentreports-csharp/blob/master/config/spark-config.xml) to resolve schema mismatch and enable config loading.

---

### 2.Issue:  DataTable newline (`\n`) does not rendering properly in the report
 
DataTable cells containing `\n` do not display new lines in the report/table view.

**Workaround**:  
Split multi-line values manually in code when iterating rows

---

### 3.Issue: Incorrect Display of Step Text in Report
Step text sometimes displays with unexpected casing or formatting.

**Status**:  
Still unresolved. Possibly caused by report rendering logic or test runner quirks.

---

### 4.Issue:  Book data for Delete case may be altered/deleted
 
Book named  “Git Pocket Guide” may change isbn due to test/dev activity, change/migration to a new db, enviroment or other unforseen causes

**Workaround**:  
Asumming a book named  “Git Pocket Guide” will always be seeded with different isbn

Use get all books api to get all books then search for that book's isbn

---

## 🚀 How to Run

### 1. Clone the repository (if you are authorized)
```bash
git clone https://gitlab.com/anhtien1306/rookies-batch8.git
git checkout TranKhaiMinhKhoi-practice-ui-cucumber
cd <your-folder>
```

### 2. Install dependencies & build
```bash
dotnet restore
dotnet build
```

### 3. Execute Tests
```bash
dotnet test
```

---

## 🔖 Tag Filtering

Run only tests with specific tag:
```bash
dotnet test --filter "Category=register"
dotnet test --filter "Category=search"
dotnet test --filter "Category=delete"
```

---

## 🧵 Parallel Execution

Enable **feature-level parallelization** in `Hooks.cs`:
```csharp
[assembly: Parallelizable(ParallelScope.Fixtures)]
```

---

## 🔗 Useful References

- 🔹 [Reqnroll Docs](https://reqnroll.net/)
- 🔹 [ExtentReports GitHub](https://github.com/extent-framework/extentreports-csharp)
- 🔹 [Selenium WebDriver C# Guide](https://www.selenium.dev/documentation/webdriver/)
- 🔹 [FluentAssertions](https://fluentassertions.com/)
- 🔹 [Run tests by tag](https://reqnroll.net/docs/v3/gherkin/filtering/)
- 🔹 [Parallel test execution](https://reqnroll.net/docs/v3/gherkin/parallel-execution/)

---

## 🙋 Author

**Tran Khai Minh Khoi**  
This repo is a practice BDD web automation project combining ReqnRoll, Selenium and ExtentReporter.

---

Thank you for your effort, dedication and feedback!
