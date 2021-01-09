using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCampaignEvents : MBWrapperBase<MBCampaignEvents, CampaignEvents>
  {
    public static IMbEvent<MBCharacterObject> ConversationEnded { get; }
    public static IMbEvent OnNewGameCreatedEvent4 { get; }
    public static IMbEvent OnNewGameCreatedEvent5 { get; }
    public static IMbEvent OnNewGameCreatedEvent6 { get; }
    public static IMbEvent OnNewGameCreatedEvent7 { get; }
    public static IMbEvent OnNewGameCreatedEvent8 { get; }
    public static IMbEvent OnNewGameCreatedEvent9 { get; }
    public static IMbEvent<CampaignGameStarter> OnGameEarlyLoadedEvent { get; }
    public static IMbEvent<MBCampaignGameStarter> OnGameLoadedEvent { get; }
    public static IMbEvent OnGameLoadFinishedEvent { get; }
    public static IMbEvent<MBMobileParty, PartyThinkParams> AiHourlyTickEvent { get; }
    public static IMbEvent TickPartialHourlyAiEvent { get; }
    public static IMbEvent<MBMobileParty> OnPartyJoinedArmyEvent { get; }
    public static IMbEvent<MBMobileParty> OnPartyArrivedArmyEvent { get; }
    public static IMbEvent<MBMobileParty> OnPartyRemovedArmyEvent { get; }
    public static IMbEvent<MBHero, Army.ArmyLeaderThinkReason> OnArmyLeaderThinkEvent { get; }
    public static IMbEvent<IMission> OnMissionEndedEvent { get; }
    public static IMbEvent<MBMapEvent> OnPlayerBattleEndEvent { get; }
    public static IMbEvent<MBMobileParty, List<CharacterObject>, int> OnDoMeetingInMapEvent { get; }
    public static IMbEvent<MBCharacterObject, int> OnUnitRecruitedEvent { get; }
    public static IMbEvent<MBHero> OnChildConceivedEvent { get; }
    public static IMbEvent<MBHero, List<Hero>, int> OnGivenBirthEvent { get; }
    public static IMbEvent<float> MissionTickEvent { get; }
    public static IMbEvent SetupPreConversationEvent { get; }
    public static IMbEvent ArmyOverlaySetDirtyEvent { get; }
    public static IMbEvent<int> PlayerDesertedBattleEvent { get; }
    public static IMbEvent OnNewGameCreatedEvent3 { get; }
    public static IMbEvent OnNewGameCreatedEvent2 { get; }
    public static IMbEvent<MBCampaignGameStarter> OnNewGameCreatedEvent { get; }
    public static IMbEvent<MBCampaignGameStarter> OnSessionLaunchedEvent { get; }
    public static IMbEvent<MBSettlement> PrisonersChangeInSettlement { get; }
    public static IMbEvent<MBHero, BoardGameHelper.BoardGameState> OnPlayerBoardGameOverEvent { get; }
    public static IMbEvent<IMission> OnMissionStartedEvent { get; }
    public static IMbEvent<MBCommonArea, CommonArea.AreaState, CommonArea.AreaState> OnCommonAreaStateChangedEvent { get; }
    public static IMbEvent BeforeMissionOpenedEvent { get; }
    public static IMbEvent<MBPartyBase> OnPartyRemovedEvent { get; }
    public static IMbEvent<MBPartyBase> OnPartySizeChangedEvent { get; }
    public static IMbEvent<MBSettlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail> OnSettlementOwnerChangedEvent { get; }
    public static IMbEvent<MBTown, Hero, Hero> OnGovernorChangedEvent { get; }
    public static IMbEvent<MBMobileParty, Settlement> OnSettlementLeftEvent { get; }
    public static IMbEvent WeeklyTickEvent { get; }
    public static IMbEvent DailyTickEvent { get; }
    public static IMbEvent<MBMobileParty> DailyTickPartyEvent { get; }
    public static IMbEvent<MBTown> DailyTickTownEvent { get; }
    public static IMbEvent<MBSettlement> DailyTickSettlementEvent { get; }
    public static IMbEvent<MBSettlement> WeeklyTickSettlementEvent { get; }
    public static IMbEvent<MBHero> DailyTickHeroEvent { get; }
    public static IMbEvent<MBClan> DailyTickClanEvent { get; }
    public static IMbEvent<MBList<CampaignTutorial>> CollectAvailableTutorialsEvent { get; }
    public static IMbEvent<string> OnTutorialCompletedEvent { get; }
    public static IMbEvent<MBTown, Building, int> OnBuildingLevelChangedEvent { get; }
    public static IMbEvent AfterDailyTickEvent { get; }
    public static IMbEvent HourlyTickEvent { get; }
    public static IMbEvent<MBMobileParty> HourlyTickPartyEvent { get; }
    public static IMbEvent<MBSettlement> HourlyTickSettlementEvent { get; }
    public static IMbEvent<MBClan> HourlyTickClanEvent { get; }
    public static IMbEvent<float> TickEvent { get; }
    public static IMbEvent<MBPartyBase> PartyVisibilityChangedEvent { get; }
    public static IMbEvent ObjectRegisteredToVisualTrackerEvent { get; }
    public static IMbEvent<MBTrack> TrackDetectedEvent { get; }
    public static IMbEvent<MBTrack> TrackLostEvent { get; }
    public static IMbEvent<Tuple<PersuasionOptionArgs, PersuasionOptionResult>> PersuasionProgressCommitedEvent { get; }
    public static IMbEvent<MBNews> OnNewsSendedToNewsManagerEvent { get; }
    public static IMbEvent<MBQuestBase, QuestBase.QuestCompleteDetails> OnQuestCompletedEvent { get; }
    public static IMbEvent<MBQuestBase> OnQuestStartedEvent { get; }
    public static IMbEvent<MBItemObject, Settlement, int> OnItemProducedEvent { get; }
    public static IMbEvent<MBItemObject, Settlement, int> OnItemConsumedEvent { get; }
    public static IMbEvent<MBMobileParty> OnPartyConsumedFoodEvent { get; }
    public static IMbEvent<MBPartyBase, PartyBase> PartyEncounteredEvent { get; }
    public static IMbEvent OnBeforeMainCharacterDiedEvent { get; }
    public static IMbEvent<MBIssueBase> OnNewIssueCreatedEvent { get; }
    public static IMbEvent<MBIssueBase, Hero> OnIssueOwnerChangedEvent { get; }
    public static IMbEvent OnGameOverEvent { get; }
    public static IMbEvent<List<(ItemRosterElement, int)>, List<(ItemRosterElement, int)>> PlayerInventoryExchangeEvent { get; }
    public static IMbEvent<MBSettlement, MobileParty, bool, bool> SiegeCompletedEvent { get; }
    public static IMbEvent<MBBattleSideEnum, MapEvent> RaidCompletedEvent { get; }
    public static IMbEvent<MBBattleSideEnum, MapEvent> ForceVolunteersCompletedEvent { get; }
    public static IMbEvent<MBBattleSideEnum, MapEvent> ForceSuppliesCompletedEvent { get; }
    public static IMbEvent<MBClan> OnClanDestroyedEvent { get; }
    public static IMbEvent<MBItemObject, Crafting.OverrideData> OnNewItemCraftedEvent { get; }
    public static IMbEvent<MBCraftingPiece> CraftingPartUnlockedEvent { get; }
    public static IMbEvent<MBWorkshop, Hero, WorkshopType> OnWorkshopChangedEvent { get; }
    public static IMbEvent<MBMobileParty> OnLordPartySpawnedEvent { get; }
    public static IMbEvent OnBeforeSaveEvent { get; }
    public static IMbEvent<MBFlattenedTroopRoster> OnPrisonerTakenEvent { get; }
    public static IMbEvent<MBFlattenedTroopRoster> OnPrisonerReleasedEvent { get; }
    public static IMbEvent<MBFlattenedTroopRoster> OnPrisonerRecruitedEvent { get; }
    public static IMbEvent<MBSiegeEvent, BattleSideEnum, SiegeEngineType> SiegeEngineBuiltEvent { get; }
    public static IMbEvent<MBHero, Hero, float> OnHeroSharedFoodWithAnotherHeroEvent { get; }
    public static IMbEvent<MBSettlement> OnHideoutClearedEvent { get; }
    public static IMbEvent<MBPartyBase, PartyBase> OnHideoutSpottedEvent { get; }
    public static IMbEvent<MBDictionary<string, int>> LocationCharactersAreReadyToSpawnEvent { get; }
    public static IMbEvent LocationCharactersSimulatedEvent { get; }
    public static IMbEvent<MBCharacterObject, CharacterObject, int> PlayerUpgradedTroopsEvent { get; }
    public static IMbEvent<MBCharacterObject> CharacterPortraitPopUpOpenedEvent { get; }
    public static IMbEvent CharacterPortraitPopUpClosedEvent { get; }
    public static IMbEvent<MBHero> PlayerStartTalkFromMenu { get; }
    public static IMbEvent<MBGameMenuOption> GameMenuOptionSelectedEvent { get; }
    public static IMbEvent<MBCharacterObject> PlayerStartRecruitmentEvent { get; }
    public static IMbEvent<MBHero, MobileParty> OnPlayerCharacterChangedEvent { get; }
    public static IMbEvent<MBHero, Hero> OnClanLeaderChangedEvent { get; }
    public static IMbEvent<MBSiegeEvent> OnSiegeEventStartedEvent { get; }
    public static IMbEvent OnPlayerSiegeStartedEvent { get; }
    public static IMbEvent<MBSiegeEvent> OnSiegeEventEndedEvent { get; }
    public static IMbEvent<MBMobileParty, Settlement, BattleSideEnum, SiegeEngineType, SiegeBombardTargets> OnSiegeBombardmentHitEvent { get; }
    public static IMbEvent<MBMobileParty, Settlement, BattleSideEnum, SiegeEngineType, bool> OnSiegeBombardmentWallHitEvent { get; }
    public static IMbEvent<MBMobileParty, Settlement, BattleSideEnum, SiegeEngineType> OnSiegeEngineDestroyedEvent { get; }
    public static IMbEvent<MBList<TradeRumor>, Settlement> OnTradeRumorIsTakenEvent { get; }
    public static IMbEvent<MBIssueArgs> OnCheckForIssueEvent { get; }
    public static IMbEvent<MBIssueBase, IssueBase.IssueUpdateDetails, Hero> OnIssueUpdatedEvent { get; }
    public static IMbEvent<MBMobileParty, TroopRoster> OnTroopsDesertedEvent { get; }
    public static IMbEvent<MBHero, Settlement, Hero, CharacterObject, int> OnTroopRecruitedEvent { get; }
    public static IMbEvent<MBHero, Settlement, TroopRoster> OnTroopGivenToSettlementEvent { get; }
    public static IMbEvent<MBPartyBase, PartyBase, ItemRosterElement, int, Settlement> OnItemSoldEvent { get; }
    public static IMbEvent<MBMobileParty, Town, List<(ItemObject, int)>> OnCaravanTransactionCompletedEvent { get; }
    public static IMbEvent<MBMobileParty, TroopRoster, Settlement> OnPrisonerSoldEvent { get; }
    public static IMbEvent<MBMobileParty> OnPartyDisbandedEvent { get; }
    public static IMbEvent<MBMobileParty> OnPartyDisbandCanceledEvent { get; }
    public static IMbEvent<MBMapEvent, PartyBase, PartyBase> MapEventStarted { get; }
    public static IMbEvent<MBMapEvent> MapEventEnded { get; }
    public static IMbEvent<MBHero, EquipmentElement> OnEquipmentSmeltedByHeroEvent { get; }
    public static IMbEvent<MBHero> CompanionRemoved { get; }
    public static IMbEvent<MBHero, Hero, Romance.RomanceLevelEnum> RomanticStateChanged { get; }
    public static IMbEvent<MBCommonArea, Hero, bool> CommonAreaOwnerChanged { get; }
    public static IMbEvent<MBMobileParty, MobileParty, Hero, Settlement> CommonAreaFightOccured { get; }
    public static IMbEvent<MBTown, int, int> MercenaryNumberChangedInTown { get; }
    public static IMbEvent<MBTown, CharacterObject, CharacterObject> MercenaryTroopChangedInTown { get; }
    public static IMbEvent<MBMobileParty, Settlement, Hero> AfterSettlementEntered { get; }
    public static IMbEvent<MBMobileParty, Settlement, Hero> SettlementEntered { get; }
    public static IMbEvent<MBVillage, Village.VillageStates, Village.VillageStates, MobileParty> VillageStateChanged { get; }
    public static IMbEvent<MBHero, PerkObject> PerkOpenedEvent { get; }
    public static IMbEvent<MBArmy, Settlement> ArmyGathered { get; }
    public static IMbEvent<MBArmy, Army.ArmyDispersionReason, bool> ArmyDispersed { get; }
    public static IMbEvent<MBArmy> ArmyCreated { get; }
    public static IMbEvent<MBKingdomDecision, DecisionOutcome, bool> KingdomDecisionConcluded { get; }
    public static IMbEvent<MBKingdomDecision, bool> KingdomDecisionCancelled { get; }
    public static IMbEvent<MBHero, Hero, bool> HeroesMarried { get; }
    public static IMbEvent<MBKingdomDecision, bool> KingdomDecisionAdded { get; }
    public static IMbEvent<MBMobileParty> BanditPartyRecruited { get; }
    public static IMbEvent<MB(Hero, PartyBase), (Hero, PartyBase), ItemObject, int, bool> HeroOrPartyGaveItem { get; }
  public static IMbEvent<MB(Hero, PartyBase), (Hero, PartyBase), (int, string), bool> HeroOrPartyTradedGold { get; }
public static IMbEvent<MBClan, Kingdom, Kingdom, bool, bool> ClanChangedKingdom { get; }
public static IMbEvent<MBClan, bool> ClanTierIncrease { get; }
public static IMbEvent<MBIssueBase, bool> IssueLogAddedEvent { get; }
public static IMbEvent<MBQuestBase, bool> QuestLogAddedEvent { get; }
public static IMbEvent<MBHero, Hero, int, bool> HeroRelationChanged { get; }
public static IMbEvent<MBHero> HeroWounded { get; }
public static IMbEvent<MBHero, Settlement> OnPlayerDonatedHeroPrisonerEvent { get; }
public static IMbEvent<MBHero, SkillObject, bool, int, bool> HeroGainedSkill { get; }
public static IMbEvent<MBHero, bool> HeroLevelledUp { get; }
public static IMbEvent<MBBarterData> BarterablesRequested { get; }
public static CampaignEvents Instance { get; }
public static IMbEvent<MBClan, Kingdom, Kingdom> MercenaryClanChangedKingdom { get; }
public static IMbEvent<int, Town> PlayerEliminatedFromTournament { get; }
public static IMbEvent<MBHero, bool> HeroCreated { get; }
public static IMbEvent<MBTown> TournamentStarted { get; }
public static IMbEvent<MBVillage> VillageBeingRaided { get; }
public static IMbEvent<MBVillage> VillageBecomeNormal { get; }
public static IMbEvent<MBKingdom> KingdomCreatedEvent { get; }
public static IMbEvent<MBKingdom> KingdomDestroyedEvent { get; }
public static IMbEvent<IFaction, IFaction> MakePeace { get; }
public static IMbEvent<MBMenuCallbackArgs> BeforeGameMenuOpenedEvent { get; }
public static IMbEvent<MBMenuCallbackArgs> AfterGameMenuOpenedEvent { get; }
public static IMbEvent<MBMenuCallbackArgs> GameMenuOpened { get; }
public static IMbEvent<IMission> AfterMissionStarted { get; }
public static IMbEvent<MBHero> NewCompanionAdded { get; }
public static IMbEvent<MBHero, int, bool> RenownGained { get; }
public static IMbEvent<MBHero> PlayerMetCharacter { get; }
public static IMbEvent<MBHero> CharacterBecameFugitive { get; }
public static IMbEvent<MBHero, PartyBase, IFaction, EndCaptivityDetail> HeroPrisonerReleased { get; }
public static IMbEvent<MBPartyBase, Hero> HeroPrisonerTaken { get; }
public static IMbEvent<MBHero, Hero> CharacterDefeated { get; }
public static IMbEvent<MBHero> HeroReachesTeenAgeEvent { get; }
public static IMbEvent<MBHero> HeroGrowsOutOfInfancyEvent { get; }
public static IMbEvent<MBHero> HeroComesOfAgeEvent { get; }
public static IMbEvent<MBHero, Hero, KillCharacterAction.KillCharacterActionDetail, bool> HeroKilledEvent { get; }
public static IMbEvent<MBHero, Hero, CharacterObject, ActionNotes> CharacterInsulted { get; }
public static IMbEvent<MBMobileParty> MobilePartyCreated { get; }
public static IMbEvent<MBMobileParty, PartyBase> MobilePartyDestroyed { get; }
public static IMbEvent<MBItemRoster> ItemsLooted { get; }
public static IMbEvent<MBTown, bool> TownRebelliosStateChanged { get; }
public static IMbEvent<MBSettlement, Clan> RebellionFinished { get; }
public static IMbEvent<MBPartyBase, PartyBase, object, bool> BattleStarted { get; }
public static IMbEvent<MBCharacterObject, Town> TournamentFinished { get; }
public static IMbEvent<IFaction, IFaction> WarDeclared { get; }
public static IMbEvent<MBVillage> VillageLooted { get; }
public static IMbEvent<MBTown> PlayerStartedTournamentMatch { get; }

public static MBCampaignEvent CreatePeriodicEvent(CampaignTime triggerPeriod, CampaignTime initialWait);
public static void RemoveListeners(object o);
public static void SetupPreConversation();
public override void CollectAvailableTutorials(ref List<CampaignTutorial> tutorials);
public override void LocationCharactersAreReadyToSpawn(Dictionary<string, int> unusedUsablePointCount);
public override void LocationCharactersSimulated();
public override void MissionTick(float dt);
public override void OnAfterMissionStarted(IMission iMission);
public override void OnAfterSettlementEntered(MobileParty party, Settlement settlement, Hero hero);
public override void OnArmyOverlaySetDirty();
public override void OnBeforeSave();
public override void OnCampaignStartUp9();
public override void OnCharacterDefeated(Hero winner, Hero loser);
public override void OnCharacterPortraitPopUpClosed();
public override void OnCharacterPortraitPopUpOpened(CharacterObject character);
public override void OnCheckForIssue(IssueArgs issueArgs);
public override void OnClanLeaderChanged(Hero oldLeader, Hero newLeader);
public override void OnCommonAreaStateChanged(CommonArea commonArea, CommonArea.AreaState oldState, CommonArea.AreaState newState);
public override void OnEquipmentSmeltedByHero(Hero hero, EquipmentElement smeltedEquipmentElement);
public override void OnGameLoadFinished();
public override void OnGameOver();
public override void OnHeroCreated(Hero hero, bool isBornNaturally = false);
public override void OnHeroWounded(Hero woundedHero);
public override void OnIssueUpdated(IssueBase issue, IssueBase.IssueUpdateDetails details, Hero issueSolver = null);
public override void OnMissionEnded(IMission mission);
public override void OnMissionStarted(IMission mission);
public override void OnNewGameCreated(CampaignGameStarter campaignGameStarter);
public override void OnNewGameCreated2();
public override void OnNewGameCreated3();
public override void OnNewGameCreated4();
public override void OnNewGameCreated5();
public override void OnNewGameCreated6();
public override void OnNewGameCreated7();
public override void OnNewGameCreated8();
public override void OnPartyVisibilityChanged(PartyBase party);
public override void OnPlayerBoardGameOver(Hero opposingHero, BoardGameHelper.BoardGameState state);
public override void OnPlayerCharacterChanged(Hero newPlayer, MobileParty newMainParty);
public override void OnPlayerDonatedHeroPrisoner(Hero donatedHero, Settlement donatedSettlement);
public override void OnPlayerEliminatedFromTournament(int round, Town town);
public override void OnPlayerSiegeStarted();
public override void OnPlayerStartedTournamentMatch(Town town);
public override void OnPlayerStartRecruitment(CharacterObject recruitTroopCharacter);
public override void OnPlayerStartTalkFromMenu(Hero hero);
public override void OnPrisonerRecruited(FlattenedTroopRoster roster);
public override void OnPrisonerReleased(FlattenedTroopRoster roster);
public override void OnPrisonersChangeInSettlement(Settlement settlement);
public override void OnPrisonerTaken(FlattenedTroopRoster roster);
public override void OnSettlementEntered(MobileParty party, Settlement settlement, Hero hero);
public override void OnSiegeEventEnded(SiegeEvent siegeEvent);
public override void OnSiegeEventStarted(SiegeEvent siegeEvent);
public void OnTick(float dt);
public override void OnTournamentFinished(CharacterObject winner, Town town);
public override void OnTournamentStarted(Town town);
public override void OnTutorialCompleted(string tutorial);
public override void OnUnitRecruited(CharacterObject character, int amount);
public static implicit operator CampaignEvents(MBCampaignEvents wrapper) => wrapper.UnwrappedObject;
public static implicit operator MBCampaignEvents(CampaignEvents obj) => MBCampaignEvents.GetWrapperFor(obj);
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
