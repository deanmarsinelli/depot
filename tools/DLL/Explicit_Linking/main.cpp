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
}

