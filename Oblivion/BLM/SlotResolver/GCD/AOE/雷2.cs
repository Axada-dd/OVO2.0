using Oblivion.BLM.QtUI;

namespace Oblivion.BLM.SlotResolver.GCD.AOE;

public class 雷2 : ISlotResolver
{
    private readonly uint _skillId = Skill.雷二.GetActionChange();
    private Spell? GetSpell()
    {
        //if (!_skillId.GetSpell().IsReadyWithCanCast()) return null;
        return BlackMageQT.GetQt(QTkey.智能aoe目标)? _skillId.GetSpellBySmartTarget() : _skillId.GetSpell();
    }
    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell != null) 
            slot.Add(spell);
    }

    public int Check()
    {
        if (!BlackMageHelper.双目标aoe() && !BlackMageHelper.三目标aoe()) return -100;
        if (!BlackMageQT.GetQt(QTkey.Dot)) return -2;
        if (BlackMageQT.GetQt(QTkey.TTK)) return -5;
        if (BattleData.Instance.正在特殊循环中) return -4;
        if (BlackMageHelper.冰状态&&Core.Me.HasAura(Buffs.雷云) && BlackMageHelper.补dot()) return 1;
        return -99;
    }
}
