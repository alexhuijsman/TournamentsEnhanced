using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

using TournamentsEnhanced.Wrappers.Core;
using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public interface IMBFaction
  {
    MBHeroList Lords { get; }
    MBMobilePartyList WarParties { get; }
    bool IsBanditFaction { get; }
    bool IsMinorFaction { get; }
    bool IsKingdomFaction { get; }
    bool IsRebelClan { get; }
    bool IsClan { get; }
    bool IsOutlaw { get; }
    bool IsMapFaction { get; }
    IMBFaction MapFaction { get; }
    float TotalStrength { get; }
    Vec2 FactionMidPoint { get; }
    MBStanceLinkList Stances { get; }
    int TributeWallet { get; set; }
    float MainHeroCrimeRating { get; set; }
    float DailyCrimeRatingChange { get; }
    float Aggressiveness { get; }
    bool IsEliminated { get; }
    MBMobilePartyList AllParties { get; }
    MBHeroList Heroes { get; }
    CampaignTime NotAttackableByPlayerUntilTime { get; set; }
    MBTownList Fiefs { get; }
    MBTextObject Name { get; }
    string StringId { get; }
    MBTextObject InformalName { get; }
    string EncyclopediaLink { get; }
    MBTextObject EncyclopediaLinkWithName { get; }
    MBTextObject EncyclopediaText { get; }
    CultureObject Culture { get; }
    StatExplainer DailyCrimeRatingChangeExplained { get; }
    Vec2 InitialPosition { get; }
    uint Color { get; }
    uint Color2 { get; }
    uint AlternativeColor { get; }
    uint AlternativeColor2 { get; }
    CharacterObject BasicTroop { get; }
    Hero Leader { get; }
    MBBanner Banner { get; }
    MBSettlementList Settlements { get; }
    uint LabelColor { get; }

    MBStanceLink GetStanceWith(IMBFaction other);
    bool IsAtWarWith(IMBFaction other);
  }
}