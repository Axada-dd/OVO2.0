using System.Numerics;
using AEAssist.Define.HotKey;

namespace Oblivion.Utils.JobView;
public class HotkeyConfig
{
    public string Name;
    public Keys Keys;
    public ModifierKey ModifierKey;
}
public class JobViewSave
{
    /// Qt窗口背景透明度
    public float QtWindowBgAlpha = QtStyle.DefaultQtWindowBgAlpha;
    
    /// 当前主题预设
    public ModernTheme.ThemePreset CurrentTheme = ModernTheme.ThemePreset.深色模式;

    public Vector2 QtButtonSize = QtStyle.DefaultButtonSize;

    public Vector2 QtHotkeySize = QtStyle.DefaultHotkeySize;


    /// 隐藏的qt列表
    public List<string> QtUnVisibleList = [];
    /// 窗口拖动
    public bool LockWindow;

    ///QT按钮一行有几个
    public int QtLineCount = 3;

    public Dictionary<string, HotkeyConfig> HotkeyConfig = new();

    public Dictionary<string, HotkeyConfig> QtHotkeyConfig = new(); 


    /// 隐藏的hotkey列表
    public List<string> HotkeyUnVisibleList = [];

    ///hotkey按钮一行有几个
    public int HotkeyLineCount = 4;

    public bool AutoReset = true;

    public bool ShowQT = true;
    public bool ShowHotkey = true;
    
    /// 主窗口小窗口状态
    public bool SmallWindow = false;
    
    /// 主窗口原始大小
    public Vector2 OriginalWindowSize = new Vector2(400, 300);
    
    /// QT窗口位置
    public Vector2 QtWindowPos = new Vector2(100, 100);
    
    /// 热键窗口位置
    public Vector2 HotkeyWindowPos = new Vector2(200, 200);
    
    /// 热键窗口是否已设置过位置（用于首次启动时使用默认位置）
    public bool HotkeyWindowPosSet = false;
}