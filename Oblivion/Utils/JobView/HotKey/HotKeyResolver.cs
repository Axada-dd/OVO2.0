using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Textures.TextureWraps;
using ImGuiNET;

namespace Oblivion.Utils.JobView.HotKey;

public class HotKeyResolver(Spell p, HotKeyTarget hotKeyTarget) : IHotkeyResolver
{
    private HotKeyTarget T = hotKeyTarget;
    private IBattleChara? characterAgent = hotKeyTarget.CharacterAgent;
    private SpellTargetType? spellTargetType = hotKeyTarget.SpellTargetType;
    private Func<IBattleChara?>? targetFunc = hotKeyTarget.Func;

    public void Draw(Vector2 size)
    {
        Vector2 size1 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(p.Id, out IDalamudTextureWrap textureWrap))
            return;
        if (textureWrap != null) ImGui.Image(textureWrap.ImGuiHandle, size1);
    }

    public void DrawExternal(Vector2 size, bool isActive)
    {
        if (characterAgent != null)
        {
            SpellHelper.DrawSpellInfo(new Spell(p.Id, characterAgent), size, isActive,
                myDrawSpellDelegate);
        }
        else if (spellTargetType != null)
        {
            SpellHelper.DrawSpellInfo(new Spell(p.Id, (SpellTargetType)spellTargetType), size, isActive,
                myDrawSpellDelegate);
        }
        else
        {
            if (targetFunc != null)
                SpellHelper.DrawSpellInfo(new Spell(p.Id, targetFunc), size, isActive, myDrawSpellDelegate);
        }
    }

    public int Check()
    {
        return 1;
    }

    private void myDrawSpellDelegate(Spell s, Vector2 hotKeySize, bool isActive)
    {
        if (s.IsAbility())
        {
            if (s.IsReadyWithCanCast())
            {
                ImGui.SetCursorPos(new Vector2(0, 0));
                if (isActive)
                {
                    //激活状态
                    if (Core.Resolve<MemApiIcon>().TryGetTexture(@"Resources\Spells\Icon\activeaction.png",
                            out IDalamudTextureWrap? textureWrap_active))
                        if (textureWrap_active != null)
                            ImGui.Image(textureWrap_active.ImGuiHandle, hotKeySize);
                }
                else
                {
                    //常规状态
                    if (Core.Resolve<MemApiIcon>().TryGetTexture(@"Resources\Spells\Icon\iconframe.png",
                            out IDalamudTextureWrap? textureWrap_normal))
                        if (textureWrap_normal != null)
                            ImGui.Image(textureWrap_normal.ImGuiHandle, hotKeySize);
                }
            }
            else
            {
                //技能不可使用
                //变黑
                ImGui.SetCursorPos(new Vector2(0, 0));
                if (Core.Resolve<MemApiIcon>().TryGetTexture(@"Resources\Spells\Icon\icona_frame_disabled.png",
                        out IDalamudTextureWrap? textureWrap_black))
                    if (textureWrap_black != null)
                        ImGui.Image(textureWrap_black.ImGuiHandle, hotKeySize);
            }
        }
        else
        {



            //技能可使用
            ImGui.SetCursorPos(new Vector2(0, 0));
            if (isActive)
            {
                //激活状态
                if (Core.Resolve<MemApiIcon>().TryGetTexture(@"Resources\Spells\Icon\activeaction.png",
                        out IDalamudTextureWrap? textureWrap_active))
                    if (textureWrap_active != null)
                        ImGui.Image(textureWrap_active.ImGuiHandle, hotKeySize);
            }
            else
            {
                //常规状态
                if (Core.Resolve<MemApiIcon>().TryGetTexture(@"Resources\Spells\Icon\iconframe.png",
                        out IDalamudTextureWrap? textureWrap_normal))
                    if (textureWrap_normal != null)
                        ImGui.Image(textureWrap_normal.ImGuiHandle, hotKeySize);
            }
        }
    }
    public void Run()
    {
        
        
        if (spellTargetType != null)
        {
            if (AI.Instance.BattleData.NextSlot != null)
            {
                AI.Instance.BattleData.NextSlot.Add(new Spell(p.Id, (SpellTargetType)spellTargetType));
            }
            else
            {
                AI.Instance.BattleData.NextSlot =
                    new Slot().Add(new Spell(p.Id, (SpellTargetType)spellTargetType));
            }
        }
        else
        {
            IBattleChara target = Core.Me;
            if (characterAgent != null)
            {
                target = characterAgent;
            }
            else
            {
                if (targetFunc != null)
                    target = targetFunc();
            }
            
            if (target == null) return;
            
            if (AI.Instance.BattleData.NextSlot != null)
            {
                AI.Instance.BattleData.NextSlot.Add(new Spell(p.Id, target));
            }
            else
            {
                AI.Instance.BattleData.NextSlot = new Slot().Add(new Spell(p.Id, target));
            }

        }
    }
}