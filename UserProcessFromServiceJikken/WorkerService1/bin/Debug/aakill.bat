@echo off

set SOLUTIONPATH="..\..\..\serviceJikken1.sln"

set SERVICENAME="CsServiceJikken"
set SERVICEDISPNAME=%SERVICENAME%
set BINPATH=%~dp0\WorkerService1.exe
set DISCRIPTION="CsSvc Description..."

echo -�T�[�r�X�̃v���Z�X�𖳗����I��������
taskkill /f /im WorkerService1.exe
taskkill /f /im ToastLauncher.exe
taskkill /f /im BlurWindowLauncher.exe

rem pause