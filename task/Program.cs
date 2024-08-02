
int[] BubbleSort(int[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        for (int j = 0; j < array.Length - i - 1; j++)
        {
            if (array[j] > array[j + 1])
            {
                int temp = array[j];
                array[j] = array[j + 1];
                array[j + 1] = temp;
            }
        }
        
    }
    return array;
}
void InsertionSort(int[] array)
{
    for (int i = 1; i < array.Length; i++)
    {
        int sorted = array[i];
        int j = i - 1;
        if(j >= 0 && array[j] > sorted)
        {
            array[j + 1] = array[j];
            j--;
        }
        array[j + 1] = sorted;
    }
}
void SelectionSort(int[] array)
{
    for (int i = 0; i < array.Length - 1; i++)
    {
        int minIndex = i;
        for (int j = i + 1; j < array.Length; j++)
        {
            if (array[j] < array[minIndex])
            {
                minIndex = j;
            }
        }
        int temp = array[minIndex];
        array[minIndex] = array[i];
        array[i] = temp;
    }
}
int[] array = { 2, 3, 1, 4 };
InsertionSort(array);
foreach (int i in array)
{
    Console.WriteLine(i);
}

