using AEAssist.IO;
using Oblivion.Utils.JobView;
using Oblivion.Utils.JobView.HotKey;

namespace Oblivion.BLM;

public class BlackMageSetting
{
    public static BlackMageSetting Instance;
    
    public bool 调试窗口 = false;
    
    public bool 锁定以太步窗口 = false;
    public int 以太步IconSize = 47;
    public bool 以太步窗口显示 = true;
    public bool TimeLinesDebug = false;

    public double 起手预读时间 = 3.5;
    public bool 核爆起手 = false;
    public bool 标准57 = false;
    public bool 开挂循环 = false;

    public bool Autotarget = false;
    public int AutoTargetMode = 0;
    public bool 提前黑魔纹 = false;
    public int TTK阈值 = 12000; 
    public bool 起手 = false;
    public bool FATE模式 = false;
    public int 无目标等待时间 = 1000;
    
    
    public Dictionary<string, bool> QtStates { get; set; } = new Dictionary<string, bool>();

    public List<string> QtUnVisibleList = [];

    public void SaveQtStates(JobViewWindow jobViewWindow)
    {
        // 获取所有 Qt 控件的名称列表
        string[] qtArray = jobViewWindow.GetQtArray();

        // 遍历所有控件名称，获取对应的状态，并保存到字典中
        foreach (var qtName in qtArray)
        {
            bool qtState = jobViewWindow.GetQt(qtName);
            QtStates[qtName] = qtState;
        }



        // 保存当前设置到 JSON 文件
        Save();
    }
    
    public void LoadQtStates(JobViewWindow jobViewWindow)
    {
        // 加载保存的所有Qt状态
        foreach (var qtState in QtStates)
        {
            jobViewWindow.SetQt(qtState.Key, qtState.Value);
        }


        // 根据 QtUnVisibleList 设置对应的QT为不可见
        foreach (var hiddenQt in Instance.JobViewSave.QtUnVisibleList)
        {
            QtUnVisibleList.Add(hiddenQt);
        }
    }
    public Dictionary<string, HotKetSpell> HotKey设置 = new()
    {
        {
            "黑魔纹", new HotKetSpell("黑魔纹", Skill.黑魔纹, 1)
        },
        {
            "三连咏唱", new HotKetSpell("三连咏唱", Skill.三连, 1)
        },
        {
            "沉稳咏唱", new HotKetSpell("沉稳咏唱", Skill.沉稳, 1)
        },
        {
            "混乱", new HotKetSpell("混乱",Skill.混乱,2)
        },
        {
            "魔罩", new HotKetSpell("魔罩",Skill.魔罩,1)
        }
    };
    
    
    public JobViewSave JobViewSave { get; set; } = new()
    {
        AutoReset = true, 
        ShowQT = true, 
        ShowHotkey = true,
        CurrentTheme = ModernTheme.ThemePreset.深色模式  // 确保有默认值
    };
    
    private static string path;
    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, nameof(BlackMageSetting) + ".json");
        if (!File.Exists(path))
        {
            Instance = new BlackMageSetting();
            Instance.Save();
            return;
        }
        try
        {
            Instance = JsonHelper.FromJson<BlackMageSetting>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new BlackMageSetting();
            LogHelper.Error(e.ToString());
        }
    }
    
    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, JsonHelper.ToJson(this));
    }
}