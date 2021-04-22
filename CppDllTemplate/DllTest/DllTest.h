#pragma once
#include <Windows.h>
#include <string.h>

// エクスポートとインポートの切り替え
#ifdef VC_DLL_EXPORTS
#undef VC_DLL_EXPORTS
#define VC_DLL_EXPORTS extern "C" __declspec(dllexport)
#else
#define VC_DLL_EXPORTS extern "C" __declspec(dllimport)
#endif

// エクスポート関数のプロトタイプ宣言
//VC_DLL_EXPORTS void __cdecl CopyStringToBuffer(wchar_t* str);

VC_DLL_EXPORTS void __cdecl RegisterCountingMethods(void (*pf1)(int), void (*pf2)(int));
VC_DLL_EXPORTS void __cdecl StartCounting();
VC_DLL_EXPORTS void __cdecl StopCounting();
