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


  public class MBHero : MBObjectBaseWrapper<MBHero, MBHero>, IMBHero
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

    public MBClan Clan => UnwrappedObject.Clan;

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

    public MBHeroList ExSpouses => UnwrappedObject.ExSpouses;

    public bool AlwaysDie => UnwrappedObject.AlwaysDie;

    public bool NeverBecomePrisoner => UnwrappedObject.NeverBecomePrisoner;

    public bool Detected => UnwrappedObject.Detected;

    public string TattooTags => UnwrappedObject.TattooTags;

    public string BeardTags => UnwrappedObject.BeardTags;

    public string HairTags => UnwrappedObject.HairTags;

    public MBTextObject Name => UnwrappedObject.Name;

    public MBTextObject FirstName => UnwrappedObject.FirstName;

    public MBCharacterObjectList VolunteerTypes => UnwrappedObject.VolunteerTypes;

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

    public MBClan CompanionOf { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public CampaignTime LastSeenTime => UnwrappedObject.LastSeenTime;

    public bool CanBeCompanion => UnwrappedObject.CanBeCompanion;

    public bool Noncombatant => UnwrappedObject.Noncombatant;

    public bool AwaitingTrial => UnwrappedObject.AwaitingTrial;

    public MBHeroList CompanionsInParty => UnwrappedObject.CompanionsInParty;

    public MBCharacterObject Template { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

    public MBSettlement BornSettlement { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public MBSettlement HomeSettlement => UnwrappedObject.HomeSettlement;

    public int Gold { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Hero.FactionRank Rank => UnwrappedObject.Rank;

    public float ProbabilityOfDeath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public MBHero Father { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public MBHero Mother { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public MBHero Spouse { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public MBHeroList Children => UnwrappedObject.Children;

    public MBHeroList Siblings => UnwrappedObject.Siblings;

    public MBHeroDeveloper HeroDeveloper => UnwrappedObject.HeroDeveloper;

    public MBWorkshopList OwnedWorkshops => UnwrappedObject.OwnedWorkshops;

    public MBCommonAreaList OwnedCommonAreas => UnwrappedObject.OwnedCommonAreas;

    public MBPartyBaseList OwnedParties => UnwrappedObject.OwnedParties;

    public CampaignTime LastMeetingTimeWithPlayer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int HitPoints { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool HasMet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Controversy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public CampaignTime DeathDay { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public float Age => UnwrappedObject.Age;

    public bool IsChild => UnwrappedObject.IsChild;

    public float Power => UnwrappedObject.Power;

    public MBBanner ClanBanner => UnwrappedObject.ClanBanner;

    public long LastExaminedLogEntryID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public MBClan SupporterOf { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public MBTown GovernorOf { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IMBFaction MapFaction => UnwrappedObject.MapFaction;

    public bool IsFactionLeader => UnwrappedObject.IsFactionLeader;

    public MBCaravanPartyComponentList OwnedCaravans => UnwrappedObject.OwnedCaravans;

    public MBMobileParty PartyBelongedTo => UnwrappedObject.PartyBelongedTo;

    public MBIssueBase Issue => UnwrappedObject.Issue;

    public MBSettlement StayingInSettlementOfNotable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public bool IsHumanPlayerCharacter => UnwrappedObject.IsHumanPlayerCharacter;

    public bool AlwaysUnconscious { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float PassedTimeAtHomeSettlement { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool IsNoble { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Hero.CharacterStates HeroState => UnwrappedObject.HeroState;

    public CampaignTime CaptivityStartTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public MBEquipment CivilianEquipment => UnwrappedObject.CivilianEquipment;

    public MBEquipment BattleEquipment => UnwrappedObject.BattleEquipment;

    public bool IsFemale => UnwrappedObject.IsFemale;

    public MBTextObject EncyclopediaLinkWithName => UnwrappedObject.EncyclopediaLinkWithName;

    public string EncyclopediaLink => UnwrappedObject.EncyclopediaLink;

    public MBTextObject EncyclopediaText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public MBCharacterObject CharacterObject => UnwrappedObject.CharacterObject;

    public bool CanHaveRecruits => UnwrappedObject.CanHaveRecruits;

    public MBBodyProperties BodyProperties => UnwrappedObject.BodyProperties;

    public float Build { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Weight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool IsMinorFactionHero { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

    public void AddInfluenceWithKingdom(float additionalInfluence)
    {
      throw new NotImplementedException();
    }

    public void AddPower(float value)
    {
      throw new NotImplementedException();
    }

    public void AddSkillXp(MBSkillObject skill, float xpAmount)
    {
      throw new NotImplementedException();
    }

    public void ChangeHeroGold(int changeAmount)
    {
      throw new NotImplementedException();
    }

    public int GetAttributeValue(CharacterAttributesEnum charAttribute)
    {
      throw new NotImplementedException();
    }

    public int GetBaseHeroRelation(MBHero otherHero)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<string> GetHeroOccupiedEvents()
    {
      throw new NotImplementedException();
    }

    public MBCharacterSkills GetHeroSkills()
    {
      throw new NotImplementedException();
    }

    public MBCharacterTraits GetHeroTraits()
    {
      throw new NotImplementedException();
    }

    public IMapPoint GetMapPoint()
    {
      throw new NotImplementedException();
    }

    public bool GetPerkValue(MBPerkObject perk)
    {
      throw new NotImplementedException();
    }

    public Vec3 GetPosition()
    {
      throw new NotImplementedException();
    }



    public int GetSkillValue(MBSkillObject skill)
    {
      throw new NotImplementedException();
    }

    public int GetTraitLevel(MBTraitObject trait)
    {
      throw new NotImplementedException();
    }

    public float GetUnmodifiedClanLeaderRelationshipWithPlayer()
    {
      throw new NotImplementedException();
    }

    public void Heal(MBPartyBase party, int healAmount, bool addXp = false)
    {
      throw new NotImplementedException();
    }

    public bool IsEnemy(MBHero otherHero)
    {
      throw new NotImplementedException();
    }

    public bool IsFriend(MBHero otherHero)
    {
      throw new NotImplementedException();
    }

    public bool IsHealthFull()
    {
      throw new NotImplementedException();
    }

    public bool IsNeutral(MBHero otherHero)
    {
      throw new NotImplementedException();
    }

    public bool IsOccupiedByAnEvent()
    {
      throw new NotImplementedException();
    }

    public void SetCharacterObject(MBCharacterObject characterObject)
    {
      throw new NotImplementedException();
    }

    public void SetPerkValue(MBPerkObject perk, bool value)
    {
      throw new NotImplementedException();
    }

    public void SetPersonalRelation(MBHero otherHero, int value)
    {
      throw new NotImplementedException();
    }

    public void SetSkillValue(MBSkillObject skill, int value)
    {
      throw new NotImplementedException();
    }

    public static implicit operator MBHero(MBHero wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBHero(MBHero obj) => MBHero.GetWrapperFor(obj);
  }

  public class MBHeroList : MBListBase<MBHero, MBHeroList>
  {
    public static implicit operator List<MBHero>(MBHeroList wrapperList) => wrapperList.Unwrap<MBHero, MBHero>();
    public static implicit operator MBHeroList(List<MBHero> objectList) => (MBHeroList)objectList.Wrap<MBHero, MBHero>();
  }
}
