using asi_avi_rest_api.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP2Console.Models.EntityFramework;

namespace asi_avi_rest_api.Models.DataManager
{
    public class UtilisateurManager : IDataRepository<Utilisateur>
    {
        readonly NotationDbContext? notationDbContext;
        public UtilisateurManager() { }
        public UtilisateurManager(NotationDbContext context)
        {
            this.notationDbContext = context;
        }
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetAllAsync()
        {
            return await notationDbContext.Utilisateurs.ToListAsync();
        }
        public async Task<ActionResult<Utilisateur>> GetByIdAsync(int id)
        {
            return await notationDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.Idutilisateur == id);
        }
        public async Task<ActionResult<Utilisateur>> GetByStringAsync(string mail)
        {
            return await notationDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.Mail.ToUpper() == mail.ToUpper());
        }
        public async Task AddAsync(Utilisateur entity)
        {
            await notationDbContext.Utilisateurs.AddAsync(entity);
            await notationDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Utilisateur utilisateur, Utilisateur entity)
        {
            notationDbContext.Entry(utilisateur).State = EntityState.Modified;
            utilisateur.Idutilisateur = entity.Idutilisateur;
            utilisateur.Nom = entity.Nom;
            utilisateur.Prenom = entity.Prenom;
            utilisateur.Mail = entity.Mail;
            utilisateur.rue = entity.rue;
            utilisateur.CodePostal = entity.CodePostal;
            utilisateur.Ville = entity.Ville;
            utilisateur.Pays = entity.Pays;
            utilisateur.Latitude = entity.Latitude;
            utilisateur.Longitude = entity.Longitude;
            utilisateur.Pwd = entity.Pwd;
            utilisateur.Mobile = entity.Mobile;
            utilisateur.NotesUtilisateur = entity.NotesUtilisateur;
            notationDbContext.SaveChanges();
        }
        public async Task DeleteAsync(Utilisateur utilisateur)
        {
            notationDbContext.Utilisateurs.Remove(utilisateur);
            await notationDbContext.SaveChangesAsync();
        }
    }
}
