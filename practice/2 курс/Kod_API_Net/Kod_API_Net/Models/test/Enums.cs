namespace practice.Models
{
    public enum UserRole
    {
        Admin,
        Teacher,
        Student
    }

    public enum TestType
    {
        KnowledgeCheck,
        Survey,
        Exam
    }

    public enum AnswerType
    {
        SingleChoice = 1,
        MultipleChoice = 2,
        TextAnswer = 3
    }
}
