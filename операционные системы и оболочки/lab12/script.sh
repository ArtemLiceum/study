#!/bin/bash

# Проверка аргументов
if [ "$#" -ne 2 ]; then
    echo "Использование: $0 <путь к каталогу> <имя файла>"
    exit 1
fi

DIRECTORY=$1
FILENAME=$2

# Проверка существования каталога
if [ ! -d "$DIRECTORY" ]; then
    echo "Ошибка: Каталог $DIRECTORY не существует или не является каталогом."
    exit 1
fi

# Компиляция C++ программы
g++ -o find_file find_file.cpp
if [ $? -ne 0 ]; then
    echo "Ошибка: Компиляция C++ программы не удалась."
    exit 1
fi

# Запуск программы
./find_file "$DIRECTORY" "$FILENAME"
if [ $? -ne 0 ]; then
    echo "Ошибка: Программа завершилась с ошибкой."
    exit 1
fi
