/* Задача 62. Напишите программу, которая заполнит спирально массив 4 на 4.
Например, на выходе получается вот такой массив:
01 02 03 04
12 13 14 05
11 16 15 06
10 09 08 07 */
 
int[] GetMaxStrLengthInCols(string[,] m)            // Метод находит максимальную длину строк-элементов матрицы для каждого столбца и записывает эти длины в одномерный массив,
                                                    // нужно чтобы учесть ширину столбца матрицы из строк - для выравнивния при выводе матрицы в консоль;
{
    int nRows = m.GetLength(0);
    int nCols = m.GetLength(1);
 
    int[] result = new int[m.GetLength(1)];
 
    for (int j = 0; j < nCols; j++)
    {
        var maxLength = m[0, j].Length;
        for (int i = 0; i < nRows; i++)
        {
            if (m[i, j] is not null && m[i, j].Length > maxLength)
            {
                maxLength = m[i, j].Length;
            }
        }
        result[j] = maxLength;
    }
 
    return result;
}
 
void PrintMatrixOfStings(string[,] m, string message)               // Метод выводит в консоль матрицу строковых эл-тов;
{
    int nRows = m.GetLength(0);
    int nCols = m.GetLength(1);
    Console.WriteLine(message);
    int[] spaces = GetMaxStrLengthInCols(m);
 
    for (int i = 0; i < nRows; i++)
    {
        string currentRow = String.Empty;
        for (int j = 0; j < nCols; j++)
        {
            if (m[i, j] is not null)
            {
                currentRow += m[i, j].PadLeft(spaces[j], ' ') + ' ';
            }
            else
            {
                currentRow += String.Empty.PadLeft(spaces[j], ' ') + ' ';
            }
        }
 
        string spareLine = String.Empty;
        foreach (char ch in currentRow)
        {
            spareLine += ' ';
        }
 
        //spareLine.PadLeft(currentRow.Length, ' ');
        Console.WriteLine("| " + currentRow + "|");
        if (i != nRows - 1)
        {
            Console.WriteLine("| " + spareLine + "|");
        }
    }
 
    Console.WriteLine();
}
 
string[,] SpiralFillSquareTable(int side)           // заполняет спирально квадратную матрицу со стороной side;
{
    string[,] m = new string[side, side];
 
    int q = 0;                                      // переменная для инкремента значений эл-ов;
    int a = 1;                                      // переменная для изменения направления прохода строк/столбцов;
    int n = 0;                                      // переменная для перескакивания (от итерации к итерации) между строками, начинаем с нулевой строки;
 
    int nRows = m.GetLength(0);                     //  чтобы видеть где столбцы, а где строки... и задел на переделку к с случаю с неквадратными матрицами;
    int nCols = m.GetLength(1);                     //
 
    for (; side > 0; side--, n += a * side, a *= -1)    // цикл проходов, каждая итерация состоит из заполнения крайних строки и столбца длиной side: 
    {                                                   // после каждой итерации side уменьшается на 1, n поочерёдно (за счёт a) увеличиваеися или уменьшается на (новый) side,
                                                        // переменная a меняет знак, чтобы проход на следующей итерации менялся от возрастания(вправо-вниз) к убыванию(влево-вверх);
                                                        // при проходе враво-вниз (а=1) задействуется правая часть &&-условий вложенных циклов, идём до индекса текущей границы (строки, а потом столбца), 
                                                        // которая справа для строки и внизу для столбца - по направлению возрастания индексов, - левая часть &&-условия при этом выполняется из-за -1*a;
                                                        // соотв., при проходе влево-вверх (a=1) задействуется левая часть &&-условий, идём до текущей границы, которая слева и вверху... - по направлению убывания индексов,
                                                        // правая часть &&-условий при этом выполняется из-за j/i*a;
 
        //Console.Write($"\nEntrd! : side = {side}, n = {n}, Jo = {nCols - 1 - n}\ta = {a}\n");
 
        if (side == 1 && n == nCols - 1 - n)            // для случая когда изначальный параметр side, сторона матрицы, равен нечётному числу: 
                                                        // тогда индекс строки начала прохода совпадает с индексом столбца продолжения прохода..., 
                                                        // в результате не выполняются условия обоих следующих циклов и середина таблицы остаётся пустой;
        {
            m[n, nCols - 1 - n] = (nRows * nCols).ToString();
        }
 
        for (int j = n; j > -1 * a * (nCols - 1 - n) && a * j < nCols - 1 - n; j += a) // проходим по столбцам - заполняем строку n
        {
 
            if (q++ < 10)                           // для значений меньше 10 добавляем незначащий 0, как в примере;/
            {
                m[n, j] = q.ToString().PadLeft(2, '0');
                //Console.Write($"col: {j}, ");
            }
            else
            {
                m[n, j] = q.ToString();
                //Console.Write($"col: {j}, ");
            }
        }
 
        for (int i = n; i > -1 * a * (nRows - 1 - n) && a * i < nRows - 1 - n; i += a) // проходим по строкам - заполняем столбец m.GetLength(1) - 1 - n
        {
            if (m[i, nCols - 1 - n] == null)   // так как при проходе cтолбец имеют пересечение со строкой (в точке границы/"поворота"), то перед заполнением ячейки матрицы нужно убедится, что она пустая;
            {
                if (q++ < 10)                           // для значений меньше 10 добавляем незначащий 0, как в примере;
                {
                    m[i, nCols - 1 - n] = q.ToString().PadLeft(2, '0');
                    //Console.Write($"row: {i}, ");
                }
                else
                {
                    m[i, nCols - 1 - n] = q.ToString();
                    //Console.Write($"row: {i}, ");
                }
            }
        }
    }
 
    Console.WriteLine();
    return m;
}
 
int side = 9;
var m = SpiralFillSquareTable(side);
PrintMatrixOfStings(m, $"\nВаша квадратная матрица со стороной {side}, заполненная спирально:\n");