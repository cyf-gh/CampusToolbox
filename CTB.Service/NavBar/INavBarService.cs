using System;
using System.Collections.Generic;
using System.Text;
using CTB.Model.ViewModels;
using CTB.Model;

namespace CTB.Service.NavBar {
    public interface INavBarService : IService {
        INavBarService Reset();
        INavBarService Cd( string nextDirectory );
        INavBarService CdUp();
        NavBarViewModel GetViewModel();
    }
}
