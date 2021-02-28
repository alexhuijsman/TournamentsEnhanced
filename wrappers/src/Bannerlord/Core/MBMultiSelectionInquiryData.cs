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
      List<MBInquiryElement> inquiryElements,
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
        inquiryElements.CastList<InquiryElement>(),
        isExitShown,
        maxSelectableOptionCount,
        affirmativeText,
        negativeText,
        affirmativeAction,
        negativeAction,
        soundEventPath);
    }

    public static implicit operator MultiSelectionInquiryData(MBMultiSelectionInquiryData wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBMultiSelectionInquiryData(MultiSelectionInquiryData obj) => GetWrapper(obj);
  }
}
