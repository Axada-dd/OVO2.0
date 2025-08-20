using AEAssist.MemoryApi;
using ImGuiNET;
using Oblivion.Utils.JobView;

namespace Oblivion.BLM.View.TAB;

public static class Dev
{
    public static void DrawDev(JobViewWindow jobViewWindow)
    {
        if (BlackMageSetting.Instance.调试窗口)
        {
            ImGui.Begin("调试窗口");
            Draw();
            ImGui.End();
        }

        Draw();
    }
    public static void Draw()
    {
        if (ImGui.Checkbox("窗口单独显示", ref BlackMageSetting.Instance.调试窗口))
        {
            BlackMageSetting.Instance.Save();
        }

        ImGui.Separator();

        ImGui.Text($"上一G：{BattleData.Instance.前一gcd}");
        ImGui.Text($"复唱时间:{GCDHelper.GetGCDDuration()}");
        ImGui.Text($"剩余GCD时间：{GCDHelper.GetGCDCooldown()}");
        ImGui.Text($"能力技卡G：{BlackMageHelper.能力技卡g}");
        ImGui.Text($"发呆中：{BlackMageHelper.在发呆()}");
        ImGui.Text($"使用瞬发：{BattleData.Instance.已使用瞬发}");
        ImGui.Text($"可瞬发：{Helper.可瞬发()}");
        //ImGui.Text($"已使用耀星：{BattleData.Instance.已使用耀星}");
        ImGui.Text($"已存在黑魔纹：{Helper.有buff(737)}");
        ImGui.Text($"三连咏唱CD：{BlackMageHelper.三连cd()}");
        //ImGui.Text($"火循环剩余gcd：{BattleData.Instance.火循环剩余gcd}");
        //ImGui.Text($"冰循环剩余gcd：{BattleData.Instance.冰循环剩余gcd}");
        //ImGui.Text($"能使用火四个数：{BattleData.Instance.能使用的火四个数}");
        //ImGui.Text($"能使用耀星：{BattleData.Instance.能使用耀星}");
        //ImGui.Text($"三连转冰：{BattleData.Instance.三连转冰}");
        ImGui.Text($"需要瞬发：{BattleData.Instance.需要瞬发gcd}");
        ImGui.Text($"需要即刻: {BattleData.Instance.需要即刻}");
        //ImGui.Text($"特供循环判断:{new 开满转火().StartCheck()}");
        //ImGui.Text($"双星灵墨泉：{new 双星灵墨泉().StartCheck()}");
        ImGui.Text($"三冰针进冰：{BattleData.Instance.三冰针进冰}");
        ImGui.Text($"双目标：{BlackMageHelper.双目标aoe()}-----三目标: {BlackMageHelper.三目标aoe()}");
        


        if (ImGui.Button("打断当前读条"))
        {
            Core.Resolve<MemApiSpell>().CancelCast();
        }

        ImGui.Separator();
        if (ImGui.TreeNode("插入技能状态"))
        {
            if (ImGui.Button("清除队列"))
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Clear();
                AI.Instance.BattleData.HighPrioritySlots_GCD.Clear();
            }

            ImGui.SameLine();
            if (ImGui.Button("清除一个"))
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Dequeue();
                AI.Instance.BattleData.HighPrioritySlots_GCD.Dequeue();
            }

            ImGui.Text("-------能力技-------");
            if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
                foreach (var action in
                         AI.Instance.BattleData.HighPrioritySlots_OffGCD.SelectMany(spell => spell.Actions))
                {
                    ImGui.Text(action.Spell.Name);
                }

            ImGui.Text("-------GCD-------");
            if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
                foreach (var action in AI.Instance.BattleData.HighPrioritySlots_GCD.SelectMany(spell => spell.Actions))
                {
                    ImGui.Text(action.Spell.Name);
                }

            ImGui.TreePop();
        }

        ImGui.Separator();

    }
}