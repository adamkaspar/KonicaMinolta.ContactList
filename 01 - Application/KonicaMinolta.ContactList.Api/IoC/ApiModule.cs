﻿using Autofac;

namespace KonicaMinolta.ContactList.Api.IoC;

public class ApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder
            .RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
            .AsImplementedInterfaces();
    }
}
