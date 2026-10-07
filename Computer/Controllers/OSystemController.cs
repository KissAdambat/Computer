using Computer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Computer.Controllers
{
    [Route("osystem")]
    [ApiController]
    public class OSystemController : ControllerBase
    {
        public ComputerShopDBContext Context = new ComputerShopDBContext();

        [HttpGet("getAll")]
        public object GetAllOsystem()
        {
            var users = Context.Osystems.ToList();
            return new { message = "Sikeres lekérdezés:",result = users };
        }

        [HttpPost("add")]
        public object AddOsystem(AddNewOsystemDTOs dto)
        {
            var osystem = new Osystem
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Version = dto.Version,
                RegisterTime = DateTime.Now,
                UpDateTime = DateTime.Now
            };
            Context.Osystems.Add(osystem);
            Context.SaveChanges();
            return StatusCode(201 , new { message = "Sikeres hozzáadás:", result = osystem });
        }

        [HttpPut("update/{id}")]    
        public object UpdateOsystem(Osystem osystem)
        {
            var existingOsystem = Context.Osystems.FirstOrDefault(o => o.Id == osystem.Id);
            if (existingOsystem == null)
            {
                return NotFound(new { message = "Nem található a megadott operációs rendszer." });
            }
            existingOsystem.Name = osystem.Name;
            existingOsystem.Version = osystem.Version;
            existingOsystem.UpDateTime = DateTime.Now;
            Context.Osystems.Update(existingOsystem);
            Context.SaveChanges();
            return StatusCode(200, new { message = "Sikeres frissítés:", result = existingOsystem });
        }

        [HttpDelete("delete/{id}")]
        public object DeleteOsystem(Guid id)
        {
            var existingOsystem = Context.Osystems.FirstOrDefault(o => o.Id == id);
            if (existingOsystem == null)
            {
                return NotFound(new { message = "Nem található a megadott operációs rendszer." });
            }
            Context.Osystems.Remove(existingOsystem);
            Context.SaveChanges();
            return StatusCode(200, new { message = "Sikeres törlés:", result = existingOsystem });
        }
    }
}
