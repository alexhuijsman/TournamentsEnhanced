namespace TournamentsEnhanced.Wrappers.Abstract
{

  public interface IStructWrapperBase<T>
  where T : struct
  {
    T UnwrappedStruct { get; set; }
  }
}