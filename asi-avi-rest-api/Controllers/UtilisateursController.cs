using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP2Console.Models.EntityFramework;

namespace asi_avi_rest_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly NotationDbContext _context;

        public UtilisateursController(NotationDbContext context)
        {
            _context = context;
        }

        // GET: api/Utilisateurs
        [HttpGet("GetUtilisateurs")]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("GetUtilisateurById/{id}", Name = "GetUtilisateurById")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);

            if (utilisateur == null)
            {
                return NotFound("Id utilisateur inconnu");
            }

            return utilisateur;
        }

        // GET: api/Utilisateurs/tclere@cpe.fr
        [HttpGet("GetUtilisateurByEmail/{mail}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurByEmail(string mail)
        {
            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Mail.ToLower() == mail.ToLower());

            if (utilisateur == null)
            {
                return NotFound("Mail utilisateur inconnu");
            }

            return utilisateur;
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutUtilisateur/{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.Idutilisateur)
            {
                return BadRequest();
            }

            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
                {
                    return NotFound("Id utilisateur inconnu");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostUtilisateur")]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilisateurById", new { id = utilisateur.Idutilisateur }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUtilisateur(int id)
        //{
        //    var utilisateur = await _context.Utilisateurs.FindAsync(id);
        //    if (utilisateur == null)
        //    {
        //        return NotFound("Id utilisateur inconnu");
        //    }

        //    _context.Utilisateurs.Remove(utilisateur);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UtilisateurExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.Idutilisateur == id);
        }
    }
}
