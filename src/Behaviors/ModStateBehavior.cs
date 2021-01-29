using System;

using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Behaviors
{
  public class ModStateBehavior : MBCampaignBehaviorBase
  {
    protected ModState ModState { get; set; } = ModState.Instance;

    public override void RegisterEvents()
    {
      MBCampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(ModState.DailyTick));
    }
  }
}