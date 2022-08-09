using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SRMSDataAccess.Models;
using SRMSRepositories.IRepositories;
using SRMSViewModel;

namespace School_Result_Management_System.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        private readonly IClassRepository _classrepo;
        private readonly IExamRepository _examrepo;

        public ExamController(IClassRepository classrepo, IExamRepository examrepo)
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
            //examsList to access the exams information
            IEnumerable<Exam> examsList = _examrepo.GetAllExams();
            ViewBag.Exams = new SelectList(examsList, "Id", "Name");

            IEnumerable<Class> classList = _classrepo.GetAllClasses();
            ViewBag.Class = new SelectList(classList, "Id", "ClassName");


            if (ModelState.IsValid)
            {
                try
                {
                    ExamClassRelation exam = new ExamClassRelation()
                    {
                        ClassID = examViewModel.ClassID,
                        ExamId = examViewModel.Examid,
                        ExamYear = examViewModel.ExamYear,

                    };
                    if (_examrepo.ExamExists(exam))
                    {
                        ModelState.AddModelError("", "Exam already registered.");
                        return View(examViewModel);
                    }
                    await _examrepo.AddExamAsync(exam);
                    ViewBag.Success = "Exam Successfully added";
                }

                catch (Exception ex)
                {
                    ViewBag.Error = "Failed to add exam";
                }
            }
            return View(examViewModel);
        }

        public IActionResult Index()
        {
            var registeredExams=_examrepo.GetAllExamDetails();
            return View();
        }


    }
}
