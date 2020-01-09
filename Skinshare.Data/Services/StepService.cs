using System.Collections.Generic;
using System.Linq;
using Skinshare.Core.Entities;
using Skinshare.Data.Interfaces;

namespace Skinshare.Data.Services
{
    public class StepService : IStepService
    {
        public IEnumerable<Step> GetSteps(Routine routine)
        {
            return routine.Steps.Select(s =>
            {
                s.Order += 1;
                return s;
            }).OrderBy(s => s.PartOfDay).ThenBy(s => s.Order);
        }

        public IEnumerable<Step> GetSteps(Routine routine, PartOfDay partOfDay)
        {
            return routine.Steps.Where(s => s.PartOfDay == partOfDay).Select(s =>
            {
                s.Order += 1;
                return s;
            }).OrderBy(s => s.Order);
        }
    }
}