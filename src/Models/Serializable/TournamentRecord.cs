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
    public bool IsNull => tournamentType == TournamentType.None;
    public bool HasInitiatingHero => !string.IsNullOrEmpty(initiatingHeroStringId);

    public MBHero FindInitiatingHero() =>
      HasInitiatingHero ?
        MBHero.Null :
        MBCharacterObject.Find(initiatingHeroStringId).HeroObject;

    public MBSettlement FindHostSettlement() => MBSettlement.Find(hostSettlementStringId);

    public MBHero FindPayorHero() => FindHostSettlement().OwnerClan.Leader;
  }
}
