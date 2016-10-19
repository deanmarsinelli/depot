// Async.cpp
// Dean Marsinelli
// Learning how to use async and futures in C++11

#include <iostream>
#include <future>
#include <vector>

// std::async runs the given function (possibly) asynchronously in a separate thread
// default behavior is implementation dependent, but you can force it to use a
// separate thread by calling std::async(std::launch::async, f, args)
// using std::launc::deferred will wait to run the function until .get()
// is called on the returned future
//
// std::async returns a future which is like a handle to a thread
// it can be "joined" using future.get()
// note about futures in VS2015: they will hang in the destructor until the
// underlying async task is completed

class simple_object
{
public:
    void print_name()
    {
        char buf[256];
        sprintf_s(buf, "I AM A SIMPLE OBJECT ON THREAD %d\n", std::this_thread::get_id());
        std::cout << buf;
    }
};


void print_threadid()
{
    char buf[256];
    sprintf_s(buf, "ASYNC TASK THREAD ID: %d\n", std::this_thread::get_id());
    std::cout << buf;
}

// std::async using a function
void async_function()
{
    // future holds the return type
    std::future<void> fut = std::async(print_threadid);

    char buf[256];
    sprintf_s(buf, "MAIN THREAD ID: %d\n", std::this_thread::get_id());
    std::cout << buf;

    // "joins" the async task, "gets" the return value
    // this will block and wait for the task to finish
    fut.get();
}

// std::async using a lambda
void async_lambda()
{
    // lambda syntax
    // [capture list](params)->ret type { body }

    // future holds the return type
    std::future<void> fut = std::async(
        []()->void 
    {
        char buf[256];
        sprintf_s(buf, "ASYNC TASK THREAD ID: %d\n", std::this_thread::get_id());
        std::cout << buf;
    });

    char buf[256];
    sprintf_s(buf, "MAIN THREAD ID: %d\n", std::this_thread::get_id());
    std::cout << buf;
   
    // "joins" the async task, "gets" the return value
    // this will block and wait for the task to finish
    fut.get();
}

// std::async to create a bunch of threads
void async_many_threads()
{
    std::vector<std::future<void>> futures;

    // launch 25 async tasks, store their futures in a vector
    for (int i = 0; i < 25; i++)
    {
        // create the tasks using a lambda, pass in i.
        // could use the capture list to capture i, but i'm 
        // passing it in as an argument (int n)
        futures.push_back(std::async(
            [](int n)->void 
        {
            char buf[256];
            sprintf_s(buf, "ASYNC TASK %d -- THREAD ID: %d\n", n + 1, std::this_thread::get_id());
            std::cout << buf;
        }, i));
    }

    char buf[256];
    sprintf_s(buf, "MAIN THREAD ID: %d\n", std::this_thread::get_id());
    std::cout << buf;

    // still call get() on the futures because if the runtime decides
    // to run these synchronously, they won't even run until we call it
    for (auto it = futures.begin(); it != futures.end(); ++it)
    {
        it->get();
    }
}

// std::async using an object and method
void async_object()
{
    simple_object obj;
    std::future<void> fut = std::async(&simple_object::print_name, obj);

    char buf[256];
    sprintf_s(buf, "MAIN THREAD ID: %d\n", std::this_thread::get_id());
    std::cout << buf;

    fut.get();
}

int main()
{
    // uncomment one of these to run
    // async_lambda();
    // async_function();
    // async_many_threads();
    // async_object();

    std::cin.get();
}
