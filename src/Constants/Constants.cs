using System.Reflection;

using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced
{
  public static class Constants
  {
    public static class DaysSince
    {
      public static readonly TournamentType[] TournamentTypes =
      {
        TournamentType.Highborn,
        TournamentType.Invitation,
        TournamentType.PlayerInitiated,
        TournamentType.Prosperity,
      };
    }

    public static class Item
    {
      public static readonly ItemTypeEnum[] TierableItemTypes =
      {
        ItemTypeEnum.Arrows,
        ItemTypeEnum.BodyArmor,
        ItemTypeEnum.Bow,
        ItemTypeEnum.Cape,
        ItemTypeEnum.ChestArmor,
        ItemTypeEnum.Crossbow,
        ItemTypeEnum.HandArmor,
        ItemTypeEnum.HeadArmor,
        ItemTypeEnum.HorseHarness,
        ItemTypeEnum.LegArmor,
        ItemTypeEnum.OneHandedWeapon,
        ItemTypeEnum.Polearm,
        ItemTypeEnum.Shield,
        ItemTypeEnum.Horse,
        ItemTypeEnum.Thrown,
        ItemTypeEnum.TwoHandedWeapon
      };
    }

    public static class Module
    {
      public static readonly string Name = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
      public static readonly string ProductName = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product;
      public static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
  }
}
