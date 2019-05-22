using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebEscola_1.Models.Contexto;
using WebEscola_1.Models.Entidades;

namespace WebEscola_1.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly Contexto _contexto;

        public ProfessorController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index(string op = null)
        {
            List<Professor> lista = null;
            if (string.IsNullOrEmpty(op))
            {
                lista = _contexto.Professor.ToList();
            }
            else
            {
                lista = new List<Professor>();
                var alunos = _contexto.Aluno.ToList();
                foreach (var professor in _contexto.Professor)
                {
                    if (professor.Alunos == null)
                    {
                        professor.Alunos = new List<Aluno>();
                        foreach (var aluno in alunos)
                            if (aluno.ProfessorId == professor.Id)
                                professor.Alunos.Add(aluno);
                    }

                    if (professor.Alunos != null && professor.Alunos.Count > 0)
                    {
                        double somaIdades = 0;
                        foreach (var aluno in professor.Alunos)
                            somaIdades += DateTime.Today.Subtract(aluno.DataNascimento).TotalDays;
                        double media = (somaIdades / professor.Alunos.Count) / 365;
                        if (media >= 15 && media <= 17)
                            lista.Add(professor);
                    }
                }
            }
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var professor = new Professor();
            return View(professor);
        }

        [HttpPost]
        public IActionResult Create(Professor professor)
        {
            if (ModelState.IsValid)
            {
                _contexto.Professor.Add(professor);
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(professor);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var professor = _contexto.Professor.Find(Id);
            return View(professor);
        }

        [HttpPost]
        public IActionResult Edit(Professor professor)
        {
            if (ModelState.IsValid)
            {
                _contexto.Professor.Update(professor);
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(professor);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var professor = _contexto.Professor.Find(Id);
            return View(professor);
        }

        [HttpPost]
        public IActionResult Delete(Professor _professor)
        {
            var professor = _contexto.Professor.Find(_professor.Id);
            if (professor != null)
            {
                _contexto.Professor.Remove(professor);
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(_professor);
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var professor = _contexto.Professor.Find(Id);
            AtualizarAlunos(professor);
            return View(professor);
        }

        private void AtualizarAlunos(Professor professor)
        {
            if (professor == null || professor.Alunos != null)
                return;
            professor.Alunos = new List<Aluno>();
            var query = from aluno
                        in _contexto.Aluno
                        where aluno.ProfessorId == professor.Id
                        select aluno;
            foreach (var aluno in query)
                if (!professor.Alunos.Contains(aluno))
                    professor.Alunos.Add(aluno);
        }

    }
}