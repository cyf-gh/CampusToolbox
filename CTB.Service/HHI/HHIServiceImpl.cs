using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Xml;
using CTB.Model;
using CTB.Model.HHI;
using static CTB.Model.HHI.HHIModel;

namespace CTB.Service.HHI {
    public class HHIServiceImpl : IHHIService {
        private readonly HHIModel _Model;

        public HHIServiceImpl() {
            using( HHIFactory factory = new HHIFactory() ) {
                _Model = (HHIModel)factory.Create( HHIFactory.CreateBy.DataWebsite, "https://www.cnblogs.com/PROJECT-IDOLPROGRAM/p/10335534.html" );
            }
        }

        public void Dispose() { }

        public IBackModel GetHHIModel() {
            return _Model;
        }

        public IModel GetUserModel() {
            #region _MOCK_
            MockModels.HHI hhi = new MockModels.HHI();
            return hhi.GetUser_Cyf_Admin();
            #endregion
        }

        /// <summary>
        /// Last Modefied Mon, Jan 28, 2019  9:42:50 PM
        /// </summary>
        /// <see cref="HHIModel"/>
        private class HHIFactory : IDisposable {
            public enum CreateBy {
                // File,
                DataWebsite
            }
            /// <summary>
            /// Create a HHI Model
            /// </summary>
            /// <param name="createBy">
            /// <see cref="CreateBy"/>
            /// </param>
            /// <param name="url">
            /// https://www.cnblogs.com/PROJECT-IDOLPROGRAM/p/10335534.html
            /// </param>
            /// <returns>
            /// <example>
            /// var model = ( HHIModel )factory.Create( HHIFactory.CreateBy.DataWebSite, "https://www.cnblogs.com/PROJECT-IDOLPROGRAM/p/10335534.html" ) 
            /// </example>
            /// <see cref="HHIModel"/>
            /// </returns>
            public object Create( object createBy, object url ) {
                HHIModel model = null;
                using( HHILoader loader = new HHILoader() ) {
                    using( HHIDataFeeder dataFeeder = new HHIDataFeeder( (string)url ) ) {
                        var xmls = dataFeeder.GetCompleteXmlFromNet();
                        loader.LoadHandIns( xmls[HHILoader._HHIRootNodeName] );
                        loader.LoadPrefixs( xmls[HHILoader._PrefixRootNodeName] );
                        loader.BindPrefixToTask();
                        model = loader.Model;
                    }
                }
                return model;
            }

            public void Dispose() { }

            public class HHIDataFeeder : IDisposable {
                public HHIDataFeeder( string weburl ) {
                    Src = weburl;
                }
                private string Src = "";

                public Dictionary<string, string> GetCompleteXmlFromNet() {
                    stLib.CS.Net.WebCrawler c = new stLib.CS.Net.WebCrawler();
                    string html = c.GetWebRequest( Src );
                    Dictionary<string, string> Xmls = new Dictionary<string, string>();

                    string[] strs = html.Split( '~' );

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml( WebUtility.HtmlDecode( strs[2] ) );
                    XmlNode rootNode = doc.SelectSingleNode( "cyfxml" );
                    foreach( XmlNode node in rootNode.ChildNodes ) {
                        Xmls.Add( node.Name, node.OuterXml );
                    }
                    return Xmls;
                }

                public void Dispose() { }
            }

            private class HHILoader : IDisposable {
                public HHIModel Model { get; set; } = new HHIModel();
                public void LoadHandIns( string xml ) {
                    _TaskXml.LoadXml( xml );
                    loadHandIn();
                }
                public void LoadPrefixs( string xml ) {
                    _PrefixXml.LoadXml( xml );
                    loadPrefixs();
                    foreach( var prefix in _PrefixModels ) {
                        BindIndexListToPrefixModel( prefix );
                    }
                }
                private void BindIndexListToPrefixModel( PrefixModel prefix ) {
                    List<int> indexList = new List<int>();
                    for( int i = 0; i < ( prefix.End - prefix.Start + 1 ); i++ ) {
                        indexList.Add( prefix.Start + i );
                    }

                    for( int i = 0; i < indexList.Count; i++ ) {
                        foreach( var exclude in prefix.ExcludeList ) {
                            if( exclude == "" ) { continue; }
                            if( Convert.ToInt32( exclude ) == indexList[i] ) {
                                indexList.RemoveAt( i );
                                break;
                            }
                        }
                    }

                    foreach( var item in prefix.IncludeList ) {
                        if( item == "" ) { continue; }
                        indexList.Add( Convert.ToInt32( item ) );
                    }

                    prefix.IndexList = indexList;
                }
                /// <summary>
                /// Should be called after LoadHandIns and LoadPrefixs
                /// </summary>
                /// <exception cref="NullReferenceException">
                /// NullReferenceException would be be thrown when the there is no such named prefix.
                /// </exception>
                public void BindPrefixToTask() {
                    foreach( var task in Model.AssignedTaskModels ) {
                        foreach( var item in _PrefixModels ) {
                            if( item.Name == task.PrefixName ) {
                                task.Prefix = item;
                            }
                        }
                        if( null == task.Prefix ) {
                            throw new System.NullReferenceException( "Null Reference At HHIDataFeeder.BindPrefixToTask.\nUnexceptional Prefix Name." );
                        }
                    }
                }
                private XmlDocument _PrefixXml = new XmlDocument();
                private XmlDocument _TaskXml = new XmlDocument();
                private List<PrefixModel> _PrefixModels { get; set; } = new List<PrefixModel>();

                public const string _PrefixRootNodeName = "prefixs";
                public const string _HHIRootNodeName = "hhis";

                private void loadPrefixs() {
                    XmlNode prefixs = _PrefixXml.SelectSingleNode( _PrefixRootNodeName ); //<prefixs>
                    int i = 0;
                    foreach( XmlNode prefix in prefixs.ChildNodes ) {
                        PrefixModel tmp = new PrefixModel();
                        tmp.Name = prefix.Attributes["name"].Value;
                        tmp.MemberNameList = new List<string>( prefix.Attributes["name-list"].Value.Split( ';' ) );
                        tmp.Start = Convert.ToInt32( prefix.Attributes["start-id"].Value );
                        tmp.End = Convert.ToInt32( prefix.Attributes["end-id"].Value );
                        tmp.IncludeList = new List<string>( prefix.Attributes["include-list"].Value.Split( ';' ) );
                        tmp.ExcludeList = new List<string>( prefix.Attributes["exclude-list"].Value.Split( ';' ) );
                        tmp.ID = i;
                        _PrefixModels.Add( tmp );
                        ++i;
                    }
                }

                private void loadHandIn() {
                    XmlNode hhis = _TaskXml.SelectSingleNode( "hhis" ); //<hhis>
                    int i = 0;
                    foreach( XmlNode hhi in hhis.ChildNodes ) {
                        AssignedTaskModel tmp = new AssignedTaskModel();
                        tmp.Name = hhi.Attributes["name"].Value;
                        tmp.IsSubItemFolder = Convert.ToBoolean( hhi.Attributes["is-sub-item-folder"].Value );
                        tmp.IsSubItemImage = Convert.ToBoolean( hhi.Attributes["is-sub-item-image"].Value );
                        tmp.Desciption = hhi.Attributes["description"].Value.Replace( "\\n", "\n" );
                        tmp.Path = hhi.Attributes["path"].Value;
                        tmp.Regex = hhi.Attributes["regex"].Value;
                        tmp.PrefixName = hhi.Attributes["prefix-name"].Value;
                        tmp.DeadLine = Convert.ToDateTime( hhi.Attributes["deadline"].Value, new DateTimeFormatInfo() { ShortDatePattern = "yyyy-MM-dd hh:mm" } );

                        tmp.ID = i;
                        Model.AssignedTaskModels.Add( tmp );
                        ++i;
                    }
                }
                public void Dispose() {
                }
            }
        }
    }
}
