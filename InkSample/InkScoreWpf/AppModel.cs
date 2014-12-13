using KLibrary.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace InkScoreWpf
{
    public class AppModel : NotifyBase
    {
        public Question[] Questions { get; private set; }

        public int? TotalScore
        {
            get { return Questions.All(q => !q.Score.HasValue) ? null : Questions.Sum(q => q.Score); }
        }

        public AppModel()
        {
            Questions = Enumerable.Range(1, 5)
                .Select(i => new Question(i, 20))
                .Reverse()
                .ToArray();

            foreach (var question in Questions)
            {
                question.AddPropertyChangedHandler("Score", () => NotifyPropertyChanged("TotalScore"));
            }
        }
    }

    [DebuggerDisplay(@"\{Q{Id}: {Score}/{Point}\}")]
    public class Question : NotifyBase
    {
        public int Id { get; private set; }
        public string ImagePath { get; private set; }
        public int Point { get; private set; }

        public AnswerResult Result
        {
            get { return GetValue<AnswerResult>(); }
            set { SetValue(value); }
        }

        [DependentOn("Result")]
        public int? Score
        {
            get
            {
                switch (Result)
                {
                    case AnswerResult.None: return null;
                    case AnswerResult.Incorrect: return 0;
                    case AnswerResult.Intermediate: return Point / 2;
                    case AnswerResult.Correct: return Point;
                    default: throw new InvalidOperationException();
                }
            }
        }

        public Question(int id, int point)
        {
            Id = id;
            ImagePath = string.Format("Images/Q{0}.jpg", id);
            Point = point;
        }
    }

    public enum AnswerResult
    {
        None,
        Incorrect,
        Intermediate,
        Correct,
    }
}
