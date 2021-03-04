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
  }
}