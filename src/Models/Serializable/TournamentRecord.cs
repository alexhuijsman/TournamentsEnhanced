using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

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
  }
}
