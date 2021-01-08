using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Abstract
{
  public abstract class ComparerBase<W> : IComparer<W>
    where W : WrapperBase
  {
    protected static readonly int XIsGreaterThanY = 1;
    protected static readonly int XIsEqualToY = 0;
    protected static readonly int XIsLessThanY = -1;

    protected MBHero InitiatingHero { get; private set; }
    protected bool WasInitiatedByHero => InitiatingHero.IsNull;
    public ComparerBase(MBHero initiatingHero)
    {
      InitiatingHero = initiatingHero;
    }

    public virtual int Compare(W x, W y)
    {
      var result = 0;

      var wasResultAssigned = TryComparePreconditions(x, y, ref result);

      return result;
    }

    protected abstract bool MeetsRequirements(W wrapper);

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
        result = y.IsNull ? XIsEqualToY : yMeetsRequirements ? XIsLessThanY : XIsGreaterThanY;
        assignedResult = true;
      }

      if (y.IsNull)
      {
        result = xMeetsRequirements ? XIsGreaterThanY : XIsLessThanY;
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
        result = !yMeetsRequirements ? XIsEqualToY : XIsLessThanY;
        assignedResult = true;
      }
      else if (!yMeetsRequirements)
      {
        result = XIsGreaterThanY;
        assignedResult = true;
      }

      return assignedResult;
    }
  }
}