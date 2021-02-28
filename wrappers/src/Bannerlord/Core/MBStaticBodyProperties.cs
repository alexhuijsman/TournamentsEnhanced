using TaleWorlds.Core;

namespace TournamentsEnhanced.Wrappers.Core
{
  public struct MBStaticBodyProperties : IMBStaticBodyProperties
  {
    public StaticBodyProperties UnwrappedStruct { get; set; }

    public ulong KeyPart6 => UnwrappedStruct.KeyPart6;

    public ulong KeyPart5 => UnwrappedStruct.KeyPart5;

    public ulong KeyPart4 => UnwrappedStruct.KeyPart4;

    public ulong KeyPart3 => UnwrappedStruct.KeyPart3;

    public ulong KeyPart2 => UnwrappedStruct.KeyPart2;

    public ulong KeyPart1 => UnwrappedStruct.KeyPart1;

    public ulong KeyPart8 => UnwrappedStruct.KeyPart8;

    public ulong KeyPart7 => UnwrappedStruct.KeyPart7;

    public static implicit operator StaticBodyProperties(MBStaticBodyProperties wrapper) => wrapper.UnwrappedStruct;
    public static implicit operator MBStaticBodyProperties(StaticBodyProperties unwrapped) => new MBStaticBodyProperties() { UnwrappedStruct = unwrapped };
  }
}
