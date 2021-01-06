using PlateformeConcours.Controllers;
using PlateformeConcours.Repository;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace PlateformeConcours
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IEtudiantRepository, EtudiantRepository>();
            container.RegisterType<IEtudiant3aRepository, Etudiant3aRepository>();
            container.RegisterType<IEtudiant4aRepository, Etudiant4aRepository>();
            container.RegisterType<IFiliereRepository, FiliereRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}