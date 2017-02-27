#pragma once

#include <Windows.h>

// notes:
// 1) __declspec(dllexport) is not required for the function definition (in the .cpp file)
// 2) DLLMain is optional, and if present it will be called once when the DLL is loaded and once when it is unloaded
// 3) DLLMain does not have to be exported. it is called when the library is loaded implicitly by a process that includes the dll, or loaded explicitly through LoadLibrary()
// 4) functions that are declared __stdcall or __cdecl should have the matching function pointer signature in the client application when using GetProcAddress()
// 5) if using GetProcAddress, definitly export the functions using extern "C" so the names don't get mangled and they are easy to import
// 6) client applications using LoadLibrary will first check for the dll in the same directory as the executable then the system directory 
//    see: https://msdn.microsoft.com/en-us/library/windows/desktop/ms682586(v=vs.85).aspx#search_order_for_desktop_applications
//    when running the application through the debugger in visual studio, the dll can live in the same directory as the project, but if a dll with the same name ALSO 
//    lives next to the .exe, the dll in the same directory as the .exe will be loaded
//

#define DLLExport __declspec(dllexport)

#define PUBLIC_FUNCTION(rval)   DLLExport rval CDECL

// define C linkage for these functions
#ifdef __cplusplus
    extern "C" {
#endif


BOOL WINAPI DllMain(_In_ HINSTANCE hinstDLL, _In_ DWORD fdwReason, _In_ LPVOID lpvReserved);

PUBLIC_FUNCTION(float) Add(float x, float y);

PUBLIC_FUNCTION(float) Subtract(float x, float y);

PUBLIC_FUNCTION(float) Multiply(float x, float y);

PUBLIC_FUNCTION(float) Divide(float x, float y);


#ifdef __cplusplus
    } // extern "C"
#endif