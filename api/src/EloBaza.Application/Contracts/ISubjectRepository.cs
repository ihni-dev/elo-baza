﻿using EloBaza.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EloBaza.Application.Contracts
{
    public interface ISubjectRepository
    {
        Task<bool> Exists(Subject subject);
        Task<Subject> Find(Guid id);
        Task<IEnumerable<Subject>> GetAll(Func<Subject, bool> condition, int skip, int take);
        Task Save(Subject subject);
    }
}
