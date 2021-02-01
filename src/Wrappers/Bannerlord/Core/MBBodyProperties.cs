using TaleWorlds.Core;

namespace TournamentsEnhanced.Wrappers.Core
{

  public struct MBBodyProperties : IMBBodyProperties
  {
    public BodyProperties UnwrappedStruct { get; set; }

    public float Age => UnwrappedStruct.Age;

    public ulong KeyPart7 => UnwrappedStruct.KeyPart7;

    public ulong KeyPart6 => UnwrappedStruct.KeyPart6;

    public ulong KeyPart5 => UnwrappedStruct.KeyPart5;

    public ulong KeyPart4 => UnwrappedStruct.KeyPart4;

    public ulong KeyPart3 => UnwrappedStruct.KeyPart3;

    public ulong KeyPart2 => UnwrappedStruct.KeyPart2;

    public ulong KeyPart1 => UnwrappedStruct.KeyPart1;

    public float Build => UnwrappedStruct.Build;

    public float Weight => UnwrappedStruct.Weight;

    public MBDynamicBodyProperties DynamicProperties => UnwrappedStruct.DynamicProperties;

    public ulong KeyPart8 => UnwrappedStruct.KeyPart8;

    public MBStaticBodyProperties StaticProperties => UnwrappedStruct.StaticProperties;

    public MBBodyProperties ClampForMultiplayer() => UnwrappedStruct.ClampForMultiplayer();

    public static implicit operator BodyProperties(MBBodyProperties wrapper) => wrapper.UnwrappedStruct;
    public static implicit operator MBBodyProperties(BodyProperties unwrapped) => new MBBodyProperties() { UnwrappedStruct = unwrapped };
  }
}
