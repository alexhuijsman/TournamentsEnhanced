using System;
using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Localization;
using TournamentsEnhanced.Wrappers.ObjectSystem;

namespace TournamentsEnhanced.Wrappers.Core
{
  public static class MBInformationManager
  {

    public static void AddHintInformation(string message);
    public static void AddNotice(InformationData data);
    public static void AddQuickInformation(TextObject message, int priorty = 0, BasicCharacterObject announcerCharacter = null, string soundEventPath = "");
    public static void AddSystemNotification(string message);
    public static void AddTooltipInformation(Type type, params object[] args);
    public static void Clear();
    public static void ClearAllMessages();
    public static void DisplayMessage(InformationMessage message);
    public static void HideInformations();
    public static void HideInquiry();
    public static void HideSceneNotification();
    public static void MapNoticeRemoved(InformationData data);
    public static void ShowInquiry(InquiryData data, bool pauseGameActiveState = false);
    public static void ShowMultiSelectionInquiry(MultiSelectionInquiryData data, bool pauseGameActiveState = false);
    public static void ShowSceneNotification(SceneNotificationData data, bool pauseGameActiveState = false);
    public static void ShowTextInquiry(TextInquiryData textData, bool pauseGameActiveState = false);
    public static void UpdateTooltipFromArray(int id, int mode, object[] props);
    public static void DisplayLoggedMessage(string message) => InformationManager.AddTooltipInformation.DisplayMessage(message);
    public static void DisplayBannerText(MBTextObject message) => InformationManager.AddQuickInformation(message);

    public static void ShowPrizeSelectionMenu(MBItemObjectList items,
                                                   Action<MBInquiryElementList> affirmativeAction,
                                                   Action<MBInquiryElementList> negativeAction)
    {
      var inquiryElements = CreateElementListFromItems(items);
      MBInquiryElementList selectedElements;
      bool isAffirmative;

      if (inquiryElements.Count > 0)
      {
        var inquiryData =
          new MBMultiSelectionInquiryData(
            "Prize Selection",
            "Pick a prize from the list below",
            inquiryElements,
            true,
            1,
            "OK",
            "Cancel",
            (List<InquiryElement> list) => OnAffirmativeAction(
              list,
              out isAffirmative,
              out selectedElements),
            (List<InquiryElement> list) => OnNegativeAction(
              list,
              out isAffirmative,
              out selectedElements));

        InformationManager.ShowMultiSelectionInquiry(inquiryData, true);
      }
      else
      {
        MBInformationManager.DisplayLoggedMessage("Error creating prize list");
      }
    }

    private static void OnAffirmativeAction(List<InquiryElement> list, out bool affirmativeResult, out MBInquiryElementList selectedInquiryElements)
    {
      affirmativeResult = true;
      selectedInquiryElements = list;
    }

    private static void OnNegativeAction(List<InquiryElement> list, out bool affirmativeResult, out MBInquiryElementList selectedInquiryElements)
    {
      affirmativeResult = false;
      selectedInquiryElements = list;
    }


    private static MBInquiryElementList CreateElementListFromItems(MBItemObjectList itemList)
    {
      var inquiryElements = new List<InquiryElement>();

      foreach (var item in itemList)
      {
        inquiryElements.Add(CreateInquiryElementFromItem(item));
      }

      return inquiryElements;
    }

    private static InquiryElement CreateInquiryElementFromItem(MBItemObject item)
    {
      var itemModifier =
          string.IsNullOrWhiteSpace(item.StringId) ? null : MBMBObjectManager.GetObjectById<ItemModifier>(item.StringId);
      var equipmentElement = new EquipmentElement(item, itemModifier);
      var imageIdentifier = new ImageIdentifier(
          equipmentElement.Item.StringId,
          ImageIdentifierType.Item,
          equipmentElement.GetModifiedItemName().ToString());

      return new InquiryElement(
          equipmentElement.Item.StringId,
          equipmentElement.GetModifiedItemName().ToString(),
          imageIdentifier,
          true,
          equipmentElement.ToString()
      );
    }
  }
}