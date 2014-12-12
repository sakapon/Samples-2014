using KLibrary.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InkScoreWpf
{
    public class AppModel : NotifyBase
    {
        public ObservableCollection<Question> Questions { get; private set; }

        public int TotalScore
        {
            get { return GetValue<int>(); }
            private set { SetValue(value); }
        }

        public AppModel()
        {
            var questionsQuery = Enumerable.Range(1, 5)
                .Select(i => new Question(i, 20))
                .Reverse();
            Questions = new ObservableCollection<Question>(questionsQuery);

            foreach (var question in Questions)
            {
                question.AddPropertyChangedHandler("Score", () => TotalScore = Questions.Sum(q => q.Score));
            }
        }
    }

    public class Question : NotifyBase
    {
        public const string Circle = "○";
        public const string Check = "✓";

        public int Id { get; private set; }
        public string ImagePath { get; private set; }
        public int Point { get; private set; }

        public string Sign
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        [DependentOn("Sign")]
        public int Score
        {
            get { return Sign == Circle ? Point : 0; }
        }

        [DependentOn("Sign")]
        public string ScoreText
        {
            get
            {
                return Sign == Circle ? Point.ToString()
                    : Sign == Check ? "0"
                    : "";
            }
        }

        public Question(int id, int point)
        {
            Id = id;
            ImagePath = string.Format("Images/Q{0}.jpg", id);
            Point = point;
        }
    }
}
