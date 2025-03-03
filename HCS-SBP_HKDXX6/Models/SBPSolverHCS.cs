using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS_SBP_HKDXX6.Models
{
    public class SBPSolverHCS(
        SbpProblem problem,
        double neighbourDistance,
        double minStopDelta,
        double constraintWeight,
        int maxStopCount)
    {
        double _lastFitness;
        int _stopCount;

        private bool StopCondition(Polygon solution)
        {
            if (Fitness(solution) - _lastFitness < minStopDelta)
            {
                _stopCount++;
            }
            else
            {
                _stopCount = 0;
            }
            return _stopCount >= maxStopCount;
        }

        private double Fitness(Polygon solution) => SbpProblem.Goal(solution) + constraintWeight * problem.Constraint(solution);

        public (Polygon solution, double fitness) Optimize(Random random)
        {
            var solution = problem.GenerateRandomSolution(random);
            _lastFitness = Fitness(solution);

            while (!StopCondition(solution))
            {
                var neighbour = problem.GetRandomNeighbour(solution, neighbourDistance, random);

                if (!(Fitness(neighbour) < Fitness(solution))) continue;
                
                solution = neighbour;
                _lastFitness = Fitness(solution);
            }

            return (solution, Fitness(solution));
        }
    }
}
