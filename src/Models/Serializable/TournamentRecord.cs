using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Models.Serializable
{
  public struct TournamentRecord
  {
    public TournamentType tournamentType;
    public string hostSettlementId;
    public string payorId;
    public PayorType payorType;
    public uint playerTeamColor;
    public bool HasPlayerTeam;

    public bool IsHeroPayor => payorType == PayorType.Hero;
    public bool IsSettlementPayor => payorType == PayorType.Settlement;

    public MBSettlement FindHostSettlement() => Settlement.Find(hostSettlementId);
    public MBSettlement FindPayorSettlement() => Settlement.Find(payorId);
    public MBHero FindPayorHero() => MBCharacterObject.Find(payorId).HeroObject;

    public bool HasExistingTournament() => tournamentType != TournamentType.None;
  }
}
