using DataAccessLayer.Abstraction;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class SolutionLinkRepository : BaseRepository<SolutionLink>, ISolutionLinkRepository
    {
        public SolutionLinkRepository(CloudjContext context) : base(context)
        {
        }
    }
}
