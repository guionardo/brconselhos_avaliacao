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

        public IActionResult Index()
        {
            var lista = _contexto.Professor.ToList();
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
            return View(professor);
        }

    }
}