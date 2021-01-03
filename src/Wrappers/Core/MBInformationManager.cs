using System;
using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.ObjectSystem;

namespace TournamentsEnhanced.Wrappers.Core
{
  public static class MBInformationManager
  {
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
        NotificationUtils.DisplayMessage("Error creating prize list");
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