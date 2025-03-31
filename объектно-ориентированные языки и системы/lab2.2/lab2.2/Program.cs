using System;
using System.IO;

public class TreeNode
{
    public string EnglishWord { get; set; }
    public string RussianWord { get; set; }
    public TreeNode Left { get; set; }
    public TreeNode Right { get; set; }

    public TreeNode(string english, string russian)
    {
        EnglishWord = english;
        RussianWord = russian;
        Left = null;
        Right = null;
    }
}

public class BinaryTree
{
    private TreeNode root;

    public void Add(string english, string russian)
    {
        root = AddRecursive(root, english, russian);
    }

    private TreeNode AddRecursive(TreeNode node, string english, string russian)
    {
        if (node == null)
        {
            return new TreeNode(english, russian);
        }

        int comparison = string.Compare(english, node.EnglishWord);
        if (comparison < 0)
        {
            node.Left = AddRecursive(node.Left, english, russian);
        }
        else if (comparison > 0)
        {
            node.Right = AddRecursive(node.Right, english, russian);
        }

        return node;
    }

    public bool Remove(string english)
    {
        root = RemoveRecursive(root, english);
        return root != null;
    }

    private TreeNode RemoveRecursive(TreeNode node, string english)
    {
        if (node == null) return null;

        int comparison = string.Compare(english, node.EnglishWord);
        if (comparison < 0)
        {
            node.Left = RemoveRecursive(node.Left, english);
        }
        else if (comparison > 0)
        {
            node.Right = RemoveRecursive(node.Right, english);
        }
        else
        {
            // Удаление узла
            if (node.Left == null) return node.Right;
            if (node.Right == null) return node.Left;

            node.EnglishWord = FindMin(node.Right).EnglishWord;
            node.RussianWord = FindMin(node.Right).RussianWord;
            node.Right = RemoveRecursive(node.Right, node.EnglishWord);
        }

        return node;
    }

    private TreeNode FindMin(TreeNode node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }

    public string Search(string english)
    {
        var node = SearchRecursive(root, english);
        return node?.RussianWord ?? "Слово не найдено.";
    }

    private TreeNode SearchRecursive(TreeNode node, string english)
    {
        if (node == null) return null;

        int comparison = string.Compare(english, node.EnglishWord);
        if (comparison < 0)
        {
            return SearchRecursive(node.Left, english);
        }
        else if (comparison > 0)
        {
            return SearchRecursive(node.Right, english);
        }
        else
        {
            return node;
        }
    }

    public void InOrderTraversal(Action<string, string> action)
    {
        InOrderTraversalRecursive(root, action);
    }

    private void InOrderTraversalRecursive(TreeNode node, Action<string, string> action)
    {
        if (node != null)
        {
            InOrderTraversalRecursive(node.Left, action);
            action(node.EnglishWord, node.RussianWord);
            InOrderTraversalRecursive(node.Right, action);
        }
    }

    public void LoadFromFile(string path)
    {
        var lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            var parts = line.Split(new[] { ',' }, 2);
            if (parts.Length == 2)
            {
                Add(parts[0].Trim(), parts[1].Trim());
            }
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        var dictionary = new BinaryTree();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Англо-русский словарь");
            Console.WriteLine("1 - Добавить слово");
            Console.WriteLine("2 - Удалить слово");
            Console.WriteLine("3 - Найти слово");
            Console.WriteLine("4 - Показать все слова");
            Console.WriteLine("5 - Загрузить из файла");
            Console.WriteLine("0 - Выйти");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Введите английское слово: ");
                    var english = Console.ReadLine();
                    Console.Write("Введите русский перевод: ");
                    var russian = Console.ReadLine();
                    dictionary.Add(english, russian);
                    Console.WriteLine("Слово добавлено!");
                    break;
                case "2":
                    Console.Write("Введите английское слово для удаления: ");
                    var wordToRemove = Console.ReadLine();
                    if (dictionary.Remove(wordToRemove))
                        Console.WriteLine("Слово удалено!");
                    else
                        Console.WriteLine("Слово не найдено.");
                    break;
                case "3":
                    Console.Write("Введите английское слово для поиска: ");
                    var wordToSearch = Console.ReadLine();
                    var translation = dictionary.Search(wordToSearch);
                    Console.WriteLine($"Перевод: {translation}");
                    break;
                case "4":
                    Console.WriteLine("Все слова в словаре:");
                    dictionary.InOrderTraversal((eng, rus) =>
                    {
                        Console.WriteLine($"{eng} - {rus}");
                    });
                    Console.ReadLine();
                    break;
                case "5":
                    Console.Write("Введите путь к файлу: ");
                    var filePath = Console.ReadLine();
                    try
                    {
                        dictionary.LoadFromFile(filePath);
                        Console.WriteLine("Слова успешно загружены!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при загрузке из файла: {ex.Message}");
                    }
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }

            Console.WriteLine("Нажмите клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}