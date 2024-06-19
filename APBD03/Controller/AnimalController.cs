using APBD03.Model;
using APBD03.Repository;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AnimalController : ControllerBase
{
    private readonly IAnimalsRepository _repository;

    public AnimalController(IAnimalsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAllAnimals([FromQuery] string orderBy = "name")
    {
        var animal = _repository.GetAllAnimals(orderBy);
        return Ok(animal);
    }

    [HttpGet("{id}")]
    public IActionResult GetAnimalById(int id)
    {
        var animal = _repository.GetAnimalById(id);
        if (animal == null) return NotFound();
        return Ok(animal);
    }

    [HttpPost]
    public IActionResult AddAnimal([FromBody] Animal animal)
    {
        int result = _repository.AddAnimal(animal);
        if (result == 0) return BadRequest();
        return CreatedAtAction(nameof(GetAnimalById), new { id = animal.IdAnimal }, animal);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAnimal(int id, [FromBody] Animal animal)
    {
        int result = _repository.UpdateAnimal(id, animal);
        if (result == 0) return BadRequest();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAnimal(int id)
    {
        int result = _repository.DeleteAnimal(id);
        if (result == 0) return NotFound();
        return NoContent();
    }
}