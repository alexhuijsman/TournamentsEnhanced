using System.Collections.Generic;

using TaleWorlds.CampaignSystem.GameMenus;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBGameMenuOption : MBWrapperBase<MBGameMenuOption, GameMenuOption>
  {
    public static implicit operator GameMenuOption(MBGameMenuOption wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBGameMenuOption(GameMenuOption obj) => MBGameMenuOption.GetWrapper(obj);
  }

  public class MBGameMenuOptionList : MBListBase<MBGameMenuOption, MBGameMenuOptionList>
  {
    public MBGameMenuOptionList(params MBGameMenuOption[] wrappers) : this((IEnumerable<MBGameMenuOption>)wrappers) { }
    public MBGameMenuOptionList(IEnumerable<MBGameMenuOption> wrappers) => AddRange(wrappers);
    public MBGameMenuOptionList(MBGameMenuOption wrapper) => Add(wrapper);
    public MBGameMenuOptionList() { }

    public static implicit operator List<GameMenuOption>(MBGameMenuOptionList wrapperList) => wrapperList.Unwrap<MBGameMenuOption, GameMenuOption>();
    public static implicit operator MBGameMenuOptionList(List<GameMenuOption> objectList) => (MBGameMenuOptionList)objectList.Wrap<MBGameMenuOption, GameMenuOption>();
    public static implicit operator MBGameMenuOption[](MBGameMenuOptionList wrapperList) => wrapperList.ToArray();
  }
}
