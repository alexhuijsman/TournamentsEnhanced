using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Models.Serializable
{
  public struct TournamentRecord
  {
    public TournamentType type;
    public string hostSettlementId;
    public string payorId;
    public PayorType payorType;
    public uint playerTeamColor;
    public bool HasPlayerTeam;

    public bool IsHeroPayor => payorType == PayorType.Hero;
    public bool IsSettlementPayor => payorType == PayorType.Settlement;

    public MBSettlement PayorSettlement => Settlement.Find(payorId);
    public MBHero PayorHero => MBCharacterObject.Find(payorId).HeroObject;
  }
}
