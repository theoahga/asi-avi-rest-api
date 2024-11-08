using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace asi_avi_web_app.Models;

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
    [NotNull]
    public string Pwd { get; set; } = null!;

    [Column("utl_cp", TypeName = "char(5)")]
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
}