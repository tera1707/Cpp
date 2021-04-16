@echo off

set SOLUTIONPATH="C:\Users\tera1\Desktop\test\serviceJikken1\serviceJikken1.sln"

set SERVICENAME="SvcName"
set SERVICEDISPNAME=%SERVICENAME%
set BINPATH=C:\Users\tera1\Desktop\test\serviceJikken1\Debug\serviceJikken1.exe
set DISCRIPTION="Svc Description..."

call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat"

echo -サービスのプロセスを無理やり終了させる
taskkill /f /im %BINPATH%

echo -サービスをアンインストール
sc delete %SERVICENAME%

echo -ソリューションをリビルドする
MSBuild %SOLUTIONPATH% /t:clean;rebuild /p:Configuration=Debug;Platform="x86" > nul
if %ERRORLEVEL% neq 0 (
    echo ErrorLevel:%ERRORLEVEL%
    echo ビルド失敗
    pause
    exit
)

echo -サービスをインストール
rem 試した限り、binPathにはフルパスを指定しないとうまくいかない
sc create %SERVICENAME% start=auto binPath= %BINPATH% DisplayName= %SERVICEDISPNAME%

echo -説明文を編集
sc description %SERVICENAME% %DISCRIPTION%

echo -サービスをスタート
rem sc start %SERVICENAME%
net start %SERVICENAME%
pause


