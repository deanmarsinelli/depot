// Win_thread.cpp
// Dean Marsinelli
// Learning how to use Win32 threads

#include <iostream>
#include <windows.h>

DWORD WINAPI print_thread_id(LPVOID params)
{
    char buf[256];
    sprintf_s(buf, "THREAD %d -- THREAD ID: %d\n", *(int*)params, GetCurrentThreadId());
    std::cout << buf;

    return 0;
}

int main()
{
    HANDLE thread1 = 0;
    HANDLE thread2 = 0;
    HANDLE thread3 = 0;

    int thread1_arg = 1;
    int thread2_arg = 2;
    int thread3_arg = 3;

    // create 3 threads
    thread1 = CreateThread(nullptr, 0, print_thread_id, &thread1_arg, 0, nullptr);
    thread2 = CreateThread(nullptr, 0, print_thread_id, &thread2_arg, 0, nullptr);
    thread3 = CreateThread(nullptr, 0, print_thread_id, &thread3_arg, 0, nullptr);

    char buf[256];
    sprintf_s(buf, "MAIN THREAD ID: %d\n", GetCurrentThreadId());
    std::cout << buf;

    // join all 3 threads
    WaitForSingleObject(thread1, INFINITE);
    WaitForSingleObject(thread2, INFINITE);
    WaitForSingleObject(thread3, INFINITE);

    // invalidate the thread handles
    CloseHandle(thread1);
    CloseHandle(thread2);
    CloseHandle(thread3);

    std::cin.get();
}
