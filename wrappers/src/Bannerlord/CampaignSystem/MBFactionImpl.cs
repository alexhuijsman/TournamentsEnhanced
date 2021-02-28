using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBFactionImpl : MBObjectBase, IFaction
  {
    private IFaction _faction;
    public virtual IFaction UnwrappedFaction => _faction;

    public virtual IFaction IFactionObject => _faction;

    public virtual TextObject Name => _faction.Name;

    public virtual TextObject InformalName => _faction.InformalName;

    public virtual string EncyclopediaLink => _faction.EncyclopediaLink;

    public virtual TextObject EncyclopediaLinkWithName => _faction.EncyclopediaLinkWithName;

    public virtual TextObject EncyclopediaText => _faction.EncyclopediaText;

    public virtual CultureObject Culture => _faction.Culture;

    public Vec2 InitialPosition => _faction.InitialPosition;

    public virtual uint LabelColor => _faction.LabelColor;

    public virtual uint Color => _faction.Color;

    public virtual uint Color2 => _faction.Color2;

    public virtual uint AlternativeColor => _faction.AlternativeColor;

    public virtual uint AlternativeColor2 => _faction.AlternativeColor2;

    public virtual CharacterObject BasicTroop => _faction.BasicTroop;

    public virtual Hero Leader => _faction.Leader;

    public virtual Banner Banner => _faction.Banner;

    public virtual MBReadOnlyList<Settlement> Settlements => _faction.Settlements;

    public virtual MBReadOnlyList<Town> Fiefs => _faction.Fiefs;

    public virtual MBReadOnlyList<Hero> Lords => _faction.Lords;

    public virtual MBReadOnlyList<Hero> Heroes => _faction.Heroes;

    public virtual bool IsBanditFaction => _faction.IsBanditFaction;

    public virtual bool IsMinorFaction => _faction.IsMinorFaction;

    public virtual bool IsKingdomFaction => _faction.IsKingdomFaction;

    public virtual bool IsRebelClan => _faction.IsRebelClan;

    public virtual bool IsClan => _faction.IsClan;

    public virtual bool IsOutlaw => _faction.IsOutlaw;

    public virtual bool IsMapFaction => _faction.IsMapFaction;

    public virtual IFaction MapFaction => _faction.MapFaction;

    public virtual float TotalStrength => _faction.TotalStrength;

    public Vec2 FactionMidPoint => _faction.FactionMidPoint;

    public virtual IEnumerable<StanceLink> Stances => _faction.Stances;

    public virtual int TributeWallet { get => _faction.TributeWallet; set => _faction.TributeWallet = value; }
    public virtual float MainHeroCrimeRating { get => _faction.MainHeroCrimeRating; set => _faction.MainHeroCrimeRating = value; }

    public virtual float DailyCrimeRatingChange => _faction.DailyCrimeRatingChange;

    public virtual float Aggressiveness => _faction.Aggressiveness;

    public virtual bool IsEliminated => _faction.IsEliminated;

    public virtual ExplainedNumber DailyCrimeRatingChangeExplained => _faction.DailyCrimeRatingChangeExplained;

    public virtual CampaignTime NotAttackableByPlayerUntilTime { get => _faction.NotAttackableByPlayerUntilTime; set => _faction.NotAttackableByPlayerUntilTime = value; }

    public MBReadOnlyList<WarPartyComponent> WarPartyComponents => _faction.WarPartyComponents;

    public MBFactionImpl(IFaction faction)
    {
      _faction = faction;
    }

    public virtual bool IsAtWarWith(IFaction other)
    {
      return _faction.IsAtWarWith(other);
    }

    public virtual StanceLink GetStanceWith(IFaction other)
    {
      return _faction.GetStanceWith(other);
    }
  }
}