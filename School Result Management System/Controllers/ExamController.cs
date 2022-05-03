﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SRMSDataAccess.Models;
using SRMSRepositories.IRepositories;
using SRMSViewModel;

namespace School_Result_Management_System.Controllers
{
    public class ExamController :Controller
    {
        private readonly IClassRepository _classrepo;
        private readonly IExamRepository _examrepo;

        public ExamController(IClassRepository classrepo,IExamRepository examrepo)
        {
            _classrepo = classrepo;
            _examrepo = examrepo;

        }
      
        
        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<Exam> examsList = _examrepo.GetAllExams();
            ViewBag.Exams = new SelectList(examsList, "Id", "Name");

            IEnumerable<Class> classList = _classrepo.GetAllClasses();
            ViewBag.Class = new SelectList(classList, "Id", "ClassName");


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ExamViewModel examViewModel)
        {

            if (ModelState.IsValid)
            {
                ExamClassRelation exam = new ExamClassRelation()
                {
                    ClassID = examViewModel.ClassID,
                    ExamId= examViewModel.Examid,
                    ExamYear = examViewModel.ExamYear,
                    ResultPublished = examViewModel.ResultPublished
                };

                await _examrepo.AddExamAsync(exam);

                return RedirectToAction("index");

            }
            return View(examViewModel);
        }
   
      [HttpGet]

      public IActionResult Edit(int id)
        {

            ExamClassRelation examClassRelation = _examrepo.GetExamClassById(id);

            IEnumerable<Exam> examsList = _examrepo.GetAllExams();
            ViewBag.Exams = new SelectList(examsList, "Id", "Name");

            IEnumerable<Class> classList = _classrepo.GetAllClasses();
            ViewBag.Class = new SelectList(classList, "Id", "ClassName");

            ExamViewModel exam = new ExamViewModel()
            {
                ResultPublished = examClassRelation.ResultPublished,
                ExamYear = examClassRelation.ExamYear

            };

            return View(exam);
            
    
        }
      

    }
}