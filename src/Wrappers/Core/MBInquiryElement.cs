using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBInquiryElement : MBWrapperBase<MBInquiryElement, InquiryElement>
  {

    public object Identifier => UnwrappedObject.Identifier;
    public static implicit operator InquiryElement(MBInquiryElement wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBInquiryElement(InquiryElement obj) => MBInquiryElement.GetWrapper(obj);
  }

  public class MBInquiryElementList : MBListBase<MBInquiryElement, MBInquiryElementList>
  {
    public MBInquiryElementList(params MBInquiryElement[] wrappers) : this((IEnumerable<MBInquiryElement>)wrappers) { }
    public MBInquiryElementList(IEnumerable<MBInquiryElement> wrappers) => AddRange(wrappers);
    public MBInquiryElementList(MBInquiryElement wrapper) => Add(wrapper);
    public MBInquiryElementList() { }

    public static implicit operator List<InquiryElement>(MBInquiryElementList wrapperList) => wrapperList.Unwrap<MBInquiryElement, InquiryElement>();
    public static implicit operator MBInquiryElementList(List<InquiryElement> objectList) => (MBInquiryElementList)objectList.Wrap<MBInquiryElement, InquiryElement>();
    public static implicit operator MBInquiryElement[](MBInquiryElementList wrapperList) => wrapperList.ToArray();
  }
}
