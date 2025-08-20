namespace Oblivion.BLM.SlotResolver.GCD.单体;

public class 火单80 : ISlotResolver
{
    private uint _skillId = 0;
    private Spell? GetSpell()
    {
        return _skillId.GetSpell();
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
            if (BlackMageHelper.火层数 <3) return Skill.火三;
            if (Core.Me.CurrentMp >= 800 && Core.Me.CurrentMp < 2400) return Skill.绝望;
            return Skill.火四;
        }

        if (BlackMageHelper.冰状态)
        {
            if (BlackMageHelper.冰层数 == 3 && BlackMageHelper.冰针 == 3) return Skill.火三;
        }
        return 0;
    }
    public int Check()
    {
        if (Core.Me.Level < 80 || Core.Me.Level >= 90) return -80;
        if (BlackMageHelper.三目标aoe() || BlackMageHelper.双目标aoe()) return -234;
        if (Helper.IsMove&&!Helper.可瞬发()) return -99;
        _skillId = GetSkillId();
        if (_skillId == 0) return -1;
        return (int)_skillId;
    }
}