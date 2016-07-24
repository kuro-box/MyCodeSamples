using K.Helpers.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace S1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var methods = ArgsConverter.ToMethods(args, typeof(Program).Assembly);

                var actions = new List<Action>();
                methods.ForEach((m) => actions.Add(new Action(() => m.Item1.Invoke(m.Item2, null))));

                Parallel.Invoke(actions.ToArray());
            }
            catch (Exception ex)
            {
                var error = ex;
                var errorMessage = error.Message.Replace(Environment.NewLine, ";");
                while (error.InnerException != null)
                {
                    error = error.InnerException;
                    errorMessage = errorMessage.Insert(0, error.Message + Environment.NewLine);
                }

                errorMessage.Print();
            }
            finally
            {
                "Finish, Please enter any key to exit...".Print();
                Console.ReadKey();
            }
        }

        public static void Test()
        {
            "Test".Print();
        }

        public static void T1()
        {
            "T1".Print();
        }

        public static void T2()
        {
            "T2".Print();
        }

        static async void DoSomethingAsync()
        {
            var val = 13;

            await Task.Delay(TimeSpan.FromSeconds(1));

            val *= 2;

            await Task.Delay(TimeSpan.FromSeconds(1));

            Trace.WriteLine(val);
        }

        static async void DoSomethingAsync1()
        {
            var val = 13;

            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);

            val *= 2;

            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);

            Trace.WriteLine(val);
        }
    }
}
