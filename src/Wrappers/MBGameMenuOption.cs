using TaleWorlds.CampaignSystem.GameMenus;

using TournamentsEnhanced.Wrappers.Abstract;

using static TaleWorlds.CampaignSystem.GameMenus.GameMenuOption;

namespace TournamentsEnhanced.Wrappers
{
  public class MBGameMenuOption : CachedWrapperBase<MBGameMenuOption, GameMenuOption>
  {
    public static implicit operator GameMenuOption(MBGameMenuOption wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBGameMenuOption(GameMenuOption obj) => MBGameMenuOption.GetWrapperFor(obj);
  }
}
