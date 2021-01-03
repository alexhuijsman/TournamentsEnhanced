using System.Collections.Generic;

using TaleWorlds.ObjectSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.ObjectSystem
{
  public class MBMBObjectManager : CachedWrapperBase<MBMBObjectManager, MBObjectManager>
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
      return UnwrapedObject.GetObject<T>(id);
    }

    public static implicit operator MBObjectManager(MBMBObjectManager wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBMBObjectManager(MBObjectManager obj) => MBMBObjectManager.GetWrapperFor(obj);
  }

  public class MBMBObjectManagerList : List<MBMBObjectManager>
  {
    public static implicit operator List<MBObjectManager>(MBMBObjectManagerList wrapperList) => wrapperList.Unwrap<MBMBObjectManager, MBObjectManager>();
    public static implicit operator MBMBObjectManagerList(List<MBObjectManager> objectList) => (MBMBObjectManagerList)objectList.Wrap<MBMBObjectManager, MBObjectManager>();
  }
}
