#include <iostream>
#include <unistd.h>
#include <sys/time.h>
#include <sys/wait.h>
#include <cstdlib>
#include <iomanip>

void printTime() {
    struct timeval tv;
    gettimeofday(&tv, nullptr);

    time_t now = tv.tv_sec;
    struct tm *local_time = localtime(&now);

    std::cout << std::setfill('0') 
              << std::setw(2) << local_time->tm_hour << ":"
              << std::setw(2) << local_time->tm_min << ":"
              << std::setw(2) << local_time->tm_sec << ":"
              << std::setw(3) << tv.tv_usec / 1000 << " ";
}

int main() {
    pid_t pid1, pid2;

    std::cout << "Parent process PID: " << getpid() << "\n";

    // Первый fork()
    pid1 = fork();
    if (pid1 == -1) {
        std::cerr << "Failed to fork first child.\n";
        return 1;
    }

    if (pid1 == 0) {
        // Первый дочерний процесс
        printTime();
        std::cout << "Child 1 PID: " << getpid() 
                  << ", Parent PID: " << getppid() << "\n";
        return 0;
    }

    // Второй fork()
    pid2 = fork();
    if (pid2 == -1) {
        std::cerr << "Failed to fork second child.\n";
        return 1;
    }

    if (pid2 == 0) {
        // Второй дочерний процесс
        printTime();
        std::cout << "Child 2 PID: " << getpid() 
                  << ", Parent PID: " << getppid() << "\n";
        return 0;
    }

    // Родительский процесс
    int status;
    waitpid(pid1, &status, 0);
    waitpid(pid2, &status, 0);

    std::cout << "Parent process is running 'ps -x':\n";
    system("ps -x");

    return 0;
}
