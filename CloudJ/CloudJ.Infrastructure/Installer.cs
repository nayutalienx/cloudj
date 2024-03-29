﻿using AutoMapper;
using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Implementation;
using BusinessLogicLayer.Implementation.AutoMapperProfiles;
using DataAccessLayer.Abstraction;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CloudJ.Infrastructure
{
    public static class Installer
    {
        public static IServiceCollection Install(this IServiceCollection serviceCollection)
        {
            //var mapperConfiguration = new MapperConfiguration(config => {
            //    config.AddProfile(new AdvertProfile());
            //    config.AddProfile(new CommentProfile());
            //    config.AddProfile(new CategoryProfile());
            //    config.AddProfile(new PhotoProfile());
            //    config.AddProfile(new AddressProfile());
            //});

            //serviceCollection.AddTransient<IAdvertManager, AdvertManager>()
            //    .AddTransient<ICategoryManager, CategoryManager>()
            //    .AddTransient<IAdvertRepository, AdvertRepository>()
            //    .AddTransient<ICategoryRepository, CategoryRepository>()
            //    .AddTransient<IAddressRepository, AddressRepository>()
            //    .AddTransient<ICommentRepository, CommentRepository>()
            //    .AddTransient<IPhotoRepository, PhotoRepository>()
            //    //.AddDbContext<AdboardContext>(ServiceLifetime.Transient)
            //    .AddSingleton(mapperConfiguration.CreateMapper());
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new CategoryProfile());
                config.AddProfile(new PhotoProfile());
                config.AddProfile(new CloudProfile());
                config.AddProfile(new PlanProfile());
                config.AddProfile(new ReviewProfile());
                config.AddProfile(new SolutionProfile());
                config.AddProfile(new OrderProfile());
                config.AddProfile(new ContainerProfile());
                config.AddProfile(new BalanceProfile());
                config.AddProfile(new CollectionProfile());
            });
            
            serviceCollection
                .AddTransient<ISolutionService, SolutionService>()
                .AddTransient<IBillingService, BillingService>()
                .AddTransient<ICategoryRepository, CategoryRepository>()
                .AddTransient<IOrderRepository, OrderRepository>()
                .AddTransient<IBalanceRepository, BalanceRepository>()
                .AddTransient<ISolutionRepository, SolutionRepository>()
                .AddTransient<IPhotoRepository, PhotoRepository>()
                .AddTransient<ICollectionRepository, CollectionRepository>()
                .AddTransient<ICollectionService, CollectionService>()
                .AddTransient<ISolutionLinkRepository, SolutionLinkRepository>()
                //.AddDbContext<CloudjContext>(ServiceLifetime.Transient)
                .AddSingleton(mapperConfiguration.CreateMapper());
            return serviceCollection;

        }
    }
}
