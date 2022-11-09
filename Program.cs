class Program
{
    static int headBit(int Val)
    {
        int BitNum = 31;
        uint CmpVal = 1u << BitNum;
        while (Val < CmpVal)
        {
            CmpVal >>= 1;
            BitNum--;
        }
        return BitNum;
    }

    static int DividePolynomMod(int dividend, int divider) // делимое, делитель
    {
        int m = headBit(divider);
        int k = headBit(dividend) - m;
        if (k < 0)
        {
            return dividend;
        }
        while (k >= 0)
        {
            int divider_tmp = divider << k;
            dividend = dividend ^ divider_tmp;
            k = headBit(dividend) - m;
        }
        return dividend;
    }


    static int NumWeight(int num)
    {
        int weight = 0;
        char[] StNum = Convert.ToString(num, 2).ToCharArray();
        for (int i = 0; i < StNum.Length; i++)
        {
            if (StNum[i] == '1') { weight++; }
        }
        return weight;
    }

    static void Main()
    {
        int g, e, m, c, s, d, output, lenOut, mess;
        Console.WriteLine("Введите g(x)");
        g = Convert.ToInt32(Console.ReadLine(), 2);
        Console.WriteLine("Введите d");
        d = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите m");
        m = Convert.ToInt32(Console.ReadLine(), 2);
        Console.WriteLine("Введите e");
        e = Convert.ToInt32(Console.ReadLine(), 2);
        m = m << headBit(g);
        c = DividePolynomMod(m, g);
        Console.WriteLine("Контрольная сумма:\nс = " + Convert.ToString(c, 2));
        output = m | c;
        lenOut = Convert.ToString(output, 2).Length;
        Console.WriteLine("На выходе кодера:\na = " + Convert.ToString(output, 2));
        m = output ^ e;
        Console.WriteLine("На выходе канала:\nb = " + Convert.ToString(m, 2));
        s = DividePolynomMod(m, g);
        Console.WriteLine("Синдром:\ns = " + Convert.ToString(s, 2));
        if (s == 0)
        {
            Console.WriteLine("E = 0, ошибки не обнаружены");
        }
        else
        {
            Console.WriteLine("E = 1, ошибки  обнаружены");
        }

        Console.WriteLine("----------------------------------");

        int MaxValue = Convert.ToInt32(Math.Pow(2, lenOut)) - 1;
        List<int> nums = Enumerable.Range(1, d - 1).ToList();
        List<int> ans = new List<int>();
        int count = 0;
        mess = 0;
        while (count <= MaxValue)
        {
            mess = output ^ count;
            foreach (int j in nums)
            {
                if (NumWeight(count) == j && DividePolynomMod(mess, g) == 0)
                {
                    ans.Add(count);
                }
            }
            mess = 0;
            count++;
        }
        if (ans.Count > 0)
        {
            Console.WriteLine("Список всех возможных векторов ошибок, для которыйх w(e)<=(d-1) и кодер не обнаружит их:");
            foreach (int i in ans)
            {
                Console.WriteLine(Convert.ToString(i, 2));
            }
        }
        else { Console.WriteLine("Нет векторов удовлетворяющих условию:"); }
    }
}
