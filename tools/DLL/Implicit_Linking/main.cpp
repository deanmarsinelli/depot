// notes:
// 1) unlike explicit linking, with implicit linking we do not control when the DLL is loaded/unloaded
//    the DLL is loaded when this application starts, and unloaded when it ends. in this example we can
//    see that "Program Finished" is printed before "DLLMain() called - detaching from process" is printed
// 2) the DLL maker must provide the header with function signatures to use, in addition to the static lib
//    to link against. these functions can then be called directly

#include <iostream>

#include "LibFunctions.h"   // header provided with the DLL

// staticly linked with the lib provided with the DLL
#pragma comment(lib, "DLLProject_64.lib")


int main()
{
    float x = 10;
    float y = 5;

    float sum = Add(x, y);
    std::cout << x << " + " << y << " = " << sum << std::endl;

    float difference = Subtract(x, y);
    std::cout << x << " - " << y << " = " << difference << std::endl;

    float product = Multiply(x, y);
    std::cout << x << " * " << y << " = " << product << std::endl;

    float quotient = Divide(x, y);
    std::cout << x << " / " << y << " = " << quotient << std::endl;

    std::cout << "Program Finished" << std::endl;
}

