namespace EtsyApi.Responses
{
    public class GenericResponse<T>
    {
        public int count { get; set; }

        public T[] results { get; set; }
    }
}
