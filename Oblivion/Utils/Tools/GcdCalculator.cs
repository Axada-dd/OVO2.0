using FFXIVClientStructs.FFXIV.Client.Game.UI;
namespace Oblivion.Utils.Tools;

public static class GcdCalculator
{
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
    
    
    public static void CalculateGcd(bool 黑魔纹加速, out double 短读条, out double 长读条)
    {

        var speed = GetSpellSpeed();
        if (!LevelModifiers.LevelTable.TryGetValue( Core.Me.Level, out var levelModifier))
            throw new ArgumentException("Invalid level");
        var sub = levelModifier.Sub;
        var div = levelModifier.Div;
        var modifier = 黑魔纹加速 ? 15 : 0;

        // GCD计算核心逻辑
        int SpeedCalc(double baseGcd, int spd) =>
            (int)Math.Floor(Math.Floor((1000.0 + Math.Ceiling(130.0 * (sub - spd) / div)) * baseGcd / 100d) * (100d - modifier) / 1000d);

        int TierCalc(double currentValue, int baseGcd) =>
            -(int)Math.Floor(Math.Floor(Math.Ceiling(currentValue * 100d * 1000d / (100d - modifier) - 0.01d) * 100 / baseGcd - 1000.01d) * div / 130d - sub);

        // 计算短读条GCD（默认2.5秒基础GCD）
        短读条 = SpeedCalc(250, speed) / 100d;


        // 计算长读条GCD（如火三）
        长读条 = SpeedCalc(350, speed) / 100d;

    }
}