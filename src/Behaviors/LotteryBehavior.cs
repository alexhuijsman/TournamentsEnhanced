using System;

using TournamentsEnhanced.Random;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Behaviors
{
  public class LotteryBehavior : MBCampaignBehaviorBase
  {
    public override void RegisterEvents()
    {
      MBCampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(Lottery.DailyTick));
    }
  }
}