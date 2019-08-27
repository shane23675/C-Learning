using System;
using System.Text.RegularExpressions;
using System.Collections;

namespace _1A2B
{
    class Progress
    {
        private static string answer;
        private static string record="";
        private static int trial = 0;
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
            trial++;
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
            //進行記錄
            string result = string.Format("{0}A{1}B", ACount, BCount);
            record += trial + ".\t" + guess + "\t" + result + "\n";
            return result;
        }
        //顯示紀錄的方法
        public static void ShowRecord()
        {
            Console.WriteLine("歷史紀錄：\n" + record);
        }
    }
    //提示類：用來計算剩餘可能，協助作答
    class Hint
    {
        private static ArrayList allPossibilities = new ArrayList();
        private static ArrayList remainPossibilities = new ArrayList();
        //初始化方法：產生所有可能的答案
        public static void Init()
        {
            //產生四位數字串num
            string num;
            for (int i = 100; i < 9999; i++)
            {
                if (i < 1000)
                {
                    num = "0" + i;
                }
                else
                {
                    num = "" + i;
                }
                //檢測是否重複，無重複則加入allPossiblities
                if (!Progress.CheckRepeat(num))
                {
                    allPossibilities.Add(num);
                }
            }
        }
        //過濾出可能答案的方法
        public static void Filter(string guess, string result)
        {
            foreach (string p in allPossibilities)
            {
                int ACount = 0;
                int BCount = 0;
                for (int i=0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (p[i] == guess[j])
                        {
                            if(i == j)
                            {
                                ACount++;
                            }
                            else
                            {
                                BCount++;
                            }
                        }
                    }
                }
                //判斷是否留下
                if (ACount.ToString()[0] == result[0] && BCount.ToString()[0] == result[2])
                {
                    //AB值與result相同，可能為答案，加入remainPossibilities
                    remainPossibilities.Add(p);
                }
            }
            //結束篩選過程
            allPossibilities = remainPossibilities;
            remainPossibilities = new ArrayList();
        }
        //顯示出可能答案的方法
        public static void ShowAllPossibilities()
        {
            Console.WriteLine("剩餘的可能答案：");
            int count = 0;
            foreach(string p in allPossibilities)
            {
                count++;
                Console.Write(p + "\t");
                if (count % 6 == 0)
                {
                    Console.Write("\n");
                }
            }
            Console.WriteLine("");
        }
    }
    class Program
    {
        //主程序
        static void Main()
        {
            //產生一個題目(在Progress內部)
            string ans = Progress.createAns();
            //初始化提示程序
            Hint.Init();
            //測試點：先輸出答案
            //Console.WriteLine(ans);
            //開始遊戲
            while (true)
            {
                Console.WriteLine("請輸入嘗試值：");
                string guess = Console.ReadLine();
                //輸入list：顯示歷史紀錄
                if (guess == "list")
                {
                    Progress.ShowRecord();
                }
                //輸入hint：提示剩餘可能列表
                else if (guess == "hint")
                {
                    Hint.ShowAllPossibilities();
                }
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
                    //合法輸入，檢查對應AB值並返回結果
                    string result = Progress.CheckAB(guess);
                    //進行提示篩選
                    Hint.Filter(guess, result);
                    if (result == "4A0B")
                    {
                        Console.WriteLine("恭喜答對！！");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("結果： "+result);
                    }
                }
            }
        }
    }
}
