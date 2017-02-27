#include "api.h"

#include <cwchar>

#define MAX_PATH    260

static HMODULE hDLLProject = NULL;


typedef float (CDECL* addFuncPtr)(float, float);
typedef float (CDECL* subtractFuncPtr)(float, float);
typedef float (CDECL* multiplyFuncPtr)(float, float);
typedef float (CDECL* divideFuncPtr)(float, float);

static struct 
{
    addFuncPtr add;
    subtractFuncPtr subtract;
    multiplyFuncPtr multiply;
    divideFuncPtr divide;
} LIB;


static bool api_LoadFunctions()
{
    LIB.add = (addFuncPtr)GetProcAddress(hDLLProject, "Add");
    LIB.subtract = (subtractFuncPtr)GetProcAddress(hDLLProject, "Subtract");
    LIB.multiply = (multiplyFuncPtr)GetProcAddress(hDLLProject, "Multiply");
    LIB.divide = (divideFuncPtr)GetProcAddress(hDLLProject, "Divide");

    if (!LIB.add || !LIB.subtract || !LIB.multiply || !LIB.divide)
    {
        return false;
    }

    return true;
}

static void api_UnloadLibrary(HMODULE hModule)
{
    if (hModule)
    {
        FreeLibrary(hModule);
    }
}

bool api_Initialize()
{
    const wchar_t* pDLLProject = L"DLLProject";
#ifdef _WIN64
    const wchar_t* pBitness = L"64";
#else
    const wchar_t* pBitness = L"32";
#endif

    wchar_t fileName[MAX_PATH];
    swprintf_s(fileName, L"%s_%s", pDLLProject, pBitness);

    HMODULE hModule = LoadLibraryW(fileName);

    if (hModule != (HMODULE)NULL)
    {
        hDLLProject = hModule;
        bool result = api_LoadFunctions();
        if (result)
        {
            return true;
        }
    }

    api_UnloadLibrary(hModule);
    return false;
}

void api_Shutdown()
{
    if (hDLLProject)
    {
        api_UnloadLibrary(hDLLProject);
    }
}

float api_Add(float x, float y)
{
    return LIB.add(x, y);
}

float api_Subtract(float x, float y)
{
    return LIB.subtract(x, y);
}

float api_Multiply(float x, float y)
{
    return LIB.multiply(x, y);
}

float api_Divide(float x, float y)
{
    return LIB.divide(x, y);
}
