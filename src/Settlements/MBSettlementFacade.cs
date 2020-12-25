using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
    public class MBSettlementFacade
    {
        public static IReadOnlyList<Settlement> AllSettlementsReadOnly => Settlement.All;
        public static IList<Settlement> AllSettlements => new List<Settlement>(AllSettlementsReadOnly);
        public static IList<Settlement> AllSettlementsShuffled => AllSettlements.Shuffle();

        public static FindTownResult FindHostTownForFaction(IFaction faction, FindTownOptions options)
        {
            var settlements = faction.Settlements.ToList().Shuffle();

            return FindHostTownFromSettlements(settlements, options);
        }

        public static FindTownResult FindHostTownForLeader(Hero leader, FindTownOptions options)
        {
            var settlements = new List<Settlement>(leader.MapFaction.Settlements).Shuffle();

            return FindHostTownFromSettlements(settlements, options);
        }

        public static FindTownResult FindHostTownFromSettlements(IList<Settlement> settlements, FindTownOptions options)
        {
            Town townWithExistingTournament = null;
            Town hostTown = null;

            foreach (var settlement in settlements)
            {
                if (settlement.IsTown && settlement.Town.HasTournament && townWithExistingTournament == null)
                {
                    townWithExistingTournament = settlement.Town;
                }

                if (!settlement.IsTown || settlement.Town.HasTournament)
                {
                    continue;
                }

                hostTown = settlement.Town;
                break;
            }

            if (hostTown == null && townWithExistingTournament != null && options.CanUseExistingTournamentAsLastResort)
            {
                hostTown = townWithExistingTournament;
            }

            return hostTown == null ? FindTownResult.Failure() : FindTownResult.Success(hostTown);
        }
    }
}
