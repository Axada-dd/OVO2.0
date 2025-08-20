using Oblivion.BLM.QtUI;

namespace Oblivion.BLM.SlotResolver.GCD;

public class 异言 : ISlotResolver
{
    private readonly uint _skillId = Skill.异言;
    private Spell? GetSpell()
    {
        return  _skillId.GetSpell();
    }
    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell != null) 
            slot.Add(spell);
    }

    public int Check()
    {
        if (Core.Me.Level < 80) return -80;
        if (!BlackMageQT.GetQt(QTkey.通晓)) return -2;
        if (BlackMageHelper.三目标aoe() || BlackMageHelper.双目标aoe()) return -3;
        if ((BlackMageQT.GetQt(QTkey.倾泻资源)|| BlackMageSetting.Instance.FATE模式) && BlackMageHelper.通晓层数 > 0 ) return 666;
        if (Core.Me.Level >= 98)
        {
            if (BlackMageHelper.通晓层数 == 3 && BlackMageHelper.通晓剩余时间 <= 10000) return 2;
            if (BlackMageHelper.通晓层数 == 3 && Skill.详述.GetSpell().AbilityCoolDownInNextXgcDsWindow(1)) return 3;
            if (BlackMageHelper.火状态)
            {
                if (Core.Me.CurrentMp < 800 && BlackMageHelper.耀星层数 != 6)
                {
                    if (Skill.墨泉.技能CD() < 300 && Skill.墨泉.GetSpell().IsReadyWithCanCast()) return -3;
                    if (Skill.墨泉.GetSpell().AbilityCoolDownInNextXgcDsWindow(2)) return 4;
                }
            }
        }

        if (Core.Me.Level >= 80 && Core.Me.Level < 98)
        {
            if (BlackMageHelper.通晓层数 == 2 && BlackMageHelper.通晓剩余时间 < 8000) return 2;
            if (BlackMageHelper.通晓层数 == 2 && Core.Me.Level >= 86 && Skill.详述.GetSpell().AbilityCoolDownInNextXgcDsWindow(1)) return 3;
        }
        
        
        return -99;
    }
}
