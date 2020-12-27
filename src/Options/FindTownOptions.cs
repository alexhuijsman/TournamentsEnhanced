namespace TournamentsEnhanced
{
    public struct FindTownOptions
    {
        public static readonly FindTownOptions UseExistingTournamentAsLastResort =
            new FindTownOptions() { CanUseExistingTournamentAsLastResort = true };
        public static readonly FindTownOptions RejectExistingTournaments =
            new FindTownOptions() { CanUseExistingTournamentAsLastResort = true };

        public bool CanUseExistingTournamentAsLastResort;
    }
}
