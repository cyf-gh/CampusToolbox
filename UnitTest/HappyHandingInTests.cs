using System;
using Xunit;
using CTB.Service.HHI;
using CTB.Model.HHI;

/// <summary>
/// Debug Sucks, Test Rocks.
/// MFIF == May Fail In the Future
/// </summary>
namespace HHIService {
    public class HHIService_Tests {
        [Fact]
        public void Could_Get_A_Correct_HHIModel() {
            try {
                IHHIService service = new HHIServiceImpl();
                var model = (HHIModel)service.GetHHIModel();
                Assert.NotNull( model );
                Assert.NotEmpty( model.AssignedTaskModels );
            } catch( Exception e ) {
                Assert.False( true );
            }
        }

        [Fact]
        public void Could_Get_Correct_Data_In_HHIModel() {
            IHHIService service = new HHIServiceImpl();
            var model = (HHIModel)service.GetHHIModel();
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
