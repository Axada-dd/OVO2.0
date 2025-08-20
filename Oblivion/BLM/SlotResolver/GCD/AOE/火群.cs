using Oblivion.BLM.QtUI;

namespace Oblivion.BLM.SlotResolver.GCD.AOE;

public class 火群 : ISlotResolver
{
    private uint _skillId = 0;
    private Spell? GetSpell()
    {
        return BlackMageQT.GetQt(QTkey.智能aoe目标)? _skillId.GetSpellBySmartTarget() : _skillId.GetSpell();
    }
    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell != null) 
            slot.Add(spell);
    }

    private uint GetSkillId()
    {
        if (BlackMageHelper.火状态)
        {
            if (Core.Me.CurrentMp >= 800) return Skill.核爆;
            if (BlackMageHelper.耀星层数 == 6) return Skill.耀星;
        }

        if (BlackMageHelper.冰状态)
        {
            if (BlackMageHelper.冰针 < 3) return 0;
            if (BlackMageHelper.三目标aoe()&&BattleData.Instance.aoe火二) return Skill.火二.GetActionChange();
            //if (BLMHelper.双目标aoe()) return Skill.火三;
        }
        return 0;
    }
    public int Check()
    {
        if (Core.Me.Level < 70) return -70;
        if (!(BlackMageHelper.三目标aoe() || BlackMageHelper.双目标aoe())) return -234;
        _skillId = GetSkillId();
        if (_skillId == 0) return -1;
        return (int)_skillId;
    }
}