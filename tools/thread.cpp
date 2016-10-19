// Thread.cpp
// Dean Marsinelli
// Learning how to use std::mutex and std::lock_guard

#include <iostream>
#include <thread>
#include <mutex>
#include <vector>

int global_int = 0;
std::mutex global_int_mutex; // protects global_int

void safe_increment(int i)
{
    // could wrap this function in global_int_mutex.lock() and
    // global_int_mutex.unlock(), but lock_guards are used for
    // exception safety. lock() will block the calling thread if
    // the mutex is already locked, whereas try_lock() will return
    // false immediately and allow further execution

    // get the lock
    std::lock_guard<std::mutex> lock(global_int_mutex);
    // this is now a critical section -- safely increment the int
    global_int++;
    std::cout << "THREAD NUM " << i << " ID: " << std::this_thread::get_id() << " -- global_int = " << global_int << std::endl;

    // when lock goes out of scope, mutex is released automatically
}

void unsafe_increment(int i)
{
    // critical section with no lock -- using this creates nonsense
    global_int++;
    std::cout << "THREAD NUM " << i << " ID: " << std::this_thread::get_id() << " -- global_int = " << global_int << std::endl;
}

// 25 std::threads incrementing a shared value using a mutex
void thread_using_mutex()
{
    std::vector<std::thread> threads;

    for (int i = 0; i < 25; i++)
    {
        threads.push_back(std::thread(safe_increment, i + 1));
    }

    for (auto it = threads.begin(); it != threads.end(); it++)
    {
        it->join();
    }

    std::cout << "Final value of global_int = " << global_int << std::endl;
}

// 25 std::threads incrementing a shared value without a mutex
void thread_without_mutex()
{
    std::vector<std::thread> threads;

    for (int i = 0; i < 25; i++)
    {
        threads.push_back(std::thread(unsafe_increment, i + 1));
    }

    for (auto it = threads.begin(); it != threads.end(); it++)
    {
        it->join();
    }
    
    std::cout << "Final value of global_int = " << global_int << std::endl;
}

int main()
{
    // thread_using_mutex();
    // thread_without_mutex();
    std::cin.get();
}
