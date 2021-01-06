using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBGameModels : MBWrapperBase<MBGameModels, GameModels>
  {
    public MBSettlementAccessModel SettlementAccessModel => UnwrappedObject.SettlementAccessModel;
    public MBTournamentModel TournamentModel => UnwrappedObject.TournamentModel;

    public static implicit operator GameModels(MBGameModels wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBGameModels(GameModels obj) => MBGameModels.GetWrapperFor(obj);
  }

  public class MBGameModelsList : MBListBase<MBGameModels, MBGameModelsList>
  {
    public MBGameModelsList(IEnumerable<MBGameModels> wrappers) => AddRange(wrappers);
    public MBGameModelsList(MBGameModels wrapper) => Add(wrapper);
    public MBGameModelsList() { }

    public static implicit operator List<GameModels>(MBGameModelsList wrapperList) => wrapperList.Unwrap<MBGameModels, GameModels>();
    public static implicit operator MBGameModelsList(List<GameModels> objectList) => (MBGameModelsList)objectList.Wrap<MBGameModels, GameModels>();
  }
}
