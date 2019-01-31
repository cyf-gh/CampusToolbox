using System;
using System.Collections.Generic;
using stLib.CS.File;

namespace CTB.Model.HHI {
    public class HHIModel : IBackModel {
        public List<AssignedTaskModel> AssignedTaskModels { get; set; } = new List<AssignedTaskModel>();
        /// <summary>
        /// Get the task by name.
        /// </summary>
        /// <see cref="AssignedTaskModel.Name"/>
        /// <returns></returns>
        public AssignedTaskModel GetTaskByName( string selectingWorkName ) {
            foreach( var item in AssignedTaskModels ) {
                if( item.Name == selectingWorkName ) {
                    return item;
                }
            }
            return null;
        }

        public IViewModel ToViewModel() {
            return null;
        }

        /// <summary>
        /// Last Modified At Mon, Jan 28, 2019  8:46:31 PM
        /// </summary>
        public class AssignedTaskModel {
            /// <summary>
            /// Chech if the Index is in the prefix index list.
            /// </summary>
            /// <param name="name">Example: 170600233cyf</param>
            /// <param name="index">Example: 170600233. Only with numbers.</param>
            /// <exception cref="InvalidOperationException">
            /// InvalidOperationException would be thrown when the name cannot be matched. Example: name="namenamename" with cause this exception
            /// </exception>
            /// <exception cref="Exception">
            /// Other Exception might be thrown by FileHelper.FileNameToIndex.
            /// </exception>
            /// <returns>In or not.</returns>
            public bool IndexExists( string name, out string index ) {
                string studentIndex = "";
                try { studentIndex = FileHelper.FileNameToIndex( name ); } catch( Exception ex ) { throw ex; }
                if( studentIndex == "" ) {
                    index = studentIndex;
                    throw new System.InvalidOperationException( "Invalid Argument name. Can not match any number in name." );
                }
                int nStudentIndex = Convert.ToInt32( studentIndex );
                index = studentIndex;
                return stLib.CS.Generic.ListHelper.IsIn( Prefix.IndexList, nStudentIndex );
            }
            public int ID { get; set; }
            /// <summary>
            /// Assigned Handing In Task Name
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Whether the works is a folder or not.
            /// </summary>
            public bool IsSubItemFolder { get; set; }
            /// <summary>
            /// Whether the work is Image or not.
            /// If the value is false, means that whether images or files is ok.
            /// </summary>
            public bool IsSubItemImage { get; set; }
            /// <summary>
            /// Path to save.
            /// </summary>
            /// <remarks>
            /// Should be a Linux Path in CTB.
            /// </remarks>
            public string Path { get; set; }
            /// <summary>
            /// Preserved Property.
            /// </summary>
            public string Regex { get; set; }

            /// <seealso cref="PrefixModel"/>
            public string PrefixName { get; set; }
            /// <summary>
            /// Prefix Reference.
            /// </summary>
            public PrefixModel Prefix { get; set; }
            /// <summary>
            /// Description Of the Task.
            /// </summary>
            /// <example>
            /// 1 dog image. 2 cat images. Totally about 3 images.
            /// </example>
            public string Desciption { get; set; }
            public DateTime DeadLine { get; set; }
        }
        /// <summary>
        /// Last Modified At Mon, Jan 28, 2019  8:46:31 PM
        /// </summary>
        public class PrefixModel {
            public int ID { get; set; }
            /// <summary>
            /// Prefix Name.
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Start Number.
            /// </summary>
            public int Start { get; set; }
            /// <summary>
            /// End Number. End > Start should be true.
            /// </summary>
            /// <seealso cref="IndexList"/>
            public int End { get; set; }

            /// <summary>
            /// Included and excluded.
            /// </summary>
            public List<int> IndexList { get; set; }

            /// <summary>
            /// Include exceptional numbers
            /// </summary>
            /// <example>
            /// 160600225;170600233
            /// </example>
            public List<string> IncludeList { get; set; }
            /// <summary>
            /// Include exceptional numbers
            /// </summary>
            /// <example>
            /// 170600205;170600215
            /// </example>
            public List<string> ExcludeList { get; set; }
            /// <summary>
            /// All Member's Name List.
            /// </summary>
            /// <example>
            /// 170600401Tom;170600402Sam
            /// </example>
            public List<string> MemberNameList { get; set; }
        }
    }
}
