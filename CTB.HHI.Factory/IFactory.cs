namespace CTB.Factory {
    interface IFactory : System.IDisposable {
        object Create( object arga, object argb );
    }
}
