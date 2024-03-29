﻿namespace SuperMemoAssistant.Plugins.EasySymbols
{
  public static class GeneralUtils
  {
    /// <summary>
    /// Determine whether the object is null
    /// </summary>
    /// <param name="obj"></param>
    public static bool IsNull(this object obj)
    {
      return obj == null;
    }

    /// <summary>
    /// Determine whether the string is null or empty
    /// </summary>
    /// <param name="str"></param>
    public static bool IsNullOrEmpty(this string str)
    {
      return string.IsNullOrEmpty(str);
    }
  }
}
