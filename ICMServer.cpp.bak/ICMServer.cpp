// ICMServer.cpp: 主要專案檔。
//        ┌─┐       ┌─┐
//     ┌──┘ ┴───────┘ ┴──┐
//     │                 │
//     │       ───       │
//     │   ─┬┘     └┬─   │
//     │                 │
//     │       ─┴─       │
//     │                 │
//     └───┐         ┌───┘
//         │         │
//         │         │
//         │         │
//         │         │
//         │         │
//         │         │
//         │         └──────────────┐
//         │                        │
//         │                        ├─┐
//         │                        ┌─┘
//         │                        │
//         └─┐  ┐  ┌───────┬──┐  ┌──┘
//           │ ─┤ ─┤       │ ─┤ ─┤
//           └──┴──┘       └──┴──┘
//                神獸保佑
//                程式碼無BUG!

#include "stdafx.h"
#include <windows.h>
#include <stdio.h> 
#include <crtdbg.h>

using namespace ICMServer::Net;
using namespace System;
using namespace System::Windows::Forms;

namespace ICMServer {
    static HANDLE g_hMutexRun;

    /// <summary>
    /// 檢查此程式是否重複開啟運行
    /// </summary>
    /// <returns>true 如果有重複開啟運行</returns>
    static bool IsThisProgramAlreadyOpened()
    {
        g_hMutexRun = CreateMutex(NULL, FALSE, __TEXT("www.ite.com.tw"));
        if (GetLastError() == ERROR_ALREADY_EXISTS)
        {
            CloseHandle(g_hMutexRun);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 應用程式的主要進入點。
    /// </summary>
    [STAThread]
    int main(array<System::String ^> ^args)
    {
        // for memory leak detect
        _CrtSetDbgFlag(_CRTDBG_CHECK_ALWAYS_DF | _CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);

        Application::EnableVisualStyles();
        Application::SetCompatibleTextRenderingDefault(false);

        // TODO: 判斷GUI應該以什麼語系呈現

        // 判斷ICMServer.exe是否重複執行
        if (IsThisProgramAlreadyOpened())
        {
            MessageBox::Show("偵測重複進程!\n系統將自動關閉");
            return 0;
        }

        ICMServer::Net::Firewall::Setup();

        // TODO: 初始化權限管理模組

        // TODO: 試著連接資料庫
        System::String^ errorMsg;
        if (!ICMServer::Models::ICMDBContext::CheckConnection(errorMsg))
        {
            MessageBox::Show(System::String::Format("數據庫連線異常!\n系統將自動關閉\n詳細訊息: {0}",
                errorMsg));
            return 0;
        }

        // TODO: 初始化 System Log 管理模組

#ifndef _DEBUG
        DialogLogin dlgLogin;
        if (dlgLogin.ShowDialog() != DialogResult::OK)
        {
            CloseHandle(g_hMutexRun);
            return 0;
        }
#endif

        // TODO: 非同步啟用 NTP server
        NtpServer::Instance->Start();
        // TODO: 非同步啟用 FTP server
        FtpServer::Instance->Start(Config::Instance->FTPServerRootDir, Config::Instance->FTPServerPort);
        // TODO: 非同步啟用 RTSP server
        // TODO: 非同步啟用 HTTP server
        HttpServer::Instance->Start(gcnew Uri(String::Format("http://localhost:{0}", Config::Instance->HTTPServerPort)));

        // TODO: 非同步啟用天氣服務 （即一個WebClient不斷去天氣查詢網站要資料）
        // TODO: 非同步啟用 Heart Beat 查詢服務
        //HeartbeatService::Instance->Start();
        Heartbeat::Instance->Start();

        Application::Run(gcnew FormMain());

        //HeartbeatService::Instance->Stop();
        Heartbeat::Instance->Stop();

        NtpServer::Instance->Stop();
        // 終結 NTP Server
        FtpServer::Instance->Stop();
        // 終結 HTTP Server
        HttpServer::Instance->Stop();

        CloseHandle(g_hMutexRun);
        return 0;
    }
}
