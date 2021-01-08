using System;
using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Localization;
using TournamentsEnhanced.Wrappers.ObjectSystem;

namespace TournamentsEnhanced.Wrappers.Core
{
  public static class MBInformationManager
  {

    public static void AddHintInformation(string message) => InformationManager.AddHintInformation(message);
    public static void AddNotice(MBInformationData data) => InformationManager.AddNotice(data);
    public static void AddQuickInformation(string message,
                                           int priorty = 0,
                                           MBBasicCharacterObject announcerCharacter = null,
                                           string soundEventPath = "")
       => InformationManager.AddQuickInformation(new MBTextObject(message), priorty, announcerCharacter, soundEventPath);
    public static void AddSystemNotification(string message) => InformationManager.AddSystemNotification(message);
    public static void AddTooltipInformation(Type type, params object[] args) => InformationManager.AddTooltipInformation(type, args);
    public static void Clear() => InformationManager.Clear();
    public static void ClearAllMessages() => InformationManager.ClearAllMessages();
    public static void DisplayMessage(string message) => InformationManager.DisplayMessage(new MBInformationMessage(message));
    public static void HideInformations() => InformationManager.HideInformations();
    public static void HideInquiry() => InformationManager.HideInquiry();
    public static void HideSceneNotification() => InformationManager.HideSceneNotification();
    public static void MapNoticeRemoved(InformationData data) => InformationManager.MapNoticeRemoved(data);
    public static void ShowInquiry(MBInquiryData data, bool pauseGameActiveState = false) => InformationManager.ShowInquiry(data, pauseGameActiveState);
    public static void ShowMultiSelectionInquiry(MultiSelectionInquiryData data, bool pauseGameActiveState = false) => InformationManager.ShowMultiSelectionInquiry(data, pauseGameActiveState);
    public static void ShowSceneNotification(SceneNotificationData data, bool pauseGameActiveState = false) => InformationManager.ShowSceneNotification(data, pauseGameActiveState);
    public static void ShowTextInquiry(TextInquiryData textData, bool pauseGameActiveState = false) => InformationManager.ShowTextInquiry(textData, pauseGameActiveState);
    public static void UpdateTooltipFromArray(int id, int mode, object[] props) => InformationManager.UpdateTooltipFromArray(id, mode, props);

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
        MBInformationManager.DisplayMessage("Error creating prize list");
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