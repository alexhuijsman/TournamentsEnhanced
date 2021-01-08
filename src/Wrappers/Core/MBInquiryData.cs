using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBInquiryData : MBWrapperBase<MBInquiryData, InquiryData>
  {
    public static implicit operator InquiryData(MBInquiryData wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBInquiryData(InquiryData obj) => MBInquiryData.GetWrapperFor(obj);
  }

  public class MBInquiryDataList : MBListBase<MBInquiryData, MBInquiryDataList>
  {
    public MBInquiryDataList(params MBInquiryData[] wrappers) : this((IEnumerable<MBInquiryData>)wrappers) { }
    public MBInquiryDataList(IEnumerable<MBInquiryData> wrappers) => AddRange(wrappers);
    public MBInquiryDataList(MBInquiryData wrapper) => Add(wrapper);
    public MBInquiryDataList() { }

    public static implicit operator List<InquiryData>(MBInquiryDataList wrapperList) => wrapperList.Unwrap<MBInquiryData, InquiryData>();
    public static implicit operator MBInquiryDataList(List<InquiryData> objectList) => (MBInquiryDataList)objectList.Wrap<MBInquiryData, InquiryData>();
    public static implicit operator MBInquiryData[](MBInquiryDataList wrapperList) => wrapperList.ToArray();
  }
}
