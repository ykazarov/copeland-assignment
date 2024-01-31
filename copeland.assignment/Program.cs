// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Autofac;
using AutoMapper;
using copeland.assignment.commands;
using copeland.assignment.core;
using copeland.assignment.models;
using copeland.assignment.services;
using copeland.assignment.services.mappers;
using Oakton;
using Oakton.Help;

// set up dependency injection
var containerBuilder = new ContainerBuilder();

containerBuilder.RegisterAssemblyTypes(typeof(IOaktonCommand).Assembly)
    .Where(t => t.IsAssignableFrom(typeof(IOaktonCommand)))
    .AsSelf()
    .AsImplementedInterfaces();

containerBuilder.RegisterAssemblyTypes(typeof(HelpCommand).Assembly)
    .AsSelf()
    .AsImplementedInterfaces();

containerBuilder.RegisterAssemblyTypes(typeof(MergeCommand).Assembly)
    .AsSelf()
    .AsImplementedInterfaces();

containerBuilder.RegisterType<FileMergeService>().As<IFileMergeService<DeviceDataTableModel>>();
containerBuilder.RegisterType<FileIOService<DeviceDataFoo1Model>>().As<IFileIOService<DeviceDataFoo1Model>>();
containerBuilder.RegisterType<FileIOService<DeviceDataFoo2Model>>().As<IFileIOService<DeviceDataFoo2Model>>();
containerBuilder.RegisterType<FileIOService<DeviceDataTableModel>>().As<IFileIOService<DeviceDataTableModel>>();

// add mapper to container

var mapperConfiguration = MapperConfigurator.ConfigureMapper();

containerBuilder.RegisterInstance(mapperConfiguration.CreateMapper()).As<IMapper>();

var container = containerBuilder.Build();

using(var scope = container.BeginLifetimeScope())
{
    return CommandExecutor
        .For(_ =>
        {
            _.DefaultCommand = typeof(MergeCommand);
            _.ConfigureRun = (run) =>
            {
                ((IResolvedCommand) run.Command).SetLifetimeScope(scope);
            };
            _.SetAppName("merge");
        }, new AutofacCommandCreator(scope))
        .Execute(args);
}