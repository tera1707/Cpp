#pragma once
#include <Windows.h>
#include <string.h>

// �G�N�X�|�[�g�ƃC���|�[�g�̐؂�ւ�
#ifdef VC_DLL_EXPORTS
#undef VC_DLL_EXPORTS
#define VC_DLL_EXPORTS extern "C" __declspec(dllexport)
#else
#define VC_DLL_EXPORTS extern "C" __declspec(dllimport)
#endif

// �G�N�X�|�[�g�֐��̃v���g�^�C�v�錾
//VC_DLL_EXPORTS void __cdecl CopyStringToBuffer(wchar_t* str);

VC_DLL_EXPORTS void __cdecl RegisterCountingMethods(void (*pf1)(int), void (*pf2)(int));
VC_DLL_EXPORTS void __cdecl StartCounting();
VC_DLL_EXPORTS void __cdecl StopCounting();
