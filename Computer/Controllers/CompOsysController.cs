using Computer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Computer.Controllers
{
    [Route("Comp+Osys")]
    [ApiController]
    public class CompOsysController : ControllerBase
    {
        public ComputerShopDBContext Context = new ComputerShopDBContext();

        [HttpGet("getComputersbyosystem")]
        public object GetComputersbyosystem()
        {
            var eredmeny = from os in Context.Osystems
                           join comp in Context.Computers
                           on os.Id equals comp.OsId into gepek
                           select new
                           {
                               Osystem = os,
                               Szamitogepek = gepek.ToList()
                           };

            var lista = eredmeny.ToList();
            return new { message = "Sikeres lekérdezés:", result = lista };
        }

        [HttpGet("getSystemsByComputers")]
        public object GetSystemsByComputers()
        {
            var eredmeny = from comp in Context.Computers
                           join os in Context.Osystems
                           on comp.OsId equals os.Id
                           select new
                           {
                               comp.id,
                               comp.Brand,
                               comp.Type,
                               comp.Display,
                               comp.Memory,
                               OperaciosRendszer = os.Name
                           };

            var lista = eredmeny.ToList();
            return new { message = "Sikeres lekérdezés:", result = lista };
        }
        [HttpGet("GetOsWithComputers/{id}")]
        public IActionResult GetOsWithComputers([FromRoute]Guid id)
        {
            var os = Context.Osystems
                .Where(o => o.Id == id)
                .Select(o => new
                {
                    o.Id,
                    o.Name,
                    o.RegisterTime,
                    o.UpDateTime,
                    Szamitogepek = Context.Computers
                        .Where(c => c.OsId == o.Id)
                        .ToList()
                })
                .FirstOrDefault();

            if (os == null)
            {
                return NotFound(new { message = "Nincs ilyen operációs rendszer!" });
            }
            else
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = os });
            }
        }

        [HttpGet("GetComputerWithOs/{id}")]
        public IActionResult GetComputerWithOs([FromRoute] Guid id)
        {
            var Comp = (from c in Context.Computers
                        join os in Context.Osystems
                        on c.OsId equals os.Id
                        where c.OsId == id
                        select new
                        {
                            c.OsId,
                            c.Brand,
                            c.Type,
                            c.Display,
                            c.Memory,
                            c.CreatedTime,
                            OperaciosRendszer = os.Name
                        }).FirstOrDefault();

            if (Comp == null)
            {
                return NotFound(new { message = "Nincs ilyen számítógép!" });
            }
            else
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = Comp });
            }
        }

        [HttpGet("GetOsWithComputersNumber/{id}")]
        public IActionResult GetOsWithComputersNumber([FromRoute] Guid id)
        {
            var os = Context.Osystems
                .Where(o => o.Id == id)
                .Select(o => new
                {
                    o.Id,
                    o.Name,
                    o.RegisterTime,
                    o.UpDateTime,
                    Szamitogepek = Context.Computers
                        .Where(c => c.OsId == o.Id)
                        .Count()
                })
                .FirstOrDefault();

            if (os == null)
            {
                return NotFound(new { message = "Nincs ilyen operációs rendszer!" });
            }
            else
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = os });
            }
        }

        [HttpGet("GetComputerByBrandsWithOs/{Brand}")]
        public IActionResult GetComputerByBrandsWithOs([FromRoute] string brand)
        {
            var Comp = (from c in Context.Computers
                        join os in Context.Osystems
                        on c.OsId equals os.Id
                        where c.Brand == brand
                        select new
                        {
                            c.OsId,
                            c.Brand,
                            c.Type,
                            c.Display,
                            c.Memory,
                            c.CreatedTime,
                            OperaciosRendszer = os.Name
                        }).FirstOrDefault();

            if (Comp == null)
            {
                return NotFound(new { message = "Nincs ilyen brandű számítógép!" });
            }
            else
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = Comp });
            }
        }
    }
}
