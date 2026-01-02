namespace Arunoki.Collections.Utilities
{
  internal static class Utils
  {
    public static bool IsEditor ()
    {
#if UNITY_EDITOR
      return true;
#else
      return false;
#endif
    }

    public static bool IsDebug ()
    {
#if DEBUG || UNITY_EDITOR
      return true;
#else
      return false;
#endif
    }
  }
}