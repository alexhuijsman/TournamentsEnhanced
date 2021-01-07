using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Finder.Abstract
{
  public abstract class FindResultBase<R, W, L, T> : ResultBase
  where R : FindResultBase<R, W, L, T>, new()
  where W : MBWrapperBase<W, T>, new()
  where L : MBListBase<W, L>
  {
    public L AllQualifiedCandidates { get; protected set; }
    public W Nominee => Failed ? AllQualifiedCandidates[0] : null;

    public static R Success(L nominees) => new R()
    {
      Status = ResultStatus.Success,
      AllQualifiedCandidates = nominees,
    };

    public static R Failure() => new R()
    {
      Status = ResultStatus.Failure
    };

    public FindResultBase() { }
  }
}
