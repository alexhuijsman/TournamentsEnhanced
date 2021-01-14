using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public interface IMBItemRosterElement : IStructWrapperBase<ItemRosterElement>
  {
    MBEquipmentElement EquipmentElement { get; }
    int Amount { get; }
    bool IsEmpty { get; }

    void Clear();
    float GetRosterElementWeight();
  }
}