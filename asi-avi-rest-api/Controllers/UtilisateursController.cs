using asi_avi_rest_api.Models.DataManager;
using asi_avi_rest_api.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP2Console.Models.EntityFramework;

namespace asi_avi_rest_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly IDataRepository<Utilisateur> dataRepository;
        //private readonly UtilisateurManager utilisateurManager;
        //private readonly NotationDbContext _context;

        public UtilisateursController(IDataRepository<Utilisateur> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Utilisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Utilisateurs/GetUtilisateurById/5
        [HttpGet("GetUtilisateurById/{id}", Name = "GetUtilisateurById")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
        {

            var utilisateur = await dataRepository.GetByIdAsync(id);
            //var utilisateur = _context.Utilisateurs.FindAsync(id);

            if (utilisateur == null)
            {
                return NotFound("Id utilisateur inconnu");
            }

            return utilisateur;
        }

        // GET: api/Utilisateurs/GetUtilisateurByEmail/tclere@cpe.fr
        [HttpGet("GetUtilisateurByEmail/{mail}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurByEmail(string mail)
        {
            var utilisateur = await dataRepository.GetByStringAsync(mail);
            //var utilisateur = _context.Utilisateurs
            //    .FirstOrDefaultAsync(u => u.Mail.ToLower() == mail.ToLower());

            if (utilisateur == null)
            {
                return NotFound("Mail utilisateur inconnu");
            }

            return utilisateur;
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateurAsync(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.Idutilisateur)
            {
                return BadRequest();
            }

            var userToUpdate = await dataRepository.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, utilisateur);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(utilisateur);

            return CreatedAtAction("GetUtilisateurById", new { id = utilisateur.Idutilisateur }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = await dataRepository.GetByIdAsync(id);
            if (utilisateur == null)
            {
                return NotFound("Id utilisateur inconnu");
            }

            dataRepository.DeleteAsync(utilisateur.Value);

            return NoContent();
        }
    }
}
