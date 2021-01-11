using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Core;
using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBFaction : MBWrapperBase<MBFaction, MBFactionImpl>
  {
    public IFaction IFactionObject => UnwrappedObject.IFactionObject;

    public MBSettlementList Settlements => UnwrappedObject.Settlements.ToList();

    public MBHeroList Lords => UnwrappedObject.Lords.ToList();

    public MBMobilePartyList WarParties => UnwrappedObject.WarParties.ToList();

    public bool IsBanditFaction => UnwrappedObject.IsBanditFaction;

    public bool IsMinorFaction => UnwrappedObject.IsMinorFaction;

    public bool IsKingdomFaction => UnwrappedObject.IsKingdomFaction;

    public bool IsRebelClan => UnwrappedObject.IsRebelClan;

    public bool IsClan => UnwrappedObject.IsClan;

    public bool IsOutlaw => UnwrappedObject.IsOutlaw;

    public bool IsMapFaction => UnwrappedObject.IsMapFaction;

    public MBFaction MapFaction => (MBFaction)UnwrappedObject.MapFaction;

    public float TotalStrength => UnwrappedObject.TotalStrength;

    public Vec2 FactionMidPoint => UnwrappedObject.FactionMidPoint;

    public MBStanceLinkList Stances => UnwrappedObject.Stances.ToList();

    public int TributeWallet { get => UnwrappedObject.TributeWallet; set => UnwrappedObject.TributeWallet = value; }
    public float MainHeroCrimeRating { get => UnwrappedObject.MainHeroCrimeRating; set => UnwrappedObject.MainHeroCrimeRating = value; }

    public float DailyCrimeRatingChange => UnwrappedObject.DailyCrimeRatingChange;

    public float Aggressiveness => UnwrappedObject.Aggressiveness;

    public bool IsEliminated => UnwrappedObject.IsEliminated;

    public MBMobilePartyList AllParties => UnwrappedObject.AllParties.ToList();

    public MBHeroList Heroes => UnwrappedObject.Heroes.ToList();

    public CampaignTime NotAttackableByPlayerUntilTime { get => UnwrappedObject.NotAttackableByPlayerUntilTime; set => UnwrappedObject.NotAttackableByPlayerUntilTime = value; }

    public MBTownList Fiefs => UnwrappedObject.Fiefs.ToList();

    public MBTextObject Name => UnwrappedObject.Name;

    public string StringId => UnwrappedObject.StringId;

    public MBTextObject InformalName => UnwrappedObject.InformalName;

    public string EncyclopediaLink => UnwrappedObject.EncyclopediaLink;

    public MBTextObject EncyclopediaLinkWithName => UnwrappedObject.EncyclopediaLinkWithName;

    public MBTextObject EncyclopediaText => UnwrappedObject.EncyclopediaText;

    public MBCultureObject Culture => UnwrappedObject.Culture;

    public MBStatExplainer DailyCrimeRatingChangeExplained => UnwrappedObject.DailyCrimeRatingChangeExplained;

    public Vec2 InitialPosition => UnwrappedObject.InitialPosition;

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
    public static implicit operator MBFaction(MBFactionImpl obj) => MBFaction.GetWrapper(obj);
  }

  public class MBFactionList : MBListBase<MBFaction, MBFactionList>
  {
    public MBFactionList(params MBFaction[] wrappers) : this((IEnumerable<MBFaction>)wrappers) { }
    public MBFactionList(IEnumerable<MBFaction> wrappers) => AddRange(wrappers);
    public MBFactionList(MBFaction wrapper) => Add(wrapper);
    public MBFactionList() { }

    public static implicit operator List<MBFactionImpl>(MBFactionList wrapperList) => wrapperList.Unwrap<MBFaction, MBFactionImpl>();
    public static implicit operator MBFactionList(List<MBFactionImpl> objectList) => (MBFactionList)objectList.Wrap<MBFaction, MBFactionImpl>();
    public static implicit operator MBFaction[](MBFactionList wrapperList) => wrapperList.ToArray();
  }
}