using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Models.Serializable
{
  public struct TournamentRecord
  {
    public TournamentType tournamentType;
    public string hostSettlementStringId;
    public string initiatingHeroStringId;
    public uint playerTeamColor;
    public bool HasPlayerTeam;

    public MBSettlement FindHostSettlement() => Settlement.Find(hostSettlementStringId);
    public MBHero FindInitiatingHero() => MBCharacterObject.Find(initiatingHeroStringId).HeroObject;
    public MBHero FindPayorHero() => FindHostSettlement().ClanLeader;

    public bool HasExistingTournament() => tournamentType != TournamentType.None;
  }
}
