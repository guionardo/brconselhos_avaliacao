using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEscola_1.Models.Entidades
{
    [Table("aluno")]
    public class Aluno
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Display(Description = "Professor")]
        public virtual Professor Professor { get; set; }

        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }

        [Display(Description = "Professor")]
        public string NomeProfessor => Professor == null ? "* SEM PROFESSOR * " : Professor.Nome;
    }
}
