using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTournamentModel : CachedWrapperBase<MBTournamentModel, TournamentModel>
  {
    public int GetRenownReward(MBHero winner, MBTown town) => UnwrappedObject.GetRenownReward(winner, town);
    public int GetNumLeaderboardVictoriesAtGameStart() => UnwrappedObject.GetNumLeaderboardVictoriesAtGameStart();
    public MBTournamentGame CreateTournament(MBTown town) => UnwrappedObject.CreateTournament(town);
    public (MBSkillObject skill, int xp) GetSkillXpGainFromTournament(MBTown town)
    {
      var result = UnwrappedObject.GetSkillXpGainFromTournament(town);
      return (result.skill, result.xp);
    }
    public float GetTournamanetSimulationScore(MBCharacterObject character) => UnwrappedObject.GetTournamanetSimulationScore(character);
    public float GetTournamentEndChance(MBTournamentGame tournament) => UnwrappedObject.GetTournamentEndChance(tournament);
    public float GetTournamentStartChance(MBTown town) => UnwrappedObject.GetTournamentStartChance(town);

    public static implicit operator TournamentModel(MBTournamentModel wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentModel(TournamentModel obj) => MBTournamentModel.GetWrapperFor(obj);
  }

  public class MBTournamentModelList : List<MBTournamentModel>
  {
    public static implicit operator List<TournamentModel>(MBTournamentModelList wrapperList) => wrapperList.Unwrap<MBTournamentModel, TournamentModel>();
    public static implicit operator MBTournamentModelList(List<TournamentModel> objectList) => (MBTournamentModelList)objectList.Wrap<MBTournamentModel, TournamentModel>();
  }
}
