﻿using SRMSDataAccess.Models;
using SRMSRepositories.IRepositories;
using SRMSServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRMSServices.Services
{
    public class SubjectService : Service<Subject, int>, ISubjectService
    {
        private readonly ISubjectRepository _repository;
        public SubjectService(ISubjectRepository repository) : base(repository)
        {
            _repository = repository;   
        }

        
        public async Task CreateSubjectAsync(Subject subject)
        {
           await _repository.AddSubjectAsync(subject);
        }
    }
}
