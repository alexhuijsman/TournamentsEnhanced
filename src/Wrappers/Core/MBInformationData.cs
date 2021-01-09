using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBInformationData : MBWrapperBase<MBInformationData, InformationData>
  {
    public static implicit operator InformationData(MBInformationData wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBInformationData(InformationData obj) => MBInformationData.GetWrapper(obj);
  }

  public class MBInformationDataList : MBListBase<MBInformationData, MBInformationDataList>
  {
    public MBInformationDataList(params MBInformationData[] wrappers) : this((IEnumerable<MBInformationData>)wrappers) { }
    public MBInformationDataList(IEnumerable<MBInformationData> wrappers) => AddRange(wrappers);
    public MBInformationDataList(MBInformationData wrapper) => Add(wrapper);
    public MBInformationDataList() { }

    public static implicit operator List<InformationData>(MBInformationDataList wrapperList) => wrapperList.Unwrap<MBInformationData, InformationData>();
    public static implicit operator MBInformationDataList(List<InformationData> objectList) => (MBInformationDataList)objectList.Wrap<MBInformationData, InformationData>();
    public static implicit operator MBInformationData[](MBInformationDataList wrapperList) => wrapperList.ToArray();
  }
}
