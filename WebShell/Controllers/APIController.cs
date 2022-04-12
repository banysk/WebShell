using Microsoft.AspNetCore.Mvc;
using WebShell.Data;
using Newtonsoft.Json;
using WebShell.Models;

namespace WebShell.Controllers
{
    public class ApiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public string GetCommands()
        {
            var data = JsonConvert.SerializeObject(_db.Commands.ToList());
            return data;
        }

        [HttpPost]
        async public Task<string> ExecuteCommand([FromBody] string content)
        {
            CommandModel command = new CommandModel { Source = content };
            command.Execute();
            _db.Commands.Add(command);
            await _db.SaveChangesAsync();
            return command.Result;
        }
    }
}
