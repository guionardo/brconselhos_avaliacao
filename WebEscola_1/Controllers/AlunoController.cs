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
        public ActionResult Index()
        {
            var alunos = _contexto.Aluno.Include(a => a.Professor);            
            return View(alunos.ToList());
        }

        // GET: Aluno/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            var aluno = _contexto.Aluno.Include(a => a.Professor).Find(id);
            return View(aluno);
        }

        // GET: Aluno/Create
        [HttpGet]
        public ActionResult Create()
        {
            var aluno = new Aluno();
            CarregaProfessores();
            return View(aluno);
        }

        // POST: Aluno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            var aluno = _contexto.Aluno.Include(a => a.Professor).Find(id);
            CarregaProfessores(aluno.Professor);
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
            var itensProfessores = from p in _contexto.Professor
                                   orderby p.Nome
                                   select p;
            ViewBag.Professores = new SelectList(itensProfessores, "ProfessorId", "Nome", selectedProfessor);
            /*            var ItensProfessores = new List<SelectListItem>();
                        foreach(var professor in _contexto.Professor.ToList())
                        {
                            ItensProfessores.Add(new SelectListItem(professor.Nome, professor.Id.ToString()));
                        }
                        ViewBag.Professores = ItensProfessores;*/
        }        

    }
}