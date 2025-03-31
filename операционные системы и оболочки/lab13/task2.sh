#!/bin/bash

# Компиляция программы
g++ task2.cpp -o task2 -lpthread
if [ $? -ne 0 ]; then
    echo "Compilation failed" >&2
    exit 1
fi

# Запуск программы
./task2
