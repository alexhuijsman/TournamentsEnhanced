using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBInformationManagerFacade
  {
    public static MBInformationManagerFacade Instance { get; } = new MBInformationManagerFacade();
    public virtual void DisplayAsLogEntry(string message)
    {
      MBInformationManager.DisplayMessage(new MBInformationMessage(message));
    }

    public virtual void DisplayAsQuickBanner(string message)
    {
      MBInformationManager.AddQuickInformation(new MBTextObject(message));
    }
  }
}