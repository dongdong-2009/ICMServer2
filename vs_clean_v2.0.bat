::@echo off
::search and delete unused files...

rmdir %cd%\.vs /s/q
rmdir %cd%\bin\Debug\data /s/q
rmdir %cd%\bin\Release\data /s/q
rmdir %cd%\DeviceAddressControlLib\bin /s/q
rmdir %cd%\DeviceAddressControlLib\obj /s/q
rmdir %cd%\Heartbeat\bin /s/q
rmdir %cd%\Heartbeat\obj /s/q
rmdir %cd%\HttpClient\bin /s/q
rmdir %cd%\HttpClient\obj /s/q
rmdir %cd%\HttpServer\bin /s/q
rmdir %cd%\HttpServer\obj /s/q
rmdir %cd%\ICMServer\Debug /s/q
rmdir %cd%\ICMServer\Release /s/q
rmdir %cd%\ICMServerLib\obj /s/q
rmdir %cd%\IniFile\bin /s/q
rmdir %cd%\IniFile\obj /s/q
rmdir %cd%\IPAddressControlLib\obj /s/q
rmdir %cd%\ipch /s/q
rmdir %cd%\Models\obj /s/q
rmdir %cd%\mooftpserv\bin\bin /s/q
rmdir %cd%\mooftpserv\bin\obj /s/q
rmdir %cd%\mooftpserv\lib\bin /s/q
rmdir %cd%\mooftpserv\lib\obj /s/q
rmdir %cd%\mooftpserv\obj /s/q
rmdir %cd%\NativeMethods\Debug /s/q
rmdir %cd%\NativeMethods\Release /s/q
rmdir %cd%\packages /s/q
rmdir %cd%\WinForms\obj /s/q
rmdir %cd%\Wpf\bin /s/q
rmdir %cd%\Wpf\obj /s/q
del *.aps /s/q/a
del *.opensdf /s/q/a
del *.sdf /s/q/a
del *.suo /s/q/a
del *.user /s/q/a
del *.pdb /s/q/a
del *.ilk /s/q/a

::rem finish
::pause