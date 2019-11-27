using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudJ.Client.Options
{
    public sealed class SolutionApiClientOptions
    {
        public string UpdateSolutionUrl { get; set; }
        public string AddSolutionUrl { get; set; }
        public string DeleteSolutionUrl { get; set; }
        public string GetAllCategoriesUrl { get; set; }
        public string AddCategoryUrl { get; set; }
        public string AddSolutionLinkUrl { get; set; }
        public string AddSolutionPlanUrl { get; set; }
        public string AddReviewUrl { get; set; }
        public string GetSolutionsByFilterUrl { get; set; }
    }
}
