using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetermineStateMachine
{
    public static class MyMethods
    {
        public static bool ContainsValueMethod(this Dictionary<string, HashSet<string>> container,HashSet<string> elem)
        {
            foreach (var item in container)
            {
                if (item.Value.SetEquals(elem))
                    return true;
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<(string, string), string> transitionFunction =
                new Dictionary<(string, string), string>();

            Dictionary<(string, string), HashSet<string>> inputTransitionFunction =
                 new Dictionary<(string, string), HashSet<string>>();

            Dictionary<string, HashSet<string>> stateNames =
                new Dictionary<string, HashSet<string>>();

            var inputTable = File.ReadAllLines(args[0]).Select(x => x.Split(';'));
            var simbols = inputTable.First();//array of simbols
            //creating input function
            foreach (var item in inputTable.Skip(1))
            {                
                for (int i = 1; i < item.Length; i++)
                {
                    var mult = item[i].Split(',');
                    if (mult[0] == "" && mult.Length == 1)
                    {
                        mult = new string[] { };
                    }
                    var res = new HashSet<string>(mult);
                    inputTransitionFunction[(item[0], simbols[i])] =
                        res;
                }
            }

            
            int index = 0;//index for a new states naming
            Queue<string> processQueue = new Queue<string>();

            processQueue.Enqueue(inputTable.Skip(1).First().First());
            stateNames[inputTable.Skip(1).First().First()] = new HashSet<string>() { inputTable.Skip(1).First().First() };
            inputTable = inputTable.Skip(1);
            // main algorithm
            while (true)
            {
                if (processQueue.Count == 0)
                {
                    break;
                }
                var currentSate = processQueue.Dequeue();
                
                for (int i = 1; i < simbols.Length; i++)
                {
                    HashSet<string> res = new HashSet<string>();
                    foreach (var item in stateNames[currentSate])
                    {
                        res.UnionWith(inputTransitionFunction[(item, simbols[i])]); 
                    }
                    if (!stateNames.ContainsValueMethod(res))
                    {
                        string newState = $"Q{index++}";
                        transitionFunction[(currentSate, simbols[i])] = newState;
                        stateNames[newState] = res;
                        processQueue.Enqueue(newState);
                    }
                }          
            }

            foreach (var item in transitionFunction)
            {
                Console.WriteLine($"f({item.Key.Item1},{item.Key.Item2}) = {item.Value}");
            }

        }
    }
}
