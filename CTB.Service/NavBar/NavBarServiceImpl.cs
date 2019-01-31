using CTB.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTB.Service.NavBar {
    public class NavBarServiceImpl : INavBarService {
        private readonly NavBarViewModel _NavbarViewModel;

        public NavBarServiceImpl() {

        }
        public INavBarService Reset() {
            _NavbarViewModel.Directorys.Clear();
            return this;
        }
        public INavBarService Cd( string nextDirectory ) {
            if( string.IsNullOrEmpty( nextDirectory ) ) {
                return this;
            }
            _NavbarViewModel.Directorys.Add( nextDirectory );
            return this;
        }
        public INavBarService CdUp() {
            if( _NavbarViewModel.Directorys.Count > 0 ) {
                _NavbarViewModel.Directorys.RemoveAt( _NavbarViewModel.Directorys.Count - 1 );
            }
            return this;
        }

        public NavBarViewModel GetViewModel() {
            return _NavbarViewModel;
        }

        public void Dispose() {
        }
    }
}
