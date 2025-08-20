using System.Diagnostics;
using System.Numerics;
using AEAssist.GUI;
using AEAssist.MemoryApi;
using ImGuiNET;
using Oblivion.BLM.SlotResolver.Opener;
using Oblivion.BLM.SlotResolver.Special;
using Oblivion.BLM.View;
using Oblivion.Utils.JobView;

namespace Oblivion.BLM.QtUI;

public static class SettingTab
{
    public static void DrawSetting(JobViewWindow jobViewWindow)
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
        if (ImGui.CollapsingHeader("起手设置", ImGuiTreeNodeFlags.DefaultOpen))
        {
            var openerSelectionIndex = 0;
            var openerSelection = "";
            if (BlackMageSetting.Instance.标准57)
                openerSelectionIndex = 1;
            if (BlackMageSetting.Instance.核爆起手)
                openerSelectionIndex = 2;
            if (BlackMageSetting.Instance.开挂循环)
                openerSelectionIndex = 3;


            switch (openerSelectionIndex)
            {
                case 0:
                    openerSelection = "请选择";
                    break;
                case 1:
                    openerSelection = "标准57";
                    break;
                case 2:
                    openerSelection = "核爆起手";
                    break;
                case 3:
                    openerSelection = "特供起手";
                    break;
            }

            if (ImGui.BeginCombo("起手选择", openerSelection))
            {
                if (ImGui.Selectable("请选择"))
                {
                }

                if (ImGui.Selectable("标准57"))
                {
                    BlackMageSetting.Instance.标准57 = true;
                    BlackMageSetting.Instance.核爆起手 = false;
                    BlackMageSetting.Instance.开挂循环 = false;
                }

                if (ImGui.Selectable("核爆起手"))
                {
                    BlackMageSetting.Instance.标准57 = false;
                    BlackMageSetting.Instance.核爆起手 = true;
                    BlackMageSetting.Instance.开挂循环 = false;
                }

                if (ImGui.Selectable("特供起手"))
                {
                    BlackMageSetting.Instance.标准57 = false;
                    BlackMageSetting.Instance.核爆起手 = false;
                    BlackMageSetting.Instance.开挂循环 = true;
                }

                ImGui.EndCombo();
            }

            bool 倒计时黑魔纹 = BlackMageSetting.Instance.提前黑魔纹;
            if (ImGui.Checkbox("提前黑魔纹", ref 倒计时黑魔纹))
            {
                if (倒计时黑魔纹) BlackMageSetting.Instance.提前黑魔纹 = true;
                else BlackMageSetting.Instance.提前黑魔纹 = false;
                BlackMageSetting.Instance.Save();
            }
        }

        if (ImGui.CollapsingHeader("循环设置"))
        {
            ImGui.Text("以下所有设置都能在时间轴中控制，非轴作者可以不用在意这些设置，不改也已经是优秀循环了");
            
            if (ImGui.Checkbox("关闭起手", ref BlackMageSetting.Instance.起手))
            {
                BlackMageSetting.Instance.Save();
            }
            ImGui.SameLine();
            if(ImGui.Checkbox("FATE/CE模式", ref BlackMageSetting.Instance.FATE模式))
            {
                BlackMageSetting.Instance.Save();
            }
            ImGui.Text("boss上天等待时间（毫秒）：");
            if (ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.Text("当目标丢失时，等待此时间后再进行转圈");
                ImGui.EndTooltip();
            }
            int 无目标等待时间 = BlackMageSetting.Instance.无目标等待时间;
            if (ImGui.InputInt("##无目标等待时间", ref 无目标等待时间, 1000, 5000))
            {
                BlackMageSetting.Instance.无目标等待时间 = 无目标等待时间;
                BlackMageSetting.Instance.Save();
            }
            ImGui.Text("以下设置每次战斗都会重置:");
            ImGui.Checkbox("留下所有三连用于走位，只针对100级循环", ref BattleData.Instance.三连走位);
            ImGui.Checkbox("低等级aoe循环中会使用火二进火", ref BattleData.Instance.aoe火二);
            ImGui.Checkbox("使用特供循环，注意可能会被绿玩出警", ref BattleData.Instance.特供循环);
            ImGui.Checkbox("压缩冰悖论，即在冰循环中不会主动打悖论，移动时正常悖论", ref BattleData.Instance.压缩冰悖论);
            ImGui.Checkbox("压缩火悖论，同上", ref BattleData.Instance.压缩火悖论);
            ImGui.Checkbox("70级循环中使用核爆收尾", ref BattleData.Instance.核爆收尾);
        }

        if (ImGui.CollapsingHeader("以太步窗口设置"))
        {
            ImGui.Text("以太步窗口开关：");
            bool 以太步窗口开关 = BlackMageSetting.Instance.以太步窗口显示;
            if (ImGui.Checkbox("##以太步窗口开关", ref 以太步窗口开关))
            {
                BlackMageSetting.Instance.以太步窗口显示 = 以太步窗口开关;
                BlackMageSetting.Instance.Save();
            }

            ImGui.Text("以太步图标大小：");
            int 以太步图标大小 = BlackMageSetting.Instance.以太步IconSize;
            if (ImGui.InputInt("##以太步图标大小", ref 以太步图标大小, 1, 50))
            {
                BlackMageSetting.Instance.以太步IconSize = 以太步图标大小;
                BlackMageSetting.Instance.Save();
            }
        }

        if (ImGui.CollapsingHeader("TTK设置"))
        {
            ImGui.Text("设置TTK阈值（毫秒）：");
            if (ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.Text("当目标剩余击杀时间低于此值时，将停止使用彩绘技能");
                ImGui.EndTooltip();
            }

            int currentTTK = BlackMageSetting.Instance.TTK阈值;
            if (ImGui.InputInt("##TTK阈值", ref currentTTK, 1000, 5000))
            {
                BlackMageSetting.Instance.TTK阈值 = currentTTK;
                BlackMageSetting.Instance.Save();
            }
        }


        ImGuiHelper.Separator();
    }
}