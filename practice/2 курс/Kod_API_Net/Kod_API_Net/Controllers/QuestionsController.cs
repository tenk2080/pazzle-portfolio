using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kod_API_Net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllQuestions() => Ok("Список  вопросов");

        [HttpGet("{id}")]
        public IActionResult GetQuestionsById(int id)
        {
            if (id == 1) return Ok("Вопрос 1");
            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateTest() => Created("/api/questions/1", "Создан  вопрос с ID=1");

        [HttpPut("{id}")]
        public IActionResult UpdateQuestions(int id) => NoContent();

        [HttpDelete("{id}")]
        public IActionResult DeleteQuestions(int id) => NoContent();
    }
}
