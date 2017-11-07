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
    /// 檢查此程式是否重复開啟運行
    /// </summary>
    /// <returns>true 如果有重复開啟運行</returns>
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

        // TODO: 判断GUI应该以什么语言呈现

        // 判断ICMServer.exe是否重复执行
        if (IsThisProgramAlreadyOpened())
        {
            MessageBox::Show("检测到重复进程!\n系统将自动关闭");
            return 0;
        }

        ICMServer::Net::Firewall::Setup();

        // TODO: 初始化权限管理模組

        // TODO: 試著连接資料库
        System::String^ errorMsg;
        if (!ICMServer::Models::ICMDBContext::CheckConnection(errorMsg))
        {
            MessageBox::Show(System::String::Format("数据库连接异常!\n系统将自动关闭\n详细信息: {0}",
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
		//HttpServer::Instance->Start(gcnew Uri(String::Format("http://localhost:8080")));

        // TODO: 非同步啟用天气服务 （即一個WebClient不断去天气查詢網站要資料）
        // TODO: 非同步啟用 Heart Beat 查詢服务
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
