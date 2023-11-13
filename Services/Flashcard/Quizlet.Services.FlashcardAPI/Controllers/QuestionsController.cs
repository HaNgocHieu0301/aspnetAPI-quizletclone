using BusinessObject.DTOs;
using BusinessObject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;
using Repository.IRepository;

namespace Quizlet.Services.FlashcardAPI.Controllers
{
    public class QuestionsController : ODataController
    {
        private readonly IQuestionRepository QuestionRepository;
        private readonly IQuestionService QuestionService;
        public QuestionsController(IQuestionRepository questionRepository, IQuestionService questionService)
        {
            QuestionRepository = questionRepository;
            QuestionService = questionService;
        }

        #region Hieuhn_GetMethods

        [HttpGet("/GetByUserId/{userId}")]
        [EnableQuery]
        public IActionResult GetByUserId(string userId)
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

        [HttpGet("/api/[controller]/GetByLessonId/{lessonId}")]
        [EnableQuery]
        public IActionResult GetByUserId(int lessonId)
        {
            try
            {
                IEnumerable<QuestionDTO> questions = QuestionService.GetQuestionsByLessonId(lessonId);
                return Ok(questions);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        #endregion

        #region Hungnm_CRUD

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

        [HttpPost("/api/[controller]/Range")]
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

        [HttpDelete("/api/[controller]/DeleteQuestion/{key}")]
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

        [HttpDelete("/api/[controller]/DeleteQuestions")]
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
        
        [HttpPut("/api/[controller]/UpdateQuestion/{key}")]
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

        [HttpPut("/api/[controller]/UpdateQuestion")]
        public IActionResult Put([FromBody] EditQuestionDTO editQuestionDto)
        {
            try
            {
                QuestionRepository.UpdateQuestion(editQuestionDto.QuestionId, editQuestionDto);
                return Ok("Update Question successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("/api/[controller]/UpdateQuestions")]
        public IActionResult PutRange([FromBody] List<EditQuestionDTO> questionDTOs)
        {
            try
            {
                // QuestionRepository.UpdateRangeQuestion(questionDTOs);
                QuestionService.UpdateRangeQuestion(questionDTOs);
                return Ok("Update Question successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        #endregion
    }
}