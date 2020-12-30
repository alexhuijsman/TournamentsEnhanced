using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public struct Payor
  {
    public bool IsTown => Town != null;
    public bool IsHero => Hero != null;
    public bool IsNoPayor => !IsTown && !IsHero;

    public Town Town { get; private set; }
    public Hero Hero { get; private set; }
    public static Payor NoPayor => new Payor((Hero)null);

    public Payor(Town town)
    {
      Town = town;
      Hero = null;
    }

    public Payor(Hero hero)
    {
      Town = null;
      Hero = hero;
    }

    public void PayForTournament()
    {
      if (IsTown)
      {
        // charge town
      }
      else if (IsHero)
      {
        // charge hero
      }
    }
  }
}