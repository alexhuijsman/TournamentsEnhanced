using System;
using System.Collections.Generic;

using TaleWorlds.Core;
using TaleWorlds.Localization;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public static class MBInformationManager
  {
    public static void ShowSelectionScreenForItems(
                                                  MBItemObjectList items,
                                                  Action<List<InquiryElement>> affirmativeAction,
                                                  Action<List<InquiryElement>> negativeAction
                                                 )
    {
      var inquiryElements = CreateInquiryElementsFromItems(items);

      if (inquiryElements.Count > 0)
      {
        TextObject textObject = new TextObject("Pick a prize from the list below", null);
        InformationManager
          .ShowMultiSelectionInquiry(
                                     new MultiSelectionInquiryData
                                     (
                                        new TextObject("Prize Selection", null).ToString(),
                                        textObject.ToString(),
                                        inquiryElements,
                                        true,
                                        1,
                                        new TextObject("OK", null).ToString(),
                                        new TextObject("Cancel", null).ToString(),
                                        affirmativeAction,
                                        negativeAction
                                     ),
                                     true);
      }
      else
      {
        NotificationUtils.DisplayMessage("Error creating prize list");
      }
    }


    private static MBInquiryElementList CreateInquiryElementsFromItems(MBItemObjectList itemList)
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