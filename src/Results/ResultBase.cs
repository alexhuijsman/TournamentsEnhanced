using System.Xml;
namespace TournamentsEnhanced
{
    public interface IResultBase
    {
        ResultStatus Status { get; }
        bool Failed { get; }
        bool Succeeded { get; }
    }

    public abstract class ResultBase : IResultBase
    {
        public ResultStatus Status { get; private set; }
        public bool Failed => Status == ResultStatus.Failure;
        public bool Succeeded => Status == ResultStatus.Success;

        protected ResultBase(ResultStatus status)
        {
            Status = status;
        }
    }
}
