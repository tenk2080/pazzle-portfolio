using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kod_API_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        [HttpGet()]
        public IActionResult AnswersAllStudents() => Ok("Список  вопросов");
        [HttpGet("{id}")]
        public IActionResult GetTestById(int id)
        {
            if (id == 1) return Ok("Вопрос 1");
            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateAnswers()
        {
            // Имитация: создали студента с id=1
            return Created("/api/students/1", "Создан вопрос с id=1"); // 201 + Location
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateAnswers(int id)
        {
            if (id <= 0) return BadRequest("Некорректный id вопроса"); // 400
                                                               // Имитация: если не существует
            if (id != 1) return NotFound(); // 404
                                            // Имитация: обновили
            return NoContent(); // 204
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteAnswers(int id)
        {
            if (id <= 0) return BadRequest("Некорректный id вопроса"); // 400
            if (id != 1) return NotFound(); // 404
            return NoContent(); // 204




        }

   

    }
}
