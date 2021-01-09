using System;
using System.Collections.Generic;

using Helpers;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Conversation.Persuasion;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.SandBox;
using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCampaignEvents : MBWrapperBase<MBCampaignEvents, CampaignEvents>
  {
    public static IMbEvent<CharacterObject> ConversationEnded => CampaignEvents.ConversationEnded;
    public static IMbEvent OnNewGameCreatedEvent4 => CampaignEvents.OnNewGameCreatedEvent4;
    public static IMbEvent OnNewGameCreatedEvent5 => CampaignEvents.OnNewGameCreatedEvent5;
    public static IMbEvent OnNewGameCreatedEvent6 => CampaignEvents.OnNewGameCreatedEvent6;
    public static IMbEvent OnNewGameCreatedEvent7 => CampaignEvents.OnNewGameCreatedEvent7;
    public static IMbEvent OnNewGameCreatedEvent8 => CampaignEvents.OnNewGameCreatedEvent8;
    public static IMbEvent OnNewGameCreatedEvent9 => CampaignEvents.OnNewGameCreatedEvent9;
    public static IMbEvent<CampaignGameStarter> OnGameEarlyLoadedEvent => CampaignEvents.OnGameEarlyLoadedEvent;
    public static IMbEvent<CampaignGameStarter> OnGameLoadedEvent => CampaignEvents.OnGameLoadedEvent;
    public static IMbEvent OnGameLoadFinishedEvent => CampaignEvents.OnGameLoadFinishedEvent;
    public static IMbEvent<MobileParty, PartyThinkParams> AiHourlyTickEvent => CampaignEvents.AiHourlyTickEvent;
    public static IMbEvent TickPartialHourlyAiEvent => CampaignEvents.TickPartialHourlyAiEvent;
    public static IMbEvent<MobileParty> OnPartyJoinedArmyEvent => CampaignEvents.OnPartyJoinedArmyEvent;
    public static IMbEvent<MobileParty> OnPartyArrivedArmyEvent => CampaignEvents.OnPartyArrivedArmyEvent;
    public static IMbEvent<MobileParty> OnPartyRemovedArmyEvent => CampaignEvents.OnPartyRemovedArmyEvent;
    public static IMbEvent<Hero, Army.ArmyLeaderThinkReason> OnArmyLeaderThinkEvent => CampaignEvents.OnArmyLeaderThinkEvent;
    public static IMbEvent<IMission> OnMissionEndedEvent => CampaignEvents.OnMissionEndedEvent;
    public static IMbEvent<MapEvent> OnPlayerBattleEndEvent => CampaignEvents.OnPlayerBattleEndEvent;
    public static IMbEvent<MobileParty, List<CharacterObject>, int> OnDoMeetingInMapEvent => CampaignEvents.OnDoMeetingInMapEvent;
    public static IMbEvent<CharacterObject, int> OnUnitRecruitedEvent => CampaignEvents.OnUnitRecruitedEvent;
    public static IMbEvent<Hero> OnChildConceivedEvent => CampaignEvents.OnChildConceivedEvent;
    public static IMbEvent<Hero, List<Hero>, int> OnGivenBirthEvent => CampaignEvents.OnGivenBirthEvent;
    public static IMbEvent<float> MissionTickEvent => CampaignEvents.MissionTickEvent;
    public static IMbEvent SetupPreConversationEvent => CampaignEvents.SetupPreConversationEvent;
    public static IMbEvent ArmyOverlaySetDirtyEvent => CampaignEvents.ArmyOverlaySetDirtyEvent;
    public static IMbEvent<int> PlayerDesertedBattleEvent => CampaignEvents.PlayerDesertedBattleEvent;
    public static IMbEvent OnNewGameCreatedEvent3 => CampaignEvents.OnNewGameCreatedEvent3;
    public static IMbEvent OnNewGameCreatedEvent2 => CampaignEvents.OnNewGameCreatedEvent2;
    public static IMbEvent<CampaignGameStarter> OnNewGameCreatedEvent => CampaignEvents.OnNewGameCreatedEvent;
    public static IMbEvent<CampaignGameStarter> OnSessionLaunchedEvent => CampaignEvents.OnSessionLaunchedEvent;
    public static IMbEvent<Settlement> PrisonersChangeInSettlement => CampaignEvents.PrisonersChangeInSettlement;
    public static IMbEvent<Hero, BoardGameHelper.BoardGameState> OnPlayerBoardGameOverEvent => CampaignEvents.OnPlayerBoardGameOverEvent;
    public static IMbEvent<IMission> OnMissionStartedEvent => CampaignEvents.OnMissionStartedEvent;
    public static IMbEvent<CommonArea, CommonArea.AreaState, CommonArea.AreaState> OnCommonAreaStateChangedEvent => CampaignEvents.OnCommonAreaStateChangedEvent;
    public static IMbEvent BeforeMissionOpenedEvent => CampaignEvents.BeforeMissionOpenedEvent;
    public static IMbEvent<PartyBase> OnPartyRemovedEvent => CampaignEvents.OnPartyRemovedEvent;
    public static IMbEvent<PartyBase> OnPartySizeChangedEvent => CampaignEvents.OnPartySizeChangedEvent;
    public static IMbEvent<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail> OnSettlementOwnerChangedEvent => CampaignEvents.OnSettlementOwnerChangedEvent;
    public static IMbEvent<Town, Hero, Hero> OnGovernorChangedEvent => CampaignEvents.OnGovernorChangedEvent;
    public static IMbEvent<MobileParty, Settlement> OnSettlementLeftEvent => CampaignEvents.OnSettlementLeftEvent;
    public static IMbEvent WeeklyTickEvent => CampaignEvents.WeeklyTickEvent;
    public static IMbEvent DailyTickEvent => CampaignEvents.DailyTickEvent;
    public static IMbEvent<MobileParty> DailyTickPartyEvent => CampaignEvents.DailyTickPartyEvent;
    public static IMbEvent<Town> DailyTickTownEvent => CampaignEvents.DailyTickTownEvent;
    public static IMbEvent<Settlement> DailyTickSettlementEvent => CampaignEvents.DailyTickSettlementEvent;
    public static IMbEvent<Settlement> WeeklyTickSettlementEvent => CampaignEvents.WeeklyTickSettlementEvent;
    public static IMbEvent<Hero> DailyTickHeroEvent => CampaignEvents.DailyTickHeroEvent;
    public static IMbEvent<Clan> DailyTickClanEvent => CampaignEvents.DailyTickClanEvent;
    public static IMbEvent<List<CampaignTutorial>> CollectAvailableTutorialsEvent => CampaignEvents.CollectAvailableTutorialsEvent;
    public static IMbEvent<string> OnTutorialCompletedEvent => CampaignEvents.OnTutorialCompletedEvent;
    public static IMbEvent<Town, Building, int> OnBuildingLevelChangedEvent => CampaignEvents.OnBuildingLevelChangedEvent;
    public static IMbEvent AfterDailyTickEvent => CampaignEvents.AfterDailyTickEvent;
    public static IMbEvent HourlyTickEvent => CampaignEvents.HourlyTickEvent;
    public static IMbEvent<MobileParty> HourlyTickPartyEvent => CampaignEvents.HourlyTickPartyEvent;
    public static IMbEvent<Settlement> HourlyTickSettlementEvent => CampaignEvents.HourlyTickSettlementEvent;
    public static IMbEvent<Clan> HourlyTickClanEvent => CampaignEvents.HourlyTickClanEvent;
    public static IMbEvent<float> TickEvent => CampaignEvents.TickEvent;
    public static IMbEvent<PartyBase> PartyVisibilityChangedEvent => CampaignEvents.PartyVisibilityChangedEvent;
    public static IMbEvent ObjectRegisteredToVisualTrackerEvent => CampaignEvents.ObjectRegisteredToVisualTrackerEvent;
    public static IMbEvent<Track> TrackDetectedEvent => CampaignEvents.TrackDetectedEvent;
    public static IMbEvent<Track> TrackLostEvent => CampaignEvents.TrackLostEvent;
    public static IMbEvent<Tuple<PersuasionOptionArgs, PersuasionOptionResult>> PersuasionProgressCommitedEvent => CampaignEvents.PersuasionProgressCommitedEvent;
    public static IMbEvent<News> OnNewsSendedToNewsManagerEvent => CampaignEvents.OnNewsSendedToNewsManagerEvent;
    public static IMbEvent<QuestBase, QuestBase.QuestCompleteDetails> OnQuestCompletedEvent => CampaignEvents.OnQuestCompletedEvent;
    public static IMbEvent<QuestBase> OnQuestStartedEvent => CampaignEvents.OnQuestStartedEvent;
    public static IMbEvent<ItemObject, Settlement, int> OnItemProducedEvent => CampaignEvents.OnItemProducedEvent;
    public static IMbEvent<ItemObject, Settlement, int> OnItemConsumedEvent => CampaignEvents.OnItemConsumedEvent;
    public static IMbEvent<MobileParty> OnPartyConsumedFoodEvent => CampaignEvents.OnPartyConsumedFoodEvent;
    public static IMbEvent<PartyBase, PartyBase> PartyEncounteredEvent => CampaignEvents.PartyEncounteredEvent;
    public static IMbEvent OnBeforeMainCharacterDiedEvent => CampaignEvents.OnBeforeMainCharacterDiedEvent;
    public static IMbEvent<IssueBase> OnNewIssueCreatedEvent => CampaignEvents.OnNewIssueCreatedEvent;
    public static IMbEvent<IssueBase, Hero> OnIssueOwnerChangedEvent => CampaignEvents.OnIssueOwnerChangedEvent;
    public static IMbEvent OnGameOverEvent => CampaignEvents.OnGameOverEvent;
    public static IMbEvent<List<(ItemRosterElement, int)>, List<(ItemRosterElement, int)>> PlayerInventoryExchangeEvent => CampaignEvents.PlayerInventoryExchangeEvent;
    public static IMbEvent<Settlement, MobileParty, bool, bool> SiegeCompletedEvent => CampaignEvents.SiegeCompletedEvent;
    public static IMbEvent<BattleSideEnum, MapEvent> RaidCompletedEvent => CampaignEvents.RaidCompletedEvent;
    public static IMbEvent<BattleSideEnum, MapEvent> ForceVolunteersCompletedEvent => CampaignEvents.ForceVolunteersCompletedEvent;
    public static IMbEvent<BattleSideEnum, MapEvent> ForceSuppliesCompletedEvent => CampaignEvents.ForceSuppliesCompletedEvent;
    public static IMbEvent<Clan> OnClanDestroyedEvent => CampaignEvents.OnClanDestroyedEvent;
    public static IMbEvent<ItemObject, Crafting.OverrideData> OnNewItemCraftedEvent => CampaignEvents.OnNewItemCraftedEvent;
    public static IMbEvent<CraftingPiece> CraftingPartUnlockedEvent => CampaignEvents.CraftingPartUnlockedEvent;
    public static IMbEvent<Workshop, Hero, WorkshopType> OnWorkshopChangedEvent => CampaignEvents.OnWorkshopChangedEvent;
    public static IMbEvent<MobileParty> OnLordPartySpawnedEvent => CampaignEvents.OnLordPartySpawnedEvent;
    public static IMbEvent OnBeforeSaveEvent => CampaignEvents.OnBeforeSaveEvent;
    public static IMbEvent<FlattenedTroopRoster> OnPrisonerTakenEvent => CampaignEvents.OnPrisonerTakenEvent;
    public static IMbEvent<FlattenedTroopRoster> OnPrisonerReleasedEvent => CampaignEvents.OnPrisonerReleasedEvent;
    public static IMbEvent<FlattenedTroopRoster> OnPrisonerRecruitedEvent => CampaignEvents.OnPrisonerRecruitedEvent;
    public static IMbEvent<SiegeEvent, BattleSideEnum, SiegeEngineType> SiegeEngineBuiltEvent => CampaignEvents.SiegeEngineBuiltEvent;
    public static IMbEvent<Hero, Hero, float> OnHeroSharedFoodWithAnotherHeroEvent => CampaignEvents.OnHeroSharedFoodWithAnotherHeroEvent;
    public static IMbEvent<Settlement> OnHideoutClearedEvent => CampaignEvents.OnHideoutClearedEvent;
    public static IMbEvent<PartyBase, PartyBase> OnHideoutSpottedEvent => CampaignEvents.OnHideoutSpottedEvent;
    public static IMbEvent<Dictionary<string, int>> LocationCharactersAreReadyToSpawnEvent => CampaignEvents.LocationCharactersAreReadyToSpawnEvent;
    public static IMbEvent LocationCharactersSimulatedEvent => CampaignEvents.LocationCharactersSimulatedEvent;
    public static IMbEvent<CharacterObject, CharacterObject, int> PlayerUpgradedTroopsEvent => CampaignEvents.PlayerUpgradedTroopsEvent;
    public static IMbEvent<CharacterObject> CharacterPortraitPopUpOpenedEvent => CampaignEvents.CharacterPortraitPopUpOpenedEvent;
    public static IMbEvent CharacterPortraitPopUpClosedEvent => CampaignEvents.CharacterPortraitPopUpClosedEvent;
    public static IMbEvent<Hero> PlayerStartTalkFromMenu => CampaignEvents.PlayerStartTalkFromMenu;
    public static IMbEvent<GameMenuOption> GameMenuOptionSelectedEvent => CampaignEvents.GameMenuOptionSelectedEvent;
    public static IMbEvent<CharacterObject> PlayerStartRecruitmentEvent => CampaignEvents.PlayerStartRecruitmentEvent;
    public static IMbEvent<Hero, MobileParty> OnPlayerCharacterChangedEvent => CampaignEvents.OnPlayerCharacterChangedEvent;
    public static IMbEvent<Hero, Hero> OnClanLeaderChangedEvent => CampaignEvents.OnClanLeaderChangedEvent;
    public static IMbEvent<SiegeEvent> OnSiegeEventStartedEvent => CampaignEvents.OnSiegeEventStartedEvent;
    public static IMbEvent OnPlayerSiegeStartedEvent => CampaignEvents.OnPlayerSiegeStartedEvent;
    public static IMbEvent<SiegeEvent> OnSiegeEventEndedEvent => CampaignEvents.OnSiegeEventEndedEvent;
    public static IMbEvent<MobileParty, Settlement, BattleSideEnum, SiegeEngineType, SiegeBombardTargets> OnSiegeBombardmentHitEvent => CampaignEvents.OnSiegeBombardmentHitEvent;
    public static IMbEvent<MobileParty, Settlement, BattleSideEnum, SiegeEngineType, bool> OnSiegeBombardmentWallHitEvent => CampaignEvents.OnSiegeBombardmentWallHitEvent;
    public static IMbEvent<MobileParty, Settlement, BattleSideEnum, SiegeEngineType> OnSiegeEngineDestroyedEvent => CampaignEvents.OnSiegeEngineDestroyedEvent;
    public static IMbEvent<List<TradeRumor>, Settlement> OnTradeRumorIsTakenEvent => CampaignEvents.OnTradeRumorIsTakenEvent;
    public static IMbEvent<IssueArgs> OnCheckForIssueEvent => CampaignEvents.OnCheckForIssueEvent;
    public static IMbEvent<IssueBase, IssueBase.IssueUpdateDetails, Hero> OnIssueUpdatedEvent => CampaignEvents.OnIssueUpdatedEvent;
    public static IMbEvent<MobileParty, TroopRoster> OnTroopsDesertedEvent => CampaignEvents.OnTroopsDesertedEvent;
    public static IMbEvent<Hero, Settlement, Hero, CharacterObject, int> OnTroopRecruitedEvent => CampaignEvents.OnTroopRecruitedEvent;
    public static IMbEvent<Hero, Settlement, TroopRoster> OnTroopGivenToSettlementEvent => CampaignEvents.OnTroopGivenToSettlementEvent;
    public static IMbEvent<PartyBase, PartyBase, ItemRosterElement, int, Settlement> OnItemSoldEvent => CampaignEvents.OnItemSoldEvent;
    public static IMbEvent<MobileParty, Town, List<(ItemObject, int)>> OnCaravanTransactionCompletedEvent => CampaignEvents.OnCaravanTransactionCompletedEvent;
    public static IMbEvent<MobileParty, TroopRoster, Settlement> OnPrisonerSoldEvent => CampaignEvents.OnPrisonerSoldEvent;
    public static IMbEvent<MobileParty> OnPartyDisbandedEvent => CampaignEvents.OnPartyDisbandedEvent;
    public static IMbEvent<MobileParty> OnPartyDisbandCanceledEvent => CampaignEvents.OnPartyDisbandCanceledEvent;
    public static IMbEvent<MapEvent, PartyBase, PartyBase> MapEventStarted => CampaignEvents.MapEventStarted;
    public static IMbEvent<MapEvent> MapEventEnded => CampaignEvents.MapEventEnded;
    public static IMbEvent<Hero, EquipmentElement> OnEquipmentSmeltedByHeroEvent => CampaignEvents.OnEquipmentSmeltedByHeroEvent;
    public static IMbEvent<Hero> CompanionRemoved => CampaignEvents.CompanionRemoved;
    public static IMbEvent<Hero, Hero, Romance.RomanceLevelEnum> RomanticStateChanged => CampaignEvents.RomanticStateChanged;
    public static IMbEvent<CommonArea, Hero, bool> CommonAreaOwnerChanged => CampaignEvents.CommonAreaOwnerChanged;
    public static IMbEvent<MobileParty, MobileParty, Hero, Settlement> CommonAreaFightOccured => CampaignEvents.CommonAreaFightOccured;
    public static IMbEvent<Town, int, int> MercenaryNumberChangedInTown => CampaignEvents.MercenaryNumberChangedInTown;
    public static IMbEvent<Town, CharacterObject, CharacterObject> MercenaryTroopChangedInTown => CampaignEvents.MercenaryTroopChangedInTown;
    public static IMbEvent<MobileParty, Settlement, Hero> AfterSettlementEntered => CampaignEvents.AfterSettlementEntered;
    public static IMbEvent<MobileParty, Settlement, Hero> SettlementEntered => CampaignEvents.SettlementEntered;
    public static IMbEvent<Village, Village.VillageStates, Village.VillageStates, MobileParty> VillageStateChanged => CampaignEvents.VillageStateChanged;
    public static IMbEvent<Hero, PerkObject> PerkOpenedEvent => CampaignEvents.PerkOpenedEvent;
    public static IMbEvent<Army, Settlement> ArmyGathered => CampaignEvents.ArmyGathered;
    public static IMbEvent<Army, Army.ArmyDispersionReason, bool> ArmyDispersed => CampaignEvents.ArmyDispersed;
    public static IMbEvent<Army> ArmyCreated => CampaignEvents.ArmyCreated;
    public static IMbEvent<KingdomDecision, DecisionOutcome, bool> KingdomDecisionConcluded => CampaignEvents.KingdomDecisionConcluded;
    public static IMbEvent<KingdomDecision, bool> KingdomDecisionCancelled => CampaignEvents.KingdomDecisionCancelled;
    public static IMbEvent<Hero, Hero, bool> HeroesMarried => CampaignEvents.HeroesMarried;
    public static IMbEvent<KingdomDecision, bool> KingdomDecisionAdded => CampaignEvents.KingdomDecisionAdded;
    public static IMbEvent<MobileParty> BanditPartyRecruited => CampaignEvents.BanditPartyRecruited;
    public static IMbEvent<(Hero, PartyBase), (Hero, PartyBase), ItemObject, int, bool> HeroOrPartyGaveItem => CampaignEvents.HeroOrPartyGaveItem;
    public static IMbEvent<(Hero, PartyBase), (Hero, PartyBase), (int, string), bool> HeroOrPartyTradedGold => CampaignEvents.HeroOrPartyTradedGold;
    public static IMbEvent<Clan, Kingdom, Kingdom, bool, bool> ClanChangedKingdom => CampaignEvents.ClanChangedKingdom;
    public static IMbEvent<Clan, bool> ClanTierIncrease => CampaignEvents.ClanTierIncrease;
    public static IMbEvent<IssueBase, bool> IssueLogAddedEvent => CampaignEvents.IssueLogAddedEvent;
    public static IMbEvent<QuestBase, bool> QuestLogAddedEvent => CampaignEvents.QuestLogAddedEvent;
    public static IMbEvent<Hero, Hero, int, bool> HeroRelationChanged => CampaignEvents.HeroRelationChanged;
    public static IMbEvent<Hero> HeroWounded => CampaignEvents.HeroWounded;
    public static IMbEvent<Hero, Settlement> OnPlayerDonatedHeroPrisonerEvent => CampaignEvents.OnPlayerDonatedHeroPrisonerEvent;
    public static IMbEvent<Hero, SkillObject, bool, int, bool> HeroGainedSkill => CampaignEvents.HeroGainedSkill;
    public static IMbEvent<Hero, bool> HeroLevelledUp => CampaignEvents.HeroLevelledUp;
    public static IMbEvent<BarterData> BarterablesRequested => CampaignEvents.BarterablesRequested;
    public static CampaignEvents Instance => CampaignEvents.Instance;
    public static IMbEvent<Clan, Kingdom, Kingdom> MercenaryClanChangedKingdom => CampaignEvents.MercenaryClanChangedKingdom;
    public static IMbEvent<int, Town> PlayerEliminatedFromTournament => CampaignEvents.PlayerEliminatedFromTournament;
    public static IMbEvent<Hero, bool> HeroCreated => CampaignEvents.HeroCreated;
    public static IMbEvent<Town> TournamentStarted => CampaignEvents.TournamentStarted;
    public static IMbEvent<Village> VillageBeingRaided => CampaignEvents.VillageBeingRaided;
    public static IMbEvent<Village> VillageBecomeNormal => CampaignEvents.VillageBecomeNormal;
    public static IMbEvent<Kingdom> KingdomCreatedEvent => CampaignEvents.KingdomCreatedEvent;
    public static IMbEvent<Kingdom> KingdomDestroyedEvent => CampaignEvents.KingdomDestroyedEvent;
    public static IMbEvent<IFaction, IFaction> MakePeace => CampaignEvents.MakePeace;
    public static IMbEvent<MenuCallbackArgs> BeforeGameMenuOpenedEvent => CampaignEvents.BeforeGameMenuOpenedEvent;
    public static IMbEvent<MenuCallbackArgs> AfterGameMenuOpenedEvent => CampaignEvents.AfterGameMenuOpenedEvent;
    public static IMbEvent<MenuCallbackArgs> GameMenuOpened => CampaignEvents.GameMenuOpened;
    public static IMbEvent<IMission> AfterMissionStarted => CampaignEvents.AfterMissionStarted;
    public static IMbEvent<Hero> NewCompanionAdded => CampaignEvents.NewCompanionAdded;
    public static IMbEvent<Hero, int, bool> RenownGained => CampaignEvents.RenownGained;
    public static IMbEvent<Hero> PlayerMetCharacter => CampaignEvents.PlayerMetCharacter;
    public static IMbEvent<Hero> CharacterBecameFugitive => CampaignEvents.CharacterBecameFugitive;
    public static IMbEvent<Hero, PartyBase, IFaction, EndCaptivityDetail> HeroPrisonerReleased => CampaignEvents.HeroPrisonerReleased;
    public static IMbEvent<PartyBase, Hero> HeroPrisonerTaken => CampaignEvents.HeroPrisonerTaken;
    public static IMbEvent<Hero, Hero> CharacterDefeated => CampaignEvents.CharacterDefeated;
    public static IMbEvent<Hero> HeroReachesTeenAgeEvent => CampaignEvents.HeroReachesTeenAgeEvent;
    public static IMbEvent<Hero> HeroGrowsOutOfInfancyEvent => CampaignEvents.HeroGrowsOutOfInfancyEvent;
    public static IMbEvent<Hero> HeroComesOfAgeEvent => CampaignEvents.HeroComesOfAgeEvent;
    public static IMbEvent<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool> HeroKilledEvent => CampaignEvents.HeroKilledEvent;
    public static IMbEvent<Hero, Hero, CharacterObject, ActionNotes> CharacterInsulted => CampaignEvents.CharacterInsulted;
    public static IMbEvent<MobileParty> MobilePartyCreated => CampaignEvents.MobilePartyCreated;
    public static IMbEvent<MobileParty, PartyBase> MobilePartyDestroyed => CampaignEvents.MobilePartyDestroyed;
    public static IMbEvent<ItemRoster> ItemsLooted => CampaignEvents.ItemsLooted;
    public static IMbEvent<Town, bool> TownRebelliosStateChanged => CampaignEvents.TownRebelliosStateChanged;
    public static IMbEvent<Settlement, Clan> RebellionFinished => CampaignEvents.RebellionFinished;
    public static IMbEvent<PartyBase, PartyBase, object, bool> BattleStarted => CampaignEvents.BattleStarted;
    public static IMbEvent<CharacterObject, Town> TournamentFinished => CampaignEvents.TournamentFinished;
    public static IMbEvent<IFaction, IFaction> WarDeclared => CampaignEvents.WarDeclared;
    public static IMbEvent<Village> VillageLooted => CampaignEvents.VillageLooted;
    public static IMbEvent<Town> PlayerStartedTournamentMatch => CampaignEvents.PlayerStartedTournamentMatch;

    public MBCampaignEvents() { }

    public static MBCampaignEvent CreatePeriodicEvent(CampaignTime triggerPeriod, CampaignTime initialWait) => CampaignEvents.CreatePeriodicEvent(triggerPeriod, initialWait);
    public static void RemoveListeners(object o) => CampaignEvents.RemoveListeners(o);
    public static void SetupPreConversation() => CampaignEvents.SetupPreConversation();
    public void CollectAvailableTutorials(ref List<CampaignTutorial> tutorials) => UnwrappedObject.CollectAvailableTutorials(ref tutorials);
    public void LocationCharactersAreReadyToSpawn(Dictionary<string, int> unusedUsablePointCount) => UnwrappedObject.LocationCharactersAreReadyToSpawn(unusedUsablePointCount);
    public void LocationCharactersSimulated() => UnwrappedObject.LocationCharactersSimulated();
    public void MissionTick(float dt) => UnwrappedObject.MissionTick(dt);
    public void OnAfterMissionStarted(IMission iMission) => UnwrappedObject.OnAfterMissionStarted(iMission);
    public void OnAfterSettlementEntered(MobileParty party, Settlement settlement, Hero hero) => UnwrappedObject.OnAfterSettlementEntered(party, settlement, hero);
    public void OnArmyOverlaySetDirty() => UnwrappedObject.OnArmyOverlaySetDirty();
    public void OnBeforeSave() => UnwrappedObject.OnBeforeSave();
    public void OnCampaignStartUp9() => UnwrappedObject.OnCampaignStartUp9();
    public void OnCharacterDefeated(Hero winner, Hero loser) => UnwrappedObject.OnCharacterDefeated(winner, loser);
    public void OnCharacterPortraitPopUpClosed() => UnwrappedObject.OnCharacterPortraitPopUpClosed();
    public void OnCharacterPortraitPopUpOpened(CharacterObject character) => UnwrappedObject.OnCharacterPortraitPopUpOpened(character);
    public void OnCheckForIssue(IssueArgs issueArgs) => UnwrappedObject.OnCheckForIssue(issueArgs);
    public void OnClanLeaderChanged(Hero oldLeader, Hero newLeader) => UnwrappedObject.OnClanLeaderChanged(oldLeader, newLeader);
    public void OnCommonAreaStateChanged(CommonArea commonArea, CommonArea.AreaState oldState, CommonArea.AreaState newState) => UnwrappedObject.OnCommonAreaStateChanged(commonArea, oldState, newState);
    public void OnEquipmentSmeltedByHero(Hero hero, EquipmentElement smeltedEquipmentElement) => UnwrappedObject.OnEquipmentSmeltedByHero(hero, smeltedEquipmentElement);
    public void OnGameLoadFinished() => UnwrappedObject.OnGameLoadFinished();
    public void OnGameOver() => UnwrappedObject.OnGameOver();
    public void OnHeroCreated(Hero hero, bool isBornNaturally = false) => UnwrappedObject.OnHeroCreated(hero, isBornNaturally = false);
    public void OnHeroWounded(Hero woundedHero) => UnwrappedObject.OnHeroWounded(woundedHero);
    public void OnIssueUpdated(IssueBase issue, IssueBase.IssueUpdateDetails details, Hero issueSolver = null) => UnwrappedObject.OnIssueUpdated(issue, details, issueSolver = null);
    public void OnMissionEnded(IMission mission) => UnwrappedObject.OnMissionEnded(mission);
    public void OnMissionStarted(IMission mission) => UnwrappedObject.OnMissionStarted(mission);
    public void OnNewGameCreated(CampaignGameStarter campaignGameStarter) => UnwrappedObject.OnNewGameCreated(campaignGameStarter);
    public void OnNewGameCreated2() => UnwrappedObject.OnNewGameCreated2();
    public void OnNewGameCreated3() => UnwrappedObject.OnNewGameCreated3();
    public void OnNewGameCreated4() => UnwrappedObject.OnNewGameCreated4();
    public void OnNewGameCreated5() => UnwrappedObject.OnNewGameCreated5();
    public void OnNewGameCreated6() => UnwrappedObject.OnNewGameCreated6();
    public void OnNewGameCreated7() => UnwrappedObject.OnNewGameCreated7();
    public void OnNewGameCreated8() => UnwrappedObject.OnNewGameCreated8();
    public void OnPartyVisibilityChanged(PartyBase party) => UnwrappedObject.OnPartyVisibilityChanged(party);
    public void OnPlayerBoardGameOver(Hero opposingHero, BoardGameHelper.BoardGameState state) => UnwrappedObject.OnPlayerBoardGameOver(opposingHero, state);
    public void OnPlayerCharacterChanged(Hero newPlayer, MobileParty newMainParty) => UnwrappedObject.OnPlayerCharacterChanged(newPlayer, newMainParty);
    public void OnPlayerDonatedHeroPrisoner(Hero donatedHero, Settlement donatedSettlement) => UnwrappedObject.OnPlayerDonatedHeroPrisoner(donatedHero, donatedSettlement);
    public void OnPlayerEliminatedFromTournament(int round, Town town) => UnwrappedObject.OnPlayerEliminatedFromTournament(round, town);
    public void OnPlayerSiegeStarted() => UnwrappedObject.OnPlayerSiegeStarted();
    public void OnPlayerStartedTournamentMatch(Town town) => UnwrappedObject.OnPlayerStartedTournamentMatch(town);
    public void OnPlayerStartRecruitment(CharacterObject recruitTroopCharacter) => UnwrappedObject.OnPlayerStartRecruitment(recruitTroopCharacter);
    public void OnPlayerStartTalkFromMenu(Hero hero) => UnwrappedObject.OnPlayerStartTalkFromMenu(hero);
    public void OnPrisonerRecruited(FlattenedTroopRoster roster) => UnwrappedObject.OnPrisonerRecruited(roster);
    public void OnPrisonerReleased(FlattenedTroopRoster roster) => UnwrappedObject.OnPrisonerReleased(roster);
    public void OnPrisonersChangeInSettlement(Settlement settlement) => UnwrappedObject.OnPrisonersChangeInSettlement(settlement);
    public void OnPrisonerTaken(FlattenedTroopRoster roster) => UnwrappedObject.OnPrisonerTaken(roster);
    public void OnSettlementEntered(MobileParty party, Settlement settlement, Hero hero) => UnwrappedObject.OnSettlementEntered(party, settlement, hero);
    public void OnSiegeEventEnded(SiegeEvent siegeEvent) => UnwrappedObject.OnSiegeEventEnded(siegeEvent);
    public void OnSiegeEventStarted(SiegeEvent siegeEvent) => UnwrappedObject.OnSiegeEventStarted(siegeEvent);
    public void OnTick(float dt) => UnwrappedObject.OnTick(dt);
    public void OnTournamentFinished(CharacterObject winner, Town town) => UnwrappedObject.OnTournamentFinished(winner, town);
    public void OnTournamentStarted(Town town) => UnwrappedObject.OnTournamentStarted(town);
    public void OnTutorialCompleted(string tutorial) => UnwrappedObject.OnTutorialCompleted(tutorial);
    public void OnUnitRecruited(CharacterObject character, int amount) => UnwrappedObject.OnUnitRecruited(character, amount);

    public static implicit operator CampaignEvents(MBCampaignEvents wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCampaignEvents(CampaignEvents obj) => MBCampaignEvents.GetWrapper(obj);
  }

  public class MBCampaignEventsList : MBListBase<MBCampaignEvents, MBCampaignEventsList>
  {
    public MBCampaignEventsList(params MBCampaignEvents[] wrappers) : this((IEnumerable<MBCampaignEvents>)wrappers) { }
    public MBCampaignEventsList(IEnumerable<MBCampaignEvents> wrappers) => AddRange(wrappers);
    public MBCampaignEventsList(MBCampaignEvents wrapper) => Add(wrapper);
    public MBCampaignEventsList() { }

    public static implicit operator List<CampaignEvents>(MBCampaignEventsList wrapperList) => wrapperList.Unwrap<MBCampaignEvents, CampaignEvents>();
    public static implicit operator MBCampaignEventsList(List<CampaignEvents> objectList) => (MBCampaignEventsList)objectList.Wrap<MBCampaignEvents, CampaignEvents>();
    public static implicit operator MBCampaignEvents[](MBCampaignEventsList wrapperList) => wrapperList.ToArray();
  }
}
