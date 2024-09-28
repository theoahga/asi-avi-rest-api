using asi_avi_rest_api.Models.EntityFramework.Complexity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TP2Console.Models.EntityFramework;

[Table("t_e_utilisateur_utl")]
public partial class Utilisateur
{
    [Key]
    [Column("utl_id")]
    public int Idutilisateur { get; set; }

    [Column("utl_nom")]
    [StringLength(50)]
    public string? Nom { get; set; } = null!;

    [Column("utl_prenom")]
    [StringLength(50)]
    public string? Prenom { get; set; } = null!;

    // Custom validation deported in MobileComplexity class
    [Column("utl_mobile", TypeName = "char(10)")]
    [MobileComplexity]
    public string? Mobile { get; set; } = null!;

    [Required]
    [Column("utl_mail")]
    [EmailAddress]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La longueur d’un email doit être comprise entre 6 et 100 caractères.")]
    public string Mail { get; set; } = null!;

    [Column("utl_rue")]
    [StringLength(200)]
    public string rue { get; set; } = null!;


    // Custom validation deported in PasswordComplexity class
    [Column("utl_pwd")]
    [PasswordComplexity]
    [NotNull]
    public string Pwd { get; set; } = null!;

    [Column("utl_cp", TypeName = "char(5)")]
    [CodePostalComplexity]
    public string? CodePostal { get; set; } = null!;

    [Column("utl_ville")]
    [StringLength(50)]
    public string? Ville { get; set; } = null!;

    [Column("utl_pays")]
    [StringLength(50)]
    public string? Pays { get; set; } = null!;

    [Column("utl_latitude", TypeName="real")]
    public float? Latitude { get; set; } = null!;

    [Column("utl_longitude", TypeName = "real")]
    public float? Longitude { get; set; } = null!;

    [Column("utl_datecreation", TypeName="date")]
    [NotNull]
    public DateTime DateCreation { get; set; } = DateTime.Now;
    public virtual ICollection<Notation> NotesUtilisateur { get; set; } = new List<Notation>();
}