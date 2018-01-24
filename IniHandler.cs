using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;

/*
 * GetPrivateProfileSectionNamesW
 * 
 *   GetPrivateProfileSectionW
 * WritePrivateProfileSectionW
 * 
 *    GetPrivateProfileStringW
 *  WritePrivateProfileStringW
 * 
 * GetPrivateProfileIntW
 * 
*/

namespace iniRun
{

  public static class IniHandler
  {
    //KERNEL32
    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNamesW", CharSet=CharSet.Unicode)]
    public  static extern int GetPrivateProfileSectionNames(
       string lpszReturnBuffer,
       int nSize,
       string lpFileName);

    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionW", CharSet=CharSet.Unicode)]
    public static extern int GetPrivateProfileSection(
       string lpAppName,
       string lpReturnedString,
       int nSize,
       string lpFileName);

    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileIntW", CharSet=CharSet.Unicode)]
    public static extern int GetPrivateProfileInt(
       string lpApplicationName,
       string lpKeyName,
       int nDefault,
       string lpFileName);

    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringW", CharSet=CharSet.Unicode)]
    public static extern int GetPrivateProfileString(
       string lpApplicationName,
       string lpKeyName,
       string lpDefault,
       string lpReturnedString,
       int nSize,
       string lpFileName);

    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStructW", CharSet=CharSet.Unicode)]
    public static extern int GetPrivateProfileStruct(
       string lpszSection,
       string lpszKey,
       int lpStruct,
       int uSizeStruct,
       string szFile);

    [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileSectionW", CharSet=CharSet.Unicode)]
    public static extern int WritePrivateProfileSection(
       string lpAppName,
       string lpString,
       string lpFileName);

    [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStructW", CharSet=CharSet.Unicode)]
    public static extern int WritePrivateProfileStruct(
       string lpszSection,
       string lpszKey,
       int lpStruct,
       int uSizeStruct,
       string szFile);

    [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringW", CharSet=CharSet.Unicode)]
    public static extern int WritePrivateProfileString(
       string lpApplicationName,
       int lpKeyName,
       int lpString,
       string lpFileName);


    //-----------------------------------------------------------------------------------------------

    public static List<string> GetPrivateProfileStringWrapper(
       string lpApplicationName,
       string lpKeyName,
       string lpDefault,
     //string lpReturnedString,
     //int nSize,
       string lpFileName)
    {
      const int nSize = 1024; //65536;
      string lpReturnedString = new string(' ', nSize);
   
      GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName);

      List<string> result = new List<string>(lpReturnedString.Split('\0'));
      if(result.Count > 2) result.RemoveRange(result.Count - 2, 2);
      
      return result;
    }

    public static string[] GetCategories(string iniFile)    {
      return GetPrivateProfileStringWrapper(null, null, null, iniFile).ToArray();
    }

    public static string[] GetKeys(string iniFile, string category)    {
      return GetPrivateProfileStringWrapper(category, null, null, iniFile).ToArray();
    }

    public static string GetValue(string iniFile, string category, string key, string defaultValue)    {
      return GetPrivateProfileStringWrapper(category, key, defaultValue, iniFile).ToArray()[0];
    }

  }
}
