using System;
using System.Collections.Generic;
using System.Text;

namespace CTB.Model.HHI {
    public enum Identity {
        User, Admin
    }
    public class HHIUserModel : IModel {
        public Identity Identity { get; set; }
        public int StudentIndex { get; set; }

        #region Functional
        public string GetFolderName( string name ) {
            return name + StudentIndex.ToString();
        }
        #endregion
    }
}
