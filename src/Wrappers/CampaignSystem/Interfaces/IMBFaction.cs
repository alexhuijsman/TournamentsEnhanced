using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Core;
using TournamentsEnhanced.Wrappers.Library;
using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public interface IMBFaction
  {
    List<MBHero> Lords { get; }
    List<MBMobileParty> WarParties { get; }
    bool IsBanditFaction { get; }
    bool IsMinorFaction { get; }
    bool IsKingdomFaction { get; }
    bool IsRebelClan { get; }
    bool IsClan { get; }
    bool IsOutlaw { get; }
    bool IsMapFaction { get; }
    IMBFaction MapFaction { get; }
    float TotalStrength { get; }
    MBVec2 FactionMidPoint { get; }
    List<MBStanceLink> Stances { get; }
    int TributeWallet { get; set; }
    float MainHeroCrimeRating { get; set; }
    float DailyCrimeRatingChange { get; }
    float Aggressiveness { get; }
    bool IsEliminated { get; }
    List<MBMobileParty> AllParties { get; }
    List<MBHero> Heroes { get; }
    CampaignTime NotAttackableByPlayerUntilTime { get; set; }
    List<MBTown> Fiefs { get; }
    MBTextObject Name { get; }
    string StringId { get; }
    MBTextObject InformalName { get; }
    string EncyclopediaLink { get; }
    MBTextObject EncyclopediaLinkWithName { get; }
    MBTextObject EncyclopediaText { get; }
    MBCultureObject Culture { get; }
    MBStatExplainer DailyCrimeRatingChangeExplained { get; }
    MBVec2 InitialPosition { get; }
    uint Color { get; }
    uint Color2 { get; }
    uint AlternativeColor { get; }
    uint AlternativeColor2 { get; }
    MBCharacterObject BasicTroop { get; }
    MBHero Leader { get; }
    MBBanner Banner { get; }
    List<MBSettlement> Settlements { get; }
    uint LabelColor { get; }

    MBStanceLink GetStanceWith(IMBFaction other);
    bool IsAtWarWith(IMBFaction other);
  }
}