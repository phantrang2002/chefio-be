# 🚀 WorkNest  

An online job portal for recruitment and job searching.  

## 📌 Table of Contents  
- [👥 Authors](#-authors)  
- [🛠 Tech Stack](#-tech-stack)  
- [💻 Run Locally](#-run-locally)   
- [✨ Migration](#-migration)   


## 👥 Authors  
- [@trangphan](https://www.linkedin.com/in/trang-phan-35b823156/)  

## 🛠 Tech Stack   
**⚙️ :** ASP.NET Core Web API 8, MySQL, Entity Framework Core, MySQL, JWT Authentication, Google Firebase Storage.

## 💻 Run Locally  

Clone the project:  

```bash
git clone https://github.com/phantrang2002/chefio-be.git
```  

```bash
cd api
```

Add the appropriate `appsettings.json` file

Run the Backend Server:  

```bash
dotnet build
dotnet watch run
``` 

## ✨ Migration
Process for Updating Tables/Columns

1. Update the entity
- Modify the entity classes in `src/domain/entities/` (e.g., add/modify/remove a property in Employee.cs).

2. Update the configuration if needed
- Edit the mapping configuration file (e.g., `EmployeeConfiguration.cs`).
- If you are creating a new table, you can ask Copilot to generate this configuration file from the new entity.

3. Generate a new migration
- Open the terminal at the root of the repo and run: Replace <MigrationName> with your chosen migration name (e.g., `AddEmployeePhone`).
```bash
cd src/infrastructure
dotnet ef migrations add <MigrationName> --startup-project ../api
```

4. Review the migration file
- The new migration will be created in `src/infrastructure/Migrations/`.
- Check the migration file to ensure it matches the intended changes.
- Remove any lines like: `MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);`

5. Update the database
- Apply the migration to the database (cd `src/infrastructure`):
```bash
dotnet ef database update --startup-project ../api
```

## 📢 Note  
🚧 This project is still under development and updates are ongoing. Stay tuned for more features and improvements! 🚀
