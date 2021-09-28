using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorMVC.Models
{
    [Table("Pessoas")]
    public class Pessoa
    {
        [Key]
        public int id { get; set; }
        [Column("Nome")]
        public string nome { get; set; }
        [Column("SobreNome")]
        public string sobreNome { get; set; }
        [Column("Aniversario")]
        public DateTime aniversario { get; set; }
    }
}
