using System.Data.SqlClient;
using System.Collections.Generic;
using APBD03.Model;
using APBD03.Repository;

public class AnimalsRepository : IAnimalsRepository
{
    private readonly IConfiguration _configuration;

    public AnimalsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        List<Animal> animals = new List<Animal>();
        using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        con.Open();
        string query = $"SELECT * FROM Animal ORDER BY {orderBy} ASC";

        using var cmd = new SqlCommand(query, con);
        using SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            animals.Add(new Animal
            {
                IdAnimal = dr.GetInt32(dr.GetOrdinal("IdAnimal")),
                Name = dr.GetString(dr.GetOrdinal("Name")),
                Description = dr.GetString(dr.GetOrdinal("Description")),
                Category = dr.GetString(dr.GetOrdinal("Category")),
                Area = dr.GetString(dr.GetOrdinal("Area"))
            });
        }
        return animals;
    }

    public Animal GetAnimalById(int id)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        con.Open();
        string query = $"SELECT * FROM Animal WHERE IdAnimal = @IdAnimal";

        using var cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@IdAnimal", id);
        using SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            return new Animal
            {
                IdAnimal = dr.GetInt32(dr.GetOrdinal("IdAnimal")),
                Name = dr.GetString(dr.GetOrdinal("Name")),
                Description = dr.GetString(dr.GetOrdinal("Description")),
                Category = dr.GetString(dr.GetOrdinal("Category")),
                Area = dr.GetString(dr.GetOrdinal("Area"))
            };
        }
        return null;
    }

    public int AddAnimal(Animal animal)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        con.Open();
        string query = "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area)";

        using var cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);
        return cmd.ExecuteNonQuery();
    }

    public int UpdateAnimal(int id, Animal animal)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        con.Open();
        string query = "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal";

        using var cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@IdAnimal", id);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);
        return cmd.ExecuteNonQuery();
    }

    public int DeleteAnimal(int id)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        con.Open();
        string query = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";

        using var cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@IdAnimal", id);
        return cmd.ExecuteNonQuery();
    }
}
