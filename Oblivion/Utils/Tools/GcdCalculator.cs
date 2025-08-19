using Dalamud.Game.ClientState.Objects.Types;
using ECommons.GameFunctions;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
namespace Oblivion.Utils.Tools;

public static class GcdCalculator
{
    private static double GcdModifier()
    {
        int level = Core.Me.Level;

        double num = level < 20 ? (level >= 1 ? 0.95 : 1.0) : (level >= 76 ? 0.8 : (level >= 40 ? 0.85 : 0.9));

        return num;
    }

    public static int CalculateSyncedGcd()
    {
        double num = 2500.0 * GcdCalculator.GcdModifier();
        byte level = Core.Me.Level;
        LevelModifier levelModifier;
        if (!LevelModifiers.LevelTable.TryGetValue( level, out levelModifier))
            throw new ArgumentException("Invalid level");
        int sub = levelModifier.Sub;
        int div = levelModifier.Div;
        return (int) Math.Floor(Math.Floor((1000.0 + Math.Ceiling(130.0 *  (sub - GcdCalculator.GetSpellSpeed()) /  div)) * num / 100.0) * 100.0 / 1000.0);
    }

    public static int GetSpell()
    {
        return GetSpellSpeed();
    }
    private static unsafe int GetSpellSpeed()
    {
        PlayerState playerState;
        try
        {
            playerState = UIState.Instance()->PlayerState;
        }
        catch (Exception ex)
        {
            return 0;
        }
        //((PlayerState) ref playerState).Attributes.Length > 45 ? ((PlayerState) ref playerState).Attributes[45] : 0
        return playerState.Attributes.Length > 46 ? playerState.Attributes[46] : 0;
    }
}