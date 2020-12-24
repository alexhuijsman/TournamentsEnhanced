using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
    public class SettlementUtils
    {
        public static IReadOnlyList<Settlement> AllSettlementsReadOnly => Settlement.All;
        public static IList<Settlement> AllSettlements => new List<Settlement>(AllSettlementsReadOnly);
        public static IList<Settlement> AllSettlementsShuffled => AllSettlements.Shuffle();

        public static FindSettlementResult FindNewOrExistingTournamentHostTownForFaction(IFaction faction)
        {
            var settlements = faction.Settlements.ToList().Shuffle();

            return FindNewOrExistingTournamentHostTownFromSettlements(settlements);
        }
        public static FindSettlementResult FindNewTournamentHostTownForFaction(IFaction faction)
        {
            var settlements = faction.Settlements.ToList().Shuffle();

            return FindNewTournamentHostTownFromSettlements(settlements);
        }

        public static FindSettlementResult FindNewOrExistingTournamentHostTownForLeader(Hero leader)
        {
            var settlements = new List<Settlement>(leader.MapFaction.Settlements).Shuffle();

            return FindNewOrExistingTournamentHostTownFromSettlements(settlements);
        }

        public static FindSettlementResult FindNewTournamentHostTownForLeader(Hero leader)
        {
            var settlements = new List<Settlement>(leader.MapFaction.Settlements).Shuffle();

            return FindNewTournamentHostTownFromSettlements(settlements);
        }

        public static FindSettlementResult FindNewOrExistingTournamentHostTownFromSettlements(IList<Settlement> settlements)
        {
            Settlement settlementWithExistingTournament = null;
            Settlement chosenSettlement = null;

            foreach (var settlement in settlements)
            {
                if (settlement.IsTown && settlement.Town.HasTournament && settlementWithExistingTournament == null)
                {
                    settlementWithExistingTournament = settlement;
                }

                if (!settlement.IsTown || settlement.Town.HasTournament)
                {
                    continue;
                }

                chosenSettlement = settlement;
                break;
            }

            if (chosenSettlement == null && settlementWithExistingTournament != null)
            {
                chosenSettlement = settlementWithExistingTournament;
            }

            return chosenSettlement == null ? FindSettlementResult.Failure : FindSettlementResult.Success(chosenSettlement, chosenSettlement.Town.HasTournament);
        }

        public static FindSettlementResult FindNewTournamentHostTownFromSettlements(IList<Settlement> settlements)
        {
            foreach (var settlement in settlements)
            {
                if (!settlement.IsTown || settlement.Town.HasTournament)
                {
                    continue;
                }

                return FindSettlementResult.Success(settlement, settlement.Town.HasTournament);
            }

            return FindSettlementResult.Failure;
        }

        public class FindSettlementResult
        {
            public readonly static FindSettlementResult Failure = new FindSettlementResult();

            public bool Succeeded => Settlement != null;
            public Settlement Settlement { get; private set; }
            public bool HadExistingTournament { get; private set; }

            public static FindSettlementResult Success(Settlement settlement, bool hadExistingTournament)
            {
                return new FindSettlementResult(settlement, hadExistingTournament);
            }

            private FindSettlementResult(Settlement settlement, bool hadExistingTournament)
            {
                Settlement = settlement;
                HadExistingTournament = hadExistingTournament;
            }

            private FindSettlementResult() { }
        }
    }
}
