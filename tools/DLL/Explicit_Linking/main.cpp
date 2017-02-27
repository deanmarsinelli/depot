// notes:
// 1) we must explicitly load the DLL using LoadLibrary() and unload it using FreeLibary()
// 2) we must manually get function pointers to the functions inside the DLL we want to use, using GetProcAddress
//    and the function pointer signature must use the same calling convention as the functions were exported with
//    inside the dll (e.g. __cdecl or __stdcall)
// 3) using a client layer (in this case api_XYZ functions) between the calling code and the DLL is helpful to wrap
//    the nastyness of function pointers, and using typedefs for function pointer signatures is a must
//

#include <iostream>
#include "api.h"

int main()
{
    std::cout << "Calling api_Initialize()" << std::endl;
    
    bool result = api_Initialize();
    if (result == false)
    {
        std::cout << "Failed to load DLL" << std::endl;
        return 1;
    }

    float x = 10;
    float y = 5;

    float sum = api_Add(x, y);
    std::cout << x << " + " << y << " = " << sum << std::endl;

    float difference = api_Subtract(x, y);
    std::cout << x << " - " << y << " = " << difference << std::endl;

    float product = api_Multiply(x, y);
    std::cout << x << " * " << y << " = " << product << std::endl;

    float quotient = api_Divide(x, y);
    std::cout << x << " / " << y << " = " << quotient << std::endl;

    std::cout << "Calling api_Shutdown()" << std::endl;
    api_Shutdown();

    std::cout << "Program Finished" << std::endl;
}

