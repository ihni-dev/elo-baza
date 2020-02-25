﻿using MediatR;
using System;

namespace EloBaza.Application.Queries
{
    public class GetSubject : IRequest<GetSubjectResult>
    {
        public Guid Id { get; set; }

        public GetSubject(Guid id)
        {
            Id = id;
        }
    }
}