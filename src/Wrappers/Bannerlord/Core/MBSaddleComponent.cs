using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBSaddleComponent : MBObjectBaseWrapper<MBSaddleComponent, SaddleComponent>
  {
    public static implicit operator SaddleComponent(MBSaddleComponent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBSaddleComponent(SaddleComponent obj) => GetWrapper(obj);
  }
}
