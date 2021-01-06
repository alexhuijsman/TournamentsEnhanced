using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Finder.Abstract
{
  public abstract class CreateResultBase<R> : ResultBase
  where R : CreateResultBase<R>, new()
  {

    public static R Success() => new R() { Status = ResultStatus.Success };
    public static R Failure() => new R() { Status = ResultStatus.Failure };

    public CreateResultBase() { }
  }
}
