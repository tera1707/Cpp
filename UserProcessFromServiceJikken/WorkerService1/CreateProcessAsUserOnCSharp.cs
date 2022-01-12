using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MyUtilily
{
    static internal class CreateProcessAsUserOnCSharp
    {
        /// <summary>
        /// ユーザーセッションでコマンドを実行する
        /// </summary>
        /// <param name="commandline">実行したいコマンド</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void CreateProcessAsUser(string commandline)
        {
            var sessionId = NativeMethods.WTSGetActiveConsoleSessionId();

            IntPtr hPToken = IntPtr.Zero;

            var ret = NativeMethods.WTSQueryUserToken(sessionId, out hPToken);

            var sa = new NativeMethods.SECURITY_ATTRIBUTES();
            sa.nLength = Marshal.SizeOf(sa);

            if (!NativeMethods.DuplicateTokenEx(hPToken, NativeMethods.TOKEN_ALL_ACCESS, ref sa, NativeMethods.SECURITY_IMPERSONATION_LEVEL.SecurityDelegation, NativeMethods.TOKEN_TYPE.TokenPrimary, out hUserTokenDup))
            {
                NativeMethods.CloseHandle(hPToken);
                throw new InvalidOperationException();
            }

            var si = new NativeMethods.STARTUPINFO()
            {
                cb = Marshal.SizeOf(sa),
                lpDesktop = @"winsta0\default",
                wShowWindow = 0,//SW_HIDE
                dwFlags = NativeMethods.STARTF_USESHOWWINDOW,
            };

            var hUserTokenDup = IntPtr.Zero;
            var creationFlags = NativeMethods.CREATE_UNICODE_ENVIRONMENT;
            var env = IntPtr.Zero;

            // アクティブユーザのセッションを設定します
            var ret2 = NativeMethods.SetTokenInformation(hUserTokenDup, NativeMethods.TOKEN_INFORMATION_CLASS.TokenSessionId, ref sessionId, sizeof(NativeMethods.TOKEN_INFORMATION_CLASS));

            // 環境変数を設定
            if (!NativeMethods.CreateEnvironmentBlock(out env, hUserTokenDup, true))
            {
                env = IntPtr.Zero;
            }

            NativeMethods.PROCESS_INFORMATION pi = new NativeMethods.PROCESS_INFORMATION();

            NativeMethods.CreateProcessAsUser(hUserTokenDup, IntPtr.Zero, commandline, IntPtr.Zero, IntPtr.Zero, false, creationFlags, env, IntPtr.Zero, ref si, out pi);

            NativeMethods.DestroyEnvironmentBlock(env);
        }

        private static class NativeMethods
        {
            [DllImport("kernel32.dll")]
            public static extern uint WTSGetActiveConsoleSessionId();

            [DllImport("Wtsapi32.dll")]
            public static extern uint WTSQueryUserToken(long sessionId, out IntPtr phToken);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

            // @OpenProcess
            [Flags]
            public enum ProcessAccessFlags : uint
            {
                All = 0x001F0FFF,
                Terminate = 0x00000001,
                CreateThread = 0x00000002,
                VirtualMemoryOperation = 0x00000008,
                VirtualMemoryRead = 0x00000010,
                VirtualMemoryWrite = 0x00000020,
                DuplicateHandle = 0x00000040,
                CreateProcess = 0x000000080,
                SetQuota = 0x00000100,
                SetInformation = 0x00000200,
                QueryInformation = 0x00000400,
                QueryLimitedInformation = 0x00001000,
                Synchronize = 0x00100000
            }

            // @OpenProcess
            public static uint MAXIMUM_ALLOWED = 0x02000000;
            public static uint TOKEN_ALL_ACCESS = 0x020f01ff;

            [DllImport("advapi32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

            [DllImport("coredll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CloseHandle(IntPtr hObject);

            // @OpenProcessToken
            public static uint TOKEN_DUPLICATE = 0x0002;

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public extern static bool DuplicateTokenEx(IntPtr hExistingToken, uint dwDesiredAccess, ref SECURITY_ATTRIBUTES lpTokenAttributes, SECURITY_IMPERSONATION_LEVEL ImpersonationLevel, TOKEN_TYPE TokenType, out IntPtr phNewToken);

            // @DuplicateTokenEx
            [StructLayout(LayoutKind.Sequential)]
            public struct SECURITY_ATTRIBUTES
            {
                public int nLength;
                public IntPtr lpSecurityDescriptor;
                public int bInheritHandle;
            }

            // @DuplicateTokenEx
            public enum SECURITY_IMPERSONATION_LEVEL
            {
                SecurityAnonymous,
                SecurityIdentification,
                SecurityImpersonation,
                SecurityDelegation
            }

            // @DuplicateTokenEx
            public enum TOKEN_TYPE
            {
                TokenPrimary = 1,
                TokenImpersonation
            }

            // @DuplicateTokenEx
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct STARTUPINFO
            {
                public Int32 cb;
                public string lpReserved;
                public string lpDesktop;
                public string lpTitle;
                public Int32 dwX;
                public Int32 dwY;
                public Int32 dwXSize;
                public Int32 dwYSize;
                public Int32 dwXCountChars;
                public Int32 dwYCountChars;
                public Int32 dwFillAttribute;
                public Int32 dwFlags;
                public Int16 wShowWindow;
                public Int16 cbReserved2;
                public IntPtr lpReserved2;
                public IntPtr hStdInput;
                public IntPtr hStdOutput;
                public IntPtr hStdError;
            }

            // @DuplicateTokenEx
            public static uint CREATE_NEW_CONSOLE = 0x00000010;
            public static uint NORMAL_PRIORITY_CLASS = 0x00000020;

            [DllImport("advapi32.dll", SetLastError = true)]
            public static extern int SetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, ref UInt32 TokenInformation, UInt32 TokenInformationLength);

            [DllImport("userenv.dll", SetLastError = true)]
            public static extern bool CreateEnvironmentBlock(out IntPtr lpEnvironment, IntPtr hToken, bool bInherit);

            // @CreateEnvironmentBlock
            public static uint CREATE_UNICODE_ENVIRONMENT = 0x00000400; 
            public static int STARTF_USESHOWWINDOW = 0x00000001; 

             [StructLayout(LayoutKind.Sequential)]
            public struct PROCESS_INFORMATION
            {
                public IntPtr hProcess;
                public IntPtr hThread;
                public int dwProcessId;
                public int dwThreadId;
            }

            [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool CreateProcessAsUser(
                IntPtr hToken,
                IntPtr lpApplicationName,
                string lpCommandLine,
                IntPtr lpProcessAttributes,
                IntPtr lpThreadAttributes,
                bool bInheritHandles,
                uint dwCreationFlags,
                IntPtr lpEnvironment,
                IntPtr lpCurrentDirectory,
                ref STARTUPINFO lpStartupInfo,
                out PROCESS_INFORMATION lpProcessInformation);


            [DllImport("userenv.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DestroyEnvironmentBlock(IntPtr lpEnvironment);

            public enum TOKEN_INFORMATION_CLASS
            {
                /// <summary>
                /// The buffer receives a TOKEN_USER structure that contains the user account of the token.
                /// </summary>
                TokenUser = 1,

                /// <summary>
                /// The buffer receives a TOKEN_GROUPS structure that contains the group accounts associated with the token.
                /// </summary>
                TokenGroups,

                /// <summary>
                /// The buffer receives a TOKEN_PRIVILEGES structure that contains the privileges of the token.
                /// </summary>
                TokenPrivileges,

                /// <summary>
                /// The buffer receives a TOKEN_OWNER structure that contains the default owner security identifier (SID) for newly created objects.
                /// </summary>
                TokenOwner,

                /// <summary>
                /// The buffer receives a TOKEN_PRIMARY_GROUP structure that contains the default primary group SID for newly created objects.
                /// </summary>
                TokenPrimaryGroup,

                /// <summary>
                /// The buffer receives a TOKEN_DEFAULT_DACL structure that contains the default DACL for newly created objects.
                /// </summary>
                TokenDefaultDacl,

                /// <summary>
                /// The buffer receives a TOKEN_SOURCE structure that contains the source of the token. TOKEN_QUERY_SOURCE access is needed to retrieve this information.
                /// </summary>
                TokenSource,

                /// <summary>
                /// The buffer receives a TOKEN_TYPE value that indicates whether the token is a primary or impersonation token.
                /// </summary>
                TokenType,

                /// <summary>
                /// The buffer receives a SECURITY_IMPERSONATION_LEVEL value that indicates the impersonation level of the token. If the access token is not an impersonation token, the function fails.
                /// </summary>
                TokenImpersonationLevel,

                /// <summary>
                /// The buffer receives a TOKEN_STATISTICS structure that contains various token statistics.
                /// </summary>
                TokenStatistics,

                /// <summary>
                /// The buffer receives a TOKEN_GROUPS structure that contains the list of restricting SIDs in a restricted token.
                /// </summary>
                TokenRestrictedSids,

                /// <summary>
                /// The buffer receives a DWORD value that indicates the Terminal Services session identifier that is associated with the token.
                /// </summary>
                TokenSessionId,

                /// <summary>
                /// The buffer receives a TOKEN_GROUPS_AND_PRIVILEGES structure that contains the user SID, the group accounts, the restricted SIDs, and the authentication ID associated with the token.
                /// </summary>
                TokenGroupsAndPrivileges,

                /// <summary>
                /// Reserved.
                /// </summary>
                TokenSessionReference,

                /// <summary>
                /// The buffer receives a DWORD value that is nonzero if the token includes the SANDBOX_INERT flag.
                /// </summary>
                TokenSandBoxInert,

                /// <summary>
                /// Reserved.
                /// </summary>
                TokenAuditPolicy,

                /// <summary>
                /// The buffer receives a TOKEN_ORIGIN value.
                /// </summary>
                TokenOrigin,

                /// <summary>
                /// The buffer receives a TOKEN_ELEVATION_TYPE value that specifies the elevation level of the token.
                /// </summary>
                TokenElevationType,

                /// <summary>
                /// The buffer receives a TOKEN_LINKED_TOKEN structure that contains a handle to another token that is linked to this token.
                /// </summary>
                TokenLinkedToken,

                /// <summary>
                /// The buffer receives a TOKEN_ELEVATION structure that specifies whether the token is elevated.
                /// </summary>
                TokenElevation,

                /// <summary>
                /// The buffer receives a DWORD value that is nonzero if the token has ever been filtered.
                /// </summary>
                TokenHasRestrictions,

                /// <summary>
                /// The buffer receives a TOKEN_ACCESS_INFORMATION structure that specifies security information contained in the token.
                /// </summary>
                TokenAccessInformation,

                /// <summary>
                /// The buffer receives a DWORD value that is nonzero if virtualization is allowed for the token.
                /// </summary>
                TokenVirtualizationAllowed,

                /// <summary>
                /// The buffer receives a DWORD value that is nonzero if virtualization is enabled for the token.
                /// </summary>
                TokenVirtualizationEnabled,

                /// <summary>
                /// The buffer receives a TOKEN_MANDATORY_LABEL structure that specifies the token's integrity level.
                /// </summary>
                TokenIntegrityLevel,

                /// <summary>
                /// The buffer receives a DWORD value that is nonzero if the token has the UIAccess flag set.
                /// </summary>
                TokenUIAccess,

                /// <summary>
                /// The buffer receives a TOKEN_MANDATORY_POLICY structure that specifies the token's mandatory integrity policy.
                /// </summary>
                TokenMandatoryPolicy,

                /// <summary>
                /// The buffer receives the token's logon security identifier (SID).
                /// </summary>
                TokenLogonSid,

                /// <summary>
                /// The maximum value for this enumeration
                /// </summary>
                MaxTokenInfoClass
            }

        }

    }
}
