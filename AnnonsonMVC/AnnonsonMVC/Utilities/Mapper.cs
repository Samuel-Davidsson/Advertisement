namespace AnnonsonMVC.Utilities
{
    internal static class Mapper
    {
        static Mapper()
        {
            ViewModelToModelMapping = new ViewModelToModelMapper();
            ModelToViewModelMapping = new ModelToViewModelMapper();
        }

        public static ViewModelToModelMapper ViewModelToModelMapping { get; }
        public static ModelToViewModelMapper ModelToViewModelMapping { get; }
    }
}
