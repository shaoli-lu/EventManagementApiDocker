using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApiDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperCloudHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero> {
                new SuperHero {
                    Id = 1,
                    Name ="Spider Man",
                    FirstName ="Peter",
                    LastName ="Parker",
                    Place ="New York City"},
                 new SuperHero {
                    Id = 2,
                    Name ="Ironman",
                    FirstName ="Tony",
                    LastName ="Stark",
                    Place ="Long ISland"}
                };

        private readonly DataContext _context;

        public SuperCloudHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(await _context.SuperHeroes.OrderBy(h => h.Name).Select(s => s).ToListAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult> AddHero([FromBody] SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found.");
            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(dbHero);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found.");

            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }


    }
}