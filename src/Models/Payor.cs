using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Models.Serializable
{
  public struct Payor
  {
    public bool IsSettlement => Settlement != null;
    public bool IsHero => Hero != null;
    public bool IsNoPayor => !IsSettlement && !IsHero;

    public MBSettlement Settlement { get; private set; }
    public MBHero Hero { get; private set; }
    public static Payor NoPayor => new Payor((Hero)null);

    public string StringId => IsNoPayor ? null : IsHero ? Hero.StringId : Settlement.StringId;

    public Payor(MBSettlement settlement)
    {
      Settlement = settlement;
      Hero = null;
    }

    public Payor(MBHero hero)
    {
      Settlement = null;
      Hero = hero;
    }
  }
}
