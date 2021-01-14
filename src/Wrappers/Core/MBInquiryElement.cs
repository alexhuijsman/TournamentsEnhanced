using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBInquiryElement : MBWrapperBase<MBInquiryElement, InquiryElement>
  {

    public object Identifier => UnwrappedObject.Identifier;
    public static implicit operator InquiryElement(MBInquiryElement wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBInquiryElement(InquiryElement obj) => GetWrapper(obj);
  }
}
