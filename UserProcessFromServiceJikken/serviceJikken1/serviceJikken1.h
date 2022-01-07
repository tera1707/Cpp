#pragma once

// DEBUG
#include <iostream>
#include <time.h>
#include <thread>   // std::this_thread::get_id()���g���̂ɕK�v
#include <fstream>  // std::wofstream���g���̂ɕK�v

const std::wstring CsTbl[] =
{
    L"INIT",
    L"SERVICE_STOPPED",
    L"SERVICE_START_PENDING",
    L"SERVICE_STOP_PENDING",
    L"SERVICE_RUNNING",
    L"SERVICE_CONTINUE_PENDING",
    L"SERVICE_PAUSE_PENDING",
    L"SERVICE_PAUSED",
};
const std::wstring PowSrcTbl[] =
{
    L"PoAc",
    L"PoDc",
    L"PoHot",
};
const std::wstring SvcCtrlTbl[] =
{
    L"UNDEFINED",
    L"SERVICE_CONTROL_STOP",
    L"SERVICE_CONTROL_PAUSE",
    L"SERVICE_CONTROL_CONTINUE",
    L"SERVICE_CONTROL_INTERROGATE",
    L"SERVICE_CONTROL_SHUTDOWN",
    L"SERVICE_CONTROL_PARAMCHANGE",
    L"SERVICE_CONTROL_NETBINDADD",
    L"SERVICE_CONTROL_NETBINDREMOVE",
    L"SERVICE_CONTROL_NETBINDENABLE",
    L"SERVICE_CONTROL_NETBINDDISABLE",
    L"SERVICE_CONTROL_DEVICEEVENT",
    L"SERVICE_CONTROL_HARDWAREPROFILECHANGE",
    L"SERVICE_CONTROL_POWEREVENT",
    L"SERVICE_CONTROL_SESSIONCHANGE",
    L"SERVICE_CONTROL_PRESHUTDOWN",
    L"SERVICE_CONTROL_TIMECHANGE",
    L"SERVICE_CONTROL_USER_LOGOFF",
    L"SERVICE_CONTROL_TRIGGEREVENT",
    L"reserved for internal use",
    L"reserved for internal use",
    L"SERVICE_CONTROL_LOWRESOURCES",
    L"SERVICE_CONTROL_SYSTEMLOWRESOURCES",
};
const std::wstring SessionScTbl[] =
{
    L"UNDEFINED",
    L"WTS_CONSOLE_CONNECT",
    L"WTS_CONSOLE_DISCONNECT",
    L"WTS_REMOTE_CONNECT",
    L"WTS_REMOTE_DISCONNECT",
    L"WTS_SESSION_LOGON",
    L"WTS_SESSION_LOGOFF",
    L"WTS_SESSION_LOCK",
    L"WTS_SESSION_UNLOCK",
    L"WTS_SESSION_REMOTE_CONTROL",
    L"WTS_SESSION_CREATE",
    L"WTS_SESSION_TERMINATE",
};

// ���O��C�h���C�u�����Ɏc��DEBUG�p�֐�
void OutputLogToCChokka(std::wstring txt)
{
    //FILE* fp = NULL;
    auto t = time(nullptr);
    auto tmv = tm();
    auto error = localtime_s(&tmv, &t); // ���[�J������(�^�C���]�[���ɍ��킹������)���擾

    WCHAR buf[256] = { 0 };
    wcsftime(buf, 256, L"%Y/%m/%d %H:%M:%S ", &tmv);

    // ���݂̃X���b�hID���o��
    auto thId = std::this_thread::get_id();

    // ���O�o��
    std::wstring logtxt = buf + txt;

    // �t�@�C�����J��(�Ȃ���΍쐬)
    // C�����̃t�@�C���ɏ����ɂ�exe���s���ɊǗ��Ҍ����ɂ���K�v�A��
    std::wofstream ofs(L"C:\\mylog.log", std::ios::app);
    if (!ofs)
    {
        return;
    }
    // ���ݎ����ƃX���b�hID��t�������O���t�@�C���ɏ�������
    ofs << thId << L"  " << logtxt.c_str() << std::endl;
    std::wcout << thId << L"  " << logtxt.c_str() << std::endl;
    // �t�@�C������
    ofs.close();
}


