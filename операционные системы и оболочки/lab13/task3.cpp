#include <iostream>
#include <cmath>
#include <pthread.h>
#include <fstream>

// Структура для передачи параметров в нить
struct ThreadData {
    int i;
    int K;
    int N;
    int n; // количество членов ряда
    double* result;
};

// Вычисление одного члена ряда Тейлора
double taylorSeries(double x, int n) {
    double term = x;
    double sum = 0.0;
    for (int k = 0; k < n; ++k) {
        if (k > 0) {
            term *= -x * x / (2 * k * (2 * k + 1));  // Рассчитываем каждый новый член ряда
        }
        sum += term;
    }
    return sum;
}

// Функция, которая будет выполняться в каждом потоке
void* computeSin(void* arg) {
    ThreadData* data = static_cast<ThreadData*>(arg);
    double x = 2 * M_PI * data->i / data->N;
    data->result[data->i] = taylorSeries(x, data->n);
    return nullptr;
}

int main() {
    int K, N, n;

    // Считываем входные данные
    std::cout << "Enter K (number of values): ";
    std::cin >> K;
    std::cout << "Enter N (parameter for the formula): ";
    std::cin >> N;
    std::cout << "Enter n (number of terms in the Taylor series): ";
    std::cin >> n;

    // Массив для хранения результатов
    double* result = new double[K];

    // Массив для хранения потоков
    pthread_t* threads = new pthread_t[K];
    ThreadData* threadData = new ThreadData[K];

    // Создаем потоки для вычисления каждого значения
    for (int i = 0; i < K; ++i) {
        threadData[i] = {i, K, N, n, result};
        if (pthread_create(&threads[i], nullptr, computeSin, &threadData[i]) != 0) {
            std::cerr << "Failed to create thread " << i << std::endl;
            return 1;
        }
    }

    // Ожидаем завершения всех потоков
    for (int i = 0; i < K; ++i) {
        pthread_join(threads[i], nullptr);
    }

    // Суммируем результаты и записываем их в файл
    std::ofstream outFile("results.txt");
    double totalSum = 0;
    for (int i = 0; i < K; ++i) {
        outFile << "y[" << i << "] = " << result[i] << std::endl;
        totalSum += result[i];
    }
    outFile << "Total sum: " << totalSum << std::endl;
    outFile.close();

    // Очистка
    delete[] result;
    delete[] threads;
    delete[] threadData;

    std::cout << "Results have been written to results.txt" << std::endl;
    return 0;
}
