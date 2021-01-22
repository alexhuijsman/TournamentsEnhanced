using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Finder.Abstract
{
  public abstract class FindResultBase<R, W, T> : ResultBase<R>
  where R : FindResultBase<R, W, T>, new()
  where W : MBWrapperBase<W, T>, new()
  where T : class
  {
    public List<W> AllQualifiedCandidates { get; protected set; }
    public W Nominee => Failed ? MBWrapperBase<W, T>.Null : AllQualifiedCandidates[0];
    public W RunnerUp => Failed ? MBWrapperBase<W, T>.Null : AllQualifiedCandidates.Count <= 1 ? MBWrapperBase<W, T>.Null : AllQualifiedCandidates[1];
    public bool HasRunnerUp => RunnerUp != MBWrapperBase<W, T>.Null;

    public FindResultBase() { }

    public new static R Success(params W[] nominees) => Success(nominees.ToList());

    public new static R Success(List<W> nominees) =>
      new R()
      {
        Status = ResultStatus.Success,
        AllQualifiedCandidates = nominees,
      };
  }
}
