namespace TournamentsEnhanced
{
    public static class InformationManagerUtils
    {
        public static void ShowMultiSelectionScreenForItems(List<ItemObject> items)
        {
            var inquiryElements = CreateInquiryElementsFromItems(prizeList);

            if (inquiryElements.Count > 0)
            {
                TextObject textObject = new TextObject("Pick a prize from the list below", null);
                InformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData(new TextObject("Prize Selection",
                        null).ToString(), textObject.ToString(), inquiryElements, true, 1,
                    new TextObject("OK", null).ToString(), new TextObject("Cancel", null).ToString(),
                    new Action<List<InquiryElement>>(BehaviorBase.OnSelectPrize),
                    new Action<List<InquiryElement>>(BehaviorBase.OnDeSelectPrize),
                    ""), true);
                GameMenu.SwitchToMenu("town_arena");
            }
            else
            {
                NotificationUtils.DisplayMessage("Error creating prize list");
            }
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
