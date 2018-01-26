<h1><img width="48" height="48" alt="" src="ini/icon.ico"/> INI - &nbsp; &nbsp; <a href="https://paypal.me/e1adkarak0" ok><img src="https://www.paypalobjects.com/webstatic/mktg/Logo/pp-logo-100px.png" alt="PayPal Donation" ok></a></h1>
<h2>[C#] INI Read/Write,<br/>With Unicode Support.</h2>
<h3>Small, <br/>Simple, <br/>Fast.</h3>
<h2>No Dependencies.</h2>
<hr/>
<h4>Using Native Windows API from kernel32.dll: GetPrivateProfileStringW, WritePrivateProfileStringW</h4>

<br/>
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
