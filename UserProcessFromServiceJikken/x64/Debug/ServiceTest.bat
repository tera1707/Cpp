@echo off

set SERVICENAME="SvcName"
set SERVICEDISPNAME=%SERVICENAME%
set BINPATH=C:\git\Cpp\UserProcessFromServiceJikken\x64\Debug\serviceJikken1.exe
set DISCRIPTION="Svc Description..."

echo -サービスのプロセスを無理やり終了させる
taskkill /f /im %BINPATH%

rem サービスをアンインストール
sc delete %SERVICENAME%


echo -サービスをインストール
rem 試した限り、binPathにはフルパスを指定しないとうまくいかない
sc create %SERVICENAME% start=auto binPath= %BINPATH% DisplayName= %SERVICEDISPNAME%


echo -説明文を編集
sc description %SERVICENAME% %DISCRIPTION%


echo -サービスをスタート
sc start %SERVICENAME%
net start %SERVICENAME%


echo -サービスをストップ
rem sc stop %SERVICENAME%



