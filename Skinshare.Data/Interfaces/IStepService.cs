using System.Collections.Generic;
using Skinshare.Core.Entities;

namespace Skinshare.Data.Interfaces
{
    public interface IStepService
    {
        IEnumerable<Step> GetSteps(Routine routine);
        IEnumerable<Step> GetSteps(Routine routine, PartOfDay partOfDay);
    }
}