// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
int myint = 555;
Console.WriteLine(myint);

string mystr = Console.ReadLine();
Console.WriteLine(mystr);
Console.WriteLine(GetAwg([1,2,3,4,5,6]));

static double GetAwg(int[] arr)
{
    double sum = 0;
    foreach (int var in arr)
    {
        sum += var;
    }

    return sum / arr.Length;
}