using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Core;
using TournamentsEnhanced.Wrappers.Library;
using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBFaction : MBWrapperBase<MBFaction, MBFactionImpl>
  {
    public IFaction IFactionObject => UnwrappedObject.IFactionObject;

    public List<MBSettlement> Settlements => UnwrappedObject.Settlements.CastList<MBSettlement>();

    public List<MBHero> Lords => UnwrappedObject.Lords.CastList<MBHero>();

    public List<MBMobileParty> WarParties => UnwrappedObject.WarParties.CastList<MBMobileParty>();

    public bool IsBanditFaction => UnwrappedObject.IsBanditFaction;

    public bool IsMinorFaction => UnwrappedObject.IsMinorFaction;

    public bool IsKingdomFaction => UnwrappedObject.IsKingdomFaction;

    public bool IsRebelClan => UnwrappedObject.IsRebelClan;

    public bool IsClan => UnwrappedObject.IsClan;

    public bool IsOutlaw => UnwrappedObject.IsOutlaw;

    public bool IsMapFaction => UnwrappedObject.IsMapFaction;

    public MBFaction MapFaction => (MBFaction)UnwrappedObject.MapFaction;

    public float TotalStrength => UnwrappedObject.TotalStrength;

    public MBVec2 FactionMidPoint => UnwrappedObject.FactionMidPoint;

    public List<MBStanceLink> Stances => UnwrappedObject.Stances.CastList<MBStanceLink>();

    public int TributeWallet { get => UnwrappedObject.TributeWallet; set => UnwrappedObject.TributeWallet = value; }
    public float MainHeroCrimeRating { get => UnwrappedObject.MainHeroCrimeRating; set => UnwrappedObject.MainHeroCrimeRating = value; }

    public float DailyCrimeRatingChange => UnwrappedObject.DailyCrimeRatingChange;

    public float Aggressiveness => UnwrappedObject.Aggressiveness;

    public bool IsEliminated => UnwrappedObject.IsEliminated;

    public List<MBMobileParty> AllParties => UnwrappedObject.AllParties.CastList<MBMobileParty>();

    public List<MBHero> Heroes => UnwrappedObject.Heroes.CastList<MBHero>();

    public CampaignTime NotAttackableByPlayerUntilTime { get => UnwrappedObject.NotAttackableByPlayerUntilTime; set => UnwrappedObject.NotAttackableByPlayerUntilTime = value; }

    public List<MBTown> Fiefs => UnwrappedObject.Fiefs.CastList<MBTown>();

    public MBTextObject Name => UnwrappedObject.Name;

    public string StringId => UnwrappedObject.StringId;

    public MBTextObject InformalName => UnwrappedObject.InformalName;

    public string EncyclopediaLink => UnwrappedObject.EncyclopediaLink;

    public MBTextObject EncyclopediaLinkWithName => UnwrappedObject.EncyclopediaLinkWithName;

    public MBTextObject EncyclopediaText => UnwrappedObject.EncyclopediaText;

    public MBCultureObject Culture => UnwrappedObject.Culture;

    public MBStatExplainer DailyCrimeRatingChangeExplained => UnwrappedObject.DailyCrimeRatingChangeExplained;

    public MBVec2 InitialPosition => UnwrappedObject.InitialPosition;

    public uint Color => UnwrappedObject.Color;

    public uint Color2 => UnwrappedObject.Color2;

    public uint AlternativeColor => UnwrappedObject.AlternativeColor;

    public uint AlternativeColor2 => UnwrappedObject.AlternativeColor2;

    public MBCharacterObject BasicTroop => UnwrappedObject.BasicTroop;

    public MBHero Leader => UnwrappedObject.Leader;

    public MBBanner Banner => UnwrappedObject.Banner;

    public uint LabelColor => UnwrappedObject.LabelColor;

    public MBStanceLink GetStanceWith(MBFaction other)
    {
      return UnwrappedObject.GetStanceWith(other.IFactionObject);
    }

    public bool IsAtWarWith(MBFaction other)
    {
      return UnwrappedObject.IsAtWarWith(other.IFactionObject);
    }

    public static implicit operator MBFactionImpl(MBFaction wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBFaction(MBFactionImpl obj) => GetWrapper(obj);
  }
}
