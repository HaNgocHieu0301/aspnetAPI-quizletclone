using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quizlet.Services.BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository blogRepository;

        public BlogsController(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        // GET: api/<BlogController>
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                return Ok(blogRepository.GetBlogs());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<BlogController>
        [HttpPost]
        public IActionResult Post([FromBody] Blog value)
        {
            try
            {
                blogRepository.AddBlog(value);
                return Created("Create new blog successfully", null);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<BlogController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Blog value)
        {
            try
            {
                blogRepository.UpdateBlog(value);
                return Ok("Update blog successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                blogRepository.DeleteBlog(id);
                return Ok("Delete blog successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
