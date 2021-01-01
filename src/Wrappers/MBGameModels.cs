using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBGameModels : CachedWrapperBase<MBGameModels, GameModels>
  {
    public MBSettlementAccessModel SettlementAccessModel => UnwrapedObject.SettlementAccessModel;
    public MBTournamentModel TournamentModel => UnwrapedObject.TournamentModel;

    public static implicit operator GameModels(MBGameModels wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBGameModels(GameModels obj) => MBGameModels.GetWrapperFor(obj);
  }

  public class MBGameModelsList : List<MBGameModels>
  {
    public static implicit operator List<GameModels>(MBGameModelsList wrapperList) => wrapperList.Unwrap<MBGameModels, GameModels>();
    public static implicit operator MBGameModelsList(List<GameModels> objectList) => (MBGameModelsList)objectList.Wrap<MBGameModels, GameModels>();
  }
}
