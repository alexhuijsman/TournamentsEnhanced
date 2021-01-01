using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced.Wrappers
{
  public class MBHero : CachedWrapperBase<MBHero, Hero>
  {
    public static MBHero MainHero => Hero.MainHero;

    public static ItemTiers GetMainHeroTournamentRewardTier()
    {
      return MainHero.GetTournamentRewardTier();
    }

    public MBClan Clan => UnwrapedObject.Clan;
    public MBTown CurrentTown => UnwrapedObject.CurrentSettlement?.Town;
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

    public static implicit operator Hero(MBHero wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBHero(Hero obj) => MBHero.GetWrapperFor(obj);
  }

  public class MBHeroList : List<MBHero>
  {
    public static implicit operator List<Hero>(MBHeroList wrapperList) => wrapperList.Unwrap<MBHero, Hero>();
    public static implicit operator MBHeroList(List<Hero> objectList) => (MBHeroList)objectList.Wrap<MBHero, Hero>();
  }
}
