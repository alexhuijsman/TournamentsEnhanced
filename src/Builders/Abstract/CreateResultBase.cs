using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Finder.Abstract
{
  public abstract class CreateResultBase<R> : ResultBase<R>
  where R : CreateResultBase<R>, new()
  {
    public CreateResultBase() { }
  }
}
