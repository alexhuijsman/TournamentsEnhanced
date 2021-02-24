using System.Xml.Serialization;
using ModLib.Definitions;
using ModLib.Definitions.Attributes;

namespace TournamentsEnhanced
{
  public class TournamentsEnhancedSettings : SettingsBase
  {
    public const string InstanceID = "TournamentsEnhancedSettings";
    public override string ModName => Main.ModuleName;
    public override string ModuleFolderName => "TournamentsEnhanced";

    [XmlElement]
    public override string ID { get; set; } = InstanceID;

    public static TournamentsEnhancedSettings Instance
    {
      get
      {
        return (TournamentsEnhancedSettings)SettingsDatabase.GetSettings<TournamentsEnhancedSettings>();
      }
    }

    //Notifications
    [SettingProperty("Prosperity Tournament Notification", "Tell me when prosperity tournaments are announced")]
    [SettingPropertyGroup("Notifications")]
    public bool ProsperityNotification { get; set; } = true;

    [SettingProperty("Peace Tournament Notification", "Tell me when peace tournaments are announced")]
    [SettingPropertyGroup("Notifications")]
    public bool PeaceNotification { get; set; } = true;

    [SettingProperty("Settlement Stat Change Notification", "Tell me when a tournament affects my settlement's stats")]
    [SettingPropertyGroup("Notifications")]
    public bool SettlementStatNotification { get; set; } = true;

    //Tournament Affects
    [SettingProperty("Prosperity Increase", -500.00f, 500.00f, "Prosperity increase for certain tournament types. Negative values decreases prosperity instead.")]
    [SettingPropertyGroup("Tournament affects on Towns")]
    public float ProsperityIncrease { get; set; } = 100.00f;

    [SettingProperty("Loyalty Increase", -30.00f, 30.00f, "Loyalty increase for certain tournament types. Negative values decreases loyalty instead.")]
    [SettingPropertyGroup("Tournament affects on Towns")]
    public float LoyaltyIncrease { get; set; } = 10.00f;

    [SettingProperty("Security Increase", -30.00f, 30.00f, "Security increase for certain tournament types. Negative values decreases security instead.")]
    [SettingPropertyGroup("Tournament affects on Towns")]
    public float SecurityIncrease { get; set; } = 10.00f;

    [SettingProperty("Food Stocks Decrease", -30.00f, 30.00f, "Food stock decrease for certain tournament types. Negative values increase food stock instead.")]
    [SettingPropertyGroup("Tournament affects on Towns")]
    public float FoodStocksDecrease { get; set; } = 20.00f;


    //Tournament Cost
    [SettingProperty("Minimum # of Weapon in Prize Pool", 0, 5, "Minimum number of weapons to offer when selecting tournament prize.")]
    [SettingPropertyGroup("Tournament Prizes")]
    public int MinimumNumberOfWeaponsInPrizePool { get; set; } = 0;

    [SettingProperty("Give Prize to Player if Subordinate Wins", "If enabled, whenever your subordinates win tournaments they will give the prize to you.")]
    [SettingPropertyGroup("Tournament Prizes")]
    public bool SubordinatesGivePrizeToPlayer { get; set; } = true;

    //Amount of initial tournaments on the map
    [SettingProperty("Tournament Cost", -10000, 10000, "Cost of tournaments")]
    [SettingPropertyGroup("Tournaments")]
    public int TournamentCost { get; set; } = 2000;

    //Amount of initial tournaments on the map
    [SettingProperty("Initial Tournament Spawns", 0, 20, "Number of tournaments spawned after a new game")]
    [SettingPropertyGroup("Tournaments")]
    public int TournamentInitialSpawnCount { get; set; } = 10;

    //Tournament Skill XP gain
    [SettingProperty("Tournament Win Skill XP", 0, 10000, "Amount of XP gained for targeted skill on a tournament win")]
    [SettingPropertyGroup("Tournaments")]
    public int TournamentSkillXp { get; set; } = 1500;

    //Tournament hit XP gain
    [SettingProperty("Tournament Hit Skill XP", 0f, 10f, "XP multiplier for hits in tournaments")]
    [SettingPropertyGroup("Tournaments")]
    public float TournamentHitXP { get; set; } = 1f;

    //Arena hit XP gain
    [SettingProperty("Arena Hit Skill XP", 0f, 10f, "XP multiplier for hits in the arena")]
    [SettingPropertyGroup("Tournaments")]
    public float ArenaHitXP { get; set; } = 1f;

    [SettingProperty("Tournament Combat AI Difficulty", "I want tournaments harder than normal battles")]
    [SettingPropertyGroup("Tournaments")]
    public bool VeryHardTournaments { get; set; } = true;

    [SettingProperty("Maximum of Heroes added", 5, 10, "Max number of faction Heroes added to tournaments (makes them more difficult)")]
    [SettingPropertyGroup("Tournaments")]
    public int UpperBoundHeroesAdded { get; set; } = 10;

    [SettingProperty("Renown Reward", 0, 100, "Amount of Renown received for winning a tournament")]
    [SettingPropertyGroup("Tournaments")]
    public int RenownReward { get; set; } = 10;

    [SettingProperty("Bring Companions", "I want my companions to join me in tournaments (on my team when possible)")]
    [SettingPropertyGroup("Tournaments")]
    public bool BringCompanions { get; set; } = true;
    [SettingProperty("Enable Create/Resolve Tournaments", "Adds town menu options for creating and resolving tournaments")]
    [SettingPropertyGroup("Debug")]
    public bool DebugCreateAndResolveTournaments { get; set; } = false;

    [SettingProperty("Enable Team Tournaments", "Adds an option for joining and participating in the tournaments as a team.")]
    [SettingPropertyGroup("Tournaments")]
    public bool EnableTeamTournaments { get; set; } = true;

    [SettingProperty("Team 1 Color Index", 0, 157, "Set Team 1's banner color by index value. (Currently cannot show color in menu screen.)")]
    [SettingPropertyGroup("Team Tournament Game Mode")]
    public int Team1Color { get; set; } = 118;
    [SettingProperty("Team 2 Color Index", 0, 157, "Set Team 2's banner color by index value. (Currently cannot show color in menu screen.)")]
    [SettingPropertyGroup("Team Tournament Game Mode")]
    public int Team2Color { get; set; } = 119;
    [SettingProperty("Team 3 Color Index", 0, 157, "Set Team 3's banner color by index value. (Currently cannot show color in menu screen.)")]
    [SettingPropertyGroup("Team Tournament Game Mode")]
    public int Team3Color { get; set; } = 120;
    [SettingProperty("Team 4 Color Index", 0, 157, "Set Team 4's banner color by index value. (Currently cannot show color in menu screen.)")]
    [SettingPropertyGroup("Team Tournament Game Mode")]
    public int Team4Color { get; set; } = 121;
  }
}
