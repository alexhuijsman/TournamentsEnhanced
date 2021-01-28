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
    public virtual IFaction IFactionObject => UnwrappedObject.IFactionObject;

    public virtual List<MBSettlement> Settlements => UnwrappedObject.Settlements.CastList<MBSettlement>();

    public virtual List<MBHero> Lords => UnwrappedObject.Lords.CastList<MBHero>();

    public virtual List<MBMobileParty> WarParties => UnwrappedObject.WarParties.CastList<MBMobileParty>();

    public virtual bool IsBanditFaction => UnwrappedObject.IsBanditFaction;

    public virtual bool IsMinorFaction => UnwrappedObject.IsMinorFaction;

    public virtual bool IsKingdomFaction => UnwrappedObject.IsKingdomFaction;

    public virtual bool IsRebelClan => UnwrappedObject.IsRebelClan;

    public virtual bool IsClan => UnwrappedObject.IsClan;

    public virtual bool IsOutlaw => UnwrappedObject.IsOutlaw;

    public virtual bool IsMapFaction => UnwrappedObject.IsMapFaction;

    public virtual MBFaction MapFaction => (MBFaction)UnwrappedObject.MapFaction;

    public virtual float TotalStrength => UnwrappedObject.TotalStrength;

    public MBVec2 FactionMidPoint => UnwrappedObject.FactionMidPoint;

    public virtual List<MBStanceLink> Stances => UnwrappedObject.Stances.CastList<MBStanceLink>();

    public virtual int TributeWallet { get => UnwrappedObject.TributeWallet; set => UnwrappedObject.TributeWallet = value; }
    public virtual float MainHeroCrimeRating { get => UnwrappedObject.MainHeroCrimeRating; set => UnwrappedObject.MainHeroCrimeRating = value; }

    public virtual float DailyCrimeRatingChange => UnwrappedObject.DailyCrimeRatingChange;

    public virtual float Aggressiveness => UnwrappedObject.Aggressiveness;

    public virtual bool IsEliminated => UnwrappedObject.IsEliminated;

    public virtual List<MBMobileParty> AllParties => UnwrappedObject.AllParties.CastList<MBMobileParty>();

    public virtual List<MBHero> Heroes => UnwrappedObject.Heroes.CastList<MBHero>();

    public virtual CampaignTime NotAttackableByPlayerUntilTime { get => UnwrappedObject.NotAttackableByPlayerUntilTime; set => UnwrappedObject.NotAttackableByPlayerUntilTime = value; }

    public virtual List<MBTown> Fiefs => UnwrappedObject.Fiefs.CastList<MBTown>();

    public virtual MBTextObject Name => UnwrappedObject.Name;

    public virtual string StringId => UnwrappedObject.StringId;

    public virtual MBTextObject InformalName => UnwrappedObject.InformalName;

    public virtual string EncyclopediaLink => UnwrappedObject.EncyclopediaLink;

    public virtual MBTextObject EncyclopediaLinkWithName => UnwrappedObject.EncyclopediaLinkWithName;

    public virtual MBTextObject EncyclopediaText => UnwrappedObject.EncyclopediaText;

    public virtual MBCultureObject Culture => UnwrappedObject.Culture;

    public virtual MBExplainedNumber DailyCrimeRatingChangeExplained => UnwrappedObject.DailyCrimeRatingChangeExplained;

    public MBVec2 InitialPosition => UnwrappedObject.InitialPosition;

    public virtual uint Color => UnwrappedObject.Color;

    public virtual uint Color2 => UnwrappedObject.Color2;

    public virtual uint AlternativeColor => UnwrappedObject.AlternativeColor;

    public virtual uint AlternativeColor2 => UnwrappedObject.AlternativeColor2;

    public virtual MBCharacterObject BasicTroop => UnwrappedObject.BasicTroop;

    public virtual MBHero Leader => UnwrappedObject.Leader;

    public virtual MBBanner Banner => UnwrappedObject.Banner;

    public virtual uint LabelColor => UnwrappedObject.LabelColor;

    public virtual MBStanceLink GetStanceWith(MBFaction other)
    {
      return UnwrappedObject.GetStanceWith(other.IFactionObject);
    }

    public virtual bool IsAtWarWith(MBFaction other)
    {
      return UnwrappedObject.IsAtWarWith(other.IFactionObject);
    }

    public static implicit operator MBFactionImpl(MBFaction wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBFaction(MBFactionImpl obj) => GetWrapper(obj);
  }
}
