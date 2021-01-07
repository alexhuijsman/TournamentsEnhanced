using System;
using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBMultiSelectionInquiryData : MBWrapperBase<MBMultiSelectionInquiryData, MultiSelectionInquiryData>
  {
    public MBMultiSelectionInquiryData() { }
    public MBMultiSelectionInquiryData(
      string titleText,
      string descriptionText,
      MBInquiryElementList inquiryElements,
      bool isExitShown,
      int maxSelectableOptionCount,
      string affirmativeText,
      string negativeText,
      Action<List<InquiryElement>> affirmativeAction,
      Action<List<InquiryElement>> negativeAction,
      string soundEventPath = "")
    {
      UnwrappedObject = new MultiSelectionInquiryData(
        titleText,
        descriptionText,
        inquiryElements,
        isExitShown,
        maxSelectableOptionCount,
        affirmativeText,
        negativeText,
        affirmativeAction,
        negativeAction,
        soundEventPath);
    }

    public static implicit operator MultiSelectionInquiryData(MBMultiSelectionInquiryData wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBMultiSelectionInquiryData(MultiSelectionInquiryData obj) => MBMultiSelectionInquiryData.GetWrapperFor(obj);
  }

  public class MBMultiSelectionInquiryDataList : MBListBase<MBMultiSelectionInquiryData, MBMultiSelectionInquiryDataList>
  {
    public MBMultiSelectionInquiryDataList(params MBMultiSelectionInquiryData[] wrappers) : this((IEnumerable<MBMultiSelectionInquiryData>)wrappers) { }
    public MBMultiSelectionInquiryDataList(IEnumerable<MBMultiSelectionInquiryData> wrappers) => AddRange(wrappers);
    public MBMultiSelectionInquiryDataList(MBMultiSelectionInquiryData wrapper) => Add(wrapper);
    public MBMultiSelectionInquiryDataList() { }

    public static implicit operator List<MultiSelectionInquiryData>(MBMultiSelectionInquiryDataList wrapperList) => wrapperList.Unwrap<MBMultiSelectionInquiryData, MultiSelectionInquiryData>();
    public static implicit operator MBMultiSelectionInquiryDataList(List<MultiSelectionInquiryData> objectList) => (MBMultiSelectionInquiryDataList)objectList.Wrap<MBMultiSelectionInquiryData, MultiSelectionInquiryData>();
  }
}
