using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository.IRepository;

namespace Quizlet.Services.FlashcardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonRepository lessonRepository;

        public LessonsController(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }

        [HttpGet]
        [EnableQuery]
        [AllowAnonymous]
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
                return Created("Create new lesson successfully", lessonRepository.AddLesson(lessonDTO));
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
                int lessonId = lessonRepository.AddLesson(lessonDTO);
                return Created("Create new lesson successfully", lessonId);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("DeleteLesson/{key}")]
        public IActionResult Delete(int key)
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

        [HttpPut("UpdateLesson")]
        public IActionResult Put([FromBody] EditLessonDTO editLessonDTO)
        {
            try
            {
                var res = lessonRepository.UpdateLesson(editLessonDTO);
                if (res == false)
                {
                    return BadRequest("Update lesson failed");
                }
                return Ok("Update lesson successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
