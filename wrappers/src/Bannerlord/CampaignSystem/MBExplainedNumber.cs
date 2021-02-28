using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public struct MBExplainedNumber : IMBExplainedNumber
  {
    public ExplainedNumber UnwrappedStruct { get; set; }

    public static implicit operator ExplainedNumber(MBExplainedNumber wrapper) => wrapper.UnwrappedStruct;
    public static implicit operator MBExplainedNumber(ExplainedNumber unwrapped) => new MBExplainedNumber() { UnwrappedStruct = unwrapped };
  }
}
