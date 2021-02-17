using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

namespace TournamentsEnhanced
{
  public class TournamentKB
  {
    private static List<TournamentKB> TournamentList = new List<TournamentKB>();

    private Settlement settlement { get; set; }
    public TournamentType TournamentType { get; private set; }
    public TournamentTeam playerTeam { get; set; }
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
      this._tournamentGame = tournamentGame;
      this.TournamentType = tournamentTypes;
      this.settlement = settlement;
      TournamentList.Add(this);
    }

    public static TournamentType GetTournamentType(Settlement settlement)
    {
      List<TournamentKB>.Enumerator enumerator = TournamentList.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current.settlement.Town.Settlement.Name.Equals(settlement.Name))
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
        if (enumerator.Current.settlement.Town.Settlement.Name.Equals(settlement.Name))
        {
          return enumerator.Current;
        }
      }
      return null;
    }

    public static void Remove(Settlement settlement)
    {
      if (GetTournamentKB(settlement) != null)
      {
        TournamentList.Remove(GetTournamentKB(settlement));
      }
    }

    public TournamentGame TournamentGame
    {
      get
      {
        if (this._tournamentGame == null)
          this._tournamentGame = Campaign.Current.TournamentManager.GetTournamentGame(settlement.Town);

        return this._tournamentGame;
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
