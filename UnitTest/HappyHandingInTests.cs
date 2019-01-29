using System;
using Xunit;
using CTB.Factory.HappyHandingIn;
using CTB.DomainModel.HappyHandingIn;
/// <summary>
/// Debug Sucks, Test Rocks.
/// MFIF == May Fail In the Future
/// </summary>
namespace CTB.UnitTest {
    public class HHIFactoryTest {
        [Fact]
        public void Correct_HHIModel() {
            try {
                HHIModel model = null;
                using( HHIFactory factory = new HHIFactory() ) {
                    model = (HHIModel)factory.Create( HHIFactory.CreateBy.DataWebsite, "https://www.cnblogs.com/PROJECT-IDOLPROGRAM/p/10335534.html" );
                }
                Assert.NotNull( model );
                Assert.NotEmpty( model.AssignedTaskModels );
            } catch( Exception e ) {
                Assert.False( true );
            }
        }

        [Fact]
        public void Correct_TargetFetch() {
            HHIModel model = null;
            using( HHIFactory factory = new HHIFactory() ) {
                model = (HHIModel)factory.Create( HHIFactory.CreateBy.DataWebsite, "https://www.cnblogs.com/PROJECT-IDOLPROGRAM/p/10335534.html" );
            }
            /// MFIF {
            Assert.True( model.AssignedTaskModels.Count == 2 );
            Assert.NotNull( model.GetTaskByName( "大货系统" ).Prefix );
            Assert.Null( model.GetTaskByName( "NoSuchTask" ) );
            Assert.NotNull( model.GetTaskByName( "大货系统" ) );
            Assert.True( "1702npp" == model.GetTaskByName( "大货系统" ).Prefix.Name );
            string pureDigitIndex = "";
            Assert.True( model.GetTaskByName( "大货系统" ).Prefix.IndexList.Count == 31 );

            Assert.True( model.GetTaskByName( "大货系统" ).IndexExists( "170600233cyf", out pureDigitIndex ) );
            Assert.True( pureDigitIndex == "170600233" );
            /// }
        }
    }
}
