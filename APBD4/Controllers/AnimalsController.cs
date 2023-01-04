using APBD4.Models;
using APBD4.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD4.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {
        private IDbService dbService;

        public AnimalsController(IDbService dbService)
        {
            this.dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetAnimals([FromQuery] string orderBy)
        {
            orderBy = orderBy.ToLower();
            if (string.IsNullOrEmpty(orderBy) || !orderBy.Equals("name") || !orderBy.Equals("description") || !orderBy.Equals("category") || !orderBy.Equals("area"))
            {
                return BadRequest("You can order only by name, description, category or area");
            }
            return Ok(dbService.GetAnimals(orderBy));
        }

        [HttpPost]
        public IActionResult AddAnimal([FromBody] Animal animal)
        {
            try
            {
                dbService.AddAnimal(animal);
            } catch (Exception)
            {
                return BadRequest("You used wrong data to create animal");
            }
            return Ok("Animal added");
        }

        [HttpPut("{idAnimal}")]
        public IActionResult UpdateAnimal([FromRoute] int id, [FromBody] Animal animal)
        {
            if (dbService.AnimalExists(id))
            {
                return Ok(dbService.UpdateAnimal(id, animal));
            } else
            {
                return NotFound("Animal with id " + id + " not found");
            }
        }

        [HttpDelete("{idAnimal}")]
        public IActionResult DeleteAnimal([FromRoute] int id)
        {
            if(dbService.AnimalExists(id))
            {
                dbService.DeleteAnimal(id);
                return Ok("Animal deleted");
            } else
            {
                return NotFound("Animal with id " + id + " not found");
            }
        }
    }
}