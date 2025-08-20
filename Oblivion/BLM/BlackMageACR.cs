using AEAssist.CombatRoutine.Module.Opener;
using Oblivion.BLM.SlotResolver.Ability;
using Oblivion.BLM.SlotResolver.GCD;
using Oblivion.BLM.SlotResolver.GCD.AOE;
using Oblivion.BLM.SlotResolver.GCD.单体;
using Oblivion.BLM.SlotResolver.Opener;
using Oblivion.BLM.SlotResolver.Special;
using Oblivion.BLM.Triggers;
using Oblivion.BLM.View;
using Oblivion.Utils;

namespace Oblivion.BLM;

public class BlackMageACR
{
    private const long Version = 202410172317;
    public static string settingFolderPath;
    
    private static readonly List<SlotResolverData> SlotResolverData =
    [
        //GCD
        new (new TTK(),SlotMode.Gcd),
        new(new 异言(), SlotMode.Gcd),
        new(new 秽浊(), SlotMode.Gcd),
        new(new 雷1(), SlotMode.Gcd),
        new(new 雷2(), SlotMode.Gcd),
        new(new 瞬发gcd触发器(), SlotMode.Gcd),
        new (new 火群(), SlotMode.Gcd),
        new (new 冰群(), SlotMode.Gcd),
        new (new 冰单100(), SlotMode.Gcd),
        new (new 火单100(), SlotMode.Gcd),
        new (new 冰单90(), SlotMode.Gcd),
        new (new 火单90(), SlotMode.Gcd),
        new (new 冰单80(), SlotMode.Gcd),
        new (new 火单80(), SlotMode.Gcd),
        new (new 冰单70(), SlotMode.Gcd),
        new (new 火单70(), SlotMode.Gcd),
        new(new 核爆补耀星(), SlotMode.Gcd),
        new(new 即刻三连(), SlotMode.Always),
        

        //Ability
        new(new 星灵移位(),SlotMode.OffGcd),
        new(new 即刻(),SlotMode.OffGcd),
        new(new 三连咏唱(),SlotMode.OffGcd),
        new(new 醒梦(),SlotMode.OffGcd),
        new(new 墨泉(),SlotMode.OffGcd),
        new(new 详述(),SlotMode.OffGcd),
        new(new 黑魔纹(),SlotMode.OffGcd),

    ];

    public static void Init(string settingFolder)
    {
        settingFolderPath = settingFolder;
        GlobalSetting.Build(settingFolder, "OVO", false);
        BlackMageSetting.Build(settingFolder);
        baseUI.Build();
    }

    public static Rotation Build()
    {
        return new Rotation(SlotResolverData)
            {
                TargetJob = Jobs.BlackMage,
                AcrType = AcrType.HighEnd,
                MinLevel = 70,
                MaxLevel = 100,
                Description = "请打开悬浮窗查看设置"
            }.AddOpener(GetOpener)
            .SetRotationEventHandler(new BlackMageEvetHandle())
            .AddSlotSequences(特殊序列.Build())
            .AddTriggerAction(new TriggerActionQt(), new TriggerActionHotkey(), new TriggerActionNewQt())
            .AddTriggerCondition(new TriggerCondQt())
            //.AddCanPauseACRCheck((() => -1))
            ;
    }
    private static IOpener? GetOpener(uint level)
    {
        //if (!QT.Instance.GetQt("起手序列")) return null;
        if (level == 100)
        {
            if (BlackMageSetting.Instance.标准57) return new Opener57();
            if (BlackMageSetting.Instance.核爆起手) return new Opener核爆();
            if (BlackMageSetting.Instance.开挂循环) return new Opener57开挂循环();
        }

        if (level >= 90 && level < 100) return new Opener_lv90();
        if (level >= 80 && level < 90) return new Opener_lv80();
        if (level >= 70 && level < 80) return new Opener_lv70();
        return null;
    }

}