﻿using Microsoft.AspNetCore.Mvc;
using SRMSDataAccess.Models;
using SRMSRepositories.IRepositories;
using SRMSViewModel;

namespace School_Result_Management_System.Controllers
{
    public class ClassController : Controller
    {
        private IClassRepository _classrepo;


        public ClassController(IClassRepository classrepo)
        {
            _classrepo = classrepo;
        }
        public IActionResult index()
        {
            IEnumerable<Class> classes = _classrepo.GetAllClasses();
            return View(classes);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ClassViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClassViewModel model)
        {

            if (ModelState.IsValid)
            {
                Class classnew = new Class()
                {
                    ClassName = model.ClassName
                };

                await _classrepo.AddClassAsync(classnew);
                return RedirectToAction("Index");
            };
            return View(model);

        }
        public IActionResult Edit(int id)
        {
            Class cl = _classrepo.GetClassById(id);
            return View(cl);
        }

        [HttpPost]
        public IActionResult Edit(Class cl)
        {
            if (ModelState.IsValid)
            {
                _classrepo.UpdateClass(cl);
                return Redirect("/Class");

            }
            return View(cl);


        }

        public IActionResult Delete(int id)
        {
            _classrepo.RemoveClass(id);
            return Redirect("/Class");
        }





    }
}
