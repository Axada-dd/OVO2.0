using AEAssist.MemoryApi;
using ImGuiNET;
using Oblivion.Utils.JobView;

namespace Oblivion.BLM.View.TAB;

public static class 循环Tab
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
        if (BattleData.Instance.冰状态gcd.Count > 0)
        {
            ImGui.Text("当前冰循环：");
            ImGui.Text($"{string.Join(",", BattleData.Instance.冰状态gcd.Select(s=>s.GetSpell().Name))}");
        }

        if (BattleData.Instance.火状态gcd.Count > 0)
        {
            ImGui.Text("当前火循环：");
            ImGui.Text($"{string.Join(",", BattleData.Instance.火状态gcd.Select(s=>s.GetSpell().Name))}");
        }

        if (BattleData.Instance.上一轮循环.Count > 0)
        {
            ImGui.Text("上一轮循环：");
            ImGui.Text($"{string.Join(",", BattleData.Instance.上一轮循环.Select(s=>s.GetSpell().Name))}");
        }
        ImGui.Separator();

    }
}