using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TournamentsEnhanced
{
    public static class InformationManagerUtils
    {
        public static void ShowSelectionScreenForItems(
            List<ItemObject> items,
            Action<List<InquiryElement>> affirmativeAction, 
            Action<List<InquiryElement>> negativeAction
            )
        {
            var inquiryElements = CreateInquiryElementsFromItems(items);

            if (inquiryElements.Count > 0)
            {
                TextObject textObject = new TextObject("Pick a prize from the list below", null);
                InformationManager.ShowMultiSelectionInquiry(
                    new MultiSelectionInquiryData(
                        new TextObject("Prize Selection", null).ToString(), 
                        textObject.ToString(), 
                        inquiryElements, 
                        true, 
                        1,
                        new TextObject("OK", null).ToString(), new TextObject("Cancel", null).ToString(),
                        affirmativeAction,
                        negativeAction), 
                    true);
            }
            else
            {
                NotificationUtils.DisplayMessage("Error creating prize list");
            }
        }


    private static List<InquiryElement> CreateInquiryElementsFromItems(IList<ItemObject> itemList)
    {
      var inquiryElements = new List<InquiryElement>();

      foreach (var item in itemList)
      {
        inquiryElements.Add(CreateInquiryElementFromItem(item));
      }

      return inquiryElements;
    }
            private static InquiryElement CreateInquiryElementFromItem(ItemObject item)
        {
            var itemModifier =
                string.IsNullOrWhiteSpace(item.StringId) ? null : MBObjectManager.Instance.GetObject<ItemModifier>(item.StringId);
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
