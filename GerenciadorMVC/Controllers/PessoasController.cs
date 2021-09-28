using GerenciadorMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GerenciadorMVC.Data;

namespace GerenciadorMVC.Controllers
{
    public class PessoasController : Controller
    {
        private BancoDeDados BancoDeDados { get; set; }
        public PessoasController(BancoDeDados bancoDeDados)
        {
            BancoDeDados = bancoDeDados;
        }

        public IActionResult Index()
        {
            var listaAniversariantesDoDia = AniversariantesDoDia();

            var listaPessoasPorAniversario = PessoasPorAniversario();

            var listasIndex = new ListasIndex();

            listasIndex.listaAniversariantesDoDia = listaAniversariantesDoDia;
            listasIndex.listaPessoasPorAniversario = listaPessoasPorAniversario;

            return View(listasIndex);
        }

        public IActionResult Listar()
        {
            var ListaPessoas = PegaListaPessoas();

            return View(ListaPessoas);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(string nome, string sobreNome, DateTime aniversario)
        {
            RegistraPessoaBanco(nome, sobreNome, aniversario);

            return Redirect("Listar");
        }

        public IActionResult Buscar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BuscarPost(string nome)
        {
            var listaResultado = PegaBusca(nome);

            return View("ResultadoBusca", listaResultado);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var pessoa = buscaPeloId(id);

            return View("Editar", pessoa);
        }

        [HttpPost]
        public IActionResult Editar(int id, string nome, string sobreNome, DateTime aniversario)
        {
            EditarPessoa(id, nome, sobreNome, aniversario);

            return Redirect("/Pessoas/Listar");
        }

        [HttpGet]
        [Route("Pessoas/Deletar")]
        public IActionResult DeletarGet(int id)
        {
            var pessoa = buscaPeloId(id);

            return View("Deletar", pessoa);
        }

        [HttpPost]
        [Route("Pessoas/Deletar")]
        public IActionResult DeletarPost(int id)
        {
            RemoverPessoa(id);

            return Redirect("/Pessoas/Listar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public Pessoa buscaPeloId(int id)
        {
            var pessoa = BancoDeDados.Pessoas.Find(id);

            return pessoa;
        }

        public void EditarPessoa(int id, string nome, string sobreNome, DateTime aniversario)
        {
            var pessoa = BancoDeDados.Pessoas.Find(id);

            pessoa.nome = nome;
            pessoa.sobreNome = sobreNome;
            pessoa.aniversario = aniversario;

            BancoDeDados.Pessoas.Update(pessoa);
            BancoDeDados.SaveChanges();
        }

        public List<Pessoa> PegaListaPessoas()
        {
            var ListaPessoas = BancoDeDados.Pessoas.ToList();

            return ListaPessoas;
        }

        public void RemoverPessoa(int id)
        {
            var pessoa = BancoDeDados.Pessoas.Find(id);
            BancoDeDados.Pessoas.Remove(pessoa);
            BancoDeDados.SaveChanges();
        }

        public void RegistraPessoaBanco(string nome, string sobreNome, DateTime aniversario)
        {
            Pessoa pessoa = new Pessoa();
            pessoa.nome = nome;
            pessoa.sobreNome = sobreNome;
            pessoa.aniversario = aniversario;

            BancoDeDados.Pessoas.Add(pessoa);
            BancoDeDados.SaveChanges();
        }

        public List<Pessoa> PegaBusca(string nomeBusca)
        {
            var listaResultado = new List<Pessoa>();
            listaResultado = BancoDeDados.Pessoas.Where(Pessoa => Pessoa.nome.StartsWith(nomeBusca)).ToList();

            return listaResultado;
        }

        public List<Pessoa> AniversariantesDoDia()
        {
            var diaHoje = DateTime.Today;
            var AniversariantesDoDia = BancoDeDados.Pessoas.Where(Pessoa => Pessoa.aniversario.Equals(diaHoje)).ToList();

            return AniversariantesDoDia;
        }

        public List<Pessoa> PessoasPorAniversario()
        {
            var listaPessoas = PegaListaPessoas();
            
            listaPessoas.OrderBy(Pessoa => Pessoa.aniversario).ToList();

            return listaPessoas;
        }
    }
}
