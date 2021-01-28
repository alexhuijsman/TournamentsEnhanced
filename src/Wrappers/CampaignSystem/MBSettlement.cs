using System;
using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Library;
using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBSettlement : MBObjectBaseWrapper<MBSettlement, Settlement>
  {
    public static List<MBSettlement> All => Settlement.All.CastList<MBSettlement>();
    public static MBSettlement CurrentSettlement => Settlement.CurrentSettlement;

    public static MBSettlement Find(string stringId) => Settlement.Find(stringId);

    public virtual MBTown Town => UnwrappedObject.Town;
    public virtual bool IsTown => UnwrappedObject.IsTown;
    public virtual MBTextObject Name => UnwrappedObject.Name;
    public virtual List<MBHero> Notables => UnwrappedObject.Notables.CastList<MBHero>();
    public virtual IMBFaction MapFaction => UnwrappedObject.MapFaction.ToIMBFaction();
    public virtual MBClan OwnerClan => UnwrappedObject.OwnerClan;
    public virtual float Prosperity { get => UnwrappedObject.Prosperity; set => UnwrappedObject.Prosperity = value; }

    public static List<MBSettlement> FindSettlementsAroundPosition(MBVec2 position, float radius, Func<Settlement, bool> condition = null) => (List<MBSettlement>)Settlement.FindSettlementsAroundPosition(position, radius, condition);

    public static implicit operator Settlement(MBSettlement wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBSettlement(Settlement obj) => GetWrapper(obj);
  }
}
