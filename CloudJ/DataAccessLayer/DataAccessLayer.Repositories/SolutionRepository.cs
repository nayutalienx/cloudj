using DataAccessLayer.Abstraction;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class SolutionRepository : BaseRepository<Solution.Models.Solution>, ISolutionRepository
    {
        public SolutionRepository(CloudjContext context) : base(context)
        {
        }
    }
}
