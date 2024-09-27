using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP2Console.Models.EntityFramework;

[Table("t_j_notation_not")]
[Index(nameof(SerieId))]
[PrimaryKey(nameof(UtilisateurId), nameof(SerieId))]
public partial class Notation
{
    [Key]
    [Column("utl_id")]
    public int UtilisateurId { get; set; }

    [Key]
    [Column("ser_id")]
    public int SerieId { get; set; }

    [Column("not_note")]
    [Range(0, 5, ErrorMessage = "Note must be between 0 and 5.")]
    public int Note { get; set; }

    [ForeignKey(nameof(UtilisateurId))]
    [InverseProperty(nameof(Utilisateur.NotesUtilisateur))]
    public virtual Utilisateur UtilisateurNotant { get; set; } = null!;

    [ForeignKey(nameof(SerieId))]
    [InverseProperty(nameof(Serie.NotesSerie))]
    public virtual Serie SerieNotee { get; set; } = null!;

}
