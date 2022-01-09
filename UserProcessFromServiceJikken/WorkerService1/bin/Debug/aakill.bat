@echo off

set SOLUTIONPATH="..\..\..\serviceJikken1.sln"

set SERVICENAME="CsServiceJikken"
set SERVICEDISPNAME=%SERVICENAME%
set BINPATH=%~dp0\WorkerService1.exe
set DISCRIPTION="CsSvc Description..."

echo -サービスのプロセスを無理やり終了させる
taskkill /f /im WorkerService1.exe
pause