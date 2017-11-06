#include "stdafx.h"
#include "ExecuteMoreThanOnceDetector.h"


ExecuteMoreThanOnceDetector::ExecuteMoreThanOnceDetector()
{
    m_hMutexRun = CreateMutex(NULL, FALSE, __TEXT("www.ite.com.tw"));
    if (GetLastError() == ERROR_ALREADY_EXISTS)
    {
        CloseHandle(m_hMutexRun);
        m_bResult = true;
    }
    m_bResult = false;
}

ExecuteMoreThanOnceDetector::~ExecuteMoreThanOnceDetector()
{
    if (m_hMutexRun != nullptr)
        CloseHandle(m_hMutexRun);
}

ExecuteMoreThanOnceDetector::!ExecuteMoreThanOnceDetector()
{
    if (m_hMutexRun != nullptr)
        CloseHandle(m_hMutexRun);
}

ExecuteMoreThanOnceDetector^ ExecuteMoreThanOnceDetector::Instance::get()
{
    if (m_Instance == nullptr)
    {
        // lock(m_SyncRoot)
        {
            if (m_Instance == nullptr)
                m_Instance = gcnew ExecuteMoreThanOnceDetector();
        }
    }

    return m_Instance;
}
