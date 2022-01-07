@echo off

set SOLUTIONPATH="C:\Users\tera1\Desktop\test\serviceJikken1\serviceJikken1.sln"

set SERVICENAME="SvcName"
set SERVICEDISPNAME=%SERVICENAME%
set BINPATH=C:\git\Cpp\UserProcessFromServiceJikken\x64\Debug\serviceJikken1.exe
set DISCRIPTION="Svc Description..."

rem call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat"

echo -サービスのプロセスを無理やり終了させる
rem taskkill /f /im %BINPATH%
taskkill /f /im serviceJikken1.exe
