using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Abstract
{
  public abstract class ComparerBase<W> : IComparer<W>
    where W : WrapperBase
  {
    protected MBHero InitiatingHero { get; private set; }
    protected bool HasInitiatingHero => InitiatingHero.IsNull;

    protected ComparerBase(MBHero initiatingHero = null)
    {
      InitiatingHero = initiatingHero ?? MBHero.Null;
    }

    public virtual int Compare(W x, W y)
    {
      var result = 0;

      TryComparePreconditions(x, y, ref result);

      return result;
    }

    protected bool TryComparePreconditions(W x, W y, ref int result) =>
      TryCompareNull(x, y, ref result) ? true :
      TryCompareRequirements(x, y, ref result);


    private bool TryCompareNull(W x, W y, ref int result)
    {
      var xMeetsRequirements = MeetsRequirements(x);
      var yMeetsRequirements = MeetsRequirements(y);
      var assignedResult = false;

      if (x.IsNull)
      {
        result = y.IsNull ? Constants.Comparer.XIsEqualToY : yMeetsRequirements ? Constants.Comparer.XIsLessThanY : Constants.Comparer.XIsGreaterThanY;
        assignedResult = true;
      }

      if (y.IsNull)
      {
        result = xMeetsRequirements ? Constants.Comparer.XIsGreaterThanY : Constants.Comparer.XIsLessThanY;
        assignedResult = true;
      }

      return assignedResult;
    }

    private bool TryCompareRequirements(W x, W y, ref int result)
    {
      var xMeetsRequirements = MeetsRequirements(x);
      var yMeetsRequirements = MeetsRequirements(y);
      var assignedResult = false;

      if (!xMeetsRequirements)
      {
        result = !yMeetsRequirements ? Constants.Comparer.XIsEqualToY : Constants.Comparer.XIsLessThanY;
        assignedResult = true;
      }
      else if (!yMeetsRequirements)
      {
        result = Constants.Comparer.XIsGreaterThanY;
        assignedResult = true;
      }

      return assignedResult;
    }

    protected abstract bool MeetsRequirements(W wrapper);
  }
}