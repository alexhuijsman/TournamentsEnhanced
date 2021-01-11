using System;
using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBSettlement : MBObjectBaseWrapper<MBSettlement, Settlement>
  {
    public static List<MBSettlement> All => Settlement.All.CastList<MBSettlement>();
    public static MBSettlement CurrentSettlement => Settlement.CurrentSettlement;

    public static MBSettlement Find(string stringId) => Settlement.Find(stringId);

    public MBTown Town => UnwrappedObject.Town;
    public bool IsTown => UnwrappedObject.IsTown;
    public MBTextObject Name => UnwrappedObject.Name;
    public List<MBHero> Notables => UnwrappedObject.Notables.CastList<MBHero>();
    public IMBFaction MapFaction => UnwrappedObject.MapFaction.ToIMBFaction();
    public MBClan OwnerClan => UnwrappedObject.OwnerClan;
    public float Prosperity { get => UnwrappedObject.Prosperity; set => UnwrappedObject.Prosperity = value; }

    public static List<MBSettlement> FindSettlementsAroundPosition(TaleWorlds.Library.Vec2 position, float radius, Func<Settlement, bool> condition = null) => (List<MBSettlement>)Settlement.FindSettlementsAroundPosition(position, radius, condition);

    public static implicit operator Settlement(MBSettlement wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBSettlement(Settlement obj) => MBSettlement.GetWrapper(obj);
  }
}
