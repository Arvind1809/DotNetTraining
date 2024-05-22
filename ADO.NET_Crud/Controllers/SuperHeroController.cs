using ADO.NET_Crud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ADO.NET_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly IConfiguration _config;
        public SuperHeroController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHero()
        {
            List<SuperHero> superheroes = new List<SuperHero>();

            try
            {
                using (SqlConnection con = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand cm = new SqlCommand("Select * from superheroes", con);
                    await con.OpenAsync();
                    SqlDataReader sdr = await cm.ExecuteReaderAsync();

                    while (await sdr.ReadAsync())
                    {
                        superheroes.Add(new SuperHero
                        {
                            Id = sdr.GetInt32(sdr.GetOrdinal("id")),
                            FirstName = sdr.GetString(sdr.GetOrdinal("firstname")),
                            LastName = sdr.GetString(sdr.GetOrdinal("lastname")),
                            PhoneNumber = sdr.GetInt64(sdr.GetOrdinal("phonenumber"))
                        });
                    }
                }

                return Ok(superheroes);
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddSuperHero([FromBody] SuperHero newHero)
        {
            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO superheroes (firstname, lastname, phonenumber) VALUES (@FirstName, @LastName, @PhoneNumber)", con);
                cmd.Parameters.AddWithValue("@FirstName", newHero.FirstName);
                cmd.Parameters.AddWithValue("@LastName", newHero.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", newHero.PhoneNumber);

                try
                {
                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return Ok();
                }
                catch (Exception e)
                {
                    Console.WriteLine("OOPs, something went wrong.\n" + e);
                    return StatusCode(500, "Internal server error");
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSuperHero(int id, [FromBody] SuperHero updatedHero)
        {
            if (id != updatedHero.Id)
            {
                return BadRequest("ID mismatch");
            }

            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                string query = "UPDATE superheroes SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", updatedHero.Id);
                cmd.Parameters.AddWithValue("@FirstName", updatedHero.FirstName);
                cmd.Parameters.AddWithValue("@LastName", updatedHero.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", (object)updatedHero.PhoneNumber ?? DBNull.Value);

                try
                {
                    await con.OpenAsync();
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("SuperHero not found");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("OOPs, something went wrong.\n" + e);
                    return StatusCode(500, "Internal server error");
                }
            }
        }


    }
}
