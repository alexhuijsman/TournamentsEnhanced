using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBInquiryData : MBWrapperBase<MBInquiryData, InquiryData>
  {
    public static implicit operator InquiryData(MBInquiryData wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBInquiryData(InquiryData obj) => MBInquiryData.GetWrapper(obj);
  }
}
