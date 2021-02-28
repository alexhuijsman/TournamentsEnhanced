using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.Core
{
  public static class MBInformationManagerFacade
  {
    public static void DisplayAsLogEntry(string message)
    {
      MBInformationManager.DisplayMessage(new MBInformationMessage(message));
    }

    public static void DisplayAsQuickBanner(string message)
    {
      MBInformationManager.AddQuickInformation(new MBTextObject(message));
    }
  }
}