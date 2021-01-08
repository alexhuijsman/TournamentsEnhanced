using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Finder.Abstract
{
  public abstract class FindResultBase<R, W, L, T> : ResultBase<R>
  where R : FindResultBase<R, W, L, T>, new()
  where W : MBWrapperBase<W, T>, new()
  where L : MBListBase<W, L>
  {
    public L AllQualifiedCandidates { get; protected set; }
    public W Nominee => Failed ? null : AllQualifiedCandidates[0];
    public W RunnerUp => Failed ? null : AllQualifiedCandidates.Count <= 1 ? null : AllQualifiedCandidates[1];
    public bool HasRunnerUp => RunnerUp != null;

    public FindResultBase() { }

    public new static R Success(params W[] nominees) => Success((L)nominees.ToList());

    public new static R Success(L nominees) =>
      new R()
      {
        Status = ResultStatus.Success,
        AllQualifiedCandidates = nominees,
      };

  }
}
