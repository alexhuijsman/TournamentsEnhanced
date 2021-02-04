using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Core;
using TournamentsEnhanced.Wrappers.Library;
using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBClan : MBObjectBaseWrapper<MBClan, Clan>, IMBFaction
  {
    public virtual float Renown => UnwrappedObject.Renown;

    public virtual MBTextObject Name => UnwrappedObject.Name;

    public virtual MBTextObject InformalName => UnwrappedObject.InformalName;

    public virtual string EncyclopediaLink => UnwrappedObject.EncyclopediaLink;

    public virtual MBTextObject EncyclopediaLinkWithName => UnwrappedObject.EncyclopediaLinkWithName;

    public virtual MBTextObject EncyclopediaText => UnwrappedObject.EncyclopediaText;

    public virtual MBCultureObject Culture => UnwrappedObject.Culture;

    public MBVec2 InitialPosition => UnwrappedObject.InitialPosition;

    public virtual uint LabelColor => UnwrappedObject.LabelColor;

    public virtual uint Color => UnwrappedObject.Color;

    public virtual uint Color2 => UnwrappedObject.Color2;

    public virtual uint AlternativeColor => UnwrappedObject.AlternativeColor;

    public virtual uint AlternativeColor2 => UnwrappedObject.AlternativeColor2;

    public virtual MBCharacterObject BasicTroop => UnwrappedObject.BasicTroop;

    public virtual MBHero Leader => UnwrappedObject.Leader;

    public virtual MBBanner Banner => UnwrappedObject.Banner;

    public virtual List<MBSettlement> Settlements => UnwrappedObject.Settlements.CastList<MBSettlement>();

    public virtual List<MBTown> Fiefs => UnwrappedObject.Fiefs.CastList<MBTown>();

    public virtual List<MBHero> Lords => UnwrappedObject.Lords.CastList<MBHero>();

    public virtual List<MBHero> Heroes => UnwrappedObject.Heroes.CastList<MBHero>();

    public virtual bool IsBanditFaction => UnwrappedObject.IsBanditFaction;

    public virtual bool IsMinorFaction => UnwrappedObject.IsMinorFaction;

    public virtual bool IsKingdomFaction => UnwrappedObject.IsKingdomFaction;

    public virtual bool IsRebelClan => UnwrappedObject.IsRebelClan;

    public virtual bool IsClan => UnwrappedObject.IsClan;

    public virtual bool IsOutlaw => UnwrappedObject.IsOutlaw;

    public virtual bool IsMapFaction => UnwrappedObject.IsMapFaction;

    public virtual IMBFaction MapFaction => UnwrappedObject.MapFaction.ToIMBFaction();

    public virtual float TotalStrength => UnwrappedObject.TotalStrength;

    public MBVec2 FactionMidPoint => UnwrappedObject.FactionMidPoint;

    public virtual List<MBStanceLink> Stances => UnwrappedObject.Stances.CastList<MBStanceLink>();

    public virtual int TributeWallet { get => UnwrappedObject.TributeWallet; set => UnwrappedObject.TributeWallet = value; }

    public virtual float MainHeroCrimeRating { get => UnwrappedObject.MainHeroCrimeRating; set => UnwrappedObject.MainHeroCrimeRating = value; }

    public virtual float DailyCrimeRatingChange => UnwrappedObject.DailyCrimeRatingChange;

    public virtual float Aggressiveness => UnwrappedObject.Aggressiveness;

    public virtual bool IsEliminated => UnwrappedObject.IsEliminated;

    public virtual MBExplainedNumber DailyCrimeRatingChangeExplained => UnwrappedObject.DailyCrimeRatingChangeExplained;

    public virtual CampaignTime NotAttackableByPlayerUntilTime { get => UnwrappedObject.NotAttackableByPlayerUntilTime; set => UnwrappedObject.NotAttackableByPlayerUntilTime = value; }

    public virtual MBStanceLink GetStanceWith(IMBFaction other) => UnwrappedObject.GetStanceWith((IFaction)other);

    public virtual bool IsAtWarWith(IMBFaction other) => UnwrappedObject.IsAtWarWith((IFaction)other);

    public static implicit operator Clan(MBClan wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBClan(Clan obj) => GetWrapper(obj);
  }
}
