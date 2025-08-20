using System.Runtime.InteropServices;
using Oblivion.BLM.QtUI;
using Oblivion.BLM.View.QT;

namespace Oblivion.BLM.SlotResolver.Special;

public class 双星灵墨泉 : ISlotSequence
{
    public List<Action<Slot>> Sequence { get; }

    public int StartCheck()
    {
        return -1;
        if (!BlackMageQT.GetQt("双星灵墨泉")) return -1;
        if (!BlackMageHelper.火状态) return -2;
        if (BlackMageHelper.耀星层数 == 6) return -3;
        if (Core.Me.CurrentMp > 800) return -4;
        if (Skill.墨泉.GetSpell().Cooldown.TotalSeconds < 3) return -7;
        if (Skill.墨泉.GetSpell().Cooldown.TotalSeconds > 12) return -5;
        if (Skill.醒梦.GetSpell().Cooldown.TotalSeconds > 1.5) return -6;
        if (BlackMageHelper.通晓层数 < 1) return -7;
        return 1;
    }

    public int StopCheck(int index)
    {
        return -1;
    }
    private static void step0(Slot slot)
    {
        BattleData.Instance.正在双星灵墨泉 = true;
        slot.Add(Skill.星灵移位.GetSpell(SpellTargetType.Self));
        if (Skill.醒梦.GetSpell().IsReadyWithCanCast())
        {
            slot.Add(Skill.醒梦.GetSpell(SpellTargetType.Self));
        }
    }
    private static void step1(Slot slot)
    {
        if (BlackMageHelper.补dot())
        {
            slot.Add(Skill.雷一.GetSpell(SpellTargetType.Target));
        }

        slot.Add(Skill.悖论.GetActionChange().GetSpell(SpellTargetType.Target));
    }

    private static void step2(Slot slot)
    {
        if (BlackMageHelper.补dot())
        {
            slot.Add(Skill.雷一.GetSpell(SpellTargetType.Target));
        }

        slot.Add(Skill.异言.GetSpell(SpellTargetType.Target));
    }
    
    public 双星灵墨泉()
    {
        List<Action<Slot>> list = new List<Action<Slot>>();
        list.Add(step0);
        list.Add(step1);
        list.Add(step2);
        Sequence = list;
    }
}