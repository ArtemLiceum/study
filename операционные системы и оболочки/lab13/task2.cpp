#include <iostream>
#include <fstream>
#include <pthread.h>
#include <unistd.h>
#include <sys/time.h>
#include <string>
#include <vector>
#include <iomanip>

// Имя файла для записи данных
const std::string FILE_NAME = "output.txt";
const int NUM_LINES = 100;

// Массив строк для потоков
std::vector<std::string> fileLines;
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER; // Мьютекс для синхронизации потоков

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

// Функция для записи данных в файл
void writeToFile() {
    std::ofstream outFile(FILE_NAME);
    if (!outFile.is_open()) {
        std::cerr << "Failed to open file for writing.\n";
        exit(1);
    }

    pid_t pid = getpid();
    for (int i = 1; i <= NUM_LINES; ++i) {
        struct timeval tv;
        gettimeofday(&tv, nullptr);

        time_t now = tv.tv_sec;
        struct tm *local_time = localtime(&now);

        outFile << i << " " << pid << " "
                << std::setfill('0') 
                << std::setw(2) << local_time->tm_hour << ":"
                << std::setw(2) << local_time->tm_min << ":"
                << std::setw(2) << local_time->tm_sec << ":"
                << std::setw(3) << tv.tv_usec / 1000 << "\n";

        // Вывод строк в левой части экрана
        std::cout << i << " " << pid << " ";
        printTime();
        std::cout << "\n";
        usleep(50000); // Задержка для наглядности (50 мс)
    }

    outFile.close();
}

// Функция, которую выполняют потоки
void* readFromFile(void* arg) {
    int threadID = *(int*)arg;

    while (true) {
        pthread_mutex_lock(&mutex); // Блокировка доступа

        if (fileLines.empty()) {
            pthread_mutex_unlock(&mutex); // Разблокировка доступа
            break;
        }

        // Чтение строки
        std::string line = fileLines.back();
        fileLines.pop_back();

        pthread_mutex_unlock(&mutex); // Разблокировка доступа

        // Вывод строки с ID потока и временем
        std::cout << "Thread " << threadID << " ";
        printTime();
        std::cout << line << "\n";
        usleep(50000); // Задержка для наглядности (50 мс)
    }

    return nullptr;
}

int main() {
    // Создаем файл и записываем данные
    writeToFile();

    // Читаем строки из файла в массив
    std::ifstream inFile(FILE_NAME);
    if (!inFile.is_open()) {
        std::cerr << "Failed to open file for reading.\n";
        exit(1);
    }

    std::string line;
    while (std::getline(inFile, line)) {
        fileLines.push_back(line);
    }
    inFile.close();

    // Создание потоков
    pthread_t threads[2];
    int threadIDs[2] = {1, 2};

    for (int i = 0; i < 2; ++i) {
        if (pthread_create(&threads[i], nullptr, readFromFile, &threadIDs[i]) != 0) {
            std::cerr << "Failed to create thread " << i + 1 << ".\n";
	}
    }
}

