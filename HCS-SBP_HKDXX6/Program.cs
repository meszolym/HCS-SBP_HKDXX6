using HCS_SBP_HKDXX6.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace HCS_SBP_HKDXX6
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var n = 12;
            var contain = new Point[n];
            for (var i = 0; i < n; i++)
            {
                contain[i] = new Point(Random.Shared.NextDouble() * 100, Random.Shared.NextDouble() * 100);
            }
            Console.WriteLine("Starting points");
            Console.WriteLine(PointArrayToString(contain));

            

            //RunOnce(solver);
            //var tasks = Enumerable.Range(0, 10).Select(i => new Task(() => RunForTime(TimeSpan.FromSeconds(60), new Random(i), contain), TaskCreationOptions.LongRunning)).ToArray();

            //foreach (var task in tasks)
            //{
            //    task.Start();
            //}
            //Task.WaitAll(tasks);

            RunForTime(TimeSpan.FromSeconds(60), Random.Shared, contain);

        }

        public static string PointArrayToString(Point[] points)
        {
            var sb = new StringBuilder();
            sb.Append('{');
            foreach (var point in points)
            {
                sb.Append($"{point}, ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append('}');
            return sb.ToString();
        }

        private static void RunOnce(SBPSolverHCS solver)
        {
            var sol = solver.Optimize(Random.Shared);
            Console.WriteLine();
            Console.WriteLine($"Solution");
            Console.WriteLine($"{sol.fitness} - {PointArrayToString(sol.solution.Points)}");
        }

        private static int _totalTimesRan = 0;

        private static void RunForTime(TimeSpan time, Random r, Point[] cont)
        {
            var contain = new Point[cont.Length];
            cont.CopyTo(contain, 0);

            var problem = new SbpProblem(contain, 15);
            var solver = new SBPSolverHCS(problem, 0.1, 0.01, 10000, 1000);
            var min = double.MaxValue;
            (Polygon solution, double fitness)? sol = null;
            var start = DateTime.Now;
            var timesRan = 0;
            while (DateTime.Now - start < time)
            {
                var currentSol = solver.Optimize(r);
                if (currentSol.fitness < min)
                {
                    min = currentSol.fitness;
                    sol = currentSol;
                }
                timesRan++;
            }

            Console.WriteLine();
            Console.WriteLine($"Solution");
            Console.WriteLine($"{sol?.fitness} - {PointArrayToString(sol?.solution.Points)}");
            Console.WriteLine($"Times ran: {timesRan}");

            Interlocked.Add(ref _totalTimesRan, timesRan);
        }

    }
}
