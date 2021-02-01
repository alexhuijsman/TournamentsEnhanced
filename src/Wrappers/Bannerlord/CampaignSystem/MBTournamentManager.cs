using System;
using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{

  public class MBTournamentManager : MBWrapperBase<MBTournamentManager, TournamentManager>
  {
    public virtual void AddLeaderboardEntry(MBHero hero) => UnwrappedObject.AddLeaderboardEntry(hero);

    public virtual void AddTournament(MBTournamentGame game) => UnwrappedObject.AddTournament(game);

    public virtual void DeleteLeaderboardEntry(MBHero hero) => UnwrappedObject.DeleteLeaderboardEntry(hero);

    public virtual MBHero GetLeaderBoardLeader() => UnwrappedObject.GetLeaderBoardLeader();

    public virtual MBTournamentGame GetTournamentGame(Town town) => UnwrappedObject.GetTournamentGame(town);

    public virtual void InitializeLeaderboardEntry(MBHero hero, int initialVictories = 0) => UnwrappedObject.InitializeLeaderboardEntry(hero, initialVictories);

    public virtual void OnPlayerJoinMatch(Type gameType) => UnwrappedObject.OnPlayerJoinMatch(gameType);

    public virtual void OnPlayerJoinTournament(Type gameType, Settlement settlement) => UnwrappedObject.OnPlayerJoinTournament(gameType, settlement);

    public virtual void OnPlayerWatchTournament(Type gameType, Settlement settlement) => UnwrappedObject.OnPlayerWatchTournament(gameType, settlement);

    public virtual void OnPlayerWinMatch(Type gameType) => UnwrappedObject.OnPlayerWinMatch(gameType);

    public virtual void OnPlayerWinTournament(Type gameType) => UnwrappedObject.OnPlayerWinTournament(gameType);

    public virtual void ResolveTournament(TournamentGame tournament, Town town) => UnwrappedObject.ResolveTournament(tournament, town);

    public static implicit operator TournamentManager(MBTournamentManager wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentManager(TournamentManager obj) => GetWrapper(obj);
  }
}
