using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using SandBox.TournamentMissions.Missions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;
using TournamentsEnhanced.TeamTournament;

namespace TournamentsEnhanced
{
  public class TournamentKB
  {
    private static List<TournamentKB> TournamentList = new List<TournamentKB>();
    private Settlement Settlement { get; set; }
    public int TeamSize { get; private set; }
    public TournamentType TournamentType { get; private set; }
    public TournamentTeam PlayerTeam { get; set; }
    public bool IsTeamTournament => SelectedRoster != null;
    public List<CharacterObject> SelectedRoster { get; internal set; }
    public int TeamsCount { get; }
    public int FirstRoundMatches { get; }
    public int Rounds { get; internal set; }


    private WeakReference<ItemObject> _selectedPrize;
    private TournamentGame _tournamentGame;
    private ItemObject[] _availablePrizes;

    public ItemObject SelectedPrize
    {
      get
      {
        if (_selectedPrize == null)
          return null;

        _selectedPrize.TryGetTarget(out var prizeOut);
        return prizeOut;
      }

      set
      {
        _selectedPrize = new WeakReference<ItemObject>(value);
        typeof(TournamentGame).GetProperty("Prize").SetValue(TournamentGame, value);
      }
    }

    public TournamentKB(Settlement settlement, TournamentType tournamentTypes, TournamentGame tournamentGame = null)
    {
      _tournamentGame = tournamentGame;
      TournamentType = tournamentTypes;
      Settlement = settlement;
      TournamentList.Add(this);
      TeamSize = MBRandom.Random.Next(2, 9); // current interface allows up to 8 per team
      TeamsCount = (int)Math.Pow(2, MBRandom.Random.Next(3, 6)); // 16, 32 teams possible -> also 4, 8 but needs more testing and fixing 
      FirstRoundMatches = TeamsCount == 32 ? 8 : TeamsCount / (MBRandom.Random.Next(2) * 2 + 2); // if full (32) => 8 rounds, else can be 4 or 8
      Rounds = (int)Math.Min(Math.Log(TeamsCount, 2), 4); // simple log2 round progression (members/2 in every round)
    }

    public static TournamentType GetTournamentType(Settlement settlement)
    {
      List<TournamentKB>.Enumerator enumerator = TournamentList.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current.Settlement.Town.Settlement.Name.Equals(settlement.Name))
        {
          return enumerator.Current.TournamentType;
        }
      }
      return TournamentType.Vanilla;
    }

    public static TournamentKB GetTournamentKB(Settlement settlement)
    {
      List<TournamentKB>.Enumerator enumerator = TournamentList.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current.Settlement.Town.Settlement.Name.Equals(settlement.Name))
        {
          return enumerator.Current;
        }
      }
      return null;
    }

    public static void Remove(Settlement settlement)
    {
      if (GetTournamentKB(settlement) != null)
        TournamentList.Remove(GetTournamentKB(settlement));
    }

    public TournamentGame TournamentGame
    {
      get
      {
        if (_tournamentGame == null)
          _tournamentGame = Campaign.Current.TournamentManager.GetTournamentGame(Settlement.Town);

        return _tournamentGame;
      }
    }

    public static bool IsCurrentPrizeSelected() => Current != null && Current.SelectedPrize != null;

    public static TournamentKB Current => GetTournamentKB(Settlement.CurrentSettlement);

    public ItemObject[] AvailableTournamentPrizes
    {
      get
      {
        if (_availablePrizes == null)
          _availablePrizes = Utilities.GetTournamentPrizes();

        return _availablePrizes;
      }
    }

    /// <summary>
    /// Removes all TournamentKB from static list except given elements (if any).
    /// </summary>
    /// <param name="exceptElements"></param>
    internal static void RemoveAll(List<TournamentKB> exceptElements = null)
    {
      if (exceptElements != null)
        TournamentList = TournamentList.Where(tournamentKB => exceptElements.Contains(tournamentKB)).ToList();
      else
        TournamentList.Clear();
    }
  }

  public enum TournamentType
  {
    Vanilla,
    Wedding,
    Birth,
    Prosperity,
    Peace,
    Invitation,
    Hosted,
    Lord
  }
}
