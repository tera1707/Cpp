
#include <windows.h>
#include <tchar.h>
#include <psapi.h>
#include <strsafe.h>
#include <string>
#include <iostream>

DWORD GetProcessIdByName(const wchar_t* targetProcessName)
{
    DWORD processIds[1024];
    DWORD cbNeeded;

    if (EnumProcesses(processIds, sizeof(processIds), &cbNeeded))
    {
        for (DWORD i = 0; i < cbNeeded / sizeof(DWORD); i++)
        {
            TCHAR processName[MAX_PATH] = { 0 };
            auto hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, FALSE, processIds[i]);

            if (hProcess != NULL)
            {
                DWORD size = MAX_PATH;
                if (QueryFullProcessImageName(hProcess, NULL, processName, &size))
                {
                    std::wcout << processName << "," << targetProcessName << std::endl;
                    if (wcsstr(processName, targetProcessName))
                    {
                        CloseHandle(hProcess);
                        return processIds[i];
                    }
                }

                CloseHandle(hProcess);
            }
        }
    }

    return -1;
}

int main()
{
    std::cout << "Hello World!\n";

    auto taa = TOKEN_ALL_ACCESS;
    auto maa = MAXIMUM_ALLOWED;

    auto dwSesId = ::WTSGetActiveConsoleSessionId();
    auto winlogonProcName = GetProcessIdByName(L"winlogon.exe");

    std::cout << "ID " << dwSesId << std::endl;
    std::cout << "ProcName " << winlogonProcName << std::endl;


    std::cin;
}

