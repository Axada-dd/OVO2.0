using System.Numerics;
using Oblivion.BLM.QtUI;
using Oblivion.BLM.QtUI.Hotkey;
using Oblivion.BLM.View.HotKey;
using Oblivion.BLM.View.QT;
using Oblivion.BLM.View.TAB;
using Oblivion.Utils.JobView;

namespace Oblivion.BLM.View;

public class baseUI
{
    public static JobViewWindow UI{get;private set;}
    public static JobViewWindow QT{get;private set;}

    public static void Build()
    {
        QT = UI = new JobViewWindow(BlackMageSetting.Instance.JobViewSave, BlackMageSetting.Instance.Save, "OVO 黑魔", ref BlackMageSetting.Instance.HotKey设置, BlackMageHotkey.List);
        UI.AddTab("说明", ReadmeTab.DrawReadme);
        UI.AddTab("设置", SettingTab.DrawSetting);
        UI.AddTab("DEV", Dev.DrawDev);
        UI.AddTab("循环", 循环Tab.DrawDev);
        BlackMageQT.CreateQt();
        BlackMageHotkey.CreateHotkey();
        UI.SetUpdateAction(OnUIUpdate);
    }
    public static Oblivion.Utils.JobView.HotKey.HotkeyWindow? 以太步窗口 { get; set; }
    public static void OnUIUpdate()
    {
        Update以太步窗口();
        var myJobViewSave = new Oblivion.Utils.JobView.JobViewSave();
        myJobViewSave.ShowHotkey = BlackMageSetting.Instance.以太步窗口显示;
        myJobViewSave.QtHotkeySize = new Vector2(BlackMageSetting.Instance.以太步IconSize);
        myJobViewSave.LockHotkeyWindow = BlackMageSetting.Instance.锁定以太步窗口;
        以太步窗口?.DrawHotkeyWindow(new Oblivion.Utils.JobView.QtStyle(BlackMageSetting.Instance.JobViewSave));
        以太步窗口 = new Oblivion.Utils.JobView.HotKey.HotkeyWindow(myJobViewSave, "以太步");
        以太步窗口.HotkeyLineCount = 1;

    }
    public static void Update以太步窗口()
    {
        PartyHelper.UpdateAllies();
        if (PartyHelper.Party.Count <= 1) return;
        for (var i = 1; i < PartyHelper.Party.Count; i++)
        {
            var index = i;
            以太步窗口?.AddHotkey("以太步: " + PartyHelper.Party[i].Name, new 以太步HotkeyResolver(index));
        }
    }
}