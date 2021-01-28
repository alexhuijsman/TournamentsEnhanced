using System.Collections.Generic;
using TaleWorlds.Core;
using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using TournamentsEnhanced.Wrappers.Library;
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
    List<MBWeaponComponentData> Weapons { get; }
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
    MBVec3 HolsterPositionShift { get; }
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
  public partial class MBItemObject : MBObjectBaseWrapper<MBItemObject, ItemObject>, IMBItemObject
  {
    public static List<MBItemObject> AllTradeGoods => ItemObject.AllTradeGoods.CastList<MBItemObject>();
    public static List<MBItemObject> All => ItemObject.All.CastList<MBItemObject>();

    public virtual MBWeaponComponent WeaponComponent => UnwrappedObject.WeaponComponent;

    public virtual MBWeaponComponentData PrimaryWeapon => UnwrappedObject.PrimaryWeapon;

    public virtual MBWeaponDesign WeaponDesign => UnwrappedObject.WeaponDesign;

    public virtual bool IsCraftedWeapon => UnwrappedObject.IsCraftedWeapon;

    public virtual int LodAtlasIndex => UnwrappedObject.LodAtlasIndex;

    public virtual bool IsCraftedByPlayer => UnwrappedObject.IsCraftedByPlayer;

    public virtual float ScaleFactor => UnwrappedObject.ScaleFactor;

    public virtual bool MultiplayerItem => UnwrappedObject.MultiplayerItem;

    public virtual MBBasicCultureObject Culture => UnwrappedObject.Culture;

    public virtual MBHorseComponent HorseComponent => UnwrappedObject.HorseComponent;

    public virtual bool IsUniqueItem => UnwrappedObject.IsUniqueItem;

    public virtual bool IsFood => UnwrappedObject.IsFood;

    public virtual string ArmBandMeshName => UnwrappedObject.ArmBandMeshName;

    public virtual bool NotMerchandise => UnwrappedObject.NotMerchandise;

    public virtual bool HasHorseComponent => UnwrappedObject.HasHorseComponent;

    public virtual bool HasSaddleComponent => UnwrappedObject.HasSaddleComponent;

    public virtual bool HasArmorComponent => UnwrappedObject.HasArmorComponent;

    public virtual MBSaddleComponent SaddleComponent => UnwrappedObject.SaddleComponent;

    public virtual bool UsingFacegenScaling => UnwrappedObject.UsingFacegenScaling;

    public virtual MBTradeItemComponent FoodComponent => UnwrappedObject.FoodComponent;

    public virtual bool HasFoodComponent => UnwrappedObject.HasFoodComponent;

    public virtual float Tierf => UnwrappedObject.Tierf;

    public virtual ItemTiers Tier => UnwrappedObject.Tier;

    public virtual MBItemObject PrerequisiteItem => UnwrappedObject.PrerequisiteItem;

    public virtual List<MBWeaponComponentData> Weapons => UnwrappedObject.Weapons.CastList<MBWeaponComponentData>();

    public virtual ItemTypeEnum ItemType => UnwrappedObject.ItemType;

    public virtual bool IsMountable => UnwrappedObject.IsMountable;

    public virtual bool IsTradeGood => UnwrappedObject.IsTradeGood;

    public virtual bool IsAnimal => UnwrappedObject.IsAnimal;

    public virtual MBSkillObject RelevantSkill => UnwrappedObject.RelevantSkill;

    public virtual MBArmorComponent ArmorComponent => UnwrappedObject.ArmorComponent;

    public virtual bool IsCivilian => UnwrappedObject.IsCivilian;

    public virtual bool IsUsingTableau => UnwrappedObject.IsUsingTableau;

    public virtual bool IsUsingTeamColor => UnwrappedObject.IsUsingTeamColor;

    public virtual MBItemComponent ItemComponent => UnwrappedObject.ItemComponent;

    public virtual string MultiMeshName => UnwrappedObject.MultiMeshName;

    public virtual string HolsterMeshName => UnwrappedObject.HolsterMeshName;

    public virtual string HolsterWithWeaponMeshName => UnwrappedObject.HolsterWithWeaponMeshName;

    public virtual bool DoesNotHideChest => UnwrappedObject.DoesNotHideChest;

    public MBVec3 HolsterPositionShift => UnwrappedObject.HolsterPositionShift;

    public virtual bool HasLowerHolsterPriority => UnwrappedObject.HasLowerHolsterPriority;

    public virtual string FlyingMeshName => UnwrappedObject.FlyingMeshName;

    public virtual string BodyName => UnwrappedObject.BodyName;

    public virtual string HolsterBodyName => UnwrappedObject.HolsterBodyName;

    public virtual string CollisionBodyName => UnwrappedObject.CollisionBodyName;

    public string[] ItemHolsters => UnwrappedObject.ItemHolsters;

    public virtual string PrefabName => UnwrappedObject.PrefabName;

    public virtual MBTextObject Name => UnwrappedObject.Name;

    public virtual ItemFlags ItemFlags => UnwrappedObject.ItemFlags;

    public virtual MBItemCategory ItemCategory => UnwrappedObject.ItemCategory;

    public virtual int Value => UnwrappedObject.Value;

    public virtual float Effectiveness => UnwrappedObject.Effectiveness;

    public virtual float Weight => UnwrappedObject.Weight;

    public virtual int Difficulty => UnwrappedObject.Difficulty;

    public virtual float Appearance => UnwrappedObject.Appearance;

    public virtual bool RecalculateBody => UnwrappedObject.RecalculateBody;

    public override int GetHashCode() => UnwrappedObject.GetHashCode();
    public override string ToString() => UnwrappedObject.ToString();

    public static implicit operator ItemObject(MBItemObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBItemObject(ItemObject obj) => GetWrapper(obj);
  }
}
