using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTournamentModel : MBWrapperBase<MBTournamentModel, TournamentModel>
  {
    public virtual int GetRenownReward(MBHero winner, MBTown town) => UnwrappedObject.GetRenownReward(winner, town);
    public virtual int GetNumLeaderboardVictoriesAtGameStart() => UnwrappedObject.GetNumLeaderboardVictoriesAtGameStart();
    public virtual MBTournamentGame CreateTournament(MBTown town) => UnwrappedObject.CreateTournament(town);
    public (MBSkillObject skill, int xp) GetSkillXpGainFromTournament(MBTown town)
    {
      var result = UnwrappedObject.GetSkillXpGainFromTournament(town);
      return (result.skill, result.xp);
    }
    public virtual float GetTournamanetSimulationScore(MBCharacterObject character) => UnwrappedObject.GetTournamanetSimulationScore(character);
    public virtual float GetTournamentEndChance(MBTournamentGame tournament) => UnwrappedObject.GetTournamentEndChance(tournament);
    public virtual float GetTournamentStartChance(MBTown town) => UnwrappedObject.GetTournamentStartChance(town);

    public static implicit operator TournamentModel(MBTournamentModel wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentModel(TournamentModel obj) => GetWrapper(obj);
  }
}
