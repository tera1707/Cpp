@echo off

set SOLUTIONPATH="C:\Users\tera1\Desktop\test\serviceJikken1\serviceJikken1.sln"

set SERVICENAME="SvcName"
set SERVICEDISPNAME=%SERVICENAME%
set BINPATH=C:\Users\tera1\Desktop\test\serviceJikken1\x64\Debug\serviceJikken1.exe
set DISCRIPTION="Svc Description..."

rem call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat"

echo -�T�[�r�X�̃v���Z�X�𖳗����I��������
rem taskkill /f /im %BINPATH%
taskkill /f /im serviceJikken1.exe

echo -�T�[�r�X���A���C���X�g�[��
sc delete %SERVICENAME%

echo -�T�[�r�X���C���X�g�[��
rem ����������AbinPath�ɂ̓t���p�X���w�肵�Ȃ��Ƃ��܂������Ȃ�
sc create %SERVICENAME% start=auto binPath= %BINPATH% DisplayName= %SERVICEDISPNAME%

echo -��������ҏW
sc description %SERVICENAME% %DISCRIPTION%

echo -�T�[�r�X���X�^�[�g
rem sc start %SERVICENAME%
net start %SERVICENAME%



