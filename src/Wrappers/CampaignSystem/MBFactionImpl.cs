using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;
using TournamentsEnhanced.Wrappers.Library;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBFactionImpl : MBObjectBase, IFaction
  {
    private IFaction _faction;
    public IFaction UnwrappedFaction => _faction;

    public IFaction IFactionObject => _faction;

    public TextObject Name => _faction.Name;

    public TextObject InformalName => _faction.InformalName;

    public string EncyclopediaLink => _faction.EncyclopediaLink;

    public TextObject EncyclopediaLinkWithName => _faction.EncyclopediaLinkWithName;

    public TextObject EncyclopediaText => _faction.EncyclopediaText;

    public CultureObject Culture => _faction.Culture;

    public Vec2 InitialPosition => _faction.InitialPosition;

    public uint LabelColor => _faction.LabelColor;

    public uint Color => _faction.Color;

    public uint Color2 => _faction.Color2;

    public uint AlternativeColor => _faction.AlternativeColor;

    public uint AlternativeColor2 => _faction.AlternativeColor2;

    public CharacterObject BasicTroop => _faction.BasicTroop;

    public Hero Leader => _faction.Leader;

    public Banner Banner => _faction.Banner;

    public IEnumerable<Settlement> Settlements => _faction.Settlements;

    public IEnumerable<Town> Fiefs => _faction.Fiefs;

    public IEnumerable<Hero> Lords => _faction.Lords;

    public IEnumerable<Hero> Heroes => _faction.Heroes;

    public IEnumerable<MobileParty> AllParties => _faction.AllParties;

    public IEnumerable<MobileParty> WarParties => _faction.WarParties;

    public bool IsBanditFaction => _faction.IsBanditFaction;

    public bool IsMinorFaction => _faction.IsMinorFaction;

    public bool IsKingdomFaction => _faction.IsKingdomFaction;

    public bool IsRebelClan => _faction.IsRebelClan;

    public bool IsClan => _faction.IsClan;

    public bool IsOutlaw => _faction.IsOutlaw;

    public bool IsMapFaction => _faction.IsMapFaction;

    public IFaction MapFaction => _faction.MapFaction;

    public float TotalStrength => _faction.TotalStrength;

    public Vec2 FactionMidPoint => _faction.FactionMidPoint;

    public IEnumerable<StanceLink> Stances => _faction.Stances;

    public int TributeWallet { get => _faction.TributeWallet; set => _faction.TributeWallet = value; }
    public float MainHeroCrimeRating { get => _faction.MainHeroCrimeRating; set => _faction.MainHeroCrimeRating = value; }

    public float DailyCrimeRatingChange => _faction.DailyCrimeRatingChange;

    public float Aggressiveness => _faction.Aggressiveness;

    public bool IsEliminated => _faction.IsEliminated;

    public StatExplainer DailyCrimeRatingChangeExplained => _faction.DailyCrimeRatingChangeExplained;

    public CampaignTime NotAttackableByPlayerUntilTime { get => _faction.NotAttackableByPlayerUntilTime; set => _faction.NotAttackableByPlayerUntilTime = value; }

    public MBFactionImpl(IFaction faction)
    {
      _faction = faction;
    }

    public bool IsAtWarWith(IFaction other)
    {
      return _faction.IsAtWarWith(other);
    }

    public StanceLink GetStanceWith(IFaction other)
    {
      return _faction.GetStanceWith(other);
    }
  }
}