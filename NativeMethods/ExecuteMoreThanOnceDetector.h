#pragma once
#include <windows.h>

using namespace System;

ref class ExecuteMoreThanOnceDetector
{
private: static ExecuteMoreThanOnceDetector^ m_Instance;
private: static Object^ m_SyncRoot = gcnew Object();
private:
    HANDLE m_hMutexRun;
    bool   m_bResult;

private:
    ExecuteMoreThanOnceDetector();
    ~ExecuteMoreThanOnceDetector();
    !ExecuteMoreThanOnceDetector();

public: static property ExecuteMoreThanOnceDetector^ Instance
{
    ExecuteMoreThanOnceDetector^ get();
}
};

