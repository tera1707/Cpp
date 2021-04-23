#include "stdafx.h"
#include <windows.h>
#include <stdio.h>
#include <combaseapi.h>
#include <wchar.h>
#include <thread>
#include <string>

#define VC_DLL_EXPORTS
#include "DllTest.h"

std::thread th_b;
int threadStop = 0;

// 関数ポインタ
void (*pfunc1)(int) = nullptr;// あほ
void (*pfunc2)(int) = nullptr;// 普通

// 関数を登録するための関数
void __cdecl RegisterCountingMethods(void (*pf1)(int), void (*pf2)(int))
{
    pfunc1 = pf1;
    pfunc2 = pf2;
}

// 3の倍数と3がつく数字かどうかを判定する関数
BOOL JudgeSaaaan(int sec)
{
    auto secStr = std::to_string(sec);
    int pos = secStr.find("3");
    return pos != std::string::npos || sec % 3 == 0;
}

// 別スレッド起動し、カウント開始
// 現在の時計の秒数が3の倍数の時はexe側からもらったアホになる関数を呼び、
// そうでない場合はexe側からもらった普通に数える関数を呼ぶ
void __cdecl StartCounting()
{
	th_b = std::thread([]
        {
            while (!threadStop)
            {
                // 時刻を取得
                auto t = time(nullptr);
                auto tmv = tm();
                localtime_s(&tmv, &t);

                // 関数呼び分け
                if (JudgeSaaaan(tmv.tm_sec)) pfunc1(tmv.tm_sec);
                else                         pfunc2(tmv.tm_sec);

                Sleep(1000);
            }
        });
}

// カウント停止
void __cdecl StopCounting()
{
    threadStop = 1;
    th_b.join();
}
