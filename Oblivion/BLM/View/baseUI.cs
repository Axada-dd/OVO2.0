using Oblivion.BLM.QtUI;
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
    }
}