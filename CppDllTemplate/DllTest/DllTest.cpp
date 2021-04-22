#include "stdafx.h"
#include <windows.h>
#include <stdio.h>
#include <combaseapi.h>
#include <wchar.h>
#include <thread>

#define VC_DLL_EXPORTS
#include "DllTest.h"

std::thread th_b;
int threadStop = 0;

void (*pfunc1)(int) = nullptr;// あほ
void (*pfunc2)(int) = nullptr;// 普通

void __cdecl RegisterCountingMethods(void (*pf1)(int), void (*pf2)(int))
{
    pfunc1 = pf1;
    pfunc2 = pf2;
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
                OutputDebugString(L"+ スレッド開始\r\n");

                // 時刻を取得
                auto t = time(nullptr);
                auto tmv = tm();
                localtime_s(&tmv, &t);

                if (tmv.tm_sec % 3 == 0)
                {
                    // 3の倍数
                    pfunc1(tmv.tm_sec);
                }
                else
                {
                    // 3の倍数でない
                    pfunc2(tmv.tm_sec);
                }

                Sleep(1000);
            }
            OutputDebugString(L"+ スレッド終了\r\n");
        });
}

// カウント停止
void __cdecl StopCounting()
{
    threadStop = 1;
    th_b.join();
}
