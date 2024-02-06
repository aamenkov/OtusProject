namespace DelegateAndEvents.Console.Models
{
    /// <summary>
    /// Вспомогательный класс
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Обобщённая функция расширения, находящая и возвращающая максимальный элемент коллекции.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="convertToNumber"></param>
        /// <returns></returns>
        public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
        {
            T maxElement = null;
            float maxValue = float.MinValue;

            foreach (var item in collection)
            {
                float value = convertToNumber(item);
                if (value > maxValue)
                {
                    maxValue = value;
                    maxElement = item;
                }
            }

            return maxElement;
        }
    }
}
