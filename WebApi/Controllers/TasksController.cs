using AutoMapper;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Task = DataAccess.Task;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper mapper;


        public TasksController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_taskService.GetAll());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(TaskDto task)
        {
            try
            {
                _taskService.Add(mapper.Map<Task>(task));
                return Ok("Created successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var task = _taskService.GetById(id);
                if(task == null)
                {
                    return BadRequest("Task not found");
                }
                return Ok(task);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, TaskDto task)
        {
            try
            {
                _taskService.Edit(id, mapper.Map<Task>(task));
                return Ok("Edit successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _taskService.Delete(id);
                return Ok($"Delete task {id} successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("Bulk")]
        public IActionResult Delete(IEnumerable<Guid> ids)
        {
            try
            {
                _taskService.DeleteRange(ids);
                return Ok("Bulk delete successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Bulk")]
        public IActionResult Create(IEnumerable<TaskDto> tasks)
        {
            try
            {
                _taskService.AddRange(mapper.Map<IEnumerable<Task>>(tasks));
                return Ok("Bulk create successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
