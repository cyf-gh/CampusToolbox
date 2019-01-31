using System;
using System.Collections.Generic;
using System.Text;
using CTB.Model.ViewModels;

namespace CTB.Model {
    public class NavBarModel : IBackModel {
        public string Path { get; set; }

        public IViewModel ToViewModel() {
            return new NavBarViewModel {
                Directorys = new List<string>( Path.Split( ";" ) )
            };
        }
    }
}
