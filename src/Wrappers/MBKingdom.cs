using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace TournamentsEnhanced.Wrappers
{
  public class MBKingdom : CachedWrapper<MBKingdom, Kingdom>
  {
    public MBTextObject Name => UnwrappedObject.Name;

    public string StringId => UnwrappedObject.StringId;

    public MBTextObject InformalName => UnwrappedObject.InformalName;

    public string EncyclopediaLink => UnwrappedObject.EncyclopediaLink;

    public MBTextObject EncyclopediaLinkWithName => UnwrappedObject.EncyclopediaLinkWithName;

    public MBTextObject EncyclopediaText => UnwrappedObject.EncyclopediaText;

    public MBCultureObject Culture => UnwrappedObject.Culture;

    public Vec2 InitialPosition => UnwrappedObject.InitialPosition;

    public uint LabelColor => UnwrappedObject.LabelColor;

    public uint Color => UnwrappedObject.Color;

    public uint Color2 => UnwrappedObject.Color2;

    public uint AlternativeColor => UnwrappedObject.AlternativeColor;

    public uint AlternativeColor2 => UnwrappedObject.AlternativeColor2;

    public MBCharacterObject BasicTroop => UnwrappedObject.BasicTroop;

    public MBHero Leader => UnwrappedObject.Leader;

    public MBBanner Banner => UnwrappedObject.Banner;

    public IEnumerable<MBSettlement> Settlements => UnwrappedObject.Settlements.WrapAll<Settlement, MBSettlement>();

    public IEnumerable<MBTown> Fiefs => UnwrappedObject.Fiefs;

    public IEnumerable<MBHero> Lords => UnwrappedObject.Lords;

    public IEnumerable<MBHero> Heroes => UnwrappedObject.Heroes;

    public IEnumerable<MBMobileParty> AllParties => UnwrappedObject.AllParties;

    public IEnumerable<MBMobileParty> WarParties => UnwrappedObject.WarParties;

    public bool IsBanditFaction => UnwrappedObject.IsBanditFaction;

    public bool IsMinorFaction => UnwrappedObject.IsMinorFaction;

    public bool IsKingdomFaction => UnwrappedObject.IsKingdomFaction;

    public bool IsRebelClan => UnwrappedObject.IsRebelClan;

    public bool IsClan => UnwrappedObject.IsClan;

    public bool IsOutlaw => UnwrappedObject.IsOutlaw;

    public bool IsMapFaction => UnwrappedObject.IsMapFaction;

    public IFaction MapFaction => UnwrappedObject.MapFaction;

    public float TotalStrength => UnwrappedObject.TotalStrength;

    public Vec2 FactionMidPoint => UnwrappedObject.FactionMidPoint;

    public IEnumerable<StanceLink> Stances => UnwrappedObject.Stances;

    public int TributeWallet { get => UnwrappedObject.TributeWallet; set => UnwrappedObject.TributeWallet = value; }

    public float MainHeroCrimeRating { get => UnwrappedObject.MainHeroCrimeRating; set => UnwrappedObject.MainHeroCrimeRating = value; }

    public float DailyCrimeRatingChange => UnwrappedObject.DailyCrimeRatingChange;

    public float Aggressiveness => UnwrappedObject.Aggressiveness;

    public bool IsEliminated => UnwrappedObject.IsEliminated;

    public StatExplainer DailyCrimeRatingChangeExplained => UnwrappedObject.DailyCrimeRatingChangeExplained;

    public CampaignTime NotAttackableByPlayerUntilTime { get => UnwrappedObject.NotAttackableByPlayerUntilTime; set => UnwrappedObject.NotAttackableByPlayerUntilTime = value; }

    public StanceLink GetStanceWith(IFaction other) => UnwrappedObject.GetStanceWith(other);

    public bool IsAtWarWith(IFaction other) => UnwrappedObject.IsAtWarWith(other);

    public static implicit operator Kingdom(MBKingdom wrapper) => wrapper.Unwrap();
    public static implicit operator MBKingdom(Kingdom obj) => MBKingdom.GetWrapperFor(obj);
  }
}
