using System;
using BaltaDataAcess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BaltaDataAcess
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString =
            "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;" +
            "Encrypt=True;TrustServerCertificate=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                // CreateCategory(connection);
                CreateManyCategory(connection);
                // UpdateCategory(connection);
                // ListCategories(connection);
                // DeleteCategory(connection);
                ListCategories(connection);
                // GetCategory(connection);
            }
        }
        static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }
        static void CreateCategory(SqlConnection connection)
        {
            var category = new Category();

            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviçoes AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertSql = @"INSERT INTO 
                    [Category] 
                VALUES(
                    @Id, 
                    @Title,
                    @Url,
                    @Summary, 
                    @Order, 
                    @Description, 
                    @Featured)";

            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured,
            });
            Console.WriteLine($"{rows} Linhas inseridas");
        }
        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid("09ce0b7b-cfca-497b-92c0-3290ad9d5142"),
                title = "Frontend 2026"
            });

            Console.WriteLine($"{rows} Registros Atualizados");
        }
        static void DeleteCategory(SqlConnection connection)
        {
            var deleteQuery = "DELETE [Category] WHERE [Id]=@id";
            var rows = connection.Execute(deleteQuery, new
            {
                id = new Guid("ea8059a2-e679-4e74-99b5-e4f0b310fe6f"),
            });

            Console.WriteLine($"{rows} registros excluídos");
        }
        static void CreateManyCategory(SqlConnection connection)
        {
            var category = new Category();

            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviçoes AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var category2 = new Category();

            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria Nova";
            category2.Url = "categoria-nova";
            category2.Description = "Categoria nova";
            category2.Order = 9;
            category2.Summary = "Categoria";
            category2.Featured = true;

            var insertSql = @"INSERT INTO 
                    [Category] 
                VALUES(
                    @Id, 
                    @Title,
                    @Url,
                    @Summary, 
                    @Order, 
                    @Description, 
                    @Featured)";

            var rows = connection.Execute(insertSql, new[]
            {
                new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured,
                },
                new
                {
                    category2.Id,
                    category2.Title,
                    category2.Url,
                    category2.Summary,
                    category2.Order,
                    category2.Description,
                    category2.Featured,
                }
            });
            Console.WriteLine($"{rows} Linhas inseridas");
        }
    }
}