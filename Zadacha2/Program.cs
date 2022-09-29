/* Задача 56: Задайте прямоугольный двумерный массив. Напишите программу, 
которая будет находить строку с наименьшей суммой элементов.
 
Например, задан массив:
 
1 4 7 2
 
5 9 2 3
 
8 4 2 4
 
5 2 6 7
 
Программа считает сумму элементов в каждой строке и выдаёт номер
строки с наименьшей суммой элементов: 1 строка */
 
int[,] FillTable(int rows, int cols, int minRange, int maxRange)                  // Метод для заполнения случ. числами 2-мерного массива, матрицы; принимает аргументы - кол-во строк и кол-во стодбцов.
{
    var result = new int[rows, cols];
    Random random = new Random();
 
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            result[i, j] = random.Next(minRange, maxRange + 1);
        }
    }
 
    return result;
}
 
int[] GetLongestNrInCols(int[,] m)                  // Метод для поиска самого "длинного"(просто сравнимаем модули эл-тов) числа в каждом столбце; принимает матрицу, на выходе - одномерный массив эл-тами, явл. самыми большими по модулю в j столбце.
{
    int nRows = m.GetLength(0);                     // записываем в перем. размерность/количество строк матрицы;
    int nCols = m.GetLength(1);                     // записываем в перем. размерность/количество столбцов матрицы;
    var result = new int[nCols];
 
    for (int j = 0; j < nCols; j++)
    {
        var LongestInCol = m[0, j];                 // для каждого j-го столбца начинаем с 0-ой строки;
 
        for (int i = 1; i < nRows; i++)             // продолжаем поиск(сравнение) более длинных эле-тов с 1-ой строки (j-го столбца);
        {
            if (Math.Abs((m[i, j])) > Math.Abs(LongestInCol))   // Math.Abs() - находит модуль числа
            {
                LongestInCol = m[i, j];
            }
        }
 
        result[j] = LongestInCol;                   // записываем j-ый (самый большой по модулю в j-м стобле матрицы) эл-т в одномерный возвращаемый массив-результат;
    }
 
    return result;
}
 
int GetNrOfDigits(int number)                       // Метод возвращает число целых десятичных разрядов в числе.
{
    int digits = 1;                                 // В любом целом десятичном числе хотя бы 1 (0-ой) разряд,
 
    for (int i = number; i >= 10 || i <= -10; digits++) // поэтому здесь в условии делим на 10 исходное число пока оно не менее 10 по модулю, т.е. i >= |10|, а когда стало меньше, то дальше уже делить не надо (т.е. не делим пока не получим 0 - для разряда единиц) - условились, что у нас на этот случай уже есть 1, т.е. начинаем с количества разрядов 1 (хотя бы 1 разряд...).
    {
        i = i / 10;
    }
 
    return digits;
}
 
void PrintMatrixMyWay(int[,] m)                     // Метод выводит в консоль матрицу. Cтолбцы выравнены по правому краю.
{
    int nRows = m.GetLength(0);
    int nCols = m.GetLength(1);
    Console.WriteLine($"\nВаша матрица размерности {nRows}x{nCols}, заполненная целыми числами:\n");
    var LongestNrInCols = GetLongestNrInCols(m);
    int[] spaces = new int[nCols];                  // массив ширин столбцов, т.е. сколько "пробелов" надо заполнить, т.е. макс. число символов (пробелов, знаков, цифр) для j-столбца, чтобы сделать выравнивание колонок по правому краю;                               
 
    for (int j = 0; j < nCols; j++)                 // идём по столбцам, j;
    {
        spaces[j] = GetNrOfDigits(LongestNrInCols[j]) + 1; //  заполняем массив ширин для каждого J-го столбца, исходя из числа цифр самого "длиного" числа в столбце; добавляем 1, чтобы учесть место под возможный знак минус;
    }
 
    for (int i = 0; i < nRows; i++)                 // начинаем печатать... идём по строкам;
    {
        string currentRow = String.Empty;           // текущая строка, вначале пустая
 
        for (int j = 0; j < nCols; j++)             // для каждой строки идём по столбцам;
        {
            string leftIndent = String.Empty;                 // переменная "левые отступы", в которую будем набирать отступы слева (в виде пробелов), чтобы обеспечить выравнивание эл-ов матрицы по правому краю;
            int nIndents = spaces[j] - GetNrOfDigits(m[i, j]); // считаем число отсупов, как разность между шириной для данного j-го столбца и кол-вом знаков текущего i-го j-го эл-та мамтрицы;
 
            for (int n = 0; n < nIndents; n++)      // в массиве n раз до числа отступов набираем отступы в переменную "левые отступы";
            {
                leftIndent += " ";
            }
 
            if (m[i, j] < 0 && nIndents != 0)       // если i-ый j-ый эл-т матрицы отрицательный и число отступов не равнo нулю, и соотв. в перем. "левые отсупы" добавился хотя бы 1 пробел... =>  
            {
                leftIndent = leftIndent.Remove(nIndents - 1, 1); // => убираем один левый отступ(пробел), место которого слева от первой цифры числа займёт знак минус (так как число отрицательное)    
            }
 
            currentRow += leftIndent + $"{m[i, j]} ";         // добавляем в текущую строку (j-го столбца i-ой строки) эл-ты матрицы путём склеивания "левых отступов", текущего эл-та и 1-го пробела справа (чтобы отделить столбцы/ эл-ты в строке);
        }
 
        string spareLine = String.Empty;            // Строка-отсуп между строками матрицы.
 
        foreach (char ch in currentRow)             // Набираем строку отсуп пробелами до длинны текущей строки.
        {
            spareLine += ' ';
        }
 
        Console.WriteLine("|" + currentRow + "|");  // Выводим текущую строку с границей матрицы.
 
        if (i != nRows - 1)                         // Если строка последняя, то после неё не нужена строка отсуп c границей.
        {
            Console.WriteLine("|" + spareLine + "|"); // Выводим строку отсуп.
        }
    }
 
    Console.WriteLine();
}
 
(int[], int) GetMinSummRowNr(int[,] m)              // Метод выводить кортеж - (массив сумм эл-тов строк матрицы, номер строки с наименьшей суммой)
{
    int rows = m.GetLength(0);
    int cols = m.GetLength(1);
    int[] RowSumms = new int[rows];
    int minRowSumm = RowSumms[0];
    int result = 0;
 
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            RowSumms[i] += m[i, j];
        }
 
        if (RowSumms[i] < minRowSumm)
        {
            minRowSumm = RowSumms[i];
            result = i + 1;
        }
    }
 
    return (RowSumms, result);
}
 
void ArrayOutput(int[] a, string message)           // Метод, чтобы аккуратно вывести решение
{
    Console.WriteLine($"{message}\n");
    int longest = a[0];
 
    for (int i = 1; i < a.Length; i++)
    {
        if (Math.Abs(a[i]) > Math.Abs(longest))
        {
            longest = a[i];
        }
    }
 
    int spaces = GetNrOfDigits(longest) + 1;
    for (int i = 0; i < a.Length; i++)
    {
        string leftindents = String.Empty;
        int nIndents = spaces - GetNrOfDigits(a[i]);
       
        for (int n = 0; n < nIndents; n++)
        {
            leftindents += " ";
        }
 
        if (a[i] < 0 && nIndents != 0)
        {
            leftindents = leftindents.Remove(nIndents - 1, 1);
        }
        Console.WriteLine($"[{leftindents}{a[i]} ]");
    }
}
 
var matrix = FillTable(rows: 4, cols: 4, minRange: -1000, maxRange: 1000);
PrintMatrixMyWay(matrix);
 
(int[] RowSums, int minSummRowNr) = GetMinSummRowNr(matrix);
 
ArrayOutput(RowSums, "Массив сумм эл-тов по строкам:");
Console.WriteLine($"\nНомер строки с наименьшей суммой элементов: {minSummRowNr}\n");



 