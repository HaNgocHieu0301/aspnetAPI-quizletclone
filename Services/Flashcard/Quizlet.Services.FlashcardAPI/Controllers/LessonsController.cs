using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository;
using Repository.IRepository;

namespace Quizlet.Services.FlashcardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private static readonly ILessonRepository lessonRepository = new LessonRepository();
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                return Ok(lessonRepository.GetLessons());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("AddLessonWithoutQuestions")]
        public IActionResult AddLessonWithoutQuestions([FromBody] LessonDTO lessonDTO)
        {
            try
            {
                lessonRepository.AddLesson(lessonDTO);
                return Created("Create new lesson successfully", null);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("AddLessonWithQuestions")]
        public IActionResult AddLessonWithQuestions([FromBody] AddLessonWithQuestionDTO lessonDTO)
        {
            try
            {
                lessonRepository.AddLesson(lessonDTO);
                return Created("Create new lesson successfully", null);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromRoute] int key)
        {
            try
            {
                lessonRepository.DeleteLesson(key);
                return Ok("Delete lesson successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromRoute] int key, [FromBody] EditLessonDTO editLessonDTO)
        {
            try
            {
                lessonRepository.UpdateLesson(key, editLessonDTO);
                return Ok("Update lesson successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
