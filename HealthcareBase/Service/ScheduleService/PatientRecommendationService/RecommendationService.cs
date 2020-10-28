// File:    RecommendationService.cs
// Author:  Lana
// Created: 02 June 2020 02:15:34
// Purpose: Definition of Class RecommendationService

using System.Collections.Generic;
using System.Linq;
using Model.Schedule.Procedures;
using Repository.ScheduleRepository.ProceduresRepository;
using Service.ScheduleService.ScheduleFittingService;

namespace Service.ScheduleService.PatientRecommendationService
{
    public class RecommendationService
    {
        private RecommendationStrategy currentStrategy;
        private readonly RecommendationStrategy defaultStrategy;
        private readonly ProcedureScheduleFittingService procedureScheduleFittingService;
        private readonly ProcedureTypeRepository procedureTypeRepository;

        public RecommendationService(RecommendationStrategy defaultStrategy,
            ProcedureScheduleFittingService procedureScheduleFittingService,
            ProcedureTypeRepository procedureTypeRepository)
        {
            this.defaultStrategy = defaultStrategy;
            this.procedureScheduleFittingService = procedureScheduleFittingService;
            this.procedureTypeRepository = procedureTypeRepository;
        }

        private IEnumerable<Examination> GetPotentialRecommendations(RecommendationRequestDTO request)
        {
            var transformedRequest = currentStrategy.TransformRequest(request);
            return procedureScheduleFittingService.FitForScheduling(transformedRequest) as IEnumerable<Examination>;
        }

        private Examination ChooseBest(IEnumerable<Examination> potentialRecommendations)
        {
            return currentStrategy.ChooseBest(potentialRecommendations);
        }

        public Examination Recommend(RecommendationRequestDTO request, RecommendationStrategy alternateStrategy)
        {
            SwapStrategy(defaultStrategy);
            var recommendations = GetPotentialRecommendations(request);

            if (recommendations.Count() == 0)
            {
                SwapStrategy(alternateStrategy);
                recommendations = GetPotentialRecommendations(request);
            }

            var best = ChooseBest(recommendations);

            return best;
        }

        public void SwapStrategy(RecommendationStrategy strategy)
        {
            strategy.PatientDefault = procedureTypeRepository.GetPatientDefault();
            currentStrategy = strategy;
        }
    }
}