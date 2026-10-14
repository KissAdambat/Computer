using Computer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Computer.Controllers
{
    [Route("computer")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        public ComputerShopDBContext Context = new ComputerShopDBContext();

        [HttpGet("getAllComputer")]
        public object GetAllComputer()
        {
            var comps = Context.Computers.ToList();
            return new { message = "Sikeres lekérdezés:", result = comps };
        }

        [HttpGet("GetAComputer/{id}")]
        public object GetAComputer([FromRoute] Guid id)
        {
            var existingComputer = Context.Computers.FirstOrDefault(u => u.id == id);
            if (existingComputer != null)
            {
                return existingComputer;
            }
            else
            {
                return NotFound(new { message = "Nem található ilyen számítógép" });
            }
        }

        [HttpPost("AddComputer")]
        public object AddComputer(AddNewComputerDTOs dto)
        {
            var osdi = dto.Os;
            var dii = Context.Osystems.FirstOrDefault(u => u.Id == osdi.Id);
            var rcont = dii.Id.ToString();  
            var comp = new Models.Computer
            {
                 Brand = dto.Brand,
                 Type = dto.Type,
                 Display = dto.Display,
                 Memory = dto.Memory,
                 Os = rcont,
                 CreatedTime = DateTime.Now
            };
            Context.Computers.Add(comp);
            Context.SaveChanges();
            return StatusCode(201, new { message = "Sikeres hozzáadás:", result = comp });
        }

        [HttpPut("UpdateComputer/{id}")]
        public object UpdateComputer([FromRoute] Guid id, [FromBody] UpdateComptersDTOs updatecompDto)
        {
            var comp = Context.Computers.FirstOrDefault(u => u.id == id);

            if (comp != null)
            {
                comp.Brand = updatecompDto.Brand;
                comp.Type = updatecompDto.Type;
                comp.Display = updatecompDto.Display;
                comp.Memory = updatecompDto.Memory;

                Context.Computers.Update(comp);
                Context.SaveChanges();

                return StatusCode(200, new { message = "Sikeres frissítés", result = comp });
            }

            return StatusCode(404, new { message = "Sikertlen frissítés", result =comp});
        }
        [HttpDelete("DeleteComputer/{id}")]
        public object DeleteComputer([FromRoute] Guid id)
        {
            var existingComp = Context.Computers.FirstOrDefault(u => u.id == id);
            if (existingComp == null)
            {
                return NotFound(new { message = "Nem található a megadott számítógép." });
            }
            Context.Computers.Remove(existingComp);
            Context.SaveChanges();
            return StatusCode(200, new { message = "Sikeres törlés:", result = existingComp });
        }
    }
}
