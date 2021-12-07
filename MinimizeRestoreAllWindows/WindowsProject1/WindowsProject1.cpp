// WindowsProject1.cpp : アプリケーションのエントリ ポイントを定義します。
//
#include <vector>
#include "framework.h"
#include "WindowsProject1.h"
#include "resource.h"
#include <string>

HINSTANCE hInst;
std::vector<HWND> _desktopWindowList;
std::vector<HWND> _uwpWindowList;

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
BOOL CALLBACK MyDlgProc(HWND, UINT, WPARAM, LPARAM);
BOOL CALLBACK EnumWindowsProc(HWND hwnd, LPARAM lp);
void OutputLog(std::wstring logtxt);
void MinimizeDesktopWindows();
void RestoreDesktopWindows();

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    hInst = hInstance;

    DialogBox(hInst, L"MyTestDlgBase_Main", NULL, (DLGPROC)MyDlgProc);

    return (int)0;
}

// ダイアログプロシージャ
BOOL CALLBACK MyDlgProc(HWND hDlg, UINT msg, WPARAM wp, LPARAM lp)
{
    switch (msg) {
    case WM_COMMAND:
        switch (LOWORD(wp)) {
        case IDOK:
            EndDialog(hDlg, IDOK);
            return TRUE;
        case IDCANCEL:
            EndDialog(hDlg, IDCANCEL);
            return TRUE;
        case IDC_MINIMIZE_WINDOWS:
            MinimizeDesktopWindows();
            break;
        case IDC_RESTORE_WINDOWS:
            RestoreDesktopWindows();
            break;
        }
        return FALSE;
    }
    return FALSE;
}

void MinimizeDesktopWindows()
{
    _desktopWindowList.clear();
    _uwpWindowList.clear();

    EnumWindows(EnumWindowsProc, NULL);

    for (auto i = 0 ; i < _desktopWindowList.size(); i++)
    {
        ShowWindow(_desktopWindowList[i], SW_MINIMIZE);
    }
    for (auto i = 0; i < _uwpWindowList.size(); i++)
    {
        ShowWindow(_uwpWindowList[i], SW_MINIMIZE);
    }
}

void RestoreDesktopWindows()
{
    _desktopWindowList.clear();
    _uwpWindowList.clear();

    EnumWindows(EnumWindowsProc, NULL);

    for (auto i = 0; i < _desktopWindowList.size(); i++)
    {
        ShowWindow(_desktopWindowList[i], SW_RESTORE);
    }
    for (auto i = 0; i < _uwpWindowList.size(); i++)
    {
        ShowWindow(_uwpWindowList[i], SW_RESTORE);
    }
}

BOOL CALLBACK EnumWindowsProc(HWND hwnd, LPARAM lp)
{
    auto textLen = GetWindowTextLength(hwnd);

    if (textLen > 0)
    {
        if (!IsWindowVisible(hwnd)) return TRUE;
        
        WCHAR buf[256] = { 0 };
        GetWindowText(hwnd, buf, sizeof(buf) / sizeof(buf[0]));
        auto title = std::wstring(buf);

        //WCHAR className[256] = { 0 };
        GetClassName(hwnd, buf, sizeof(buf) / sizeof(buf[0]));
        auto titlclassName = std::wstring(buf);

        if (titlclassName != L"Windows.UI.Core.CoreWindow"
            && titlclassName != L"ApplicationFrameWindow")
        {
            // デスクトップのウインドウ
            _desktopWindowList.push_back(hwnd);
            OutputLog(std::wstring(L"desktop : ") + title);
        }
        else if (titlclassName == L"ApplicationFrameWindow")
        {
            // UWPアプリのウインドウ
            _uwpWindowList.push_back(hwnd);
            OutputLog(std::wstring(L"uwp : ") + title);
        }
    }
    return TRUE;
}

void OutputLog(std::wstring logtxt)
{
    OutputDebugString(logtxt.c_str());
    OutputDebugString(L"\r\n");
}
