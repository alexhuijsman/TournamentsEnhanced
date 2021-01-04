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
  public class MBItemObject : MBObjectBaseWrapper<MBItemObject, ItemObject>, IMBItemObject
  {
    public static MBItemObjectList AllTradeGoods => ItemObject.AllTradeGoods.ToList();
    public static MBItemObjectList All => ItemObject.All.ToList();

    public MBWeaponComponent WeaponComponent => UnwrappedObject.WeaponComponent;

    public MBWeaponComponentData PrimaryWeapon => UnwrappedObject.PrimaryWeapon;

    public MBWeaponDesign WeaponDesign => UnwrappedObject.WeaponDesign;

    public bool IsCraftedWeapon => UnwrappedObject.IsCraftedWeapon;

    public int LodAtlasIndex => UnwrappedObject.LodAtlasIndex;

    public bool IsCraftedByPlayer => UnwrappedObject.IsCraftedByPlayer;

    public float ScaleFactor => UnwrappedObject.ScaleFactor;

    public bool MultiplayerItem => UnwrappedObject.MultiplayerItem;

    public MBBasicCultureObject Culture => UnwrappedObject.Culture;

    public MBHorseComponent HorseComponent => UnwrappedObject.HorseComponent;

    public bool IsUniqueItem => UnwrappedObject.IsUniqueItem;

    public bool IsFood => UnwrappedObject.IsFood;

    public string ArmBandMeshName => UnwrappedObject.ArmBandMeshName;

    public bool NotMerchandise => UnwrappedObject.NotMerchandise;

    public bool HasHorseComponent => UnwrappedObject.HasHorseComponent;

    public bool HasSaddleComponent => UnwrappedObject.HasSaddleComponent;

    public bool HasArmorComponent => UnwrappedObject.HasArmorComponent;

    public MBSaddleComponent SaddleComponent => UnwrappedObject.SaddleComponent;

    public bool UsingFacegenScaling => UnwrappedObject.UsingFacegenScaling;

    public MBTradeItemComponent FoodComponent => UnwrappedObject.FoodComponent;

    public bool HasFoodComponent => UnwrappedObject.HasFoodComponent;

    public float Tierf => UnwrappedObject.Tierf;

    public ItemTiers Tier => UnwrappedObject.Tier;

    public MBItemObject PrerequisiteItem => UnwrappedObject.PrerequisiteItem;

    public MBWeaponComponentDataList Weapons => UnwrappedObject.Weapons.ToList();

    public ItemTypeEnum ItemType => UnwrappedObject.ItemType;

    public bool IsMountable => UnwrappedObject.IsMountable;

    public bool IsTradeGood => UnwrappedObject.IsTradeGood;

    public bool IsAnimal => UnwrappedObject.IsAnimal;

    public MBSkillObject RelevantSkill => UnwrappedObject.RelevantSkill;

    public MBArmorComponent ArmorComponent => UnwrappedObject.ArmorComponent;

    public bool IsCivilian => UnwrappedObject.IsCivilian;

    public bool IsUsingTableau => UnwrappedObject.IsUsingTableau;

    public bool IsUsingTeamColor => UnwrappedObject.IsUsingTeamColor;

    public MBItemComponent ItemComponent => UnwrappedObject.ItemComponent;

    public string MultiMeshName => UnwrappedObject.MultiMeshName;

    public string HolsterMeshName => UnwrappedObject.HolsterMeshName;

    public string HolsterWithWeaponMeshName => UnwrappedObject.HolsterWithWeaponMeshName;

    public bool DoesNotHideChest => UnwrappedObject.DoesNotHideChest;

    public Vec3 HolsterPositionShift => UnwrappedObject.HolsterPositionShift;

    public bool HasLowerHolsterPriority => UnwrappedObject.HasLowerHolsterPriority;

    public string FlyingMeshName => UnwrappedObject.FlyingMeshName;

    public string BodyName => UnwrappedObject.BodyName;

    public string HolsterBodyName => UnwrappedObject.HolsterBodyName;

    public string CollisionBodyName => UnwrappedObject.CollisionBodyName;

    public string[] ItemHolsters => UnwrappedObject.ItemHolsters;

    public string PrefabName => UnwrappedObject.PrefabName;

    public MBTextObject Name => UnwrappedObject.Name;

    public ItemFlags ItemFlags => UnwrappedObject.ItemFlags;

    public MBItemCategory ItemCategory => UnwrappedObject.ItemCategory;

    public int Value => UnwrappedObject.Value;

    public float Effectiveness => UnwrappedObject.Effectiveness;

    public float Weight => UnwrappedObject.Weight;

    public int Difficulty => UnwrappedObject.Difficulty;

    public float Appearance => UnwrappedObject.Appearance;

    public bool RecalculateBody => UnwrappedObject.RecalculateBody;

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
      return IsTierable() && UnwrappedObject.Tier == MBHero.GetMainHeroTournamentRewardTier();
    }

    public bool IsTierable()
    {
      return IsOfAnyMatchingType(ItemConstants.TierableItemTypes);
    }

    private bool IsOfAnyMatchingType(params ItemTypeEnum[] matchingItemTypes)
    {
      var actualItemType = UnwrappedObject.ItemType;
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

    public override int GetHashCode() => UnwrappedObject.GetHashCode();
    public override string ToString() => UnwrappedObject.ToString();

    public static implicit operator ItemObject(MBItemObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBItemObject(ItemObject obj) => MBItemObject.GetWrapperFor(obj);
  }

  public class MBItemObjectList : MBListBase<MBItemObject, MBItemObjectList>
  {
    public static implicit operator List<ItemObject>(MBItemObjectList wrapperList) => wrapperList.Unwrap<MBItemObject, ItemObject>();
    public static implicit operator MBItemObjectList(List<ItemObject> objectList) => (MBItemObjectList)objectList.Wrap<MBItemObject, ItemObject>();
  }
}
