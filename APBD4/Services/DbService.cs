using APBD4.Models;
using System.Data.SqlClient;

namespace APBD4.Services
{
    public class DbService : IDbService
    {
        private IConfiguration _configuration;

        public DbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Animal AddAnimal(Animal animal)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                con.Open();
                com.CommandText = "INSERT INTO ANIMAL(name, description, category, area) VALUES (@name, @description, @category, @area)";
                com.Parameters.AddWithValue("@name", animal.Name.ToString());
                com.Parameters.AddWithValue("@description", animal.Description.ToString());
                com.Parameters.AddWithValue("@category", animal.Category.ToString());
                com.Parameters.AddWithValue("@area", animal.Area.ToString());
                SqlDataReader dr = com.ExecuteReader();
            }
            return animal;
        }

        public bool AnimalExists(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                con.Open();
                com.CommandText = "SELECT COUNT(IdAnimal) FROM ANIMAL WHERE idAnimal =@idAnimal";
                com.Parameters.AddWithValue("@idAnimal", id);
                return (int)com.ExecuteScalar() > 0;
            }
        }

        public void DeleteAnimal(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                con.Open();
                com.CommandText = "DELETE FROM ANIMAL WHERE idAnimal =@idAnimal";
                com.Parameters.AddWithValue("@idAnimal", id);
                SqlDataReader dr = com.ExecuteReader();
            }
        }

        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            List<Animal> animals = new List<Animal>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                con.Open();
                if (string.IsNullOrEmpty(orderBy))
                {
                    com.CommandText = "Select * from Animal ORDER BY NAME ASC";
                }
                else if (orderBy == "name" || orderBy == "description" || orderBy == "category" || orderBy == "area")
                {
                    com.CommandText = "Select * from Animal ORDER BY " + orderBy;
                }
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    Animal animal = new Animal
                    {
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Category = dr["Category"].ToString(),
                        Area = dr["Area"].ToString()
                    };
                    animals.Add(animal);
                }
            }
            return animals;
        }

        public Animal UpdateAnimal(int id, Animal animal)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                con.Open();
                com.CommandText = "UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE idAnimal =@idAnimal";
                com.Parameters.AddWithValue("@name", animal.Name.ToString());
                com.Parameters.AddWithValue("@description", animal.Description.ToString());
                com.Parameters.AddWithValue("@category", animal.Category.ToString());
                com.Parameters.AddWithValue("@area", animal.Area.ToString());
                com.Parameters.AddWithValue("@idAnimal", id);
                SqlDataReader dr = com.ExecuteReader();
            }
            return animal;
        }
    }
}
