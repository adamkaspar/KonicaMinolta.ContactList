using Autofac;

namespace KonicaMinolta.ContactList.Business.IoC;

public class BusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder
            .RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
            .AsImplementedInterfaces();
    }
}
