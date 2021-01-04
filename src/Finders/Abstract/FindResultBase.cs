using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Finder.Abstract
{
  public abstract class FindResultBase<R, W, L, T> : ResultBase
  where R : FindResultBase<R, W, L, T>, new()
  where W : MBWrapperBase<W, T>, new()
  where L : MBListBase<W, L>
  {
    public W Nominee { get; protected set; }

    public static R Success(W nominee) => new R() { Status = ResultStatus.Success, Nominee = nominee };
    public static R Failure() => new R() { Status = ResultStatus.Failure };

    public FindResultBase() { }
  }
}
