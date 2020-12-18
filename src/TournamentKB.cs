using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

namespace TournamentsEnhanced
{
  class TournamentKB
  {

    public TournamentKB(Settlement settlement, TournamentTypes tournamentTypes)
    {
      //this.tournament = Campaign.Current.TournamentManager.GetTournamentGame(settlement.Town);
      this.tournamentType = tournamentTypes;
      this.settlement = settlement;
      //this.numberofTeams = 4;
      TournamentList.Add(this);
    }

    //Return type outside of class
    public static TournamentTypes GetTournamentType(Settlement settlement)
    {
      List<TournamentKB>.Enumerator enumerator = TournamentList.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current.settlement.Town.Settlement.Name.Equals(settlement.Name))
        {
          return enumerator.Current.tournamentType;
        }
      }
      return TournamentTypes.Vanilla;
    }

    //Find instance to be removed
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

    //Remove instance once tournament is over
    public static void Remove(Settlement settlement)
    {
      if (GetTournamentKB(settlement) != null)
      {
        TournamentList.Remove(GetTournamentKB(settlement));
      }

    }

    //private TournamentGame tournament { get; set; }
    private Settlement settlement { get; set; }
    private TournamentTypes tournamentType { get; set; }
    public TournamentTeam playerTeam { get; set; }

    private static List<TournamentKB> TournamentList = new List<TournamentKB>();
  }

  public enum TournamentTypes
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
