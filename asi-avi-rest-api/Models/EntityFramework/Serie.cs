using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.X86;

[Table("t_e_serie_ser")]
[Index(nameof(Titre))]
public class Serie
{
    [Key]
    [Column("ser_id")]
    public int SerieId { get; set; }

    [Column("ser_titre")]
    [StringLength(100)]
    [NotNull]
    public string Titre { get; set; } = null!;

    [Column("ser_resume", TypeName="text")]
    public string? Resume { get; set; } = null!;

    [Column("ser_nbsaisons")]
    public int? ser_nbsaisons { get; set; }

    [Column("ser_nbepisodes")]
    public int? NbEpisodes { get; set; }

    [Column("ser_anneecreation")]
    public int? AnneeCreation { get; set; }

    [Column("ser_network")]
    [StringLength(50)]
    [MaybeNull]
    public string? Network { get; set; } = null!;

    [InverseProperty(nameof(Notation.SerieNotee))]
    public virtual ICollection<Notation> NotesSerie { get; set; } = new List<Notation>();
}
