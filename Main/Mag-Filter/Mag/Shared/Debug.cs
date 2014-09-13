namespace Mag.Shared
{
  using Decal.Adapter;
  using System;
  using System.Globalization;
  using System.IO;
  using System.Runtime.InteropServices;

  internal static class Debug
  {
    private static string _errorLogPath;
    private static string _pluginName;

    public static void Init(string errorLogPath, string pluginName)
    {
      _errorLogPath = errorLogPath;
      _pluginName = pluginName;
    }

    public static void LogException(Exception ex, string note = null)
    {
      try
      {
        if (note != null)
        {
          CoreManager.Current.Actions.AddChatText("<{" + _pluginName + "}>: Exception caught: " + ex.Message + Environment.NewLine + ex.Source + Environment.NewLine + ex.StackTrace + Environment.NewLine + "Note: " + note, 5);
        }
        else
        {
          CoreManager.Current.Actions.AddChatText("<{" + _pluginName + "}>: Exception caught: " + ex.Message + Environment.NewLine + ex.Source + Environment.NewLine + ex.StackTrace, 5);
        }
        if (!string.IsNullOrEmpty(_errorLogPath))
        {
          FileInfo info = new FileInfo(_errorLogPath);
          bool append = !info.Exists || (info.Length <= 0x100000L);
          using (StreamWriter writer = new StreamWriter(info.FullName, append))
          {
            writer.WriteLine("============================================================================");
            writer.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            writer.WriteLine(ex);
            if (note != null)
            {
              writer.WriteLine("Note: " + note);
            }
            writer.WriteLine("============================================================================");
            writer.WriteLine("");
            writer.Close();
          }
        }
      }
      catch
      {
      }
    }

    public static void LogText(string text)
    {
      try
      {
        CoreManager.Current.Actions.AddChatText("<{" + _pluginName + "}>: Log Text: " + text, 5);
        if (!string.IsNullOrEmpty(_errorLogPath))
        {
          FileInfo info = new FileInfo(_errorLogPath);
          bool append = !info.Exists || (info.Length <= 0x100000L);
          using (StreamWriter writer = new StreamWriter(info.FullName, append))
          {
            writer.WriteLine(DateTime.Now + ": " + text);
            writer.Close();
          }
        }
      }
      catch (Exception exception)
      {
        LogException(exception, null);
      }
    }

    public static void WriteToChat(string message, int color = 5, int target = 1)
    {
      try
      {
        CoreManager.Current.Actions.AddChatText("<{" + _pluginName + "}>: " + message, color, target);
      }
      catch (Exception exception)
      {
        LogException(exception, null);
      }
    }
  }
}

