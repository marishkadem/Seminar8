/* Задача 60. ...Сформируйте трёхмерный массив из неповторяющихся двузначных чисел. Напишите программу, которая будет построчно выводить массив, добавляя индексы каждого элемента.
Массив размером 2 x 2 x 2
66(0,0,0) 25(0,1,0)
34(1,0,0) 41(1,1,0)
27(0,0,1) 90(0,1,1)
26(1,0,1) 55(1,1,1) */
 
bool IsFoundInArray3D(int[,,] a, int value)
{
    int x = a.GetLength(0);
    int y = a.GetLength(1);
    int z = a.GetLength(2);
 
    for (int i = 0; i < x; i++)
    {
        for (int j = 0; j < y; j++)
        {
            for (int k = 0; k < z; k++)
            {
                if (a[i, j, k] == value)
                {
                    return true;
                }
            }
        }
    }
    return false;
}
 
int[,,] FillArray3D(int x, int y, int z, int minRange, int maxRange)
{
    int[,,] result = new int[x, y, z];
    Random random = new Random();
 
    for (int i = 0; i < x; i++)
    {
        for (int j = 0; j < y; j++)
        {
            for (int k = 0; k < z; k++)
            {
                while (true)
                {
                    int value = random.Next(minRange, maxRange);
                    if (IsFoundInArray3D(result, value) == false)
                    {
                        result[i, j, k] = value;
                        break;
                    }
                }
            }
        }
    }
 
    return result;
}
 
void PrintArray3D(int[,,] a)
{
    int x = a.GetLength(0);
    int y = a.GetLength(1);
    int z = a.GetLength(2);
 
    Console.WriteLine();
    for (int k = 0; k < z; k++)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Console.Write($"{a[i, j, k]}{(i, j, k)} ");
            }
 
            Console.WriteLine();
        }
    }
    
    Console.WriteLine();
}
 
var a = FillArray3D(2, 2, 2, 10, 99);
PrintArray3D(a);