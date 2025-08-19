using System.Numerics;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Oblivion.BLM.QtUI.Hotkey;
using Oblivion.Utils.JobView;
using Oblivion.Utils.JobView.HotKey;

namespace Oblivion.BLM.View.HotKey;

public static class BlackMageHotkey
{
    
    public static void CreateHotkey()
    {
        baseUI.UI.AddHotkey("爆发药", new HotKeyResolver_Potion());
        baseUI.UI.AddHotkey("异言", new HotKeyResolver_NormalSpell(Skill.异言, SpellTargetType.Target));
        baseUI.UI.AddHotkey("秽浊", new HotKeyResolver_NormalSpell(Skill.秽浊, SpellTargetType.Target));
        baseUI.UI.AddHotkey("即刻", new HotKeyResolver_NormalSpell(Skill.即刻, SpellTargetType.Self));
        baseUI.UI.AddHotkey("黑魔纹", new HotKeyResolver_NormalSpell(Skill.黑魔纹, SpellTargetType.Self));
        baseUI.UI.AddHotkey("三连咏唱", new HotKeyResolver_NormalSpell(Skill.三连, SpellTargetType.Self));
        baseUI.UI.AddHotkey("冲刺", new HotKeyResolver_疾跑());
        baseUI.UI.AddHotkey("沉稳咏唱", new HotKeyResolver_NormalSpell(Skill.沉稳, SpellTargetType.Self));
        baseUI.UI.AddHotkey("混乱", new HotKeyResolver_NormalSpell(Skill.混乱, SpellTargetType.Target, true));
        baseUI.UI.AddHotkey("魔罩", new HotKeyResolver_NormalSpell(Skill.魔罩, SpellTargetType.Self));
        baseUI.UI.SetUpdateAction(OnUIUpdate);
    }

    public static string[] GetHotkeyArray()
    {
        return baseUI.UI.GetHotkeyArray();
    }

    public static void SetHotkey(string key)
    {
        baseUI.UI.SetHotkey(key);
    }
    public static HotKeyWindow 以太步窗口 { get; set; }
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
    public static void OnUIUpdate()
    {
        Update以太步窗口();
        var myJobViewSave = new JobViewSave();
        myJobViewSave.ShowHotkey = BlackMageSetting.Instance.以太步窗口显示;
        myJobViewSave.QtHotkeySize = new Vector2(BlackMageSetting.Instance.以太步IconSize);
        myJobViewSave.LockWindow = BlackMageSetting.Instance.锁定以太步窗口;
        以太步窗口?.DrawHotkeyWindow(new QtStyle(BlackMageSetting.Instance.JobViewSave));
        以太步窗口 = new HotKeyWindow(myJobViewSave, "以太步");
        以太步窗口.HotkeyLineCount = 1;

    }
}