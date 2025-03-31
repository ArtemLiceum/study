#!/bin/bash
# Проверка, что переданы имена входного и выходного файлов
if [ "$#" -ne 2 ]; then
  echo "Использование: $0 <входной_файл> <выходной_файл>"
  exit 1
fi
INPUT_FILE=$1
OUTPUT_FILE=$2
# Проверка существования входного файла
if [ ! -f "$INPUT_FILE" ]; then
  echo "Ошибка: файл $INPUT_FILE не найден!"
  exit 1
fi
# Обнуление содержимого выходного файла
> "$OUTPUT_FILE"
# Инициализация переменных для подсчёта строк и слов
total_lines=0
total_words=0
# Чтение входного файла построчно
while IFS= read -r line || [ -n "$line" ]; do
  # Подсчёт числа слов в строке
  word_count=$(echo "$line" | wc -w)
  # Увеличение счётчиков строк и слов
  total_lines=$((total_lines + 1))
  total_words=$((total_words + word_count))
  # Запись результатов по строкам в выходной файл
  echo "$total_lines. $word_count слов(а)" >> "$OUTPUT_FILE"
done < "$INPUT_FILE"
# Запись итогов в выходной файл
echo "итого: $total_lines строк(и) $total_words слов(а)" >> "$OUTPUT_FILE"
# Сообщение о завершении
echo "Результаты записаны в $OUTPUTFILE"
