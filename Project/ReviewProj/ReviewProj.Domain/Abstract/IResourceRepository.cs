﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewProj.Domain.Entities;

namespace ReviewProj.Domain.Abstract
{
    class IResourceRepository
    {
        IQueryable<Resource> Resources { get; }
    }
}
