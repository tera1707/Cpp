@echo off

set SOLUTIONPATH="..\..\..\serviceJikken1.sln"

set SERVICENAME="CsServiceJikken"
set SERVICEDISPNAME=%SERVICENAME%
set BINPATH=%~dp0\WorkerService1.exe
set DISCRIPTION="CsSvc Description..."

call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

echo -�T�[�r�X�̃v���Z�X�𖳗����I��������
taskkill /f /im WorkerService1.exe
taskkill /f /im ToastLauncher.exe
taskkill /f /im BlurWindowLauncher.exe

cd %~dp0
MSBuild %SOLUTIONPATH% /t:clean;build /p:Configuration=Debug;Platform="x64"
if %ERRORLEVEL% neq 0 (
    echo ErrorLevel:%ERRORLEVEL%
    echo �r���h���s
)


rem ---------------------

rem �T�[�r�X���A���C���X�g�[��
sc delete %SERVICENAME%


echo -�T�[�r�X���C���X�g�[��
rem ����������AbinPath�ɂ̓t���p�X���w�肵�Ȃ��Ƃ��܂������Ȃ�
sc create %SERVICENAME% start=auto binPath= %BINPATH% DisplayName= %SERVICEDISPNAME%


echo -��������ҏW
sc description %SERVICENAME% %DISCRIPTION%


echo -�T�[�r�X���X�^�[�g
sc start %SERVICENAME%
rem net start %SERVICENAME%


