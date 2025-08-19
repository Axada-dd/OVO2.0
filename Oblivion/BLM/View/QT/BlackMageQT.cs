using Oblivion.BLM.QtUI;

namespace Oblivion.BLM.View.QT;

public static class BlackMageQT
{
    public static bool GetQt(string qtName)
    {
        return baseUI.QT.GetQt(qtName);
    }

    public static bool ReverseQt(string qtName)
    {
        return baseUI.QT.ReverseQt(qtName);
    }

    public static void SetQt(string qtName, bool qtValue)
    {
        baseUI.QT.SetQt(qtName, qtValue);
    }
    public static void Reset()
    {
        baseUI.QT.SetQt(QTkey.黑魔纹, true);
        baseUI.QT.SetQt(QTkey.墨泉, true);
        baseUI.QT.SetQt(QTkey.Dot, true);
        baseUI.QT.SetQt(QTkey.Aoe, true);
        baseUI.QT.SetQt(QTkey.通晓, true);
        baseUI.QT.SetQt(QTkey.智能aoe目标, false);
        baseUI.QT.SetQt(QTkey.倾泻资源, false);
        baseUI.QT.SetQt(QTkey.Boss上天,true);
        baseUI.QT.SetQt(QTkey.TTK,false);
    }

    public static void NewDefault(string qtName, bool newDefault)
    {
        baseUI.QT.NewDefault(qtName, newDefault);
    }

    public static void SetDefaultFromNow()
    {
        baseUI.QT.SetDefaultFromNow();
    }

    public static string[] GetQtArray()
    {
        return baseUI.QT.GetQtArray();
    }
    public static void CreateQt()
    {
        baseUI.QT.AddQt(QTkey.通晓,true,"通晓系技能");

        baseUI.QT.AddQt(QTkey.爆发药, false);
        baseUI.QT.AddQt(QTkey.黑魔纹, true);
        baseUI.QT.AddQt(QTkey.墨泉, true);
        baseUI.QT.AddQt(QTkey.Dot, true);
        baseUI.QT.AddQt(QTkey.智能aoe目标, false);
        baseUI.QT.AddQt(QTkey.Aoe, true,"开关所有aoe");
        baseUI.QT.AddQt(QTkey.倾泻资源, false,"清空通晓");
        baseUI.QT.AddQt(QTkey.Boss上天, true,"如果boss上天时间>10秒，请开启此选项,随开随关");
        baseUI.QT.AddQt(QTkey.TTK,false);
        baseUI.QT.AddQt(QTkey.起手不三连, false,"只对普通循环有效");
        //WhiteMageSettings.Instance.JobViewSave.QtUnVisibleList.Clear();
        // WhiteMageSettings.Instance.JobViewSave.QtUnVisibleList.Add("4人本再生");
    }
}