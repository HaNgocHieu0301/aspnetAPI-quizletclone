using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;
using Repository.IRepository;

namespace Quizlet.Services.FlashcardAPI.Controllers
{
    public class QuestionsController : ODataController
    {
        private static readonly IQuestionRepository QuestionRepository = new QuestionRepository();
        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                return Ok(QuestionRepository.GetQuestions());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [EnableQuery]
        public IActionResult Post([FromBody] AddQuestionDTO addQuestionDTO)
        {
            try
            {
                QuestionRepository.AddQuestion(addQuestionDTO);
                return Created("Create new Question successfully", null);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("/[controller]/Range")]
        public IActionResult PostRange([FromBody] List<AddQuestionDTO> addQuestionDTOs)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, QuestionRepository.AddRangeQuestion(addQuestionDTOs));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public IActionResult Delete([FromRoute] int key)
        {
            try
            {
                QuestionRepository.DeleteQuestion(key);
                return Ok("Delete Question successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("/[controller]/Range")]
        public IActionResult DeleteRange([FromBody] List<QuestionDTO> questionDTOs)
        {
            try
            {
                QuestionRepository.DeleteRangeQuestion(questionDTOs);
                return Ok("Delete Question successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public IActionResult Put([FromRoute] int key, [FromBody] EditQuestionDTO editQuestionDTO)
        {
            try
            {
                QuestionRepository.UpdateQuestion(key, editQuestionDTO);
                return Ok("Update Question successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("/[controller]/Range")]
        public IActionResult PutRange([FromBody] List<QuestionDTO> questionDTOs)
        {
            try
            {
                QuestionRepository.UpdateRangeQuestion(questionDTOs);
                return Ok("Update Question successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
