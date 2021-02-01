using System;
using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Localization;
using TournamentsEnhanced.Wrappers.ObjectSystem;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBInformationManager
  {

    public static void AddHintInformation(string message) => InformationManager.AddHintInformation(message);
    public static void AddNotice(MBInformationData data) => InformationManager.AddNotice(data);
    public static void AddQuickInformation(MBTextObject message,
                                           int priorty = 0,
                                           MBBasicCharacterObject announcerCharacter = null,
                                           string soundEventPath = "")
       => InformationManager.AddQuickInformation(message, priorty, announcerCharacter, soundEventPath);
    public static void AddSystemNotification(string message) => InformationManager.AddSystemNotification(message);
    public static void AddTooltipInformation(Type type, params object[] args) => InformationManager.AddTooltipInformation(type, args);
    public static void Clear() => InformationManager.Clear();
    public static void ClearAllMessages() => InformationManager.ClearAllMessages();
    public static void DisplayMessage(MBInformationMessage message) => InformationManager.DisplayMessage(message);
    public static void HideInformations() => InformationManager.HideInformations();
    public static void HideInquiry() => InformationManager.HideInquiry();
    public static void HideSceneNotification() => InformationManager.HideSceneNotification();
    public static void MapNoticeRemoved(InformationData data) => InformationManager.MapNoticeRemoved(data);
    public static void ShowInquiry(MBInquiryData data, bool pauseGameActiveState = false) => InformationManager.ShowInquiry(data, pauseGameActiveState);
    public static void ShowMultiSelectionInquiry(MultiSelectionInquiryData data, bool pauseGameActiveState = false) => InformationManager.ShowMultiSelectionInquiry(data, pauseGameActiveState);
    public static void ShowSceneNotification(SceneNotificationData data, bool pauseGameActiveState = false) => InformationManager.ShowSceneNotification(data, pauseGameActiveState);
    public static void ShowTextInquiry(TextInquiryData textData, bool pauseGameActiveState = false) => InformationManager.ShowTextInquiry(textData, pauseGameActiveState);
    public static void UpdateTooltipFromArray(int id, int mode, object[] props) => InformationManager.UpdateTooltipFromArray(id, mode, props);

    public static void ShowPrizeSelectionMenu(List<MBItemObject> items,
                                                   Action<List<MBInquiryElement>> affirmativeAction,
                                                   Action<List<MBInquiryElement>> negativeAction)
    {
      var inquiryElements = CreateElementListFromItems(items);
      List<MBInquiryElement> selectedElements;
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

        MBInformationManager.ShowMultiSelectionInquiry(inquiryData, true);
      }
      else
      {
        MBInformationManagerFacade.DisplayAsLogEntry("Error creating prize list");
      }
    }

    private static void OnAffirmativeAction(List<InquiryElement> list, out bool affirmativeResult, out List<MBInquiryElement> selectedInquiryElements)
    {
      affirmativeResult = true;
      selectedInquiryElements = list.CastList<MBInquiryElement>();
    }

    private static void OnNegativeAction(List<InquiryElement> list, out bool affirmativeResult, out List<MBInquiryElement> selectedInquiryElements)
    {
      affirmativeResult = false;
      selectedInquiryElements = list.CastList<MBInquiryElement>();
    }


    private static List<MBInquiryElement> CreateElementListFromItems(List<MBItemObject> itemList)
    {
      var inquiryElements = new List<MBInquiryElement>();

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