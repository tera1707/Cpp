@echo off

set SOLUTIONPATH="C:\Users\tera1\Desktop\test\serviceJikken1\serviceJikken1.sln"

set SERVICENAME="SvcName"
set SERVICEDISPNAME=%SERVICENAME%
set BINPATH=C:\Users\tera1\Desktop\test\serviceJikken1\Debug\serviceJikken1.exe
set DISCRIPTION="Svc Description..."

call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat"

echo -�T�[�r�X�̃v���Z�X�𖳗����I��������
taskkill /f /im %BINPATH%

echo -�T�[�r�X���A���C���X�g�[��
sc delete %SERVICENAME%

echo -�\�����[�V���������r���h����
MSBuild %SOLUTIONPATH% /t:clean;rebuild /p:Configuration=Debug;Platform="x86" > nul
if %ERRORLEVEL% neq 0 (
    echo ErrorLevel:%ERRORLEVEL%
    echo �r���h���s
    pause
    exit
)

echo -�T�[�r�X���C���X�g�[��
rem ����������AbinPath�ɂ̓t���p�X���w�肵�Ȃ��Ƃ��܂������Ȃ�
sc create %SERVICENAME% start=auto binPath= %BINPATH% DisplayName= %SERVICEDISPNAME%

echo -��������ҏW
sc description %SERVICENAME% %DISCRIPTION%

echo -�T�[�r�X���X�^�[�g
rem sc start %SERVICENAME%
net start %SERVICENAME%
pause


