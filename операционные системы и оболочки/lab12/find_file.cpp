#include <iostream>
#include <filesystem>
#include <fstream>
#include <sys/stat.h>
#include <unistd.h>

namespace fs = std::filesystem;

void print_file_info(const fs::path& file_path) {
    struct stat file_stat;
    if (stat(file_path.c_str(), &file_stat) != 0) {
        std::cerr << "Ошибка: Не удалось получить информацию о файле " << file_path << "\n";
        return;
    }

    std::cout << "Путь: " << file_path << "\n";
    std::cout << "Имя файла: " << file_path.filename() << "\n";
    std::cout << "Размер: " << file_stat.st_size << " байт\n";
    std::cout << "Дата создания: " << ctime(&file_stat.st_ctime);
    std::cout << "Права доступа: " << std::oct << (file_stat.st_mode & 0777) << "\n";
    std::cout << "Индексный дескриптор: " << file_stat.st_ino << "\n\n";
}

void search_directory(const fs::path& dir_path, const std::string& file_name, int& file_count, int& dir_count) {
    try {
        for (const auto& entry : fs::directory_iterator(dir_path)) {
            if (entry.is_directory()) {
                ++dir_count;
                search_directory(entry.path(), file_name, file_count, dir_count);
            } else if (entry.is_regular_file()) {
                ++file_count;
                if (entry.path().filename() == file_name) {
                    print_file_info(entry.path());
                }
            }
        }
    } catch (const std::exception& e) {
        std::cerr << "Ошибка: " << e.what() << " при обработке " << dir_path << "\n";
    }
}

int main(int argc, char* argv[]) {
    if (argc != 3) {
        std::cerr << "Использование: " << argv[0] << " <путь к каталогу> <имя файла>\n";
        return EXIT_FAILURE;
    }

    const fs::path dir_path = argv[1];
    const std::string file_name = argv[2];

    if (!fs::exists(dir_path) || !fs::is_directory(dir_path)) {
        std::cerr << "Ошибка: Указанный путь не существует или не является каталогом.\n";
        return EXIT_FAILURE;
    }

    int file_count = 0;
    int dir_count = 0;

    std::cout << "Поиск файла '" << file_name << "' в каталоге '" << dir_path << "' и его подкаталогах...\n";
    search_directory(dir_path, file_name, file_count, dir_count);

    std::cout << "Общее количество просмотренных файлов: " << file_count << "\n";
    std::cout << "Общее количество просмотренных каталогов: " << dir_count << "\n";

    return EXIT_SUCCESS;
}