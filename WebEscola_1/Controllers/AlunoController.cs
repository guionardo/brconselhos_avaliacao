using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebEscola_1.Models.Contexto;
using WebEscola_1.Models.Entidades;

namespace WebEscola_1.Controllers
{
    public class AlunoController : Controller
    {
        private Contexto _contexto;

        public AlunoController(Contexto contexto)
        {
            _contexto = contexto;
        }
        // GET: Aluno
        public ActionResult Index(string maiores=null)
        {
            var alunos = _contexto.Aluno.Include(a => a.Professor);
            List<Aluno> lista;
            if (string.IsNullOrEmpty(maiores))
            {
                lista = alunos.ToList();
                ViewData["Message"] = "Todos os alunos.";
                ViewBag.Title = "Todos os alunos";
            }
            else
            {
                ViewData["Message"] = "Alunos com mais de 16 anos.";
                ViewBag.Title = "Alunos com mais de 16 anos";
                lista = new List<Aluno>();
                foreach (var aluno in alunos)
                {
                    if (DateTime.Now.Subtract(aluno.DataNascimento).TotalDays / 365 > 16)
                    {
                        lista.Add(aluno);
                    }
                }
            }
            return View(lista);
        }


        // GET: Aluno/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            var aluno = _contexto.Aluno.Find(id);
            aluno.Professor = _contexto.Professor.Find(aluno.ProfessorId);
            return View(aluno);
        }

        // GET: Aluno/Create
        [HttpGet]
        public ActionResult Create()
        {
            var aluno = new Aluno();
            ViewBag.ProfessorId = new SelectList(_contexto.Professor, "Id", "Nome");
            return View(aluno);
        }

        // POST: Aluno/Create
        [HttpPost]        
        public ActionResult Create(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _contexto.Aluno.Add(aluno);
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aluno);
        }

        // GET: Aluno/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var aluno = _contexto.Aluno.Find(id);
            ViewBag.ProfessorId = new SelectList(_contexto.Professor, "Id", "Nome",aluno.ProfessorId);

            return View(aluno);
        }

        // POST: Aluno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Aluno _aluno)
        {
            if (ModelState.IsValid)
            {
                _contexto.Aluno.Update(_aluno);
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }
            CarregaProfessores(_aluno.Professor);
            return View(_aluno);
        }

        // GET: Aluno/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var aluno = _contexto.Aluno.Find(id);
            aluno.Professor = _contexto.Professor.Find(aluno.ProfessorId);
            return View(aluno);
        }

       
        // POST: Aluno/Delete/5
        [HttpPost]
        public ActionResult Delete(Aluno _aluno)
        {
            var aluno = _contexto.Aluno.Find(_aluno.Id);
            if (aluno != null)
            {
                _contexto.Aluno.Remove(aluno);
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_aluno);
        }

        public void CarregaProfessores(object selectedProfessor = null)
        {
            var ItensProfessores = new List<SelectListItem>();
            foreach (var professor in _contexto.Professor.ToList())
            {
                ItensProfessores.Add(new SelectListItem(professor.Nome, professor.Id.ToString()));
            }
            ViewBag.Professores = ItensProfessores;
        }

        public List<Professor> ListaProfessores()
        {
            return _contexto.Professor.ToList();
        }

    }
}