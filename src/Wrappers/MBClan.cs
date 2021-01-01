using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBClan : CachedWrapperBase<MBClan, Clan>
  {
    public float Renown => UnwrappedObject.Renown;

    public TextObject Name => UnwrappedObject.Name;

    public string StringId => UnwrappedObject.StringId;

    public TextObject InformalName => UnwrappedObject.InformalName;

    public string EncyclopediaLink => UnwrappedObject.EncyclopediaLink;

    public TextObject EncyclopediaLinkWithName => UnwrappedObject.EncyclopediaLinkWithName;

    public TextObject EncyclopediaText => UnwrappedObject.EncyclopediaText;

    public CultureObject Culture => UnwrappedObject.Culture;

    public Vec2 InitialPosition => UnwrappedObject.InitialPosition;

    public uint LabelColor => UnwrappedObject.LabelColor;

    public uint Color => UnwrappedObject.Color;

    public uint Color2 => UnwrappedObject.Color2;

    public uint AlternativeColor => UnwrappedObject.AlternativeColor;

    public uint AlternativeColor2 => UnwrappedObject.AlternativeColor2;

    public CharacterObject BasicTroop => UnwrappedObject.BasicTroop;

    public Hero Leader => UnwrappedObject.Leader;

    public Banner Banner => UnwrappedObject.Banner;

    public IEnumerable<Settlement> Settlements => UnwrappedObject.Settlements;

    public IEnumerable<Town> Fiefs => UnwrappedObject.Fiefs;

    public IEnumerable<Hero> Lords => UnwrappedObject.Lords;

    public IEnumerable<Hero> Heroes => UnwrappedObject.Heroes;

    public IEnumerable<MobileParty> AllParties => UnwrappedObject.AllParties;

    public IEnumerable<MobileParty> WarParties => UnwrappedObject.WarParties;

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

    public static implicit operator Clan(MBClan wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBClan(Clan obj) => MBClan.GetWrapperFor(obj);
  }
}
