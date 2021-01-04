namespace TournamentsEnhanced.Finder.Abstract
{
  public abstract class ResultBase
  {
    public ResultStatus Status { get; protected set; }
    public bool Failed => Status == ResultStatus.Failure;
    public bool Succeeded => Status == ResultStatus.Success;

    public ResultBase() { }
  }
}
