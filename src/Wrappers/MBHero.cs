using TaleWorlds.CampaignSystem;

using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced.Wrappers
{
  public class MBHero : CachedWrapper<MBHero, Hero>
  {
    public static MBHero MainHero => Hero.MainHero;

    public MBClan Clan => UnwrappedObject.Clan;
    public MBTown CurrentTown => UnwrappedObject.CurrentSettlement?.Town;
    public ItemTiers GetTournamentRewardTier()
    {
      ItemTiers itemTier;
      if (Clan.Renown <= 300)
      {
        itemTier = ItemTiers.Tier4;
      }
      else if (Clan.Renown <= 600)
      {
        itemTier = ItemTiers.Tier5;
      }
      else
      {
        itemTier = ItemTiers.Tier6;
      }

      return itemTier;
    }

    public static implicit operator Hero(MBHero wrapper) => wrapper.Unwrap();
    public static implicit operator MBHero(Hero obj) => MBHero.GetWrapperFor(obj);
  }
}
