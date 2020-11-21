// File:    RecommendationService.cs
// Author:  Lana
// Created: 02 June 2020 02:15:34
// Purpose: Definition of Class RecommendationService

using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository;
using HealthcareBase.Service.ScheduleService.ScheduleFittingService;

namespace HealthcareBase.Service.ScheduleService.PatientRecommendationService
{
    public class RecommendationService
    {
        private readonly RecommendationStrategy defaultStrategy;
        private readonly ProcedureScheduleFittingService procedureScheduleFittingService;
        private readonly RepositoryWrapper<ProcedureTypeRepository> procedureTypeRepository;
        private RecommendationStrategy currentStrategy;

        public RecommendationService(RecommendationStrategy defaultStrategy,
            ProcedureScheduleFittingService procedureScheduleFittingService,
            ProcedureTypeRepository procedureTypeRepository)
        {
            this.defaultStrategy = defaultStrategy;
            this.procedureScheduleFittingService = procedureScheduleFittingService;
            this.procedureTypeRepository = new RepositoryWrapper<ProcedureTypeRepository>(procedureTypeRepository);
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
            strategy.PatientDefault = procedureTypeRepository.Repository.GetPatientDefault();
            currentStrategy = strategy;
        }
    }
}