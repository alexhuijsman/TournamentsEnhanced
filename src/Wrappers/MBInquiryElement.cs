using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBInquiryElement : CachedWrapperBase<MBInquiryElement, InquiryElement>
  {
    public static implicit operator InquiryElement(MBInquiryElement wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBInquiryElement(InquiryElement obj) => MBInquiryElement.GetWrapperFor(obj);
  }

  public class MBInquiryElementList : List<MBInquiryElement>
  {
    public static implicit operator List<InquiryElement>(MBInquiryElementList wrapperList) => wrapperList.Unwrap<MBInquiryElement, InquiryElement>();
    public static implicit operator MBInquiryElementList(List<InquiryElement> objectList) => (MBInquiryElementList)objectList.Wrap<MBInquiryElement, InquiryElement>();
  }
}
