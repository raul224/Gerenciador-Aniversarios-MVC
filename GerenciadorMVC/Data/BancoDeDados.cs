using GerenciadorMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorMVC.Data
{
    public class BancoDeDados : DbContext
    {
        public BancoDeDados(DbContextOptions options) : base(options)
        {
        }
        //representa tabela
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
