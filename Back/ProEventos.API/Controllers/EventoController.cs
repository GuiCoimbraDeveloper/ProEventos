using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly DataContext context;

        public EventoController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Evento>>> GetAsync()
        {
            return Ok(await context.Eventos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            return Ok(await context.Eventos.FirstOrDefaultAsync(x => x.EventoId == id));
        }

        [HttpPost]
        public ActionResult Post()
        {
            return null;
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return null;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return null;
        }
    }
}
