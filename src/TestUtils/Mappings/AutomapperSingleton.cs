﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Mappings;

namespace TestUtils.Mappings
{
    public static class AutomapperSingleton
    {
        public static TDestination MapTo<TDestination>(this object any)
        {
            return AutomapperSingleton.Map<TDestination>(any);
        }

        private static IMapper _mapper;

        public static IMapper Mapper => _mapper ?? throw new InvalidOperationException("Mapper was not initialized");

        private static readonly object _lock = new object();

        public static TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public static void Initialize()
        {
            Initialize(CoreMappings.GetProfiles());
        }

        public static void Initialize(IEnumerable<Profile> profiles)
        {
            InitializeInternal(profiles.ToArray());
        }

        private static void InitializeInternal(IReadOnlyCollection<Profile> profiles)
        {
            if (_mapper != null)
            {
                return;
            }

            lock (_lock)
            {
                if (profiles == null || profiles.Count == 0)
                {
                    throw new ArgumentException("There is no profile source for configuration");
                }

                _mapper = InitMapper(profiles);
            }
        }

        private static IMapper InitMapper(IReadOnlyCollection<Profile> profiles)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfiles(profiles);
            });

            try
            {
                mappingConfig.AssertConfigurationIsValid();

                return mappingConfig.CreateMapper();
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException(
                    $"Cannot create Automapper instance because of errors:\r\n{exception.Message}",
                    exception);
            }
        }
    }
}
