#!/bin/bash

# Компиляция C++ программы
g++ task1.cpp -o task1
if [ $? -ne 0 ]; then
    echo "Compilation failed" >&2
    exit 1
fi

# Запуск программы
./task1
