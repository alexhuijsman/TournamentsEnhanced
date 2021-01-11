using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBInformationMessage : MBWrapperBase<MBInformationMessage, InformationMessage>
  {
    public MBInformationMessage(string message)
    {
      UnwrappedObject = new InformationMessage(message);
    }
    public MBInformationMessage() : base() { }

    public static implicit operator InformationMessage(MBInformationMessage wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBInformationMessage(InformationMessage obj) => MBInformationMessage.GetWrapper(obj);
  }
}
