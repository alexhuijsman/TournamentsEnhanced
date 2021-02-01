using TaleWorlds.Core;

namespace TournamentsEnhanced.Wrappers.Core
{
  public struct MBDynamicBodyProperties : IMBDynamicBodyProperties
  {
    public DynamicBodyProperties UnwrappedStruct { get; set; }

    public float Age => UnwrappedStruct.Age;
    public float Weight => UnwrappedStruct.Weight;
    public float Build => UnwrappedStruct.Build;

    public static implicit operator DynamicBodyProperties(MBDynamicBodyProperties wrapper) => wrapper.UnwrappedStruct;
    public static implicit operator MBDynamicBodyProperties(DynamicBodyProperties unwrapped) => new MBDynamicBodyProperties() { UnwrappedStruct = unwrapped };
  }
}
