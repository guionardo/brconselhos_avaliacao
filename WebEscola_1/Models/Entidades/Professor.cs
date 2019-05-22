using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebEscola_1.Models.Entidades
{
    [Table("professor")]
    public class Professor
    {
        [Display(Description = "Código")]
        public int Id { get; set; }

        [Display(Description = "Nome")]
        public string Nome { get; set; }

        public ICollection<Aluno> Alunos { get; set; }
    }
}
