using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

namespace TournamentsEnhanced
{
  public struct TournamentRecord
  {
    public TournamentType type;
    public string hostTownName;
    public string payorName;
    public PayorType payorType;
    public uint playerTeamColor;
    public bool HasPlayerTeam;
  }
}
