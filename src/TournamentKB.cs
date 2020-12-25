using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

namespace TournamentsEnhanced
{
  public class TournamentKB
  {
    private static List<TournamentKB> TournamentList = new List<TournamentKB>();

    private Settlement settlement { get; set; }
    private TournamentType tournamentType { get; set; }
    public TournamentTeam playerTeam { get; set; }

    public TournamentKB(Settlement settlement, TournamentType tournamentTypes)
    {
      this.tournamentType = tournamentTypes;
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
          return enumerator.Current.tournamentType;
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
