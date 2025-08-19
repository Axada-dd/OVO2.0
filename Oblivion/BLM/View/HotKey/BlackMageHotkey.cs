using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;

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
    }

    public static string[] GetHotkeyArray()
    {
        return baseUI.UI.GetHotkeyArray();
    }

    public static void SetHotkey(string key)
    {
        baseUI.UI.SetHotkey(key);
    }
}