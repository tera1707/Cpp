@echo off

set SERVICENAME="SvcName"
set SERVICEDISPNAME=%SERVICENAME%
set BINPATH=C:\git\Cpp\UserProcessFromServiceJikken\x64\Debug\serviceJikken1.exe
set DISCRIPTION="Svc Description..."

echo -�T�[�r�X�̃v���Z�X�𖳗����I��������
taskkill /f /im %BINPATH%

rem �T�[�r�X���A���C���X�g�[��
sc delete %SERVICENAME%


echo -�T�[�r�X���C���X�g�[��
rem ����������AbinPath�ɂ̓t���p�X���w�肵�Ȃ��Ƃ��܂������Ȃ�
sc create %SERVICENAME% start=auto binPath= %BINPATH% DisplayName= %SERVICEDISPNAME%


echo -��������ҏW
sc description %SERVICENAME% %DISCRIPTION%


echo -�T�[�r�X���X�^�[�g
sc start %SERVICENAME%
net start %SERVICENAME%


echo -�T�[�r�X���X�g�b�v
rem sc stop %SERVICENAME%



