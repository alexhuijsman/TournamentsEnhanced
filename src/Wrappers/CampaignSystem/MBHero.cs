using System;
using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Core;
using TournamentsEnhanced.Wrappers.Localization;

using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{


  public class MBHero : MBObjectBaseWrapper<MBHero, Hero>, IMBHero
  {
    public static MBHeroList All => MBHero.All.ToList();
    public static MBHero MainHero => MBHero.MainHero;
    public static bool IsMainHeroIll => MBHero.IsMainHeroIll;
    public static MBHeroList ConversationHeroes => MBHero.ConversationHeroes.ToList();
    public static MBHero OneToOneConversationHero => MBHero.OneToOneConversationHero;
    public static MBHeroList FindAll(Func<MBHero, bool> predicate) => (MBHeroList)MBHero.FindAll((Func<MBHero, bool>)predicate);
    public static MBHero FindFirst(Func<MBHero, bool> predicate) => MBHero.FindFirst((Func<MBHero, bool>)predicate);
    public float GetRelationWithPlayer() => UnwrappedObject.GetRelationWithPlayer();
    public float GetRelation(MBHero otherHero) => UnwrappedObject.GetRelation(otherHero);
    public static ItemTiers GetMainHeroTournamentRewardTier() => GetRewardTierForHero(MainHero);

    public MBClan Clan { get => UnwrappedObject.Clan; set => UnwrappedObject.Clan = value; }

    public bool IsPregnant => UnwrappedObject.IsPregnant;

    public bool IsFertile => UnwrappedObject.IsFertile;

    public int RandomValueRarelyChanging => UnwrappedObject.RandomValueRarelyChanging;

    public int RandomValueDeterministic => UnwrappedObject.RandomValueDeterministic;

    public int RandomValue => UnwrappedObject.RandomValue;

    public MBItemObjectList SpecialItems => UnwrappedObject.SpecialItems;

    public MBCultureObject Culture => UnwrappedObject.Culture;

    public int SpcDaysInLocation => UnwrappedObject.SpcDaysInLocation;

    public bool IsMercenary => UnwrappedObject.IsMercenary;

    public int Level => UnwrappedObject.Level;

    public MBHeroList ExSpouses => UnwrappedObject.ExSpouses.ToList();

    public bool AlwaysDie => UnwrappedObject.AlwaysDie;

    public bool NeverBecomePrisoner => UnwrappedObject.NeverBecomePrisoner;

    public bool Detected => UnwrappedObject.Detected;

    public string TattooTags => UnwrappedObject.TattooTags;

    public string BeardTags => UnwrappedObject.BeardTags;

    public string HairTags => UnwrappedObject.HairTags;

    public MBTextObject Name => UnwrappedObject.Name;

    public MBTextObject FirstName => UnwrappedObject.FirstName;

    public MBCharacterObjectList VolunteerTypes => UnwrappedObject.VolunteerTypes.ToList();

    public int LastTimeStampForActivity => UnwrappedObject.LastTimeStampForActivity;

    public float LastVisitTimeOfHomeSettlement => UnwrappedObject.LastVisitTimeOfHomeSettlement;

    public bool IsHeadman => UnwrappedObject.IsHeadman;

    public bool IsWounded => UnwrappedObject.IsWounded;

    public bool IsPlayerCompanion => UnwrappedObject.IsPlayerCompanion;

    public bool IsMerchant => UnwrappedObject.IsMerchant;

    public bool IsPreacher => UnwrappedObject.IsPreacher;

    public bool IsGangLeader => UnwrappedObject.IsGangLeader;

    public bool IsNotable => UnwrappedObject.IsNotable;

    public bool IsRuralNotable => UnwrappedObject.IsRuralNotable;

    public bool IsOutlaw => UnwrappedObject.IsOutlaw;

    public bool IsSpecial => UnwrappedObject.IsSpecial;

    public bool IsRebel => UnwrappedObject.IsRebel;

    public bool IsCommander => UnwrappedObject.IsCommander;

    public bool IsPartyLeader => UnwrappedObject.IsPartyLeader;

    public bool IsTemplate => UnwrappedObject.IsTemplate;

    public bool IsArtisan => UnwrappedObject.IsArtisan;

    public bool IsWanderer => UnwrappedObject.IsWanderer;

    public MBClan CompanionOf { get => UnwrappedObject.CompanionOf; set => UnwrappedObject.CompanionOf = value; }

    public CampaignTime LastSeenTime => UnwrappedObject.LastSeenTime;

    public bool CanBeCompanion => UnwrappedObject.CanBeCompanion;

    public bool Noncombatant => UnwrappedObject.Noncombatant;

    public bool AwaitingTrial => UnwrappedObject.AwaitingTrial;

    public MBHeroList CompanionsInParty => UnwrappedObject.CompanionsInParty.ToList();

    public MBCharacterObject Template { get => UnwrappedObject.Template; set => UnwrappedObject.Template = value; }

    public bool IsDead => UnwrappedObject.IsDead;

    public bool IsFugitive => UnwrappedObject.IsFugitive;

    public bool LastSeenInSettlement => UnwrappedObject.LastSeenInSettlement;

    public bool IsPrisoner => UnwrappedObject.IsPrisoner;

    public bool IsActive => UnwrappedObject.IsActive;

    public bool IsNotSpawned => UnwrappedObject.IsNotSpawned;

    public bool IsDisabled => UnwrappedObject.IsDisabled;

    public bool IsAlive => UnwrappedObject.IsAlive;

    public MBHero DeathMarkKillerHero => UnwrappedObject.DeathMarkKillerHero;

    public MBSettlement LastSeenPlace => UnwrappedObject.LastSeenPlace;

    public bool IsReleased => UnwrappedObject.IsReleased;

    public int MaxHitPoints => UnwrappedObject.MaxHitPoints;

    public MBPartyBase PartyBelongedToAsPrisoner => UnwrappedObject.PartyBelongedToAsPrisoner;

    public CampaignTime BirthDay => UnwrappedObject.BirthDay;

    public MBSettlement BornSettlement { get => UnwrappedObject.BornSettlement; set => UnwrappedObject.BornSettlement = value; }

    public MBSettlement HomeSettlement => UnwrappedObject.HomeSettlement;

    public int Gold { get => UnwrappedObject.Gold; set => UnwrappedObject.Gold = value; }

    public Hero.FactionRank Rank => UnwrappedObject.Rank;

    public float ProbabilityOfDeath { get => UnwrappedObject.ProbabilityOfDeath; set => UnwrappedObject.ProbabilityOfDeath = value; }
    public MBHero Father { get => UnwrappedObject.Father; set => UnwrappedObject.Father = value; }
    public MBHero Mother { get => UnwrappedObject.Mother; set => UnwrappedObject.Mother = value; }
    public MBHero Spouse { get => UnwrappedObject.Spouse; set => UnwrappedObject.Spouse = value; }

    public MBHeroList Children => UnwrappedObject.Children;

    public MBHeroList Siblings => UnwrappedObject.Siblings.ToList();

    public MBHeroDeveloper HeroDeveloper => UnwrappedObject.HeroDeveloper;

    public MBWorkshopList OwnedWorkshops => UnwrappedObject.OwnedWorkshops.ToList();

    public MBCommonAreaList OwnedCommonAreas => UnwrappedObject.OwnedCommonAreas.ToList();

    public MBPartyBaseList OwnedParties => UnwrappedObject.OwnedParties.ToList();

    public CampaignTime LastMeetingTimeWithPlayer { get => UnwrappedObject.LastMeetingTimeWithPlayer; set => UnwrappedObject.LastMeetingTimeWithPlayer = value; }
    public int HitPoints { get => UnwrappedObject.HitPoints; set => UnwrappedObject.HitPoints = value; }
    public bool HasMet { get => UnwrappedObject.HasMet; set => UnwrappedObject.HasMet = value; }
    public float Controversy { get => UnwrappedObject.Controversy; set => UnwrappedObject.Controversy = value; }
    public CampaignTime DeathDay { get => UnwrappedObject.DeathDay; set => UnwrappedObject.DeathDay = value; }

    public float Age => UnwrappedObject.Age;

    public bool IsChild => UnwrappedObject.IsChild;

    public float Power => UnwrappedObject.Power;

    public MBBanner ClanBanner => UnwrappedObject.ClanBanner;

    public long LastExaminedLogEntryID { get => UnwrappedObject.LastExaminedLogEntryID; set => UnwrappedObject.LastExaminedLogEntryID = value; }
    public MBClan SupporterOf { get => UnwrappedObject.SupporterOf; set => UnwrappedObject.SupporterOf = value; }
    public MBTown GovernorOf { get => UnwrappedObject.GovernorOf; set => UnwrappedObject.GovernorOf = value; }

    public IMBFaction MapFaction => UnwrappedObject.MapFaction.ToIMBFaction();

    public bool IsFactionLeader => UnwrappedObject.IsFactionLeader;

    public MBCaravanPartyComponentList OwnedCaravans => UnwrappedObject.OwnedCaravans;

    public MBMobileParty PartyBelongedTo => UnwrappedObject.PartyBelongedTo;

    public MBIssueBase Issue => UnwrappedObject.Issue;

    public MBSettlement StayingInSettlementOfNotable { get => UnwrappedObject.StayingInSettlementOfNotable; set => UnwrappedObject.StayingInSettlementOfNotable = value; }

    public bool IsHumanPlayerCharacter => UnwrappedObject.IsHumanPlayerCharacter;

    public bool AlwaysUnconscious { get => UnwrappedObject.AlwaysUnconscious; set => UnwrappedObject.AlwaysUnconscious = value; }
    public float PassedTimeAtHomeSettlement { get => UnwrappedObject.PassedTimeAtHomeSettlement; set => UnwrappedObject.PassedTimeAtHomeSettlement = value; }
    public bool IsNoble { get => UnwrappedObject.IsNoble; set => UnwrappedObject.IsNoble = value; }

    public Hero.CharacterStates HeroState => UnwrappedObject.HeroState;

    public CampaignTime CaptivityStartTime { get => UnwrappedObject.CaptivityStartTime; set => UnwrappedObject.CaptivityStartTime = value; }

    public MBEquipment CivilianEquipment => UnwrappedObject.CivilianEquipment;

    public MBEquipment BattleEquipment => UnwrappedObject.BattleEquipment;

    public bool IsFemale => UnwrappedObject.IsFemale;

    public MBTextObject EncyclopediaLinkWithName => UnwrappedObject.EncyclopediaLinkWithName;

    public string EncyclopediaLink => UnwrappedObject.EncyclopediaLink;

    public MBTextObject EncyclopediaText { get => UnwrappedObject.EncyclopediaText; set => UnwrappedObject.EncyclopediaText = value; }

    public MBCharacterObject CharacterObject => UnwrappedObject.CharacterObject;

    public bool CanHaveRecruits => UnwrappedObject.CanHaveRecruits;

    public MBBodyProperties BodyProperties => UnwrappedObject.BodyProperties;

    public float Build { get => UnwrappedObject.Build; set => UnwrappedObject.Build = value; }
    public float Weight { get => UnwrappedObject.Weight; set => UnwrappedObject.Weight = value; }
    public bool IsMinorFactionHero { get => UnwrappedObject.IsMinorFactionHero; set => UnwrappedObject.IsMinorFactionHero = value; }

    public MBSettlement CurrentSettlement => UnwrappedObject.CurrentSettlement;

    private static ItemTiers GetRewardTierForHero(MBHero hero)
    {
      var renown = hero.Clan.Renown;

      ItemTiers itemTier;
      if (renown <= 300)
      {
        itemTier = ItemTiers.Tier4;
      }
      else if (renown <= 600)
      {
        itemTier = ItemTiers.Tier5;
      }
      else
      {
        itemTier = ItemTiers.Tier6;
      }

      return itemTier;
    }

    public void AddInfluenceWithKingdom(float additionalInfluence) => UnwrappedObject.AddInfluenceWithKingdom(additionalInfluence);

    public void AddPower(float value) => UnwrappedObject.AddPower(value);

    public void AddSkillXp(MBSkillObject skill, float xpAmount) => UnwrappedObject.AddSkillXp(skill, xpAmount);

    public void ChangeHeroGold(int changeAmount) => UnwrappedObject.ChangeHeroGold(changeAmount);

    public int GetAttributeValue(CharacterAttributesEnum charAttribute) => UnwrappedObject.GetAttributeValue(charAttribute);

    public int GetBaseHeroRelation(MBHero otherHero) => UnwrappedObject.GetBaseHeroRelation(otherHero);

    public IEnumerable<string> GetHeroOccupiedEvents() => UnwrappedObject.GetHeroOccupiedEvents();

    public MBCharacterSkills GetHeroSkills() => UnwrappedObject.GetHeroSkills();

    public MBCharacterTraits GetHeroTraits() => UnwrappedObject.GetHeroTraits();

    public IMapPoint GetMapPoint() => UnwrappedObject.GetMapPoint();

    public bool GetPerkValue(MBPerkObject perk) => UnwrappedObject.GetPerkValue(perk);

    public Vec3 GetPosition() => UnwrappedObject.GetPosition();



    public int GetSkillValue(MBSkillObject skill) => UnwrappedObject.GetSkillValue(skill);

    public int GetTraitLevel(MBTraitObject trait) => UnwrappedObject.GetTraitLevel(trait);

    public float GetUnmodifiedClanLeaderRelationshipWithPlayer() => UnwrappedObject.GetUnmodifiedClanLeaderRelationshipWithPlayer();

    public void Heal(MBPartyBase party, int healAmount, bool addXp = false) => UnwrappedObject.Heal(party, healAmount, addXp);

    public bool IsEnemy(MBHero otherHero) => UnwrappedObject.IsEnemy(otherHero);

    public bool IsFriend(MBHero otherHero) => UnwrappedObject.IsFriend(otherHero);

    public bool IsHealthFull() => UnwrappedObject.IsHealthFull();

    public bool IsNeutral(MBHero otherHero) => UnwrappedObject.IsNeutral(otherHero);

    public bool IsOccupiedByAnEvent() => UnwrappedObject.IsOccupiedByAnEvent();

    public void SetCharacterObject(MBCharacterObject characterObject) => UnwrappedObject.SetCharacterObject(characterObject);

    public void SetPerkValue(MBPerkObject perk, bool value) => UnwrappedObject.SetPerkValue(perk, value);

    public void SetPersonalRelation(MBHero otherHero, int value) => UnwrappedObject.SetPersonalRelation(otherHero, value);

    public void SetSkillValue(MBSkillObject skill, int value) => UnwrappedObject.SetSkillValue(skill, value);

    public static implicit operator Hero(MBHero wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBHero(Hero obj) => MBHero.GetWrapperFor(obj);
  }

  public class MBHeroList : MBListBase<MBHero, MBHeroList>
  {
    public MBHeroList(IEnumerable<MBHero> wrappers) => AddRange(wrappers);
    public MBHeroList(MBHero wrapper) => Add(wrapper);
    public MBHeroList() { }

    public static implicit operator List<Hero>(MBHeroList wrapperList) => wrapperList.Unwrap<MBHero, Hero>();
    public static implicit operator MBHeroList(List<Hero> objectList) => (MBHeroList)objectList.Wrap<MBHero, Hero>();
  }
}
