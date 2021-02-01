using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public interface IMBStaticBodyProperties : IStructWrapperBase<StaticBodyProperties>
  {
    ulong KeyPart6 { get; }
    ulong KeyPart5 { get; }
    ulong KeyPart4 { get; }
    ulong KeyPart3 { get; }
    ulong KeyPart2 { get; }
    ulong KeyPart1 { get; }
    ulong KeyPart8 { get; }
    ulong KeyPart7 { get; }
  }
}