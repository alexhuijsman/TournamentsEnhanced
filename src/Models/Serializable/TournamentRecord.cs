using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Models.Serializable
{
  public class TournamentRecord
  {
    protected MBCharacterObject MBCharacterObject { get; set; } = MBCharacterObject.Instance;
    public TournamentType tournamentType;
    public string hostSettlementStringId;
    public string initiatingHeroStringId;
    public uint playerTeamColor;
    public bool HasPlayerTeam;
    public virtual bool IsNull => tournamentType == TournamentType.None;
    public virtual bool HasInitiatingHero => !string.IsNullOrEmpty(initiatingHeroStringId);

    public MBHero FindInitiatingHero() =>
      HasInitiatingHero ?
        MBHero.Null :
        MBCharacterObject.Find(initiatingHeroStringId).HeroObject;

    public virtual MBSettlement FindHostSettlement() => MBSettlement.Find(hostSettlementStringId);

    public virtual MBHero FindPayorHero() => FindHostSettlement().OwnerClan.Leader;
  }
}
