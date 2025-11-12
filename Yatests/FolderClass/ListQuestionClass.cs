using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatests.FolderData;

namespace Yatests.FolderClass
{
    class ListQuestionClass
    {
        private static List<Question> questions { get; set; } = new List<Question>();
        private static int TimePoint { get; set; } = 0;
        private static int IndexQuestion { get; set; } = 0;
        private static int Count { get; set; } = 0;


        public static List<Question> GetQuestions()
        {
            return questions;
        }
        /// <summary>
        /// Присвоить список вопросов
        /// </summary>
        /// <param name="questionsLoads"></param>
        public static void SetQuestions(List<Question> questionsLoads)
        {
            questions = questionsLoads;
        }


        /// <summary>
        /// Очишение значений
        /// </summary>
        public static void Clear()
        {
            questions = null;
            TimePoint = 0;
            IndexQuestion = 0;
            Count = 0;
        }
    }
}
