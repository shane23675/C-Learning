using System;
using System.Text.RegularExpressions;

namespace _1A2B
{
    class Progress
    {
        private static string answer;
        //產生隨機題目的方法
        public static string createAns()
        {
            while (true)
            {
                //產生一個隨機值最為答案
                Random random = new Random();
                int answerInt = random.Next(0, 9999);
                //若值不足1000，前面補一個零
                if (answerInt < 1000)
                {
                    answer = "0" + answerInt;
                }
                else
                {
                    answer = "" + answerInt;
                }
                //檢查是否有重複數字，若無重複則回傳answer
                if (!CheckRepeat(answer)) { return answer; }
            }
        }
        //檢查輸入值是否合法的方法
        public static bool CheckValid(string input)
        {
            Regex reg = new Regex("\\D");
            if (input.Length != 4 || reg.IsMatch(input))
            {
                return false;
            }
            return true;
        }

        //檢查數值是否有重複的方法(有重複回傳true
        public static bool CheckRepeat(string input)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    if (input[i] == input[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //檢查有幾A幾B的方法
        public static string CheckAB(string guess)
        {
            int ACount = 0, BCount = 0;
            for (int i = 0; i < 4; i++)
            {
                //用cur依序指向guess的每個字符
                char cur = guess[i];
                //依序核對answer
                for (int j=0; j< 4; j++)
                {
                    if (cur == answer[j] )
                    {
                        if(i == j)
                        {
                            ACount += 1;
                        }
                        else
                        {
                            BCount += 1;
                        }
                    }
                }
            }
            return string.Format("結果： {0}A{1}B", ACount, BCount);
        }
    }
    class Program
    {
        static void Main()
        {
            //產生一個題目(在Progress內部)
            string ans = Progress.createAns();
            //先輸出答案
            Console.WriteLine(ans);
            //開始遊戲
            while (true)
            {
                Console.WriteLine("請輸入嘗試值：");
                string guess = Console.ReadLine();
                //檢查點：輸入exit可以退出循環
                if (guess == "exit") { break; }
                //若輸入值不合法則輸出提示
                else if (!Progress.CheckValid(guess))
                {
                    Console.WriteLine("請輸入合法數值!!");
                }
                //若輸入值有重複則輸出提示
                else if(Progress.CheckRepeat(guess))
                {
                    Console.WriteLine("請輸入不重複的數字!!");
                }
                else
                {
                    string result = Progress.CheckAB(guess);
                    if (result == "4A0B")
                    {
                        Console.WriteLine("恭喜答對！！");
                    }
                    else
                    {
                        Console.WriteLine(result);
                    }
                }
            }
        }
    }
}
