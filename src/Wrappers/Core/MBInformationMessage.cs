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

  public class MBInformationMessageList : MBListBase<MBInformationMessage, MBInformationMessageList>
  {
    public MBInformationMessageList(params MBInformationMessage[] wrappers) : this((IEnumerable<MBInformationMessage>)wrappers) { }
    public MBInformationMessageList(IEnumerable<MBInformationMessage> wrappers) => AddRange(wrappers);
    public MBInformationMessageList(MBInformationMessage wrapper) => Add(wrapper);
    public MBInformationMessageList() { }

    public static implicit operator List<InformationMessage>(MBInformationMessageList wrapperList) => wrapperList.Unwrap<MBInformationMessage, InformationMessage>();
    public static implicit operator MBInformationMessageList(List<InformationMessage> objectList) => (MBInformationMessageList)objectList.Wrap<MBInformationMessage, InformationMessage>();
    public static implicit operator MBInformationMessage[](MBInformationMessageList wrapperList) => wrapperList.ToArray();
  }
}
