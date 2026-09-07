using System;
using System.Collections.Generic;

[Serializable]
public class Question {
    public String enunciado;
    public String[] alternativas;
    public byte correctIndex = 0;

    public Question() {}

    public Question(String enunciado, String[] alternativas, byte correctIndex) {
        this.enunciado = enunciado;
        this.alternativas = alternativas;
        this.correctIndex = correctIndex;
    }
}

[Serializable]
public class QuestionWrapper {
    public List<Question> perguntas;
}

public static class ListQuestions{
    public static List<Question> listQuestion;
}