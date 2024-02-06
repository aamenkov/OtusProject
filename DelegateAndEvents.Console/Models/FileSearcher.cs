namespace DelegateAndEvents.Console.Models
{
    public class FileSearcher
    {
        public event EventHandler<FileArgs> FileFound;
        public event EventHandler SearchCancelled;


        /// <summary>
        /// Поиск файлов по директории
        /// </summary>
        /// <param name="directory"></param>
        public void SearchFiles(string directory)
        {
            foreach (string file in Directory.GetFiles(directory))
            {
                OnFileFound(new FileArgs(file));

                // Проверка на отмену поиска
                if (searchCancelled)
                {
                    OnSearchCancelled();
                    break;
                }
            }
        }


        private bool searchCancelled = false;
        public void CancelSearch()
        {
            searchCancelled = true;
        }


        #region Обработчики

        protected virtual void OnFileFound(FileArgs e)
        {
            FileFound?.Invoke(this, e);
        }

        protected virtual void OnSearchCancelled()
        {
            SearchCancelled?.Invoke(this, EventArgs.Empty);
        }

        #endregion

    }
}
