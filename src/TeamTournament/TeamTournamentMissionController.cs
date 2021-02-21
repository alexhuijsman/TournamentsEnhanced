using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Source.Missions.AgentControllers;
using SandBox.TournamentMissions.Missions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment.Managers;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace TournamentsEnhanced.TeamTournament
{
  public class TeamTournamentMissionController : MissionLogic, ITournamentGameBehavior
  {
    private const float XpShareForDamage = 0.5f;

    public TeamTournamentMissionController(CultureObject culture)
    {
      this._culture = culture;
    }

    public override void AfterStart()
    {
      TournamentBehavior.DeleteTournamentSetsExcept(base.Mission.Scene.FindEntityWithTag("tournament_fight"));
      this._spawnPoints = new List<GameEntity>();

      for (int i = 0; i < 4; i++)
      {
        var gameEntity = base.Mission.Scene.FindEntityWithTag("sp_arena_" + (i + 1));
        if (gameEntity != null)
          this._spawnPoints.Add(gameEntity);
      }

      if (this._spawnPoints.Count < 4)
        this._spawnPoints = base.Mission.Scene.FindEntitiesWithTag("sp_arena").ToList<GameEntity>();
    }

    public void PrepareForMatch() // also called from skip SkipMatch
    {
      var numMembers = Math.Max(Math.Min(this._match.Teams.Max(x => x.Members.Count()), 4), 2);
      var teamWeaponEquipmentList = this.GetTeamWeaponEquipmentList(numMembers);

      foreach (var team in _match.Teams)
      {
        int num = 0;
        foreach (var tournamentMember in team.Members)
        {
          tournamentMember.MatchEquipment = teamWeaponEquipmentList[num].Clone(false);
          this.AddRandomClothes(this._culture, tournamentMember);
          num = num++ % numMembers;
        }
      }
    }

    public void StartMatch(TeamTournamentMatch match, bool isLastRound)
    {
      this._match = match;
      this._isLastRound = isLastRound;
      this.PrepareForMatch();
      base.Mission.SetMissionMode(MissionMode.Battle, true);
      var tmpList = new List<Team>();
      int count = this._spawnPoints.Count;
      foreach (var tournamentTeam in this._match.Teams)
      {
        var side = tournamentTeam.IsPlayerTeam ? BattleSideEnum.Defender : BattleSideEnum.Attacker;
        var team = base.Mission.Teams.Add(side, tournamentTeam.TeamColor, uint.MaxValue, tournamentTeam.TeamBanner, true, false, true);
        var spawnPoint = this._spawnPoints[tmpList.Count % count];

        foreach (var tournamentMember in tournamentTeam.Members)
          this.SpawnTournamentMember(spawnPoint, tournamentMember, team);

        tmpList.ForEach(x => x.SetIsEnemyOf(team, true));
        tmpList.Add(team);
      }
      this._aliveMembers = new List<TeamTournamentMember>(this._match.MatchMembers);
      this._aliveTeams = new List<TeamTournamentTeam>(this._match.Teams);
    }

    private void SpawnTournamentMember(GameEntity spawnPoint, TeamTournamentMember member, Team team)
    {
      MatrixFrame globalFrame = spawnPoint.GetGlobalFrame();
      globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
      this.SpawnAgentWithRandomItems(member, team, globalFrame);
    }

    private List<Equipment> GetTeamWeaponEquipmentList(int teamSize)
    {
      var retList = new List<Equipment>();
      CultureObject culture = PlayerEncounter.EncounterSettlement.Culture;
      var equiptmentSet = teamSize == 4 ? culture.TournamentTeamTemplatesForFourParticipant : (teamSize == 2 ? culture.TournamentTeamTemplatesForTwoParticipant : culture.TournamentTeamTemplatesForOneParticipant);
      CharacterObject characterObject;

      if (equiptmentSet.Count > 0)
        characterObject = equiptmentSet[MBRandom.RandomInt(equiptmentSet.Count)];
      else
        characterObject = teamSize == 4 ? this._defaultWeaponTemplatesIdTeamSizeFour : (teamSize == 2 ? this._defaultWeaponTemplatesIdTeamSizeTwo : this._defaultWeaponTemplatesIdTeamSizeOne);

      foreach (var sourceEquipment in characterObject.BattleEquipments)
      {
        var equipment = new Equipment();
        equipment.FillFrom(sourceEquipment, true);
        retList.Add(equipment);
      }
      return retList;
    }

    public void SkipMatch(TeamTournamentMatch match)
    {
      this._match = match;
      this.PrepareForMatch();
      this.Simulate();
    }

    public bool IsMatchEnded()
    {
      if (this._isSimulated || this._match == null)
        return true;

      if ((this._endTimer != null && this._endTimer.ElapsedTime > 6f) || this._forceEndMatch)
      {
        this._forceEndMatch = false;
        this._endTimer = null;
        return true;
      }

      if (this._cheerTimer != null && this._cheerTimer.ElapsedTime > 1f)
      {
        this.OnMatchResultsReady();
        this._cheerTimer = null;

        foreach (Agent agent in base.Mission.Agents)
        {
          if (agent.IsAIControlled)
            Mission.GetMissionBehaviour<AgentVictoryLogic>().SetTimersOfVictoryReactions(agent, 1f, 3f);
        }

        return false;
      }

      if (this._endTimer == null && !this.CheckIfIsThereAnyEnemies())
      {
        this._endTimer = new BasicTimer(MBCommon.TimeType.Mission);
        this._cheerTimer = new BasicTimer(MBCommon.TimeType.Mission);
      }

      return false;
    }

    public void OnMatchResultsReady()
    {
      if (!this._match.IsPlayerParticipating)
        InformationManager.AddQuickInformation(new TextObject("{=UBd0dEPp}Match is over", null), 0, null, "");
      else if (this._match.IsPlayerTeamWinner)
      {
        if (this._isLastRound)
          InformationManager.AddQuickInformation(new TextObject("{=wOqOQuJl}Round is over, your team survived the final round of the tournament.", null), 0, null, "");
        else
          InformationManager.AddQuickInformation(new TextObject("{=fkOYvnVG}Round is over, your team is qualified for the next stage of the tournament.", null), 0, null, "");
      }
      else
        InformationManager.AddQuickInformation(new TextObject("{=MLyBN51z}Round is over, your team is disqualified from the tournament.", null), 0, null, "");
    }

    public void OnMatchEnded()
    {
      for (int i = base.Mission.Agents.Count - 1; i >= 0; i--)
      {
        base.Mission.Agents[i].FadeOut(true, false);
      }
      base.Mission.ClearCorpses(false);
      base.Mission.Teams.Clear();
      base.Mission.RemoveSpawnedItemsAndMissiles();
      this._match = null;
      this._endTimer = null;
      this._cheerTimer = null;
      this._isSimulated = false;
    }

    private void SpawnAgentWithRandomItems(TeamTournamentMember member, Team team, MatrixFrame frame)
    {
      frame.Strafe((float)MBRandom.RandomInt(-2, 2) * 1f);
      frame.Advance((float)MBRandom.RandomInt(0, 2) * 1f);
      var character = member.Character;
      var agentBuildData = new AgentBuildData(new SimpleAgentOrigin(character, -1, null, member.Descriptor)).Team(team).InitialFrame(frame).Equipment(member.MatchEquipment).ClothingColor1(team.Color).Banner(team.Banner).Controller(character.IsPlayerCharacter ? Agent.ControllerType.Player : Agent.ControllerType.AI);
      var agent = base.Mission.SpawnAgent(agentBuildData, false, 0);
      if (character.IsPlayerCharacter)
      {
        agent.Health = (float)character.HeroObject.HitPoints;
        base.Mission.PlayerTeam = team;
      }
      else
      {
        agent.AddController(typeof(FighterAgentController));
        agent.SetWatchState(AgentAIStateFlagComponent.WatchState.Alarmed);
      }
      agent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp);
    }

    private void AddRandomClothes(CultureObject culture, TeamTournamentMember member)
    {
      var randomBattleEquipment = member.Character.RandomBattleEquipment;
      for (int i = 5; i < 10; i++)
      {
        var equipmentFromSlot = randomBattleEquipment.GetEquipmentFromSlot((EquipmentIndex)i);
        if (equipmentFromSlot.Item != null)
        {
          member.MatchEquipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, equipmentFromSlot);
        }
      }
    }

    private bool IsTeamDead(TeamTournamentTeam team) => !this._aliveMembers.Any(x => x.Team == team);

    private void AddScoreToRemainingTeams() => this._aliveTeams.ForEach(x => x.AddScore(1));

    private void AddScoreToKillerTeam(int killerUniqueSeed)
    {
      this._aliveTeams.FirstOrDefault(x => x.Members.Any(m => m.IsCharWithDescriptor(killerUniqueSeed))).AddScore(1);
    }

    private void AddLastTeamScore()
    {
      this._aliveTeams.First().AddScore(_match.Teams.Count());
    }

    public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
    {
      if (!this.IsMatchEnded() && affectorAgent != null && affectedAgent != affectorAgent && affectedAgent.IsHuman && affectorAgent.IsHuman)
      {
        var member = this._match.MatchMembers.FirstOrDefault(x => x.IsCharWithDescriptor(affectedAgent.Origin.UniqueSeed));

        this._aliveMembers.Remove(member);

        // apply score only if not on same team
        if (affectedAgent.Team != affectorAgent.Team)
          this.AddScoreToKillerTeam(affectorAgent.Origin.UniqueSeed);

        if (this.IsTeamDead(member.Team))
        {
          this._aliveTeams.Remove(member.Team);

          // give last team a bonus
          if (this._aliveTeams.Count == 1)
            this.AddLastTeamScore();
        }
      }
    }

    public override void OnScoreHit(
      Agent affectedAgent,
      Agent affectorAgent,
      WeaponComponentData attackerWeapon,
      bool isBlocked,
      float damage,
      float movementSpeedDamageModifier,
      float hitDistance,
      AgentAttackType attackType,
      float shotDifficulty,
      BoneBodyPartType victimHitBodyPart)
    {
      if (affectorAgent != null)
      {
        if (affectorAgent.Character != null && affectedAgent.Character != null)
        {
          if (damage > affectedAgent.HealthLimit)
            damage = affectedAgent.HealthLimit;

          this.EnemyHitReward(affectedAgent, affectorAgent, movementSpeedDamageModifier, shotDifficulty, attackerWeapon, XpShareForDamage * damage / affectedAgent.HealthLimit, damage);
        }
      }
    }

    private void EnemyHitReward(
      Agent affectedAgent,
      Agent affectorAgent,
      float lastSpeedBonus,
      float lastShotDifficulty,
      WeaponComponentData lastAttackerWeapon,
      float hitpointRatio,
      float damageAmount)
    {
      CharacterObject affectedCharacter = (CharacterObject)affectedAgent.Character;
      CharacterObject affectorCharacter = (CharacterObject)affectorAgent.Character;
      if (affectedAgent.Origin != null && affectorAgent != null && affectorAgent.Origin != null)
      {
        SkillLevelingManager.OnCombatHit(affectorCharacter,
          affectedCharacter,
          null,
          null,
          lastSpeedBonus,
          lastShotDifficulty,
          lastAttackerWeapon,
          hitpointRatio,
          CombatXpModel.MissionTypeEnum.Tournament,
          affectorAgent.MountAgent != null,
          affectorAgent.Team == affectedAgent.Team,
          false, damageAmount,
          affectedAgent.Health < 1f);
      }
    }

    public bool CheckIfIsThereAnyEnemies()
    {
      Team team = null;
      foreach (var agent in Mission.Agents.Where(x => x.IsHuman && x.Team != null))
      {
        if (team == null)
          team = agent.Team;
        else if (team != agent.Team)
          return true;
      }
      return false;
    }

    private void Simulate()
    {
      this._isSimulated = false;

      if (base.Mission.Agents.Count == 0)
      {
        this._aliveMembers = new List<TeamTournamentMember>(this._match.MatchMembers);
        this._aliveTeams = new List<TeamTournamentTeam>(this._match.Teams);
      }

      var player = this._aliveMembers.FirstOrDefault(x => x.IsPlayer);

      // if player is still alive => player quit, remove and take teams score too
      if (player != null)
      {
        foreach (var member in player.Team.Members)
        {
          member.ResetScore();
          this._aliveMembers.Remove(member);
        }
        this._aliveTeams.Remove(player.Team);
        player.Team?.ResetScore();
        this.AddScoreToRemainingTeams();
      }

      var simAttacks = new Dictionary<TeamTournamentMember, Tuple<float, float>>();
      foreach (var member in this._aliveMembers)
      {
        member.Character.GetSimulationAttackPower(out float item, out float item2, member.MatchEquipment);
        simAttacks.Add(member, new Tuple<float, float>(item, item2));
      }

      int runningIndex = 0;
      while (this._aliveMembers.Count() > 1 && this._aliveTeams.Count() > 1)
      {
        runningIndex = ++runningIndex % this._aliveMembers.Count();
        var currentFighter = this._aliveMembers[runningIndex];
        int nextIndex;

        TeamTournamentMember nextFighter;
        do
        {
          nextIndex = MBRandom.RandomInt(this._aliveMembers.Count);
          nextFighter = this._aliveMembers[nextIndex];
        }
        while (currentFighter == nextFighter || currentFighter.Team == nextFighter.Team);

        if (simAttacks[nextFighter].Item2 - simAttacks[currentFighter].Item1 > 0f)
        {
          simAttacks[nextFighter] = new Tuple<float, float>(simAttacks[nextFighter].Item1, simAttacks[nextFighter].Item2 - simAttacks[currentFighter].Item1);
        }
        else
        {
          simAttacks.Remove(nextFighter);
          this._aliveMembers.Remove(nextFighter);

          if (this.IsTeamDead(nextFighter.Team))
          {
            this._aliveTeams.Remove(nextFighter.Team);
            this.AddScoreToRemainingTeams();
          }

          if (nextIndex < runningIndex)
            runningIndex--;
        }
      }
      this._isSimulated = true;
    }

    private bool IsThereAnyPlayerAgent() => Mission.MainAgent != null && base.Mission.MainAgent.IsActive() || Mission.Agents.Any(agent => agent.IsPlayerControlled);

    public override InquiryData OnEndMissionRequest(out bool canPlayerLeave)
    {
      InquiryData result = null;
      canPlayerLeave = true;
      var missionBehaviour = Mission.Current.GetMissionBehaviour<TeamTournamentBehavior>();
      if (this._match != null && missionBehaviour != null)
      {
        if (this._match.IsPlayerParticipating)
        {
          MBTextManager.SetTextVariable("SETTLEMENT_NAME", Hero.MainHero.CurrentSettlement.EncyclopediaLinkWithName, false);
          if (this.IsThereAnyPlayerAgent())
          {
            if (base.Mission.IsPlayerCloseToAnEnemy(5f))
            {
              canPlayerLeave = false;
              InformationManager.AddQuickInformation(GameTexts.FindText("str_can_not_retreat", null), 0, null, "");
            }
            else if (this.CheckIfIsThereAnyEnemies())
            {
              result = new InquiryData(GameTexts.FindText("str_tournament", null).ToString(), GameTexts.FindText("str_tournament_forfeit_game", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), new Action(missionBehaviour.SkipMatch), null, "");
            }
            else
            {
              this._forceEndMatch = true;
              canPlayerLeave = false;
            }
          }
          else if (this.CheckIfIsThereAnyEnemies())
          {
            result = new InquiryData(GameTexts.FindText("str_tournament", null).ToString(), GameTexts.FindText("str_tournament_skip", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), new Action(missionBehaviour.SkipMatch), null, "");
          }
          else
          {
            this._forceEndMatch = true;
            canPlayerLeave = false;
          }
        }
        else if (this.CheckIfIsThereAnyEnemies())
        {
          result = new InquiryData(GameTexts.FindText("str_tournament", null).ToString(), GameTexts.FindText("str_tournament_skip", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), new Action(missionBehaviour.SkipMatch), null, "");
        }
        else
        {
          this._forceEndMatch = true;
          canPlayerLeave = false;
        }
      }
      return result;
    }

    /// <summary>
    /// just so we keep the interface intact, should never be called
    /// </summary>
    /// <param name="match"></param>
    /// <param name="isLastRound"></param>
    public void StartMatch(TournamentMatch match, bool isLastRound)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// just so we keep the interface intact, should never be called
    /// </summary>
    /// <param name="match"></param>
    public void SkipMatch(TournamentMatch match)
    {
      throw new NotImplementedException();
    }

    private readonly CharacterObject _defaultWeaponTemplatesIdTeamSizeOne = MBObjectManager.Instance.GetObject<CharacterObject>("tournament_template_empire_one_participant_set_v1");
    private readonly CharacterObject _defaultWeaponTemplatesIdTeamSizeTwo = MBObjectManager.Instance.GetObject<CharacterObject>("tournament_template_empire_two_participant_set_v1");
    private readonly CharacterObject _defaultWeaponTemplatesIdTeamSizeFour = MBObjectManager.Instance.GetObject<CharacterObject>("tournament_template_empire_four_participant_set_v1");
    private TeamTournamentMatch _match;
    private bool _isLastRound;
    private BasicTimer _endTimer;
    private BasicTimer _cheerTimer;
    private List<GameEntity> _spawnPoints;
    private bool _isSimulated;
    private bool _forceEndMatch;
    private CultureObject _culture;
    private List<TeamTournamentMember> _aliveMembers;
    private List<TeamTournamentTeam> _aliveTeams;
  }
}