using AutoMapper;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Task = DataAccess.Task;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_taskService.GetAll());
        }

        [HttpPost]
        public IActionResult Post(TaskDto task)
        {
            _taskService.Add(mapper.Map<Task>(task));
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_taskService.GetById(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, TaskDto task)
        {
            _taskService.Edit(id,mapper.Map<Task>(task));
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            _taskService.Delete(id);
            return Ok();
        }

        [HttpDelete("Bulk")]
        public IActionResult Delete(IEnumerable<Guid> ids)
        {
            _taskService.DeleteRange(ids);
            return Ok();
        }

        [HttpPost("Bulk")]
        public IActionResult Create(IEnumerable<TaskDto> tasks)
        {
            _taskService.AddRange(mapper.Map<IEnumerable<Task>>(tasks));
            return Ok();
        }
    }
}
