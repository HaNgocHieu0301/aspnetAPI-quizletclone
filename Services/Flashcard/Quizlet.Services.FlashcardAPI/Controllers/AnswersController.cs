using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository;
using Repository.IRepository;

namespace Quizlet.Services.FlashcardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private static readonly IAnswerRepository AnswerRepository = new AnswerRepository();

        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(AnswerRepository.GetAnswers());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AnswerDTO AnswerDTO)
        {
            try
            {
                AnswerRepository.AddAnswer(AnswerDTO);
                return Created("Create new Answer successfully", null);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("Range")]
        public IActionResult PostRange([FromBody] List<AnswerDTO> answerDTOs)
        {
            try
            {
                AnswerRepository.AddRangeAnswer(answerDTOs);
                return Created("Create new Answer successfully", null);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("DeleteAnswer/{key}")]
        public IActionResult DeleteAnswer([FromRoute] int key)
        {
            try
            {
                AnswerRepository.DeleteAnswer(key);
                return Ok("Delete Answer successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("Range")]
        public IActionResult DeleteRange([FromBody] List<AnswerDTO> answerDTOs)
        {
            try
            {
                AnswerRepository.DeleteRangeAnswer(answerDTOs);
                return Ok("Delete Answer successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromRoute] int key, [FromBody] EditAnswerDTO editAnswerDTO)
        {
            try
            {
                AnswerRepository.UpdateAnswer(key, editAnswerDTO);
                return Ok("Update Answer successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("Range")]
        public IActionResult PutRange([FromBody] List<AnswerDTO> answerDTOs)
        {
            try
            {
                AnswerRepository.UpdateRangeAnswer(answerDTOs);
                return Ok("Update Answer successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
