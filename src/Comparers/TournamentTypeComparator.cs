using System.Collections;

namespace TournamentsEnhanced
{
  public class TournamentTypeComparer : IComparer
  {
    public int Compare(object x, object y)
    {
      throw new System.NotImplementedException();
    }

    public static bool HasPriorityOver(this TournamentType type, TournamentType other)
    {
      return false;
    }
  }
}