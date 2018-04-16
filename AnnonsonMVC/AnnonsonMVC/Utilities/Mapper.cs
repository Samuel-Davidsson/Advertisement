namespace AnnonsonMVC.Utilities
{
    internal static class Mapper
    {
        static Mapper()
        {
            ViewModelToModelMapping = new ViewModelToModelMapper();
        }

        public static ViewModelToModelMapper ViewModelToModelMapping { get; }
    }
}
