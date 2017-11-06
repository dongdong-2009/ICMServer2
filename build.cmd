if exist "C:\Program Files (x86)\Microsoft Visual Studio 13.0\VC\bin\vcvars32.bat" (
    call "C:\Program Files (x86)\Microsoft Visual Studio 13.0\VC\bin\vcvars32.bat"
)
if exist "C:\Program Files\Microsoft Visual Studio 13.0\VC\bin\vcvars32.bat" (
    call "C:\Program Files\Microsoft Visual Studio 13.0\VC\bin\vcvars32.bat"
)

if exist "C:\Program Files (x86)\Microsoft Visual Studio 12.0\VC\bin\vcvars32.bat" (
    call "C:\Program Files (x86)\Microsoft Visual Studio 12.0\VC\bin\vcvars32.bat"
)
if exist "C:\Program Files\Microsoft Visual Studio 12.0\VC\bin\vcvars32.bat" (
    call "C:\Program Files\Microsoft Visual Studio 12.0\VC\bin\vcvars32.bat"
)

if exist "C:\Program Files (x86)\NSIS\makensis.exe" (
    set NSIS="C:\Program Files (x86)\NSIS\makensis.exe"
)
if exist "C:\Program Files\NSIS\makensis.exe" (
    set NSIS="C:\Program Files\NSIS\makensis.exe"
)

@echo on
set WORKDIR=%cd%
msbuild "%WORKDIR%\ICMServer.sln" /p:Configuration=Debug
msbuild "%WORKDIR%\ICMServer.sln" /p:Configuration=Release
::copy "%WORKDIR%\bin\Release\*.exe" "%WORKDIR%\..\Control_Server_package\"
::copy "%WORKDIR%\bin\Release\*.manifest" "%WORKDIR%\..\Control_Server_package\"
::copy "%WORKDIR%\bin\Release\*.dll"  "%WORKDIR%\..\Control_Server_package\dll\"
::copy "%WORKDIR%\bin\Release\*.cfg" "%WORKDIR%\..\Control_Server_package\"
::copy "%WORKDIR%\bin\Release\zh-CN\*.dll" "%WORKDIR%\..\Control_Server_package\zh-CN\"
call %NSIS% "%cd%\ICMSInstall.nsi"
pause