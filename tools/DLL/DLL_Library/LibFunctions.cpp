#include "LibFunctions.h"

#include <iostream>

BOOL WINAPI DllMain(_In_ HINSTANCE hinstDLL, _In_ DWORD fdwReason, _In_ LPVOID lpvReserved)
{
    if (fdwReason == DLL_PROCESS_ATTACH)
    {
        std::cout << "DLLMain() called - attaching to process" << std::endl;
    }
    else if (fdwReason == DLL_PROCESS_DETACH)
    {
        std::cout << "DLLMain() called - detaching from process" << std::endl;
    }
    return TRUE;
}

float Add(float x, float y)
{
    return x + y;
}

float Subtract(float x, float y)
{
    return x - y;
}

float Multiply(float x, float y)
{
    return x * y;
}

float Divide(float x, float y)
{
    if (y != 0)
    {
        return x / y;
    }
    else
    {
        return 0.0f;
    }
}

