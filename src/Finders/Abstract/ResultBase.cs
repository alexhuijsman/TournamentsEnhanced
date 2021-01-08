using System.Xml;
namespace TournamentsEnhanced.Finder.Abstract
{
  public abstract class ResultBase
  {
    public ResultStatus Status { get; protected set; }
    public bool Failed => Status == ResultStatus.Failure;
    public bool Succeeded => Status == ResultStatus.Success;

    public ResultBase() { }
  }

  public abstract class ResultBase<R> : ResultBase
    where R : ResultBase<R>, new()
  {
    protected static readonly R FailureResult = new R() { Status = ResultStatus.Failure };
    protected static readonly R SuccessResult = new R() { Status = ResultStatus.Success };

    public static R Failure => FailureResult;
    public static R Success => SuccessResult;

    public ResultBase() { }
  }
}
