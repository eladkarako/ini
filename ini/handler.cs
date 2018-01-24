using System;
using System.Linq;
using System.Runtime.InteropServices;
//NOTE: SAMPLE.INI FILE SHOULD BE UTF-8 WITHOUT BOM (WITHOUT SIGNATURE).

namespace ini
{
  public static class Handler
  {
    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode)]
    public static extern int GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, string lpReturnedString, int nSize, string lpFileName);

    [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringW", CharSet = CharSet.Unicode)]
    public static extern int WritePrivateProfileString(string lpApplicationName, int lpKeyName, int lpString, string lpFileName);

    //-----------------------------------------------------------------------------------------------

    public static string[] GetPrivateProfileStringWrapper(
      string lpApplicationName,
      string lpKeyName,
      string lpDefault,
      string lpFileName)
    {
      int nSize = 1024;
      string lpReturnedString = new string(' ', nSize);
      GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName);


      int limit = (lpReturnedString.IndexOf('\0') == lpReturnedString.LastIndexOf('\0')) ? lpReturnedString.LastIndexOf('\0') : lpReturnedString.LastIndexOf('\0') - 1; //single string (\0 at the end) or multi-string-separated-with-\0 (\0 is a separator and we need to remove "last empty-cell" too).
      lpReturnedString = lpReturnedString.Substring(0, limit);

      return lpReturnedString.Split('\0');
    }

    public static string[] GetCategories(string iniFile)
    {
      return GetPrivateProfileStringWrapper(null, null, null, iniFile);
    }

    public static string[] GetKeys(string iniFile, string category)
    {
      return GetPrivateProfileStringWrapper(category, null, null, iniFile);
    }

    public static string GetValue(string iniFile, string category, string key, string defaultValue)
    {
      return GetPrivateProfileStringWrapper(category, key, defaultValue, iniFile)[0];
    }
  }
}
