using TournamentsEnhanced.Finder.Abstract;

namespace TournamentsEnhanced.Builders.Abstract
{
  public abstract class CreateResultBase<R> : ResultBase<R>
  where R : CreateResultBase<R>, new()
  {
    public CreateResultBase() { }
  }
}
