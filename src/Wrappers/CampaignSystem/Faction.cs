using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

using TournamentsEnhanced.Wrappers.Core;
using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class Faction : IMBFaction
  {
    private IFaction _faction;
    public MBHeroList Lords => _faction.Lords.ToList();
    public MBMobilePartyList WarParties => _faction.WarParties.ToList();
    public bool IsBanditFaction => _faction.IsBanditFaction;
    public bool IsMinorFaction => _faction.IsMinorFaction;
    public bool IsKingdomFaction => _faction.IsKingdomFaction;
    public bool IsRebelClan => _faction.IsRebelClan;
    public bool IsClan => _faction.IsClan;
    public bool IsOutlaw => _faction.IsOutlaw;
    public bool IsMapFaction => _faction.IsMapFaction;
    public IMBFaction MapFaction => _faction.MapFaction.ToIMBFaction();
    public float TotalStrength => _faction.TotalStrength;
    public Vec2 FactionMidPoint => _faction.FactionMidPoint;
    public MBStanceLinkList Stances => _faction.Stances.ToList();
    public int TributeWallet { get => _faction.TributeWallet; set => _faction.TributeWallet = value; }
    float MainHeroCrimeRating { get => _faction.MainHeroCrimeRating; set => _faction.MainHeroCrimeRating = value; }
    public float DailyCrimeRatingChange => _faction.DailyCrimeRatingChange;
    public float Aggressiveness => _faction.Aggressiveness;
    public bool IsEliminated => _faction.IsEliminated;
    public MBMobilePartyList AllParties => _faction.AllParties.ToList();
    public MBHeroList Heroes => _faction.Heroes.ToList();
    public CampaignTime NotAttackableByPlayerUntilTime { get => _faction.NotAttackableByPlayerUntilTime; set => _faction.NotAttackableByPlayerUntilTime = value; }
    public MBTownList Fiefs => _faction.Fiefs.ToList();
    public MBTextObject Name => _faction.Name;
    public string StringId => _faction.StringId;
    public MBTextObject InformalName => _faction.InformalName;
    public string EncyclopediaLink => _faction.EncyclopediaLink;
    public MBTextObject EncyclopediaLinkWithName => _faction.EncyclopediaLinkWithName;
    public MBTextObject EncyclopediaText => _faction.EncyclopediaText;
    public MBCultureObject Culture => _faction.Culture;
    public MBStatExplainer DailyCrimeRatingChangeExplained => _faction.DailyCrimeRatingChangeExplained;
    public Vec2 InitialPosition => _faction.InitialPosition;
    public uint Color => _faction.Color;
    public uint Color2 => _faction.Color2;
    public uint AlternativeColor => _faction.AlternativeColor;
    public uint AlternativeColor2 => _faction.AlternativeColor2;
    public MBCharacterObject BasicTroop => _faction.BasicTroop;
    public MBHero Leader => _faction.Leader;
    public MBBanner Banner => _faction.Banner;
    public MBSettlementList Settlements => _faction.Settlements.ToList();
    public uint LabelColor => _faction.LabelColor;

    public MBStanceLink GetStanceWith(IMBFaction other) => _faction.GetStanceWith(other);
    public bool IsAtWarWith(IMBFaction other) => _faction.IsAtWarWith(other);
  }
}