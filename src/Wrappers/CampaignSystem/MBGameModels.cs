using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBGameModels : MBWrapperBase<MBGameModels, GameModels>
  {
    public virtual MBSettlementAccessModel SettlementAccessModel => UnwrappedObject.SettlementAccessModel;
    public virtual MBTournamentModel TournamentModel => UnwrappedObject.TournamentModel;

    public static implicit operator GameModels(MBGameModels wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBGameModels(GameModels obj) => GetWrapper(obj);
  }
}
