using System;
using System.Collections.Generic;
using System.Text;

namespace CTB.Model.ViewModels {
    public class NavBarViewModel : IViewModel {
        public List<string> Directorys;

        public IBackModel ToBackModel() {
            string MakePath = "";
            foreach( var dir in Directorys ) {
                MakePath += dir;
                MakePath += ";";
            }
            MakePath.TrimEnd(';');
            return new NavBarModel {
                Path = MakePath
            };
        }
    }
}
