using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class InitiatingHeroRankComparer : ExistingTournamentComparer
  {
    public InitiatingHeroRankComparer(MBHero initiatingHero) : base(initiatingHero, true) { }

    protected override bool MeetsRequirements(MBSettlement settlement)
    {
      var hasRecord = ModState.TournamentRecords.ContainsSettlement(settlement);
      var record = hasRecord ? ModState.TournamentRecords[settlement] : default(TournamentRecord);

      return HasExistingTournament(settlement) &&
             InitiatingHeroOutranksOther(record);
    }

    protected bool InitiatingHeroOutranksOther(TournamentRecord record) =>
      HeroIsKingdomLeader(InitiatingHero) &&
      !InitiatingHeroIsSameAs(record);

  }
}
