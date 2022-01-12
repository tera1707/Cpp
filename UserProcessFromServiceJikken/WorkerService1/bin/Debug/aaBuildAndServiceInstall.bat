@echo off

set SOLUTIONPATH="..\..\..\serviceJikken1.sln"

set SERVICENAME="CsServiceJikken"
set SERVICEDISPNAME=%SERVICENAME%
set BINPATH=%~dp0\WorkerService1.exe
set DISCRIPTION="CsSvc Description..."

call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

echo -サービスのプロセスを無理やり終了させる
taskkill /f /im WorkerService1.exe
taskkill /f /im ToastLauncher.exe
taskkill /f /im BlurWindowLauncher.exe

cd %~dp0
MSBuild %SOLUTIONPATH% /t:clean;build /p:Configuration=Debug;Platform="x64"
if %ERRORLEVEL% neq 0 (
    echo ErrorLevel:%ERRORLEVEL%
    echo ビルド失敗
)


rem ---------------------

rem サービスをアンインストール
sc delete %SERVICENAME%


echo -サービスをインストール
rem 試した限り、binPathにはフルパスを指定しないとうまくいかない
sc create %SERVICENAME% start=auto binPath= %BINPATH% DisplayName= %SERVICEDISPNAME%


echo -説明文を編集
sc description %SERVICENAME% %DISCRIPTION%


echo -サービスをスタート
sc start %SERVICENAME%
rem net start %SERVICENAME%


