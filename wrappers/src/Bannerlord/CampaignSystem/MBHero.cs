using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Core;
using TournamentsEnhanced.Wrappers.Library;
using TournamentsEnhanced.Wrappers.Localization;
using static TaleWorlds.Core.ItemObject;
using MBCharacterSkills = TournamentsEnhanced.Wrappers.Core.MBCharacterSkills;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{


  public class MBHero : MBObjectBaseWrapper<MBHero, Hero>, IMBHero
  {
    public static MBHero Instance { get; } = new MBHero();

    public virtual List<MBHero> All => Hero.All.CastList<MBHero>();
    public virtual MBHero MainHero => Hero.MainHero;
    public virtual bool IsMainHeroIll => Hero.IsMainHeroIll;
    public virtual List<MBHero> ConversationHeroes => Hero.ConversationHeroes.CastList<MBHero>();
    public virtual MBHero OneToOneConversationHero => Hero.OneToOneConversationHero;
    public virtual List<MBHero> FindAll(Func<Hero, bool> predicate) => (List<MBHero>)Hero.FindAll((Func<Hero, bool>)predicate);
    public virtual MBHero FindFirst(Func<Hero, bool> predicate) => Hero.FindFirst((Func<Hero, bool>)predicate);
    public virtual float GetRelationWithPlayer() => UnwrappedObject.GetRelationWithPlayer();
    public virtual float GetRelation(MBHero otherHero) => UnwrappedObject.GetRelation(otherHero);
    public virtual ItemTiers GetMainHeroTournamentRewardTier() => GetRewardTierForHero(MainHero);

    public virtual MBClan Clan { get => UnwrappedObject.Clan; set => UnwrappedObject.Clan = value; }

    public virtual bool IsPregnant => UnwrappedObject.IsPregnant;

    public virtual bool IsFertile => UnwrappedObject.IsFertile;

    public virtual int RandomValueRarelyChanging => UnwrappedObject.RandomValueRarelyChanging;

    public virtual int RandomValueDeterministic => UnwrappedObject.RandomValueDeterministic;

    public virtual int RandomValue => UnwrappedObject.RandomValue;

    public virtual List<MBItemObject> SpecialItems => UnwrappedObject.SpecialItems.CastList<MBItemObject>();

    public virtual MBCultureObject Culture => UnwrappedObject.Culture;

    public virtual int SpcDaysInLocation => UnwrappedObject.SpcDaysInLocation;

    public virtual bool IsMercenary => UnwrappedObject.IsMercenary;

    public virtual int Level => UnwrappedObject.Level;

    public virtual List<MBHero> ExSpouses => UnwrappedObject.ExSpouses.CastList<MBHero>();

    public virtual bool AlwaysDie => UnwrappedObject.AlwaysDie;

    public virtual bool NeverBecomePrisoner => UnwrappedObject.NeverBecomePrisoner;

    public virtual bool Detected => UnwrappedObject.Detected;

    public virtual string TattooTags => UnwrappedObject.TattooTags;

    public virtual string BeardTags => UnwrappedObject.BeardTags;

    public virtual string HairTags => UnwrappedObject.HairTags;

    public virtual MBTextObject Name => UnwrappedObject.Name;

    public virtual MBTextObject FirstName => UnwrappedObject.FirstName;

    public virtual List<MBCharacterObject> VolunteerTypes => UnwrappedObject.VolunteerTypes.CastList<MBCharacterObject>();

    public virtual int LastTimeStampForActivity => UnwrappedObject.LastTimeStampForActivity;

    public virtual float LastVisitTimeOfHomeSettlement => UnwrappedObject.LastVisitTimeOfHomeSettlement;

    public virtual bool IsHeadman => UnwrappedObject.IsHeadman;

    public virtual bool IsWounded => UnwrappedObject.IsWounded;

    public virtual bool IsPlayerCompanion => UnwrappedObject.IsPlayerCompanion;

    public virtual bool IsMerchant => UnwrappedObject.IsMerchant;

    public virtual bool IsPreacher => UnwrappedObject.IsPreacher;

    public virtual bool IsGangLeader => UnwrappedObject.IsGangLeader;

    public virtual bool IsNotable => UnwrappedObject.IsNotable;

    public virtual bool IsRuralNotable => UnwrappedObject.IsRuralNotable;

    public virtual bool IsOutlaw => UnwrappedObject.IsOutlaw;

    public virtual bool IsSpecial => UnwrappedObject.IsSpecial;

    public virtual bool IsRebel => UnwrappedObject.IsRebel;

    public virtual bool IsCommander => UnwrappedObject.IsCommander;

    public virtual bool IsPartyLeader => UnwrappedObject.IsPartyLeader;

    public virtual bool IsTemplate => UnwrappedObject.IsTemplate;

    public virtual bool IsArtisan => UnwrappedObject.IsArtisan;

    public virtual bool IsWanderer => UnwrappedObject.IsWanderer;

    public virtual MBClan CompanionOf { get => UnwrappedObject.CompanionOf; set => UnwrappedObject.CompanionOf = value; }

    public virtual CampaignTime LastSeenTime => UnwrappedObject.LastSeenTime;

    public virtual bool CanBeCompanion => UnwrappedObject.CanBeCompanion;

    public virtual bool Noncombatant => UnwrappedObject.Noncombatant;

    public virtual bool AwaitingTrial => UnwrappedObject.AwaitingTrial;

    public virtual List<MBHero> CompanionsInParty => UnwrappedObject.CompanionsInParty.CastList<MBHero>();

    public virtual MBCharacterObject Template { get => UnwrappedObject.Template; set => UnwrappedObject.Template = value; }

    public virtual bool IsDead => UnwrappedObject.IsDead;

    public virtual bool IsFugitive => UnwrappedObject.IsFugitive;

    public virtual bool LastSeenInSettlement => UnwrappedObject.LastSeenInSettlement;

    public virtual bool IsPrisoner => UnwrappedObject.IsPrisoner;

    public virtual bool IsActive => UnwrappedObject.IsActive;

    public virtual bool IsNotSpawned => UnwrappedObject.IsNotSpawned;

    public virtual bool IsDisabled => UnwrappedObject.IsDisabled;

    public virtual bool IsAlive => UnwrappedObject.IsAlive;

    public virtual MBHero DeathMarkKillerHero => UnwrappedObject.DeathMarkKillerHero;

    public virtual MBSettlement LastSeenPlace => UnwrappedObject.LastSeenPlace;

    public virtual bool IsReleased => UnwrappedObject.IsReleased;

    public virtual int MaxHitPoints => UnwrappedObject.MaxHitPoints;

    public virtual MBPartyBase PartyBelongedToAsPrisoner => UnwrappedObject.PartyBelongedToAsPrisoner;

    public virtual CampaignTime BirthDay => UnwrappedObject.BirthDay;

    public virtual MBSettlement BornSettlement { get => UnwrappedObject.BornSettlement; set => UnwrappedObject.BornSettlement = value; }

    public virtual MBSettlement HomeSettlement => UnwrappedObject.HomeSettlement;

    public virtual int Gold { get => UnwrappedObject.Gold; set => UnwrappedObject.Gold = value; }

    public Hero.FactionRank Rank => UnwrappedObject.Rank;

    public virtual float ProbabilityOfDeath { get => UnwrappedObject.ProbabilityOfDeath; set => UnwrappedObject.ProbabilityOfDeath = value; }
    public virtual MBHero Father { get => UnwrappedObject.Father; set => UnwrappedObject.Father = value; }
    public virtual MBHero Mother { get => UnwrappedObject.Mother; set => UnwrappedObject.Mother = value; }
    public virtual MBHero Spouse { get => UnwrappedObject.Spouse; set => UnwrappedObject.Spouse = value; }

    public virtual List<MBHero> Children => UnwrappedObject.Children.CastList<MBHero>();

    public virtual List<MBHero> Siblings => UnwrappedObject.Siblings.CastList<MBHero>();

    public virtual MBHeroDeveloper HeroDeveloper => UnwrappedObject.HeroDeveloper;

    public virtual List<MBWorkshop> OwnedWorkshops => UnwrappedObject.OwnedWorkshops.CastList<MBWorkshop>();

    public virtual List<MBCommonArea> OwnedCommonAreas => UnwrappedObject.OwnedCommonAreas.CastList<MBCommonArea>();

    public virtual CampaignTime LastMeetingTimeWithPlayer { get => UnwrappedObject.LastMeetingTimeWithPlayer; set => UnwrappedObject.LastMeetingTimeWithPlayer = value; }
    public virtual int HitPoints { get => UnwrappedObject.HitPoints; set => UnwrappedObject.HitPoints = value; }
    public virtual bool HasMet { get => UnwrappedObject.HasMet; set => UnwrappedObject.HasMet = value; }
    public virtual float Controversy { get => UnwrappedObject.Controversy; set => UnwrappedObject.Controversy = value; }
    public virtual CampaignTime DeathDay { get => UnwrappedObject.DeathDay; set => UnwrappedObject.DeathDay = value; }

    public virtual float Age => UnwrappedObject.Age;

    public virtual bool IsChild => UnwrappedObject.IsChild;

    public virtual float Power => UnwrappedObject.Power;

    public virtual MBBanner ClanBanner => UnwrappedObject.ClanBanner;

    public virtual long LastExaminedLogEntryID { get => UnwrappedObject.LastExaminedLogEntryID; set => UnwrappedObject.LastExaminedLogEntryID = value; }
    public virtual MBClan SupporterOf { get => UnwrappedObject.SupporterOf; set => UnwrappedObject.SupporterOf = value; }
    public virtual MBTown GovernorOf { get => UnwrappedObject.GovernorOf; set => UnwrappedObject.GovernorOf = value; }

    public virtual IMBFaction MapFaction => UnwrappedObject.MapFaction.ToIMBFaction();

    public virtual bool IsFactionLeader => UnwrappedObject.IsFactionLeader;

    public virtual List<MBCaravanPartyComponent> OwnedCaravans => UnwrappedObject.OwnedCaravans.CastList<MBCaravanPartyComponent>();

    public virtual MBMobileParty PartyBelongedTo => UnwrappedObject.PartyBelongedTo;

    public virtual MBIssueBase Issue => UnwrappedObject.Issue;

    public virtual MBSettlement StayingInSettlementOfNotable { get => UnwrappedObject.StayingInSettlementOfNotable; set => UnwrappedObject.StayingInSettlementOfNotable = value; }

    public virtual bool IsHumanPlayerCharacter => UnwrappedObject.IsHumanPlayerCharacter;

    public virtual bool AlwaysUnconscious { get => UnwrappedObject.AlwaysUnconscious; set => UnwrappedObject.AlwaysUnconscious = value; }
    public virtual float PassedTimeAtHomeSettlement { get => UnwrappedObject.PassedTimeAtHomeSettlement; set => UnwrappedObject.PassedTimeAtHomeSettlement = value; }
    public virtual bool IsNoble { get => UnwrappedObject.IsNoble; set => UnwrappedObject.IsNoble = value; }

    public Hero.CharacterStates HeroState => UnwrappedObject.HeroState;

    public virtual CampaignTime CaptivityStartTime { get => UnwrappedObject.CaptivityStartTime; set => UnwrappedObject.CaptivityStartTime = value; }

    public virtual MBEquipment CivilianEquipment => UnwrappedObject.CivilianEquipment;

    public virtual MBEquipment BattleEquipment => UnwrappedObject.BattleEquipment;

    public virtual bool IsFemale => UnwrappedObject.IsFemale;

    public virtual MBTextObject EncyclopediaLinkWithName => UnwrappedObject.EncyclopediaLinkWithName;

    public virtual string EncyclopediaLink => UnwrappedObject.EncyclopediaLink;

    public virtual MBTextObject EncyclopediaText { get => UnwrappedObject.EncyclopediaText; set => UnwrappedObject.EncyclopediaText = value; }

    public virtual MBCharacterObject CharacterObject => UnwrappedObject.CharacterObject;

    public virtual bool CanHaveRecruits => UnwrappedObject.CanHaveRecruits;

    public virtual MBBodyProperties BodyProperties => UnwrappedObject.BodyProperties;

    public virtual float Build { get => UnwrappedObject.Build; set => UnwrappedObject.Build = value; }
    public virtual float Weight { get => UnwrappedObject.Weight; set => UnwrappedObject.Weight = value; }
    public virtual bool IsMinorFactionHero { get => UnwrappedObject.IsMinorFactionHero; set => UnwrappedObject.IsMinorFactionHero = value; }

    public virtual MBSettlement CurrentSettlement => UnwrappedObject.CurrentSettlement;

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

    public virtual void AddInfluenceWithKingdom(float additionalInfluence) => UnwrappedObject.AddInfluenceWithKingdom(additionalInfluence);

    public virtual void AddPower(float value) => UnwrappedObject.AddPower(value);

    public virtual void AddSkillXp(MBSkillObject skill, float xpAmount) => UnwrappedObject.AddSkillXp(skill, xpAmount);

    public virtual void ChangeHeroGold(int changeAmount) => UnwrappedObject.ChangeHeroGold(changeAmount);

    public virtual int GetAttributeValue(CharacterAttributesEnum charAttribute) => UnwrappedObject.GetAttributeValue(charAttribute);

    public virtual int GetBaseHeroRelation(MBHero otherHero) => UnwrappedObject.GetBaseHeroRelation(otherHero);

    public virtual IEnumerable<string> GetHeroOccupiedEvents() => UnwrappedObject.GetHeroOccupiedEvents();

    public virtual MBCharacterTraits GetHeroTraits() => UnwrappedObject.GetHeroTraits();

    public virtual IMapPoint GetMapPoint() => UnwrappedObject.GetMapPoint();

    public virtual bool GetPerkValue(MBPerkObject perk) => UnwrappedObject.GetPerkValue(perk);

    public MBVec3 GetPosition() => UnwrappedObject.GetPosition();



    public virtual int GetSkillValue(MBSkillObject skill) => UnwrappedObject.GetSkillValue(skill);

    public virtual int GetTraitLevel(MBTraitObject trait) => UnwrappedObject.GetTraitLevel(trait);

    public virtual float GetUnmodifiedClanLeaderRelationshipWithPlayer() => UnwrappedObject.GetUnmodifiedClanLeaderRelationshipWithPlayer();

    public virtual void Heal(MBPartyBase party, int healAmount, bool addXp = false) => UnwrappedObject.Heal(party, healAmount, addXp);

    public virtual bool IsEnemy(MBHero otherHero) => UnwrappedObject.IsEnemy(otherHero);

    public virtual bool IsFriend(MBHero otherHero) => UnwrappedObject.IsFriend(otherHero);

    public virtual bool IsHealthFull() => UnwrappedObject.IsHealthFull();

    public virtual bool IsNeutral(MBHero otherHero) => UnwrappedObject.IsNeutral(otherHero);

    public virtual bool IsOccupiedByAnEvent() => UnwrappedObject.IsOccupiedByAnEvent();

    public virtual void SetCharacterObject(MBCharacterObject characterObject) => UnwrappedObject.SetCharacterObject(characterObject);

    public virtual void SetPerkValue(MBPerkObject perk, bool value) => UnwrappedObject.SetPerkValue(perk, value);

    public virtual void SetPersonalRelation(MBHero otherHero, int value) => UnwrappedObject.SetPersonalRelation(otherHero, value);

    public virtual void SetSkillValue(MBSkillObject skill, int value) => UnwrappedObject.SetSkillValue(skill, value);

    public static implicit operator Hero(MBHero wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBHero(Hero obj) => GetWrapper(obj);
  }
}
