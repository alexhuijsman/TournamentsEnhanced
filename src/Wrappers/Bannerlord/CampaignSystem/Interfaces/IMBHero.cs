using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TournamentsEnhanced.Wrappers.Core;
using TournamentsEnhanced.Wrappers.Library;
using TournamentsEnhanced.Wrappers.Localization;
using static TaleWorlds.CampaignSystem.Hero;
using MBCharacterSkills = TournamentsEnhanced.Wrappers.Core.MBCharacterSkills;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public interface IMBHero
  {
    bool IsPregnant { get; }
    bool IsFertile { get; }
    int RandomValueRarelyChanging { get; }
    int RandomValueDeterministic { get; }
    int RandomValue { get; }
    List<MBItemObject> SpecialItems { get; }
    MBCultureObject Culture { get; }
    int SpcDaysInLocation { get; }
    bool IsMercenary { get; }
    int Level { get; }
    List<MBHero> ExSpouses { get; }
    bool AlwaysDie { get; }
    bool NeverBecomePrisoner { get; }
    bool Detected { get; }
    string TattooTags { get; }
    string BeardTags { get; }
    string HairTags { get; }
    MBTextObject Name { get; }
    MBTextObject FirstName { get; }
    List<MBCharacterObject> VolunteerTypes { get; }
    int LastTimeStampForActivity { get; }
    float LastVisitTimeOfHomeSettlement { get; }
    bool IsHeadman { get; }
    bool IsWounded { get; }
    bool IsPlayerCompanion { get; }
    bool IsMerchant { get; }
    bool IsPreacher { get; }
    bool IsGangLeader { get; }
    bool IsNotable { get; }
    bool IsRuralNotable { get; }
    bool IsOutlaw { get; }
    bool IsSpecial { get; }
    bool IsRebel { get; }
    bool IsCommander { get; }
    bool IsPartyLeader { get; }
    bool IsTemplate { get; }
    bool IsArtisan { get; }
    bool IsWanderer { get; }
    MBClan CompanionOf { get; set; }
    CampaignTime LastSeenTime { get; }
    bool CanBeCompanion { get; }
    bool Noncombatant { get; }
    bool AwaitingTrial { get; }
    List<MBHero> CompanionsInParty { get; }
    MBCharacterObject Template { get; set; }
    bool IsDead { get; }
    bool IsFugitive { get; }
    bool LastSeenInSettlement { get; }
    bool IsPrisoner { get; }
    bool IsActive { get; }
    bool IsNotSpawned { get; }
    bool IsDisabled { get; }
    bool IsAlive { get; }
    MBHero DeathMarkKillerHero { get; }
    MBSettlement LastSeenPlace { get; }
    bool IsReleased { get; }
    int MaxHitPoints { get; }
    MBPartyBase PartyBelongedToAsPrisoner { get; }
    CampaignTime BirthDay { get; }
    MBSettlement BornSettlement { get; set; }
    MBSettlement HomeSettlement { get; }
    MBSettlement CurrentSettlement { get; }
    int Gold { get; set; }
    FactionRank Rank { get; }
    float ProbabilityOfDeath { get; set; }
    MBHero Father { get; set; }
    MBHero Mother { get; set; }
    MBHero Spouse { get; set; }
    List<MBHero> Children { get; }
    List<MBHero> Siblings { get; }
    MBHeroDeveloper HeroDeveloper { get; }
    List<MBWorkshop> OwnedWorkshops { get; }
    List<MBCommonArea> OwnedCommonAreas { get; }
    CampaignTime LastMeetingTimeWithPlayer { get; set; }
    int HitPoints { get; set; }
    bool HasMet { get; set; }
    float Controversy { get; set; }
    CampaignTime DeathDay { get; set; }
    float Age { get; }
    bool IsChild { get; }
    float Power { get; }
    MBBanner ClanBanner { get; }
    long LastExaminedLogEntryID { get; set; }
    MBClan Clan { get; set; }
    MBClan SupporterOf { get; set; }
    MBTown GovernorOf { get; set; }
    IMBFaction MapFaction { get; }
    bool IsFactionLeader { get; }
    List<MBCaravanPartyComponent> OwnedCaravans { get; }
    MBMobileParty PartyBelongedTo { get; }
    MBIssueBase Issue { get; }
    MBSettlement StayingInSettlementOfNotable { get; set; }
    bool IsHumanPlayerCharacter { get; }
    bool AlwaysUnconscious { get; set; }
    float PassedTimeAtHomeSettlement { get; set; }
    bool IsNoble { get; set; }
    CharacterStates HeroState { get; }
    CampaignTime CaptivityStartTime { get; set; }
    MBEquipment CivilianEquipment { get; }
    MBEquipment BattleEquipment { get; }
    bool IsFemale { get; }
    MBTextObject EncyclopediaLinkWithName { get; }
    string EncyclopediaLink { get; }
    MBTextObject EncyclopediaText { get; set; }
    MBCharacterObject CharacterObject { get; }
    bool CanHaveRecruits { get; }
    MBBodyProperties BodyProperties { get; }
    float Build { get; set; }
    float Weight { get; set; }
    bool IsMinorFactionHero { get; set; }


    void AddInfluenceWithKingdom(float additionalInfluence);
    void AddPower(float value);
    void AddSkillXp(MBSkillObject skill, float xpAmount);
    void ChangeHeroGold(int changeAmount);
    int GetAttributeValue(CharacterAttributesEnum charAttribute);
    int GetBaseHeroRelation(MBHero otherHero);
    IEnumerable<string> GetHeroOccupiedEvents();
    MBCharacterTraits GetHeroTraits();
    IMapPoint GetMapPoint();
    bool GetPerkValue(MBPerkObject perk);
    MBVec3 GetPosition();
    float GetRelation(MBHero otherHero);
    float GetRelationWithPlayer();
    int GetSkillValue(MBSkillObject skill);
    int GetTraitLevel(MBTraitObject trait);
    float GetUnmodifiedClanLeaderRelationshipWithPlayer();
    void Heal(MBPartyBase party, int healAmount, bool addXp = false);
    bool IsEnemy(MBHero otherHero);
    bool IsFriend(MBHero otherHero);
    bool IsHealthFull();
    bool IsNeutral(MBHero otherHero);
    bool IsOccupiedByAnEvent();
    void SetCharacterObject(MBCharacterObject characterObject);
    void SetPerkValue(MBPerkObject perk, bool value);
    void SetPersonalRelation(MBHero otherHero, int value);
    void SetSkillValue(MBSkillObject skill, int value);
  }
}