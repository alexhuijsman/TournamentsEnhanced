using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced.TeamTournament
{
  public static class TeamTournamentHelpers
  {
    /// <summary>
    /// Returns all found characters which are heroes inside a settlement.
    /// </summary>
    /// <param name="settlement">Settlement to find the heroes in</param>
    /// <returns>List of all found hero characters inside the settlement</returns>
    public static IEnumerable<CharacterObject> GetHeroesInSettlement(this Settlement settlement)
    {
      return settlement.LocationComplex
        .GetListOfCharacters()
        .Where(x =>
            x != null
            && x.Character.IsHero
            && !x.IsHidden)
        .Select(sel => sel.Character);
    }
  }
}
