using System.Collections.Generic;

using TaleWorlds.Core;
using TaleWorlds.Library;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using TournamentsEnhanced.Wrappers.Localization;

using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced.Wrappers.Core
{
  public interface IMBItemObject
  {
    MBWeaponComponent WeaponComponent { get; }
    MBWeaponComponentData PrimaryWeapon { get; }
    MBWeaponDesign WeaponDesign { get; }
    bool IsCraftedWeapon { get; }
    int LodAtlasIndex { get; }
    bool IsCraftedByPlayer { get; }
    float ScaleFactor { get; }
    bool MultiplayerItem { get; }
    MBBasicCultureObject Culture { get; }
    MBHorseComponent HorseComponent { get; }
    bool IsUniqueItem { get; }
    bool IsFood { get; }
    string ArmBandMeshName { get; }
    bool NotMerchandise { get; }
    bool HasHorseComponent { get; }
    bool HasSaddleComponent { get; }
    bool HasArmorComponent { get; }
    MBSaddleComponent SaddleComponent { get; }
    bool UsingFacegenScaling { get; }
    MBTradeItemComponent FoodComponent { get; }
    bool HasFoodComponent { get; }
    float Tierf { get; }
    ItemTiers Tier { get; }
    MBItemObject PrerequisiteItem { get; }
    MBWeaponComponentDataList Weapons { get; }
    ItemTypeEnum ItemType { get; }
    bool IsMountable { get; }
    bool IsTradeGood { get; }
    bool IsAnimal { get; }
    MBSkillObject RelevantSkill { get; }
    MBArmorComponent ArmorComponent { get; }
    bool IsCivilian { get; }
    bool IsUsingTableau { get; }
    bool IsUsingTeamColor { get; }
    MBItemComponent ItemComponent { get; }
    string MultiMeshName { get; }
    string HolsterMeshName { get; }
    string HolsterWithWeaponMeshName { get; }
    bool DoesNotHideChest { get; }
    Vec3 HolsterPositionShift { get; }
    bool HasLowerHolsterPriority { get; }
    string FlyingMeshName { get; }
    string BodyName { get; }
    string HolsterBodyName { get; }
    string CollisionBodyName { get; }
    string[] ItemHolsters { get; }
    string PrefabName { get; }
    MBTextObject Name { get; }
    ItemFlags ItemFlags { get; }
    MBItemCategory ItemCategory { get; }
    int Value { get; }
    float Effectiveness { get; }
    float Weight { get; }
    int Difficulty { get; }
    float Appearance { get; }
    bool RecalculateBody { get; }
  }
  public class MBItemObject : CachedWrapperBase<MBItemObject, ItemObject>, IMBItemObject
  {
    public static MBItemObjectList AllTradeGoods => ItemObject.AllTradeGoods.ToList();
    public static MBItemObjectList All => ItemObject.All.ToList();

    public string StringId => UnwrapedObject.StringId;
    public MBWeaponComponent WeaponComponent => UnwrapedObject.WeaponComponent;

    public MBWeaponComponentData PrimaryWeapon => UnwrapedObject.PrimaryWeapon;

    public MBWeaponDesign WeaponDesign => UnwrapedObject.WeaponDesign;

    public bool IsCraftedWeapon => UnwrapedObject.IsCraftedWeapon;

    public int LodAtlasIndex => UnwrapedObject.LodAtlasIndex;

    public bool IsCraftedByPlayer => UnwrapedObject.IsCraftedByPlayer;

    public float ScaleFactor => UnwrapedObject.ScaleFactor;

    public bool MultiplayerItem => UnwrapedObject.MultiplayerItem;

    public MBBasicCultureObject Culture => UnwrapedObject.Culture;

    public MBHorseComponent HorseComponent => UnwrapedObject.HorseComponent;

    public bool IsUniqueItem => UnwrapedObject.IsUniqueItem;

    public bool IsFood => UnwrapedObject.IsFood;

    public string ArmBandMeshName => UnwrapedObject.ArmBandMeshName;

    public bool NotMerchandise => UnwrapedObject.NotMerchandise;

    public bool HasHorseComponent => UnwrapedObject.HasHorseComponent;

    public bool HasSaddleComponent => UnwrapedObject.HasSaddleComponent;

    public bool HasArmorComponent => UnwrapedObject.HasArmorComponent;

    public MBSaddleComponent SaddleComponent => UnwrapedObject.SaddleComponent;

    public bool UsingFacegenScaling => UnwrapedObject.UsingFacegenScaling;

    public MBTradeItemComponent FoodComponent => UnwrapedObject.FoodComponent;

    public bool HasFoodComponent => UnwrapedObject.HasFoodComponent;

    public float Tierf => UnwrapedObject.Tierf;

    public ItemTiers Tier => UnwrapedObject.Tier;

    public MBItemObject PrerequisiteItem => UnwrapedObject.PrerequisiteItem;

    public MBWeaponComponentDataList Weapons => UnwrapedObject.Weapons.ToList();

    public ItemTypeEnum ItemType => UnwrapedObject.ItemType;

    public bool IsMountable => UnwrapedObject.IsMountable;

    public bool IsTradeGood => UnwrapedObject.IsTradeGood;

    public bool IsAnimal => UnwrapedObject.IsAnimal;

    public MBSkillObject RelevantSkill => UnwrapedObject.RelevantSkill;

    public MBArmorComponent ArmorComponent => UnwrapedObject.ArmorComponent;

    public bool IsCivilian => UnwrapedObject.IsCivilian;

    public bool IsUsingTableau => UnwrapedObject.IsUsingTableau;

    public bool IsUsingTeamColor => UnwrapedObject.IsUsingTeamColor;

    public MBItemComponent ItemComponent => UnwrapedObject.ItemComponent;

    public string MultiMeshName => UnwrapedObject.MultiMeshName;

    public string HolsterMeshName => UnwrapedObject.HolsterMeshName;

    public string HolsterWithWeaponMeshName => UnwrapedObject.HolsterWithWeaponMeshName;

    public bool DoesNotHideChest => UnwrapedObject.DoesNotHideChest;

    public Vec3 HolsterPositionShift => UnwrapedObject.HolsterPositionShift;

    public bool HasLowerHolsterPriority => UnwrapedObject.HasLowerHolsterPriority;

    public string FlyingMeshName => UnwrapedObject.FlyingMeshName;

    public string BodyName => UnwrapedObject.BodyName;

    public string HolsterBodyName => UnwrapedObject.HolsterBodyName;

    public string CollisionBodyName => UnwrapedObject.CollisionBodyName;

    public string[] ItemHolsters => UnwrapedObject.ItemHolsters;

    public string PrefabName => UnwrapedObject.PrefabName;

    public MBTextObject Name => UnwrapedObject.Name;

    public ItemFlags ItemFlags => UnwrapedObject.ItemFlags;

    public MBItemCategory ItemCategory => UnwrapedObject.ItemCategory;

    public int Value => UnwrapedObject.Value;

    public float Effectiveness => UnwrapedObject.Effectiveness;

    public float Weight => UnwrapedObject.Weight;

    public int Difficulty => UnwrapedObject.Difficulty;

    public float Appearance => UnwrapedObject.Appearance;

    public bool RecalculateBody => UnwrapedObject.RecalculateBody;

    public static MBItemObjectList GetAvailableTournamentPrizes()
    {
      var allItems = All;
      var prizeItems = (MBItemObjectList)allItems.FindAll((MBItemObject item) => item.IsWorthyTournamentPrizeForMainHero());

      if (prizeItems.Count == 0)
      {
        prizeItems.Add(allItems.GetRandomElement());
      }

      return prizeItems;
    }

    public bool IsWorthyTournamentPrizeForMainHero()
    {
      return IsTierable() && UnwrapedObject.Tier == MBHero.GetMainHeroTournamentRewardTier();
    }

    public bool IsTierable()
    {
      return IsOfAnyMatchingType(ItemConstants.TierableItemTypes);
    }

    private bool IsOfAnyMatchingType(params ItemTypeEnum[] matchingItemTypes)
    {
      var actualItemType = UnwrapedObject.ItemType;
      var foundMatch = false;

      foreach (var matchingItemType in matchingItemTypes)
      {
        if (actualItemType == matchingItemType)
        {
          foundMatch = true;
          break;
        }
      }

      return foundMatch;
    }

    public override int GetHashCode() => UnwrapedObject.GetHashCode();
    public override string ToString() => UnwrapedObject.ToString();

    public static implicit operator ItemObject(MBItemObject wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBItemObject(ItemObject obj) => MBItemObject.GetWrapperFor(obj);
  }

  public class MBItemObjectList : List<MBItemObject>
  {
    public static implicit operator List<ItemObject>(MBItemObjectList wrapperList) => wrapperList.Unwrap<MBItemObject, ItemObject>();
    public static implicit operator MBItemObjectList(List<ItemObject> objectList) => (MBItemObjectList)objectList.Wrap<MBItemObject, ItemObject>();
  }
}
