using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TournamentsEnhanced
{
    public class NotificationUtils
    {
        public static void DisplayMessage(string message)
        {
            InformationManager.DisplayMessage(new InformationMessage(message));
        }

        public static void DisplayBannerMessage(string message)
        {
            InformationManager.AddQuickInformation(new TextObject(message));
        }

    }
}
