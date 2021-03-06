<h1><img width="48" height="48" alt="" src="ini/icon.ico"/> INI</h1>
<h2>[C#] INI Read/Write,<br/>With Unicode Support.</h2>
<h3>Small, <br/>Simple, <br/>Fast.</h3>
<h2>No Dependencies.</h2>
<hr/>
<h4>Using Native Windows API from kernel32.dll: GetPrivateProfileStringW, WritePrivateProfileStringW</h4>

<br/>
<hr/>

<h3>Note <sup><em>(to self?)</em></sup></h3>
content should be Windows-EOL (<code>\r\n</code>), <br/>
content must be UTF-8 without BOM, so <br/>
make sure to remove those characters (by hex.-value) from the content: <br/>
(just after reading the raw content/ just before writing to file):
<ol>
<li>
<a href="https://en.wikipedia.org/wiki/Byte_order_mark">UTF-8 BOM</a> <code>0xEF, 0xBB, 0xBF</code>
</li>
<li>
UTF-16 big-endian BOM <code>0xFE,0xFF</code>
</li>
<li>
UTF-16 little-endian BOM <code>0xFF,0xFE</code>
</li>
<li>
UTF-32 big-endian BOM <code>0x00,0x00,0xFE,0xFF</code>
</li>
<li>
UTF-32 little-endian <code>0xFF,0xFE,0x00,0x00</code>
</li>
<li>
UTF-7 BOM <code>0x2B,0x2F,0x76,0x38</code>, <code>0x2B,0x2F,0x76,0x39</code>, <code>0x2B,0x2F,0x76,0x2B</code>, <code>0x2B,0x2F,0x76,0x2F</code>, <code>0x2B,0x2F,0x76,0x38,0x2D</code>
</li>
<li>
UTF-1 BOM <code>0xF7,0x64,0x4C</code>
</li>
<li>
UTF-EBCDIC BOM <code>0xDD,0x73,0x66,0x73</code>
</li>
<li>
SCSU BOM <code>0x0E,0xFE,0xFF</code>
</li>
<li>
BOCU-1 BOM <code>0xFB,0xEE,0x28</code>
</li>
<li>
GB-18030 BOM <code>0x84,0x31,0x95,0x33</code> 
</li>
</ol>

<hr/>

The purpose of this project?
A "fork" of <a href="https://github.com/eladkarako/iniRun/">github.com/eladkarako/iniRun</a>,<br/>
which uses C# instead of VB6 <sup><em>^_^</em></sup>, with Unicode support, using minimal code, basically relaying on Windows API.

<hr/>
Some Notes:


<ul>
<li>The Windows-API currently used:

```c#
[DllImport("kernel32.dll", EntryPoint="GetPrivateProfileStringW", CharSet=CharSet.Unicode)]
static extern int GetPrivateProfileString ( 
	 string lpApplicationName,
	 string lpKeyName,
	 string lpDefault,
	 string lpReturnedString,
	 int nSize,
	 string lpFileName);

[DllImport("kernel32.dll", EntryPoint="WritePrivateProfileStringW", CharSet=CharSet.Unicode)]
static extern int WritePrivateProfileString ( 
	 string lpApplicationName,
	 int lpKeyName,
	 int lpString,
	 string lpFileName);
```

</li>
<li>
Make sure the files used (text, ini) are encoded as either ASCII or UTF-8 without BOM/signature.
</li>
<li>
String storage at <code>lpReturnedString</code>,<br/>
handling multi-strings: <code>string1\0string2\0.....stringN\0</code><br/>
single-string: <code>string1\0</code>.
</li>
</ul>

<hr/>

Notes about <code>GetPrivateProfileStringW</code>:

When using this <code>sample.ini</code>:

```ini
[foods]
pizza=margherita
bread=full grain

[drinks]
cola=zero
7up=diet
```

<ul>
<li>Getting the "categories" by providing <code>null</code> values to all:

```c#
GetPrivateProfileStringW(null, null, null, 1024, @".\sample.ini")
```

Will return a <code>lpReturnedString</code> with <code>foods<sub>\0</sub>drinks<sub>\0</sub></code>.
</li>
<li>Getting all of the "keys", of a single "category" (by providing a category and <code>null</code> values to the rest):

```c#
GetPrivateProfileStringW("foods", null, null, 1024, @".\sample.ini")
```

Will return a <code>lpReturnedString</code> with <code>pizza<sub>\0</sub>bread<sub>\0</sub></code>.
</li>

<li>Getting the value of a key, by providing both category and key (you can provide default/fallback):

```c#
GetPrivateProfileStringW("foods", "pizza", "vegan", 1024, @".\sample.ini")
```

Will return a <code>lpReturnedString</code> with <code>margherita<sub>\0</sub></code>.
</li>
</ul>

<hr/>

You can try other variations, keep in mind that the return string (<code>lpReturnedString</code>) acts kind-of-funny
on single/multiple strings. Using the function itself requires you to initialize <code>lpReturnedString</code>,
and trim/prase it again after the call to the function, keeping just the valid textual-content.

Here is how I do it, <code>\0</code>-trimming is quite-simple too.

```c#
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
```

Checking if the returned-string has a single string or multiple string is done by checking how much <code>\0</code> are there.. 
or more simple- if there are at-least two <code>\0</code> characters in different positions.

<br/>