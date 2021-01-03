using TaleWorlds.Core;
using TaleWorlds.Localization;

using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced
{
  public class NotificationUtils
  {
    public static void DisplayMessage(string message)
    {
      MBInformationManager.DisplayMessage(new MBInformationMessage(message));
    }

    public static void DisplayBannerMessage(string message)
    {
      InformationManager.AddQuickInformation(new TextObject(message));
    }

  }
}
