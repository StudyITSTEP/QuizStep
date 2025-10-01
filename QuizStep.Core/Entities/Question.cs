using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace QuizStep.Core.Entities;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }
    [IgnoreDataMember]
    [NotMapped]
    public int CorrectAnswerIndex { get; set; }
    
    public List<Answer> Answers { get; set; } = null!;
    
}