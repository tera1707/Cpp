#include <windows.h>
#include <string>
#include "framework.h"
#include "WindowsProject1.h"
#include "resource.h"
#include "DllTest.h"

HINSTANCE hInst;
HWND hDlgWnd;

// このコード モジュールに含まれる関数の宣言を転送します:
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
BOOL CALLBACK MyDlgProc(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    hInst = hInstance;

    DialogBox(hInst, L"MyTestDlgBase_Main", NULL, (DLGPROC)MyDlgProc);

    return (int)0;
}

// あほに数える関数
void SanNoBaisuu(int sec)
{
    SendMessage(GetDlgItem(hDlgWnd, IDC_LIST1), LB_INSERTSTRING, 0, (LPARAM)(std::to_wstring(sec) + L"ぁ～～ん").c_str());
}

// 真面目に数える関数
void SanNoBaisuuDehaNai(int sec)
{
    SendMessage(GetDlgItem(hDlgWnd, IDC_LIST1), LB_INSERTSTRING, 0, (LPARAM)std::to_wstring(sec).c_str());
}

// ダイアログプロシージャ
BOOL CALLBACK MyDlgProc(HWND hDlg, UINT msg, WPARAM wp, LPARAM lp)
{
    switch (msg) {
    case WM_INITDIALOG:
        hDlgWnd = hDlg;
        // 関数を登録
        RegisterCountingMethods(SanNoBaisuu, SanNoBaisuuDehaNai);
        break;
    case WM_COMMAND:
        switch (LOWORD(wp)) {
            case IDC_BUTTON1:
                // 世界のナベアツ カウントスタート
                StartCounting();
                break;
        }
        return FALSE;
    case WM_CLOSE:
        // ナベアツ終了
        StopCounting();
        EndDialog(hDlg, 0);
        return TRUE;
    }
    return FALSE;
}
