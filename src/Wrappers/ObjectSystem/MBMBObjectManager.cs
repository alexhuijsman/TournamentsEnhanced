using System.Collections.Generic;

using TaleWorlds.ObjectSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.ObjectSystem
{
  public class MBMBObjectManager : MBWrapperBase<MBMBObjectManager, MBObjectManager>
  {
    public static MBMBObjectManager Instance => MBObjectManager.Instance;

    public static T GetObjectById<T>(string id)
    where T : MBObjectBase
    {
      return Instance.GetObject<T>(id);
    }

    private T GetObject<T>(string id)
    where T : MBObjectBase
    {
      return UnwrappedObject.GetObject<T>(id);
    }

    public static implicit operator MBObjectManager(MBMBObjectManager wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBMBObjectManager(MBObjectManager obj) => MBMBObjectManager.GetWrapperFor(obj);
  }

  public class MBMBObjectManagerList : MBListBase<MBMBObjectManager, MBMBObjectManagerList>
  {
    public static implicit operator List<MBObjectManager>(MBMBObjectManagerList wrapperList) => wrapperList.Unwrap<MBMBObjectManager, MBObjectManager>();
    public static implicit operator MBMBObjectManagerList(List<MBObjectManager> objectList) => (MBMBObjectManagerList)objectList.Wrap<MBMBObjectManager, MBObjectManager>();
  }
}
