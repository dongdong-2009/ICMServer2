// NativeMethods.h

#pragma once
#include <windows.h> 
#include <vcclr.h>
#include <AtlBase.h>

using namespace System;
using namespace System::Runtime::InteropServices;

namespace ICMServer {

	public ref class NativeMethods
	{
    public: 
        static int WritePrivateProfileString(String^ lpAppName, String^ lpKeyName, String^ lpString, String^ lpFileName)
        {
            pin_ptr<const wchar_t> appname = PtrToStringChars(lpAppName);
            pin_ptr<const wchar_t> keyname = PtrToStringChars(lpKeyName);
            pin_ptr<const wchar_t> string  = PtrToStringChars(lpString);
            pin_ptr<const wchar_t> filename = PtrToStringChars(lpFileName);
            return ::WritePrivateProfileString(CW2CT(appname), CW2CT(keyname), CW2CT(string), CW2CT(filename));
        }

        static int GetPrivateProfileString(String^ lpAppName, String^ lpKeyName, String^ lpDefault, [Out]String^% lpReturnedString, int nSize, String^ lpFileName)
        {
            int result = 0;
            pin_ptr<const wchar_t> appname = PtrToStringChars(lpAppName);
            pin_ptr<const wchar_t> keyname = PtrToStringChars(lpKeyName);
            pin_ptr<const wchar_t> default = PtrToStringChars(lpDefault);
            pin_ptr<const wchar_t> filename = PtrToStringChars(lpFileName);
            LPTSTR returnstring = new TCHAR[nSize];
            result = ::GetPrivateProfileString(CW2CT(appname), CW2CT(keyname), CW2CT(default), returnstring, nSize, CW2CT(filename));
            lpReturnedString = gcnew String(returnstring);
            delete returnstring;
            return result;
        }
	};
}
